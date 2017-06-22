using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
    public class FieldConfigurationModeXApplicationRole: BaseClass
    {
        
        static string DataStoreKey = "";
        static int ApplicationId;

        static FieldConfigurationModeXApplicationRole()
        {
            ApplicationId = SetupConfiguration.ApplicationId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationModeXApplicationRole");
        }

        #region ToSQLParameter

        public static string ToSQLParameter(DomainModel.FieldConfigurationModeXApplicationRole data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {

                case DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeXApplicationRoleId:
                    if (data.FieldConfigurationModeXApplicationRoleId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeXApplicationRoleId, data.FieldConfigurationModeXApplicationRoleId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeXApplicationRoleId);
                    }
                    break;

                case DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeId:
                    if (data.FieldConfigurationModeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeId, data.FieldConfigurationModeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeId);
                    }
                    break;

                case DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.ApplicationRoleId:
                    if (data.ApplicationRoleId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.ApplicationRoleId, data.ApplicationRoleId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.ApplicationRoleId);
                    }
                    break;

                case DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeAccessModeId:
                    if (data.FieldConfigurationModeAccessModeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeAccessModeId, data.FieldConfigurationModeAccessModeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeAccessModeId);
                    }
                    break;

                case DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationMode:
                    if (!string.IsNullOrEmpty(data.FieldConfigurationMode))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationMode, data.FieldConfigurationMode);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationMode);
                    }
                    break;

                case DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.ApplicationRole:
                    if (!string.IsNullOrEmpty(data.ApplicationRole))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.ApplicationRole, data.ApplicationRole);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.ApplicationRole);
                    }
                    break;

                case DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeAccessMode:
                    if (!string.IsNullOrEmpty(data.FieldConfigurationModeAccessMode))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeAccessMode, data.FieldConfigurationModeAccessMode);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeAccessMode);
                    }
                    break;

                default:
                    returnValue = StandardClass.ToSQLParameter(data, dataColumnName);
                    break;
            }
            return returnValue;
        }

        #endregion

        #region GetList

        public static DataTable GetList(int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeXApplicationRoleList" +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationModeXApplicationRole.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        public static DataTable GetDetails(DomainModel.FieldConfigurationModeXApplicationRole data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeXApplicationRoleDetails " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeXApplicationRoleId);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationModeXApplicationRole.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static List<DomainModel.FieldConfigurationModeXApplicationRole> GetEntityList(DomainModel.FieldConfigurationModeXApplicationRole data, int auditId)
        {
            var oDT = GetDetails(data, auditId);

            var dataList = new List<DomainModel.FieldConfigurationModeXApplicationRole>();

            if (oDT.Rows.Count == 1)
            {
                var oData = new DomainModel.FieldConfigurationModeXApplicationRole();

                var row = oDT.Rows[0];

                oData.FieldConfigurationModeXApplicationRoleId = (int?)row[DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeXApplicationRoleId];

                oData.FieldConfigurationModeId                 = (int?)row[DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeId];
                oData.ApplicationRoleId                        = (int?)row[DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.ApplicationRoleId];
                oData.FieldConfigurationModeAccessModeId       = (int?)row[DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeAccessModeId];

                oData.FieldConfigurationMode                   = (string)row[DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationMode];
                oData.ApplicationRole                          = (string)row[DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.ApplicationRole];
                oData.FieldConfigurationModeAccessMode         = (string)row[DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeAccessMode];
               
                dataList.Add(oData);
            }

            return dataList;
        }

        #endregion

        #region Create

        public static int Create(DomainModel.FieldConfigurationModeXApplicationRole data, int auditId)
        {
            var sql = Save(data, auditId, "Create");
            var FieldConfigurationModeXApplicationRoleId = DataAccess.DBDML.RunScalarSQL("FieldConfigurationModeXApplicationRole.Insert", sql, DataStoreKey);
            return Convert.ToInt32(FieldConfigurationModeXApplicationRoleId);
        }

        #endregion

        #region Update

        public static void Update(DomainModel.FieldConfigurationModeXApplicationRole data, int auditId)
        {
            var sql = Save(data, auditId, "Update");
            DataAccess.DBDML.RunSQL("FieldConfigurationModeXApplicationRole.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(DomainModel.FieldConfigurationModeXApplicationRole data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationModeXApplicationRoleDelete " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeXApplicationRoleId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeAccessModeId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.ApplicationRoleId);

            DataAccess.DBDML.RunSQL("FieldConfigurationModeXApplicationRole.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static DataTable Search(DomainModel.FieldConfigurationModeXApplicationRole data, int auditId)
        {
            // formulate SQL
            var sql = "EXEC dbo.FieldConfigurationModeXApplicationRoleSearch " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeXApplicationRoleId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.ApplicationRoleId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeAccessModeId);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationModeXApplicationRole.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static DataTable SearchByFCModeAccessMode(DomainModel.FieldConfigurationModeXApplicationRole data, int auditId)
        {
            // formulate SQL
            var sql = "EXEC dbo.FieldConfigurationModeXApplicationRoleSearchByFCModeAccessModeByFCModeAccessMode " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.ApplicationRoleId) +
                ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeAccessMode);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationModeXApplicationRole.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        private static string Save(DomainModel.FieldConfigurationModeXApplicationRole data, int auditId, string sqlcmd)
        {
            var sql = "EXEC ";

            switch (sqlcmd)
            {
                case "Create":
                    sql += "dbo.FieldConfigurationModeXApplicationRoleInsert  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.FieldConfigurationModeXApplicationRoleUpdate  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeXApplicationRoleId) +
                        ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeId) +
                        ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.ApplicationRoleId) +
                        ", " + ToSQLParameter(data, DomainModel.FieldConfigurationModeXApplicationRole.DataColumns.FieldConfigurationModeAccessModeId);

            return sql;
        }

        #endregion

    }
}
