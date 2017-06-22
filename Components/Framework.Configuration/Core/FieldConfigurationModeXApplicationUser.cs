using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Configuration;

namespace Framework.Components.UserPreference
{

    public partial class FieldConfigurationModeXApplicationUserDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static FieldConfigurationModeXApplicationUserDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationModeXApplicationUser");
        }

        #region ToSqlParameter

        public static string ToSQLParameter(FieldConfigurationModeXApplicationUserDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeXApplicationUserId:
                    if (data.FieldConfigurationModeXApplicationUserId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeXApplicationUserId, data.FieldConfigurationModeXApplicationUserId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeXApplicationUserId);
                    }
                    break;

                case FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeId:
                    if (data.FieldConfigurationModeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeId, data.FieldConfigurationModeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeId);
                    }
                    break;

                case FieldConfigurationModeXApplicationUserDataModel.DataColumns.ApplicationUserId:
                    if (data.ApplicationUserId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationModeXApplicationUserDataModel.DataColumns.ApplicationUserId, data.ApplicationUserId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeXApplicationUserDataModel.DataColumns.ApplicationUserId);
                    }
                    break;

                case FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeAccessModeId:
                    if (data.FieldConfigurationModeAccessModeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeAccessModeId, data.FieldConfigurationModeAccessModeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeAccessModeId);
                    }
                    break;

                case FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationMode:
                    if (!string.IsNullOrEmpty(data.FieldConfigurationMode))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationMode, data.FieldConfigurationMode);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationMode);
                    }
                    break;

                case FieldConfigurationModeXApplicationUserDataModel.DataColumns.ApplicationUser:
                    if (!string.IsNullOrEmpty(data.ApplicationUser))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationModeXApplicationUserDataModel.DataColumns.ApplicationUser, data.ApplicationUser);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeXApplicationUserDataModel.DataColumns.ApplicationUser);
                    }
                    break;

                case FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeAccessMode:
                    if (!string.IsNullOrEmpty(data.FieldConfigurationModeAccessMode))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeAccessMode, data.FieldConfigurationModeAccessMode);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeAccessMode);
                    }
                    break;

                default:
                    returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
                    break;
            }
            return returnValue;
        }

        #endregion

        #region Get By ApplicationUser

        public static DataTable GetByApplicationUser(int applicationUserId, RequestProfile requestProfile)
        {
            var sql = "EXEC FieldConfigurationModeXApplicationUserSearch @ApplicationUserId     =" + applicationUserId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Delete

		public static void Delete(FieldConfigurationModeXApplicationUserDataModel data, RequestProfile requestProfile)
        {
			const string sql = @"dbo.FieldConfigurationModeXApplicationUserDelete ";

			var parameters =	new
								{
										AuditId										= requestProfile.AuditId
									,	FieldConfigurationModeXApplicationUserId	= data.FieldConfigurationModeXApplicationUserId
									,	FieldConfigurationModeId					= data.FieldConfigurationModeId
									,	ApplicationUserId							= data.ApplicationUserId
									,	FieldConfigurationModeAccessModeId			= data.FieldConfigurationModeAccessModeId
								};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
        }

        #endregion

        #region GetList

        public static List<FieldConfigurationModeXApplicationUserDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FieldConfigurationModeXApplicationUserDataModel.Empty, requestProfile, 0);
        }

        #endregion

        #region GetDetails

        public static FieldConfigurationModeXApplicationUserDataModel GetDetails(FieldConfigurationModeXApplicationUserDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        #endregion

        #region Search

        public static DataTable Search(FieldConfigurationModeXApplicationUserDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.FieldConfigurationModeXApplicationUserSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeXApplicationUserId) +
                ", " + ToSQLParameter(data, FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeId) +
                ", " + ToSQLParameter(data, FieldConfigurationModeXApplicationUserDataModel.DataColumns.ApplicationUserId) +
                ", " + ToSQLParameter(data, FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeAccessModeId); 

            var oDT = new DBDataTable("FieldConfigurationModeXApplicationUser.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static List<FieldConfigurationModeXApplicationUserDataModel> GetEntityDetails(FieldConfigurationModeXApplicationUserDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.FieldConfigurationModeXApplicationUserSearch ";

            var parameters =
            new
            {
                    AuditId                 = requestProfile.AuditId
                ,   ApplicationId           = requestProfile.ApplicationId
                ,   ReturnAuditInfo         = returnAuditInfo
                ,   FieldConfigurationModeXApplicationUserId             = dataQuery.FieldConfigurationModeXApplicationUserId
                ,   FieldConfigurationModeId                    = dataQuery.FieldConfigurationModeId
                ,   ApplicationUserId               = dataQuery.ApplicationUserId
                ,   FieldConfigurationModeAccessModeId               = dataQuery.FieldConfigurationModeAccessModeId
            };

            List<FieldConfigurationModeXApplicationUserDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<FieldConfigurationModeXApplicationUserDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }


		public static DataTable SearchByFCModeAccessMode(FieldConfigurationModeXApplicationUserDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.FieldConfigurationModeXApplicationUserSearchByFCModeAccessModeByFCModeAccessMode " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, FieldConfigurationModeXApplicationUserDataModel.DataColumns.ApplicationUserId) +
                ", " + ToSQLParameter(data, FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeAccessMode);

            var oDT = new DBDataTable("FieldConfigurationModeXApplicationRole.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Create

		public static void Create(FieldConfigurationModeXApplicationUserDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DBDML.RunSQL("FieldConfigurationModeXApplicationUser.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

		public static void Update(FieldConfigurationModeXApplicationUserDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DBDML.RunSQL("FieldConfigurationModeXApplicationUser.Update", sql, DataStoreKey);
        }

        #endregion

        #region Save

		private static string Save(FieldConfigurationModeXApplicationUserDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.FieldConfigurationModeXApplicationUserInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.FieldConfigurationModeXApplicationUserUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeXApplicationUserId) +
                        ", " + ToSQLParameter(data, FieldConfigurationModeXApplicationUserDataModel.DataColumns.ApplicationUserId) +
                        ", " + ToSQLParameter(data, FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeId) +
                ", " + ToSQLParameter(data, FieldConfigurationModeXApplicationUserDataModel.DataColumns.FieldConfigurationModeAccessModeId); 
            return sql;
        }

        #endregion

    }

}
