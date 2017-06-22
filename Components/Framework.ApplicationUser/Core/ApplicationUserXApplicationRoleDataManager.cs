using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace Framework.Components.ApplicationUser
{
    public partial class ApplicationUserXApplicationRoleDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static ApplicationUserXApplicationRoleDataManager()
        {
            DataStoreKey = DataAccess.SetupConfiguration.GetDataStoreKey("ApplicationUserXApplicationRole");
        }

        #region ToSqlParameter

        public string ToSQLParameter(ApplicationUserXApplicationRoleDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {

                case ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationUserXApplicationRoleId:
                    if (data.ApplicationUserXApplicationRoleId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationUserXApplicationRoleId, data.ApplicationUserXApplicationRoleId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationUserXApplicationRoleId);
                    }
                    break;

                case ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationRoleId:
                    if (data.ApplicationRoleId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationRoleId, data.ApplicationRoleId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationRoleId);

                    }
                    break;

                case ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationUserId:
                    if (data.ApplicationUserId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationUserId, data.ApplicationUserId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationUserId);

                    }
                    break;

                case ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationRole:
                    if (!string.IsNullOrEmpty(data.ApplicationRole))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationRole, data.ApplicationRole);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationRole);
                    }
                    break;

                case ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationUser:
                    if (!string.IsNullOrEmpty(data.ApplicationUser))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationUser, data.ApplicationUser);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationUserXApplicationRoleDataModel.DataColumns.ApplicationUser);
                    }
                    break;

            }
            return returnValue;
        }

        #endregion

        #region CreateByApplicationUser

        public static void CreateByApplicationUser(int applicationUserId, int[] applicationRoleIds, RequestProfile requestProfile)
        {
            foreach (var applicationRoleId in applicationRoleIds)
            {
                {
                    var sql = "EXEC dbo.ApplicationUserXApplicationRoleInsert " +
                                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                                ", @ApplicationUserId						=" + applicationUserId +
                                ", @ApplicationRoleId						=" + applicationRoleId;

                    DataAccess.DBDML.RunSQL("ApplicationUserXApplicationRole_Insert", sql,
                                                                 DataStoreKey);
                }
            }
        }

        #endregion CreateByApplicationUser

        #region CreateByApplicationRole

        public static void CreateByApplicationRole(int applicationRoleId, int[] applicationUserIds, RequestProfile requestProfile)
        {
            foreach (int applicationUserId in applicationUserIds)
            {
                var sql = "EXEC dbo.ApplicationUserXApplicationRoleInsert  " +
                                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                                ", @ApplicationUserId						 =" + applicationUserId +
                                ", @ApplicationRoleId						 =" + applicationRoleId;
                DataAccess.DBDML.RunSQL("ApplicationUserXApplicationRole_Insert", sql, DataStoreKey);
            }
        }

        #endregion CreateByApplicationRole

        #region GetByApplicationRole

        public static DataTable GetByApplicationRole(int applicationRoleId, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationUserXApplicationRoleSearch " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", @ApplicationRoleId	=" + applicationRoleId;

            var oDT = new DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion GetByApplicationRole

        #region GetByApplicationUser

        public static DataTable GetByApplicationUser(int applicationUserId, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationUserXApplicationRoleSearch " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", @ApplicationUserId	=" + applicationUserId;

            var oDT = new DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion GetByApplicationUser

        #region DeleteByApplicationRole

        public static void DeleteByApplicationRole(int applicationRoleId, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationUserXApplicationRoleDelete " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", @ApplicationRoleId	=" + applicationRoleId;
            DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion DeleteByApplicationRole

        #region Delete By ApplicationUser

        public static void DeleteByApplicationUser(int applicationUserId, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ApplicationUserXApplicationRoleDelete " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", @ApplicationUserId	=" + applicationUserId;

            DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

    }
}

