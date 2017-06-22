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
	public partial class TabParentStructureDataManager : StandardDataManager
	{
		static readonly string DataStoreKey = "";

		static TabParentStructureDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TabParentStructure");
		}

		#region GetList

        public static List<TabParentStructureDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(TabParentStructureDataModel.Empty, requestProfile, 0);
		}

		public static List<TabParentStructureDataModel> GetTabParentStructureList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TabParentStructureSearch" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var result = new List<TabParentStructureDataModel>();

			using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new TabParentStructureDataModel();

					dataItem.TabParentStructureId = (int)dbReader[TabParentStructureDataModel.DataColumns.TabParentStructureId];
					dataItem.ApplicationId = (int)dbReader[BaseDataModel.BaseDataColumns.ApplicationId];

					SetStandardInfo(dataItem, dbReader);

					SetStandardInfo(dataItem, dbReader);

					result.Add(dataItem);
				}

			}

			return result;
		}

		#endregion

		#region GetDetails

        public static TabParentStructureDataModel GetDetails(TabParentStructureDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		#endregion

		#region GetEntitySearch

		public static List<TabParentStructureDataModel> GetEntityDetails(TabParentStructureDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TabParentStructureSearch ";

			var parameters =
			new
			{
				    AuditId              = requestProfile.AuditId
				,	TabParentStructureId = dataQuery.TabParentStructureId
				,   Name                 = dataQuery.Name
   				,	ApplicationId        = requestProfile.ApplicationId				
			};

			List<TabParentStructureDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				result = dataAccess.Connection.Query<TabParentStructureDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();


			}

			return result;
		}

		#endregion

		#region Create

		public static void Create(TabParentStructureDataModel data, RequestProfile requestProfile)
		{
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DBDML.RunSQL("TabParentStructure.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Update

        public static void Update(TabParentStructureDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("TabParentStructure.Update", sql, DataStoreKey);
        }

		#endregion

		#region Delete

		public static void Delete(TabParentStructureDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.TabParentStructureDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				TabParentStructureId = dataQuery.TabParentStructureId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

        public static string ToSQLParameter(TabParentStructureDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {

                case TabParentStructureDataModel.DataColumns.TabParentStructureId:
                    if (data.TabParentStructureId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TabParentStructureDataModel.DataColumns.TabParentStructureId, data.TabParentStructureId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TabParentStructureDataModel.DataColumns.TabParentStructureId);
                    }
                    break;
                case TabParentStructureDataModel.DataColumns.IsAllTab:
                    if (data.IsAllTab != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TabParentStructureDataModel.DataColumns.IsAllTab, data.IsAllTab);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TabParentStructureDataModel.DataColumns.IsAllTab);
                    }
                    break;
               

                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }
            return returnValue;
        }

		public static DataTable Search(TabParentStructureDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		#endregion

        #region CreateOrUpdate

        private static string CreateOrUpdate(TabParentStructureDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.TabParentStructureInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +                      
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.TabParentStructureUpdate  " + "\r\n" +
                           " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                       
                    break;

                default:
                    break;
            }

            sql = sql + ", " + ToSQLParameter(data, TabParentStructureDataModel.DataColumns.TabParentStructureId) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
                        ", " + ToSQLParameter(data, TabParentStructureDataModel.DataColumns.IsAllTab) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

            return sql;
        }

        #endregion CreateOrUpdate

		#region DoesExist

		public static bool DoesExist(TabParentStructureDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new TabParentStructureDataModel();
			doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

        #region GetChildren

        private static DataSet GetChildren(TabParentStructureDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TabParentStructureChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, TabParentStructureDataModel.DataColumns.TabParentStructureId);

            var oDT = new DBDataSet("TabParentStructure.GetChildren", sql, DataStoreKey);

            return oDT.DBDataset;
        }

        #endregion

        #region DeleteChildren

        public static DataSet DeleteChildren(TabParentStructureDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TabParentStructureChildrenDelete " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, TabParentStructureDataModel.DataColumns.TabParentStructureId);

            var oDt = new DBDataSet("TabParentStructure.DeleteChildren", sql, DataStoreKey);

            return oDt.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(TabParentStructureDataModel data, RequestProfile requestProfile)
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
