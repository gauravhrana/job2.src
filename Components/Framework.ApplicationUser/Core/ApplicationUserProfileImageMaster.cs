using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Data.SqlClient;
using Dapper;

namespace Framework.Components.ApplicationUser
{
    public partial class ApplicationUserProfileImageMasterDataManager : BaseDataManager
    {
        static string DataStoreKey = "";
        
		static ApplicationUserProfileImageMasterDataManager()
        {
            DataStoreKey = DataAccess.SetupConfiguration.GetDataStoreKey("ApplicationUserProfileImageMaster");
        }

        #region ToSqlParameter

        public static string ToSQLParameter(ApplicationUserProfileImageMasterDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {

                case ApplicationUserProfileImageMasterDataModel.DataColumns.ApplicationUserProfileImageMasterId:
                    if (data.ApplicationUserProfileImageMasterId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationUserProfileImageMasterDataModel.DataColumns.ApplicationUserProfileImageMasterId, data.ApplicationUserProfileImageMasterId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserProfileImageMasterDataModel.DataColumns.ApplicationUserProfileImageMasterId);
                    }
                    break;

                case ApplicationUserProfileImageMasterDataModel.DataColumns.Title:
                    if (!string.IsNullOrEmpty(data.Title))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationUserProfileImageMasterDataModel.DataColumns.Title, data.Title);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserProfileImageMasterDataModel.DataColumns.Title);
                    }
                    break;

                case ApplicationUserProfileImageMasterDataModel.DataColumns.Image:
                    if (data.Image != null)
                    {
                        returnValue = Convert.ToString(data.Image);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserProfileImageMasterDataModel.DataColumns.Image);
                    }
                    break;

                case ApplicationUserProfileImageMasterDataModel.DataColumns.Application:
                    if (!string.IsNullOrEmpty(data.Application))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationUserProfileImageMasterDataModel.DataColumns.Application, data.Application);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserProfileImageMasterDataModel.DataColumns.Application);
                    }
                    break;

                default:
                    returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }
            return returnValue;
        }

        #endregion

        #region GetList

        public static DataTable GetList(RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationUserProfileImageMasterSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

            var oDT = new DataAccess.DBDataTable("ApplicationUserProfileImageMaster.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

		public static DataTable GetDetails(ApplicationUserProfileImageMasterDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationUserProfileImageMasterSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				//", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo,BaseDataManager.ReturnAduitInfoOnDetails) +
                ", " + ToSQLParameter(data, ApplicationUserProfileImageMasterDataModel.DataColumns.ApplicationUserProfileImageMasterId);

            var oDT = new DataAccess.DBDataTable("ApplicationUserProfileImageMaster.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Create

		public static void Create(ApplicationUserProfileImageMasterDataModel data, RequestProfile requestProfile)
        {
            var sql = "ApplicationUserProfileImageMasterInsert"; //Save(data, auditId, "Create");

            var parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId));
            parameters.Add(new SqlParameter(BaseDataModel.BaseDataColumns.ApplicationId, data.ApplicationId));
            parameters.Add(new SqlParameter(ApplicationUserProfileImageMasterDataModel.DataColumns.Title, data.Title));
            parameters.Add(new SqlParameter(ApplicationUserProfileImageMasterDataModel.DataColumns.Image, data.Image));

            DataAccess.DBDML.RunSQLWithParameters("ApplicationUserProfileImageMaster.Insert", sql, parameters.ToArray(), DataStoreKey);
        }

        #endregion

        #region Update

		public static void Update(ApplicationUserProfileImageMasterDataModel data, RequestProfile requestProfile)
        {
			var sql = CreateOrUpdate(data, requestProfile, "Update");

            var parameters = new List<SqlParameter>();
			parameters.Add(new SqlParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId));
            parameters.Add(new SqlParameter(BaseDataModel.BaseDataColumns.ApplicationId, data.ApplicationId));
            parameters.Add(new SqlParameter(ApplicationUserProfileImageMasterDataModel.DataColumns.Image, data.Image));

            DataAccess.DBDML.RunSQLWithParameters("ApplicationUserProfileImageMaster.Insert", sql, parameters.ToArray(), DataStoreKey);
        }

        #endregion

        #region Delete

		public static void Delete(ApplicationUserProfileImageMasterDataModel dataQuery, RequestProfile requestProfile)
        {
			const string sql = @"dbo.ApplicationUserProfileImageMasterDelete ";

			var parameters =
			new
			{
					AuditId									= requestProfile.AuditId
				,	ApplicationUserProfileImageMasterId		= dataQuery.ApplicationUserProfileImageMasterId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
        }

        #endregion

        #region Search

		public static DataTable Search(ApplicationUserProfileImageMasterDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.ApplicationUserProfileImageMasterSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, requestProfile.ApplicationModeId) +
                ", " + ToSQLParameter(data, BaseDataModel.BaseDataColumns.ApplicationId) +
                ", " + ToSQLParameter(data, ApplicationUserProfileImageMasterDataModel.DataColumns.ApplicationUserProfileImageMasterId) +
                ", " + ToSQLParameter(data, ApplicationUserProfileImageMasterDataModel.DataColumns.Title);

            var oDT = new DataAccess.DBDataTable("ApplicationUserProfileImageMaster.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region CreateOrUpdate

		private static string CreateOrUpdate(ApplicationUserProfileImageMasterDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
					sql += "dbo.ApplicationUserProfileImageMasterInsert  " + " " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) + ", ";
                    break;

                case "Update":
                    sql += "dbo.ApplicationUserProfileImageMasterUpdate " + " ";
                    break;

                default:
                    break;

            }

			sql = sql + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(data, ApplicationUserProfileImageMasterDataModel.DataColumns.ApplicationUserProfileImageMasterId) +
                        ", " + ToSQLParameter(data, ApplicationUserProfileImageMasterDataModel.DataColumns.Title) +
                        ", " + ToSQLParameter(data, ApplicationUserProfileImageMasterDataModel.DataColumns.Image);
            return sql;
        }

        #endregion

        #region DoesExist

		public static DataTable DoesExist(ApplicationUserProfileImageMasterDataModel data, RequestProfile requestProfile)
        {
			var doesExistRequest = new ApplicationUserProfileImageMasterDataModel();
			doesExistRequest.ApplicationId = data.ApplicationId;

			return Search(doesExistRequest, requestProfile);
        }

        #endregion

    }
}
