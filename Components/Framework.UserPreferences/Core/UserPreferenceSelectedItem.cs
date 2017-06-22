using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using System.Reflection;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
    public partial class UserPreferenceSelectedItem : BaseClass
    {
        static readonly string DataStoreKey = "";
        static readonly int ApplicationId;

        static UserPreferenceSelectedItem()
        {
            ApplicationId = SetupConfiguration.ApplicationId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("UserPreferenceSelectedItem");
        }

        #region GetList

        public static DataTable GetList(int auditId)
        {
            var sql = String.Empty;
            try
            {
                sql = "EXEC dbo.UserPreferenceSelectedItemList" +
                    " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                    ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                var oDT = new DataAccess.DBDataTable("UserPreferenceSelectedItem.List", sql, DataStoreKey);
                return oDT.DBTable;
            }
            catch (Exception ex)
            {
                Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), ApplicationId, ex);
                throw ex;
            }
        }

        #endregion

        #region GetDetails

        public static DataTable GetDetails(Data data, int auditId)
        {
            var sql = String.Empty;
            try
            {
                sql = "EXEC dbo.UserPreferenceSelectedItemDetails " +
                    " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                    ", " + data.ToSQLParameter(DataColumns.UserPreferenceSelectedItemId);

                var oDT = new DataAccess.DBDataTable("UserPreferenceSelectedItem.Details", sql, DataStoreKey);
                return oDT.DBTable;
            }
            catch (Exception ex)
            {
                Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), ApplicationId, ex);
                throw ex;
            }
        }

        public static List<Data> GetEntityDetails(Data data, int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceSelectedItemDetails " +
                      " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                      ", " + data.ToSQLParameter(DataColumns.UserPreferenceSelectedItemId);

            var result = new List<Data>();

            using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
            {
                var dbReader = reader.DBReader;

                while (dbReader.Read())
                {
                    var dataItem = new Data();

                    dataItem.UserPreferenceSelectedItemId   = (int)dbReader[DataColumns.UserPreferenceSelectedItemId];
                    dataItem.UserPreferenceKeyId            = (int)dbReader[DataColumns.UserPreferenceKeyId];
                    dataItem.ApplicationUserId              = (int)dbReader[DataColumns.ApplicationUserId];
                    dataItem.Value                          = dbReader[DataColumns.ApplicationUserId].ToString();
                    dataItem. SortOrder                     = (int)dbReader[DataColumns.SortOrder];

					SetBaseInfo(dataItem, dbReader);

                    result.Add(dataItem);
                }
            }

            return result;
        }


        #endregion

        #region Create

        public static void Create(Data data, int auditId)
        {
            var sql = String.Empty;
            try
            {
                sql = Save(data, auditId, "Create");
                DataAccess.DBDML.RunSQL("UserPreferenceSelectedItem.Insert", sql, DataStoreKey);
            }
            catch (Exception ex)
            {
                Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), ApplicationId, ex);
                throw ex;
            }
        }

        #endregion

        #region Update

        public static void Update(Data data, int auditId)
        {
            var sql = String.Empty;
            try
            {
                sql = Save(data, auditId, "Update");
                DataAccess.DBDML.RunSQL("UserPreferenceSelectedItem.Update", sql, DataStoreKey);
            }
            catch (Exception ex)
            {
                Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), ApplicationId, ex);
                throw ex;
            }
        }

        #endregion

        #region Delete

        public static void Delete(Data data, int auditId)
        {
            var sql = String.Empty;
            try
            {
                sql = "EXEC dbo.UserPreferenceSelectedItemDelete " +
               " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
               ", " + data.ToSQLParameter(DataColumns.UserPreferenceSelectedItemId);

                DataAccess.DBDML.RunSQL("UserPreferenceSelectedItem.Delete", sql, DataStoreKey);
            }
            catch (Exception ex)
            {
                Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), ApplicationId, ex);
                throw ex;
            }
        }

        #endregion

        #region Search

        public static DataTable Search(Data data, int auditId)
        {
            var sql = String.Empty;
            try
            {
                // formulate SQL
                sql = "EXEC dbo.UserPreferenceSelectedItemSearch " +
                    " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                    ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                    ", " + data.ToSQLParameter(DataColumns.UserPreferenceSelectedItemId) +
                    ", " + data.ToSQLParameter(DataColumns.UserPreferenceKeyId) +
                    ", " + data.ToSQLParameter(DataColumns.ParentKey) +
                    ", " + data.ToSQLParameter(DataColumns.ApplicationUserId);

                var oDT = new DataAccess.DBDataTable("UserPreferenceSelectedItem.Search", sql, DataStoreKey);
                return oDT.DBTable;
            }
            catch (Exception ex)
            {
                Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), ApplicationId, ex);
                throw ex;
            }
        }

        #endregion

        #region Save

        private static string Save(Data data, int auditId, string sqlcmd)
        {
            var sql = "EXEC ";

            switch (sqlcmd)
            {
                case "Create":
                    sql += "dbo.UserPreferenceSelectedItemInsert  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.UserPreferenceSelectedItemUpdate  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + data.ToSQLParameter(DataColumns.UserPreferenceSelectedItemId) +
                        ", " + data.ToSQLParameter(DataColumns.UserPreferenceKeyId) +
                        ", " + data.ToSQLParameter(DataColumns.Value) +
                        ", " + data.ToSQLParameter(DataColumns.ParentKey) +
                        ", " + data.ToSQLParameter(DataColumns.SortOrder) +
                        ", " + data.ToSQLParameter(DataColumns.ApplicationUserId);
            return sql;
        }

        #endregion

        #region DoesExist

        public static DataTable DoesExist(Data data, int auditId)
        {
            var sql = String.Empty;
            try
            {
                // formulate SQL
                sql = "EXEC dbo.UserPreferenceSelectedItemDoesExist " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                    ", " + data.ToSQLParameter(DataColumns.UserPreferenceSelectedItemId) +
                    ", " + data.ToSQLParameter(DataColumns.UserPreferenceKeyId) +
                    ", " + data.ToSQLParameter(DataColumns.ParentKey) +
                    ", " + data.ToSQLParameter(DataColumns.Value);

                var oDT = new DataAccess.DBDataTable("UserPreferenceSelectedItem.DoesExist", sql, DataStoreKey);
                return oDT.DBTable;
            }
            catch (Exception ex)
            {
                Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), ApplicationId, ex);
                throw ex;
            }
        }

        #endregion

    }

}


