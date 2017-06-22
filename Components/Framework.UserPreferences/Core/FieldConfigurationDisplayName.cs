using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{

    public partial class FieldConfigurationDisplayName : DataAccess.BaseClass
    {

        static string DataStoreKey = "";
        static readonly int ApplicationId;

        static FieldConfigurationDisplayName()
        {
            ApplicationId = SetupConfiguration.ApplicationId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationDisplayName");
        }

        #region GetList

        public static DataTable GetList(int auditId)
        {

            DataStoreKey = SetupConfiguration.GetDataStoreKey("FieldConfigurationDisplayName");
            var sql = "EXEC dbo.FieldConfigurationDisplayNameList" +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationDisplayName.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        public static DataTable GetDetails(Data data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationDisplayNameDetails " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + data.ToSQLParameter(DataColumns.FieldConfigurationDisplayNameId);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationDisplayName.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Create

        public static void Create(Data data, int auditId)
        {
            var sql = Save(data, auditId, "Create");
            DataAccess.DBDML.RunSQL("FieldConfigurationDisplayName.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(Data data, int auditId)
        {
            var sql = Save(data, auditId, "Update");
            DataAccess.DBDML.RunSQL("FieldConfigurationDisplayName.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(Data data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationDisplayNameDelete " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + data.ToSQLParameter(DataColumns.FieldConfigurationDisplayNameId);

            DataAccess.DBDML.RunSQL("FieldConfigurationDisplayName.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static DataTable Search(Data data, int auditId)
        {
            // formulate SQL
            var sql = "EXEC dbo.FieldConfigurationDisplayNameSearch " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + data.ToSQLParameter(DataColumns.FieldConfigurationDisplayNameId) +
                ", " + data.ToSQLParameter(DataColumns.LanguageId) +
                ", " + data.ToSQLParameter(DataColumns.Value) +
                ", " + data.ToSQLParameter(DataColumns.FieldConfigurationId) +
                ", " + data.ToSQLParameter(DataColumns.IsDefault);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationDisplayName.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        private static string Save(Data data, int auditId, string sqlcmd)
        {
            var sql = "EXEC ";

            switch (sqlcmd)
            {
                case "Create":
                    sql += "dbo.FieldConfigurationDisplayNameInsert  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.FieldConfigurationDisplayNameUpdate  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + data.ToSQLParameter(DataColumns.FieldConfigurationDisplayNameId) +
                ", " + data.ToSQLParameter(DataColumns.LanguageId) +
                ", " + data.ToSQLParameter(DataColumns.Value) +
                ", " + data.ToSQLParameter(DataColumns.FieldConfigurationId) +
                ", " + data.ToSQLParameter(DataColumns.IsDefault);
            return sql;
        }

        #endregion

        #region DoesExist

        public static DataTable DoesExist(Data data, int auditId)
        {
            var sql = "EXEC dbo.FieldConfigurationDisplayNameDoesExist " +
            " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
            ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + data.ToSQLParameter(DataColumns.LanguageId) +
                ", " + data.ToSQLParameter(DataColumns.FieldConfigurationId);

            var oDT = new DataAccess.DBDataTable("FieldConfigurationDisplayName.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion


    }

}
