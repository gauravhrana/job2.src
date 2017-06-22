using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
	public partial class NeedDataManager : StandardDataManager
	{
		private static string DataStoreKey = "";		

		static NeedDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Need");			
		}

		#region GetList

        public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NeedSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
			var oDT = new DBDataTable("Need.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion GetList

		#region Search
		public static string ToSQLParameter(NeedDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case NeedDataModel.DataColumns.NeedId:
					if (data.NeedId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, NeedDataModel.DataColumns.NeedId, data.NeedId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, NeedDataModel.DataColumns.NeedId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

        public static DataTable Search(NeedDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL  
			var sql = "EXEC dbo.NeedSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, requestProfile.ApplicationModeId) +			
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
				", " + ToSQLParameter(data, NeedDataModel.DataColumns.NeedId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description);

			var oDT = new DBDataTable("Need.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion Search

		#region GetDetails

        public static DataTable GetDetails(NeedDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);

            var table = list.ToDataTable();

            return table;
        }

        public static List<NeedDataModel> GetEntityDetails(NeedDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.NeedSearch ";

            var parameters =
            new
            {
                    AuditId              = requestProfile.AuditId
                ,   ApplicationId        = requestProfile.ApplicationId
                ,   ReturnAuditInfo      = returnAuditInfo
                ,   NeedId               = dataQuery.NeedId
                ,   Name                 = dataQuery.Name
                ,   Description          = dataQuery.Description
            };

            List<NeedDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<NeedDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();                
            }

            return result;
        }

        //public static List<NeedDataModel> GetEntityDetails(NeedDataModel data, int auditId)
        //{
        //    var sql = "EXEC dbo.NeedSearch " +
        //        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
        //        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId) +				
        //        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
        //        ", " + ToSQLParameter(data, NeedDataModel.DataColumns.NeedId) +
        //        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description);

        //    var result = new List<NeedDataModel>();

        //    using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
        //    {
        //        var dbReader = reader.DBReader;

        //        while (dbReader.Read())
        //        {
        //            var dataItem = new NeedDataModel();

        //            dataItem.NeedId = (int)dbReader[NeedDataModel.DataColumns.NeedId];

        //            SetStandardInfo(dataItem, dbReader);

        //            SetBaseInfo(dataItem, dbReader);

        //            result.Add(dataItem);
        //        }
        //    }

        //    return result;
        //}

		#endregion GetDetails

		#region CreateOrUpdate
		private static string CreateOrUpdate(NeedDataModel data, RequestProfile requestProfile, string action)
		{
            var traceId = TraceDataManager.GetNextTraceId(requestProfile);
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.NeedInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.TraceId, traceId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.NeedUpdate  " + "\r\n" +
                           " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.TraceId, traceId);					
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, NeedDataModel.DataColumns.NeedId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

		#region Create
        public static int Create(NeedDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");

            var needId = DBDML.RunScalarSQL("Need.Insert", sql, DataStoreKey);
            return Convert.ToInt32(needId);
        }
		#endregion Create

		#region Update
        public static void Update(NeedDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("Need.Update", sql, DataStoreKey);
        }

		#endregion Update

		#region Renumber
        public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NeedRenumber " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("Need.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

        public static void Delete(NeedDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.NeedDelete ";

            var parameters =
            new
            {
                    AuditId = requestProfile.AuditId
                ,   NeedId   = dataQuery.NeedId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                

                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

                
            }
        }

		#endregion Delete

		#region DoesExist

        public static DataTable DoesExist(NeedDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NeedSearch " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
							", " + ToSQLParameter(data, NeedDataModel.DataColumns.NeedId) +
							", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name);

			var oDT = new DBDataTable("Need.DoesExist", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion DoesExist

		#region GetChildren

        private static DataSet GetChildren(NeedDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NeedChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, NeedDataModel.DataColumns.NeedId);

			var oDT = new DBDataSet("Need.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

        public static DataSet DeleteChildren(NeedDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.NeedChildrenDelete " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, NeedDataModel.DataColumns.NeedId);

			var oDT = new DBDataSet("Need.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

        public static bool IsDeletable(NeedDataModel data, RequestProfile requestProfile)
		{
			var isDeletable = true;

            var ds = GetChildren(data, requestProfile);

			if (ds != null && ds.Tables.Count > 0)
			{
				if (ds.Tables.Cast<DataTable>().Any(dt => dt.Rows.Count > 0))
				{
					isDeletable = false;
				}
			}

			return isDeletable;
		}

		#endregion
	}
}
