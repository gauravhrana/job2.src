using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;


namespace Framework.Components.ApplicationUser
{

    public partial class ApplicationDataManager : StandardDataManager
    {
        static readonly string DataStoreKey = "";

		static ApplicationDataManager()
        {            
            DataStoreKey = SetupConfiguration.GetDataStoreKey("Application");
        }

        #region GetList

        public static DataTable GetList(RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			var oDT = new DataAccess.DBDataTable("Application.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

		public static List<ApplicationDataModel> GetApplicationList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ApplicationSearch " +
			   " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

			var result = new List<ApplicationDataModel>();

			using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new ApplicationDataModel();

					dataItem.ApplicationId = (int)dbReader[ApplicationDataModel.DataColumns.ApplicationId];

					SetStandardInfo(dataItem, dbReader);					

					result.Add(dataItem);
				}
			}

			return result;
		}

        #endregion

        #region GetDetails

		public static DataTable GetDetails(ApplicationDataModel data, RequestProfile requestProfile)
        {
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
        }

		#endregion

		#region GetEntitySearch

		public static List<ApplicationDataModel> GetEntityDetails(ApplicationDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
			const string sql = @"dbo.ApplicationSearch ";

			var parameters =
			new
			{
					AuditId						= requestProfile.AuditId
				,	ApplicationId				= dataQuery.ApplicationId
				,	ApplicationMode				= requestProfile.ApplicationModeId
				,	ReturnAuditInfo				= returnAuditInfo
				,	Name						= dataQuery.Name
			};

			List<ApplicationDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ApplicationDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}


            return result;
        }

        #endregion GetDetails

        #region Create

		public static void Create(ApplicationDataModel data, RequestProfile requestProfile)
        {
			var sql = CreateOrUpdate(data, requestProfile, "Create");
			DataAccess.DBDML.RunSQL("Application.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

		public static void Update(ApplicationDataModel data, RequestProfile requestProfile)
        {
			var sql = CreateOrUpdate(data, requestProfile, "Update");
			DataAccess.DBDML.RunSQL("Application.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

		public static void Delete(ApplicationDataModel dataQuery, RequestProfile requestProfile)
        {
			const string sql = @"dbo.ApplicationDelete ";

			var parameters =
			new
			{
					AuditId			= requestProfile.AuditId
				,	ApplicationId	= dataQuery.ApplicationId
				
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
        }

        #endregion

        #region Search

		public static string ToSQLParameter(ApplicationDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ApplicationDataModel.DataColumns.ApplicationId:
					if (data.ApplicationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationDataModel.DataColumns.ApplicationId, data.ApplicationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationDataModel.DataColumns.ApplicationId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

		public static DataTable Search(ApplicationDataModel data, RequestProfile requestProfile)
        {
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
        }

        #endregion

        #region CreateOrUpdate

		private static string CreateOrUpdate(ApplicationDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ApplicationInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                case "Update":
                    sql += "dbo.ApplicationUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

			sql = sql + ", " + ToSQLParameter(data, ApplicationDataModel.DataColumns.ApplicationId) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);
            return sql;
        }

        #endregion

        #region DoesExist

		public static DataTable DoesExist(ApplicationDataModel data, RequestProfile requestProfile)
        {
			var doesExistRequest = new ApplicationDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
        }

        #endregion

        #region GetChildren

		private static DataSet GetChildren(ApplicationDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ApplicationDataModel.DataColumns.ApplicationId);

            var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

		public static bool IsDeletable(ApplicationDataModel data, RequestProfile requestProfile)
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

        #region DeleteChildren

		static public DataSet DeleteChildren(ApplicationDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ApplicationDataModel.DataColumns.ApplicationId);

            var oDT = new Framework.Components.DataAccess.DBDataSet("Delete Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

    }

}
