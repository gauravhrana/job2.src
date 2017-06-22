using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.Core
{
	public partial class TimeZoneDataManger : BaseDataManager
	{

		static readonly string DataStoreKey = "";

		static TimeZoneDataManger()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TimeZone");
		}

		#region GetList

		public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TimeZoneSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var oDT = new DBDataTable("TimeZone.List", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetDetails

		public static DataTable GetDetails(TimeZoneDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TimeZoneSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, ReturnAuditInfoOnDetails) +
				", " + ToSQLParameter(data, TimeZoneDataModel.DataColumns.TimeZoneId);

			var oDT = new DBDataTable("TimeZone.Details", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetEntitySearch

		public static List<TimeZoneDataModel> GetEntityDetails(TimeZoneDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.TimeZoneSearch ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				TimeZoneId = dataQuery.TimeZoneId
				,
				ReturnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails
				,
				ApplicationMode = requestProfile.ApplicationModeId
				,
				Name = dataQuery.Name
			};

			List<TimeZoneDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TimeZoneDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(TimeZoneDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("TimeZone.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

		public static void Update(TimeZoneDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DBDML.RunSQL("TimeZone.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(TimeZoneDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.TimeZoneDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				TimeZoneId = dataQuery.TimeZoneId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static string ToSQLParameter(TimeZoneDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TimeZoneDataModel.DataColumns.TimeZoneId:
					if (data.TimeZoneId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TimeZoneDataModel.DataColumns.TimeZoneId, data.TimeZoneId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TimeZoneDataModel.DataColumns.TimeZoneId);
					}
					break;
                case TimeZoneDataModel.DataColumns.TimeDifference:
                    if (data.TimeDifference != null)
					{
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TimeZoneDataModel.DataColumns.TimeDifference, data.TimeDifference);
					}
					else
					{
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TimeZoneDataModel.DataColumns.TimeDifference);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}


		public static DataTable Search(TimeZoneDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL
			var sql = "EXEC dbo.TimeZoneSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, requestProfile.ApplicationModeId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", " + ToSQLParameter(data, TimeZoneDataModel.DataColumns.TimeZoneId) +
				", " + ToSQLParameter(data, TimeZoneDataModel.DataColumns.Name);

			var oDT = new DBDataTable("TimeZone.Search", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region CreateOrUpdate

		private static string CreateOrUpdate(TimeZoneDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.TimeZoneInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TimeZoneUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, TimeZoneDataModel.DataColumns.TimeZoneId) +
						", " + ToSQLParameter(data, TimeZoneDataModel.DataColumns.Name) +
						", " + ToSQLParameter(data, TimeZoneDataModel.DataColumns.TimeDifference) +
						", " + ToSQLParameter(data, TimeZoneDataModel.DataColumns.Description) +
						", " + ToSQLParameter(data, TimeZoneDataModel.DataColumns.SortOrder);
			return sql;
		}

		#endregion

		#region DoesExist

		public static DataTable DoesExist(TimeZoneDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TimeZoneSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, data.ApplicationId) +
                ", " + ToSQLParameter(data, TimeZoneDataModel.DataColumns.Name) +
				", " + ToSQLParameter(data, TimeZoneDataModel.DataColumns.TimeZoneId);

			var oDT = new DBDataTable("TimeZone.DoesExist", sql, DataStoreKey);
			return oDT.DBTable;
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(TimeZoneDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TimeZoneChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TimeZoneDataModel.DataColumns.TimeZoneId);

			var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(TimeZoneDataModel data, RequestProfile requestProfile)
		{
			var isDeletable = true;
			var ds = GetChildren(data, requestProfile);
			if (ds != null && ds.Tables.Count > 0)
			{
				foreach (DataTable dt in ds.Tables)
				{
					if (dt.Rows.Count > 0)
					{
						isDeletable = false;
						break;
					}
				}
			}
			return isDeletable;
		}

        #endregion

        public static void Sync(int newApplicationId, int oldApplicationId, RequestProfile requestProfile)
        {
            // get all records for old Application Id
            var sql = "EXEC dbo.TimeZoneSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, oldApplicationId);

            var oDT = new DBDataTable("TimeZone.Search", sql, DataStoreKey);

            // formaulate a new request Profile which will have new Applicationid
            var newRequestProfile = new RequestProfile();
            newRequestProfile.ApplicationId = newApplicationId;
            newRequestProfile.AuditId = requestProfile.AuditId;

            foreach (DataRow dr in oDT.DBTable.Rows)
            {
                var data = new TimeZoneDataModel();
                data.ApplicationId = newApplicationId;
                data.Name = dr[StandardDataModel.StandardDataColumns.Name].ToString();

                // check for existing record in new Application Id
                var dt = DoesExist(data, newRequestProfile);
                if (dt == null || dt.Rows.Count == 0)
                {
                    data.Description    = dr[StandardDataModel.StandardDataColumns.Description].ToString();
                    data.SortOrder      = Convert.ToInt32(dr[StandardDataModel.StandardDataColumns.SortOrder]);
                    data.TimeDifference = Convert.ToDecimal(dr[TimeZoneDataModel.DataColumns.TimeDifference]);

                    //create in new application id
                    Create(data, newRequestProfile);

                }

            }
        }

	}
}
