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
    public partial class ApplicationOperationXApplicationRoleDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static ApplicationOperationXApplicationRoleDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationOperationXApplicationRole");
        }

        #region ToSqlParameter

        public static string ToSQLParameter(ApplicationOperationXApplicationRoleDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {

                case ApplicationOperationXApplicationRoleDataModel.DataColumns.ApplicationOperationXApplicationRoleId:
                    if (data.ApplicationOperationXApplicationRoleId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationOperationXApplicationRoleDataModel.DataColumns.ApplicationOperationXApplicationRoleId, data.ApplicationOperationXApplicationRoleId);
                    }

                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationOperationXApplicationRoleDataModel.DataColumns.ApplicationOperationXApplicationRoleId);
                    }
                    break;

                case ApplicationOperationXApplicationRoleDataModel.DataColumns.ApplicationOperationId:
                    if (data.ApplicationOperationId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationOperationXApplicationRoleDataModel.DataColumns.ApplicationOperationId, data.ApplicationOperationId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationOperationXApplicationRoleDataModel.DataColumns.ApplicationOperationId);

                    }
                    break;

                case ApplicationOperationXApplicationRoleDataModel.DataColumns.ApplicationRoleId:
                    if (data.ApplicationRoleId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ApplicationOperationXApplicationRoleDataModel.DataColumns.ApplicationRoleId, data.ApplicationRoleId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ApplicationOperationXApplicationRoleDataModel.DataColumns.ApplicationRoleId);

                    }
                    break;

                default:
                    returnValue = BaseDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }
            return returnValue;
        }

        #endregion

        #region Create By ApplicationOperation

        public static void Create(int applicationOperationId, int[] ApplicationRoleIds, RequestProfile requestProfile)
        {
            foreach (int applicationRoleId in ApplicationRoleIds)
            {
                var sql = "EXEC dbo.ApplicationOperationXApplicationRoleInsert " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                        ", @ApplicationOperationId		= " + applicationOperationId +
                        ", @ApplicationRoleId			= " + applicationRoleId;

                DataAccess.DBDML.RunSQL("ApplicationOperationXApplicationRole_Insert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Create By ApplicationRoles

        public static void CreateByApplicationRole(int applicationRoleId, int[] ApplicationOperationIds, RequestProfile requestProfile)
        {
            foreach (int applicationOperationId in ApplicationOperationIds)
            {
                var sql = "EXEC dbo.ApplicationOperationXApplicationRoleInsert " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                        ", @ApplicationOperationId					= " + applicationOperationId +
                        ", @ApplicationRoleId						= " + applicationRoleId;
                DataAccess.DBDML.RunSQL("ApplicationOperationXApplicationRole_Insert", sql, DataStoreKey);
            }
        }
        #endregion

        #region Get By ApplicationRole

        public static DataTable GetByApplicationRole(int applicationRoleId, RequestProfile requestProfile)
        {
            var sql = "EXEC ApplicationOperationXApplicationRoleSearch " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", @ApplicationRoleId	=" + applicationRoleId;

            var oDT = new DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Get By ApplicationOperation

        public static DataTable GetByApplicationOperation(int applicationOperationId, RequestProfile requestProfile)
        {
            var sql = "EXEC ApplicationOperationXApplicationRoleSearch " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", @ApplicationOperationId		=" + applicationOperationId;
            var oDT = new DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Delete By ApplicationRole

        public static void DeleteByApplicationRole(int applicationRoleId, RequestProfile requestProfile)
        {
            var sql = "EXEC ApplicationOperationXApplicationRoleDelete " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", @ApplicationRoleId	=" + applicationRoleId;
            DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Delete By ApplicationOperation

        public static void DeleteByApplicationOperation(int applicationOperationId, RequestProfile requestProfile)
        {
            var sql = "EXEC ApplicationOperationXApplicationRoleDelete " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", @ApplicationOperationId		=" + applicationOperationId;

            DataAccess.DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion
    }
}
