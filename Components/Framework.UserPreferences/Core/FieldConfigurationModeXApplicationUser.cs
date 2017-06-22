using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{

    public partial class FieldConfigurationModeXApplicationUser : BaseClass
    {
        static string DataStoreKey = "";
        static int ApplicationId;

        static FieldConfigurationModeXApplicationUser()
        {
            ApplicationId = SetupConfiguration.ApplicationId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationModeXApplicationUser");
        }

        #region ToSqlParameter

        public static string ToSQLParameter(DomainModel.FieldConfigurationModeXApplicationUser data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeXApplicationUserId:
                    if (data.FieldConfigurationModeXApplicationUserId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeXApplicationUserId, data.FieldConfigurationModeXApplicationUserId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeXApplicationUserId);
                    }
                    break;

                case DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeId:
                    if (data.FieldConfigurationModeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeId, data.FieldConfigurationModeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeId);
                    }
                    break;

                case DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.ApplicationUserId:
                    if (data.ApplicationUserId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.ApplicationUserId, data.ApplicationUserId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.ApplicationUserId);
                    }
                    break;

                case DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeAccessModeId:
                    if (data.FieldConfigurationModeAccessModeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeAccessModeId, data.FieldConfigurationModeAccessModeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeAccessModeId);
                    }
                    break;

                case DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationMode:
                    if (!string.IsNullOrEmpty(data.FieldConfigurationMode))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationMode, data.FieldConfigurationMode);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationMode);
                    }
                    break;

                case DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.ApplicationUser:
                    if (!string.IsNullOrEmpty(data.ApplicationUser))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.ApplicationUser, data.ApplicationUser);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.ApplicationUser);
                    }
                    break;

                case DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeAccessMode:
                    if (!string.IsNullOrEmpty(data.FieldConfigurationModeAccessMode))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeAccessMode, data.FieldConfigurationModeAccessMode);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeAccessMode);
                    }
                    break;

                default:
                    returnValue = BaseClass.ToSQLParameter(data, dataColumnName);
                    break;
            }
            return returnValue;
        }

        #endregion

        #region Get By ApplicationUser

        public static DataTable GetByApplicationUser(int applicationUserId, int auditId)
        {
            var sql = "EXEC FieldConfigurationModeXApplicationUserSearch @ApplicationUserId     =" + applicationUserId + ", " +
                          " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId);
            var oDT = new Framework.Components.DataAccess.DBDataTable("GetDetails", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Delete

        public static void Delete(DomainModel.FieldConfigurationModeXApplicationUser data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeXApplicationUserDelete " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeXApplicationUserId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.ApplicationUserId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeAccessModeId);


            Framework.Components.DataAccess.DBDML.RunSQL("FieldConfigurationModeXApplicationUser.Delete", sql, DataStoreKey);
        }

        #endregion

        #region GetList

        public static DataTable GetList(int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeXApplicationUserList" +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("FieldConfigurationModeXApplicationUser.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        public static DataTable GetDetails(DomainModel.FieldConfigurationModeXApplicationUser data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeXApplicationUserDetails " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeXApplicationUserId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("FieldConfigurationModeXApplicationUser.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Search

        public static DataTable Search(DomainModel.FieldConfigurationModeXApplicationUser data, int auditId)
        {
            // formulate SQL
            var sql = "EXEC dbo.FieldConfigurationModeXApplicationUserSearch " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeXApplicationUserId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.ApplicationUserId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeAccessModeId); 

            var oDT = new Framework.Components.DataAccess.DBDataTable("FieldConfigurationModeXApplicationUser.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static DataTable SearchByFCModeAccessMode(DomainModel.FieldConfigurationModeXApplicationUser data, int auditId)
        {
            // formulate SQL
            var sql = "EXEC dbo.FieldConfigurationModeXApplicationUserSearchByFCModeAccessModeByFCModeAccessMode " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.ApplicationUserId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeAccessMode);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationModeXApplicationRole.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Create

        public static void Create(DomainModel.FieldConfigurationModeXApplicationUser data, int auditId)
        {
            var sql = Save(data, auditId, "Create");
            Framework.Components.DataAccess.DBDML.RunSQL("FieldConfigurationModeXApplicationUser.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(DomainModel.FieldConfigurationModeXApplicationUser data, int auditId)
        {
            var sql = Save(data, auditId, "Update");
            Framework.Components.DataAccess.DBDML.RunSQL("FieldConfigurationModeXApplicationUser.Update", sql, DataStoreKey);
        }

        #endregion

        #region Save

        private static string Save(DomainModel.FieldConfigurationModeXApplicationUser data, int auditId, string sqlcmd)
        {
            var sql = "EXEC ";

            switch (sqlcmd)
            {
                case "Create":
                    sql += "dbo.FieldConfigurationModeXApplicationUserInsert  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.FieldConfigurationModeXApplicationUserUpdate  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeXApplicationUserId) +
                        ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.ApplicationUserId) +
                        ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationUser.DataColumns.FieldConfigurationModeAccessModeId); 
            return sql;
        }

        #endregion

    }

}
