using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
    public partial class UserPreferenceKey : BaseClass
    {

        static readonly string DataStoreKey = "";
        static readonly int ApplicationId;

        static UserPreferenceKey()
        {
            ApplicationId = SetupConfiguration.ApplicationId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("UserPreferenceKey");
        }

        #region GetList

        public static DataTable GetList(int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceKeyList" +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);

            var oDT = new DataAccess.DBDataTable("UserPreferenceKey.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        public static DataTable GetDetails(DomainModel.UserPreferenceKey data, int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceKeyDetails " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(data, DomainModel.UserPreferenceKey.DataColumns.UserPreferenceKeyId);

            var oDT = new DataAccess.DBDataTable("UserPreferenceKey.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static List<DomainModel.UserPreferenceKey> GetEntityDetails(DomainModel.UserPreferenceKey data, int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceKeyDetails " +
                      " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                      ", " + ToSQLParameter(data, DomainModel.UserPreferenceKey.DataColumns.UserPreferenceKeyId);

            var result = new List<DomainModel.UserPreferenceKey>();

            using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
            {
                var dbReader = reader.DBReader;

                while (dbReader.Read())
                {
                    var dataItem = new DomainModel.UserPreferenceKey();

                    dataItem.UserPreferenceKeyId    = (int)dbReader[DomainModel.UserPreferenceKey.DataColumns.UserPreferenceKeyId];
                    dataItem.Name                   = dbReader[DomainModel.UserPreferenceKey.DataColumns.Name].ToString();
                    dataItem.Description            = dbReader[DomainModel.UserPreferenceKey.DataColumns.Description].ToString();
                    dataItem.SortOrder              = (int)dbReader[DomainModel.UserPreferenceKey.DataColumns.SortOrder];

					SetBaseInfo(dataItem, dbReader);

                    result.Add(dataItem);
                }
            }

            return result;
        }

        #endregion

        #region Create

        public static void Create(DomainModel.UserPreferenceKey data, int auditId)
        {
            var sql = Save(data, auditId, "Create");
            DataAccess.DBDML.RunSQL("UserPreferenceKey.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(DomainModel.UserPreferenceKey data, int auditId)
        {
            var sql = Save(data, auditId, "Update");
            DataAccess.DBDML.RunSQL("UserPreferenceKey.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(DomainModel.UserPreferenceKey data, int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceKeyDelete " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(data, DomainModel.UserPreferenceKey.DataColumns.UserPreferenceKeyId);

            DataAccess.DBDML.RunSQL("UserPreferenceKey.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static DataTable Search(DomainModel.UserPreferenceKey data, int auditId)
        {
            // formulate SQL
            var sql = "EXEC dbo.UserPreferenceKeySearch " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + ToSQLParameter(data, DomainModel.UserPreferenceKey.DataColumns.UserPreferenceKeyId) +
                ", " + ToSQLParameter(data, DomainModel.UserPreferenceKey.DataColumns.Name) +
                ", " + ToSQLParameter(data, DomainModel.UserPreferenceKey.DataColumns.DataTypeId);

            var oDT = new DataAccess.DBDataTable("UserPreferenceKey.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        private static string Save(DomainModel.UserPreferenceKey data, int auditId, string sqlcmd)
        {
            var sql = "EXEC ";

            switch (sqlcmd)
            {
                case "Create":
                    sql += "dbo.UserPreferenceKeyInsert  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.UserPreferenceKeyUpdate  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, DomainModel.UserPreferenceKey.DataColumns.UserPreferenceKeyId) +
                        ", " + ToSQLParameter(data, DomainModel.UserPreferenceKey.DataColumns.Name) +
                        ", " + ToSQLParameter(data, DomainModel.UserPreferenceKey.DataColumns.Value) +
                        ", " + ToSQLParameter(data, DomainModel.UserPreferenceKey.DataColumns.DataTypeId) +
                        ", " + ToSQLParameter(data, DomainModel.UserPreferenceKey.DataColumns.Description) +
                        ", " + ToSQLParameter(data, DomainModel.UserPreferenceKey.DataColumns.SortOrder);
            return sql;
        }

        #endregion

        #region DoesExist

        public static DataTable DoesExist(DomainModel.UserPreferenceKey data, int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceKeyDoesExist " +
            " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
            ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + ToSQLParameter(data, DomainModel.UserPreferenceKey.DataColumns.UserPreferenceKeyId);

            var oDT = new DataAccess.DBDataTable("UserPreferenceKey.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetChildren

        private static DataSet GetChildren(DomainModel.UserPreferenceKey data, int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceKeyChildrenGet " +
                            " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                            ", " + ToSQLParameter(data, DomainModel.UserPreferenceKey.DataColumns.UserPreferenceKeyId);

            var oDT = new DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(DomainModel.UserPreferenceKey data, int auditId)
        {
            var isDeletable = true;
            var ds = GetChildren(data, auditId);
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

    }
}
