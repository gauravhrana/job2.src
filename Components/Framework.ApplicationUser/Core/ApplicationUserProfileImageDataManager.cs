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
    public partial class ApplicationUserProfileImageDataManager : BaseDataManager
    {

        static readonly string DataStoreKey = "";

        static ApplicationUserProfileImageDataManager()
        {
            DataStoreKey = DataAccess.SetupConfiguration.GetDataStoreKey("ApplicationUserProfileImage");
        }

        #region ToSqlParameter

        public static string ToSQLParameter(ApplicationUserProfileImageDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {

                case ApplicationUserProfileImageDataModel.DataColumns.ApplicationUserProfileImageId:
                    if (data.ApplicationUserProfileImageId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationUserProfileImageDataModel.DataColumns.ApplicationUserProfileImageId, data.ApplicationUserProfileImageId);
                    }

                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserProfileImageDataModel.DataColumns.ApplicationUserProfileImageId);

                    }
                    break;

                case ApplicationUserProfileImageDataModel.DataColumns.ApplicationUserId:
                    if (data.ApplicationUserId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationUserProfileImageDataModel.DataColumns.ApplicationUserId, data.ApplicationUserId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserProfileImageDataModel.DataColumns.ApplicationUserId);

                    }
                    break;

                case ApplicationUserProfileImageDataModel.DataColumns.Image:
                    if (data.Image != null)
                    {
                        returnValue = Convert.ToString(data.Image);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserProfileImageDataModel.DataColumns.Image);
                    }
                    break;

                case ApplicationUserProfileImageDataModel.DataColumns.ApplicationId:
                    if (data.ApplicationId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationUserProfileImageDataModel.DataColumns.ApplicationId, data.ApplicationId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserProfileImageDataModel.DataColumns.ApplicationId);

                    }
                    break;

                case ApplicationUserProfileImageDataModel.DataColumns.ApplicationUserName:
                    if (!string.IsNullOrEmpty(data.ApplicationUserName))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationUserProfileImageDataModel.DataColumns.ApplicationUserName, data.ApplicationUserName);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserProfileImageDataModel.DataColumns.ApplicationUserName);
                    }
                    break;

                case ApplicationUserProfileImageDataModel.DataColumns.Application:
                    if (!string.IsNullOrEmpty(data.Application))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationUserProfileImageDataModel.DataColumns.Application, data.Application);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserProfileImageDataModel.DataColumns.Application);
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

        public static List<ApplicationUserProfileImageDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ApplicationUserProfileImageDataModel.Empty, requestProfile, 0);
        }

        #endregion

        #region GetDetails

        public static ApplicationUserProfileImageDataModel GetDetails(ApplicationUserProfileImageDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

		public static List<ApplicationUserProfileImageDataModel> GetEntityDetails(ApplicationUserProfileImageDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ApplicationUserProfileImageSearch ";

			var parameters =
			new
			{
				ApplicationId = dataQuery.ApplicationId
				,
				ApplicationUserId = dataQuery.ApplicationUserId
				,
				ApplicationUserProfileImageId = dataQuery.ApplicationUserProfileImageId
			};

			List<ApplicationUserProfileImageDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ApplicationUserProfileImageDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

        #endregion

        #region Create

        public static void Create(ApplicationUserProfileImageDataModel data, RequestProfile requestProfile)
        {
            var sql = "ApplicationUserProfileImageInsert"; //Save(data, auditId, "Create");

            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId));
            parameters.Add(new SqlParameter(ApplicationUserProfileImageDataModel.DataColumns.ApplicationId, data.ApplicationId));
            parameters.Add(new SqlParameter(ApplicationUserProfileImageDataModel.DataColumns.ApplicationUserId, data.ApplicationUserId));
            parameters.Add(new SqlParameter(ApplicationUserProfileImageDataModel.DataColumns.Image, data.Image));

            DataAccess.DBDML.RunSQLWithParameters("ApplicationUserProfileImage.Insert", sql, parameters.ToArray(), DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(ApplicationUserProfileImageDataModel data, RequestProfile requestProfile)
        {
            var sql = "ApplicationUserProfileImageUpdate";
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId));
            parameters.Add(new SqlParameter(ApplicationUserProfileImageDataModel.DataColumns.ApplicationUserProfileImageId, data.ApplicationUserProfileImageId));
            parameters.Add(new SqlParameter(ApplicationUserProfileImageDataModel.DataColumns.ApplicationId, data.ApplicationId));
            parameters.Add(new SqlParameter(ApplicationUserProfileImageDataModel.DataColumns.ApplicationUserId, data.ApplicationUserId));
            parameters.Add(new SqlParameter(ApplicationUserProfileImageDataModel.DataColumns.Image, data.Image));

            DataAccess.DBDML.RunSQLWithParameters("ApplicationUserProfileImage.Insert", sql, parameters.ToArray(), DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(ApplicationUserProfileImageDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.ApplicationUserProfileImageDelete ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                ApplicationUserProfileImageId = dataQuery.ApplicationUserProfileImageId

            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion

        #region Search

        public static DataTable Search(ApplicationUserProfileImageDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.ApplicationUserProfileImageSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, requestProfile.ApplicationModeId) +
                ", " + ToSQLParameter(data, ApplicationUserProfileImageDataModel.DataColumns.ApplicationId) +
                ", " + ToSQLParameter(data, ApplicationUserProfileImageDataModel.DataColumns.ApplicationUserProfileImageId) +
                ", " + ToSQLParameter(data, ApplicationUserProfileImageDataModel.DataColumns.ApplicationUserId);

            var oDT = new DataAccess.DBDataTable("ApplicationUserProfileImage.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region CreateOrUpdate

        private static string CreateOrUpdate(ApplicationUserProfileImageDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ApplicationUserProfileImageInsert  " + " " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ApplicationUserProfileImageUpdate  " + " ";
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(data, ApplicationUserProfileImageDataModel.DataColumns.ApplicationUserProfileImageId) +
                        ", " + ToSQLParameter(data, ApplicationUserProfileImageDataModel.DataColumns.ApplicationUserId) +
                        ", " + ToSQLParameter(data, ApplicationUserProfileImageDataModel.DataColumns.Image);
            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(ApplicationUserProfileImageDataModel data, RequestProfile requestProfile)
        {
            var doesExistRequest = new ApplicationUserProfileImageDataModel();
            doesExistRequest.ApplicationId = data.ApplicationId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
        }

        #endregion

    }
}
