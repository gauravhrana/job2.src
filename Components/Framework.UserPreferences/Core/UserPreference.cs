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
    public partial class UserPreference : BaseClass
    {
        static readonly string DataStoreKey = "";
        static readonly int ApplicationId;

        static UserPreference()
        {
            ApplicationId = SetupConfiguration.ApplicationId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("UserPreference");
        }

        #region GetList

        public static DataTable GetList(int auditId)
        {
            var sql = String.Empty;
            try
            {
                sql = "EXEC dbo.UserPreferenceList" +
                    " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                    ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                var oDT = new DataAccess.DBDataTable("UserPreference.List", sql, DataStoreKey);
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
                sql = "EXEC dbo.UserPreferenceDetails " +
                    " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                    ", " + data.ToSQLParameter(DataColumns.UserPreferenceId);

                var oDT = new DataAccess.DBDataTable("UserPreference.Details", sql, DataStoreKey);
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
            var sql = "EXEC dbo.UserPreferenceDetails " +
                      " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                      ", " + data.ToSQLParameter(DataColumns.UserPreferenceId);

            var result = new List<Data>();

            using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
            {
                var dbReader = reader.DBReader;

                while (dbReader.Read())
                {
                    var dataItem = new Data();

                    dataItem.UserPreferenceId           = (int)dbReader[DataColumns.UserPreferenceId];
                    dataItem.UserPreferenceCategoryId   = (int)dbReader[DataColumns.UserPreferenceCategoryId];
                    dataItem.UserPreferenceKeyId        = (int)dbReader[DataColumns.UserPreferenceKeyId];
                    dataItem.ApplicationUserId          = (int)dbReader[DataColumns.ApplicationUserId];
                    dataItem.DataTypeId                 = (int)dbReader[DataColumns.DataTypeId];
                    dataItem.Value                      = dbReader[DataColumns.Value].ToString();

					SetBaseInfo(dataItem, dbReader);

                    result.Add(dataItem);
                }
            }

            return result;
        }

        #endregion

        #region Create

        public static int Create(Data data, int auditId)
        {
            var sql = String.Empty;
            try
            {
                sql = Save(data, auditId, "Create");
                var id = DataAccess.DBDML.RunScalarSQL("UserPreference.Insert", sql, DataStoreKey);
                return Convert.ToInt32(id);
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
                DataAccess.DBDML.RunSQL("UserPreference.Update", sql, DataStoreKey);
            }
            catch (Exception ex)
            {
                Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), ApplicationId, ex);
                throw ex;
            }
        }

        public static void UpdateValueOnly(Data data, int auditId)
        {
            var sql = String.Empty;
            try
            {
                sql = "EXEC ";
                sql += "dbo.UserPreferenceUpdateValueOnly  " +
                    " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                    ", " + data.ToSQLParameter(DataColumns.UserPreferenceId) +
                    ", " + data.ToSQLParameter(DataColumns.Value);
                DataAccess.DBDML.RunSQL("UserPreference.Update", sql, DataStoreKey);
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
                sql = "EXEC dbo.UserPreferenceDelete " +
               " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
               ", " + data.ToSQLParameter(DataColumns.UserPreferenceId);

                DataAccess.DBDML.RunSQL("UserPreference.Delete", sql, DataStoreKey);
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
                sql = "EXEC dbo.UserPreferenceSearch " +
                    " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                    ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                    ", " + data.ToSQLParameter(DataColumns.UserPreferenceId) +
                    ", " + data.ToSQLParameter(DataColumns.UserPreferenceKeyId) +
                    ", " + data.ToSQLParameter(DataColumns.UserPreferenceKey) +
                    ", " + data.ToSQLParameter(DataColumns.UserPreferenceCategoryId) +
                    ", " + data.ToSQLParameter(DataColumns.DataTypeId) +
                    ", " + data.ToSQLParameter(DataColumns.Value) +
                    ", " + data.ToSQLParameter(DataColumns.ApplicationUserId);

                var oDT = new DataAccess.DBDataTable("UserPreference.Search", sql, DataStoreKey);
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
                    sql += "dbo.UserPreferenceInsert  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.UserPreferenceUpdate  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + data.ToSQLParameter(DataColumns.UserPreferenceId) +
                        ", " + data.ToSQLParameter(DataColumns.UserPreferenceKeyId) +
                        ", " + data.ToSQLParameter(DataColumns.Value) +
                        ", " + data.ToSQLParameter(DataColumns.DataTypeId) +
                        ", " + data.ToSQLParameter(DataColumns.UserPreferenceCategoryId) +
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
                sql = "EXEC dbo.UserPreferenceDoesExist " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                    ", " + data.ToSQLParameter(DataColumns.UserPreferenceId) +
                    ", " + data.ToSQLParameter(DataColumns.UserPreferenceKeyId);

                var oDT = new DataAccess.DBDataTable("UserPreference.DoesExist", sql, DataStoreKey);
                return oDT.DBTable;
            }
            catch (Exception ex)
            {
                Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), ApplicationId, ex);
                throw ex;
            }
        }

        #endregion

        #region Get User Preferences

        public static int? GetUserTimeZone(int applicationUserId, int auditId)
        {
            var dataKey = new DomainModel.UserPreferenceKey();
            dataKey.Name = "UserTimeZone";

            var dtKeys = UserPreferenceKey.Search(dataKey, auditId);

            if (dtKeys != null && dtKeys.Rows.Count > 0)
            {
                var keyId = Convert.ToInt32(dtKeys.Rows[0][DomainModel.UserPreferenceKey.DataColumns.UserPreferenceKeyId]);

                var data = new Data();
                data.ApplicationUserId = SetupConfiguration.ApplicationId;
                data.UserPreferenceKeyId = keyId;

                var dt = Search(data, auditId);

                if (dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0][DomainModel.UserPreferenceKey.DataColumns.Value]);
                }
            }

            return null;
        }

        public static Dictionary<string, string> GetUserPreferences(int applicationUserId, int auditId)
        {
            var objDictionary = new Dictionary<string, string>();
            var sql = String.Empty;
            try
            {
                // formulate SQL
                sql = string.Format("EXEC dbo.UserPreferenceSearch @UserPreferenceId={0}," +
                                                                   "@ApplicationUserId={1}," +
                                                                   "@DataTypeId={2}," +
                                                                   "@UserPreferenceKeyId={3}," +
                                                                   "@ApplicationId={4}," +
                                                                   "@AuditId={5}," +
                                                                   "@UserPreferenceCategoryId={6}",
                                                                   "NULL", applicationUserId, "NULL", "NULL", "NULL", auditId, "NULL");

                var oDT = new DataAccess.DBDataTable("EXEC dbo.UserPreferenceSearch", sql, DataStoreKey);

                if (oDT.DBTable != null && oDT.DBTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in oDT.DBTable.Rows)
                    {
                        if (!objDictionary.Keys.Contains(Convert.ToString(dr["UserPreferenceKey"])))
                        {
                            objDictionary.Add(Convert.ToString(dr["UserPreferenceKey"]), Convert.ToString(dr["Value"]));
                        }
                    }
                }

                return objDictionary;
            }
            catch (Exception ex)
            {
                Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), ApplicationId, ex);
                throw ex;
            }
        }

        public static Dictionary<string, string> GetUserPreferences(int applicationUserId, int userPreferenceCategoryId, int auditId)
        {
            Dictionary<string, string> objDictionary = new Dictionary<string, string>();
            var sql = String.Empty;
            try
            {
                // formulate SQL
                sql = string.Format("EXEC dbo.UserPreferenceSearch @UserPreferenceId={0}, " +
                                                                "@ApplicationUserId={1}, " +
                                                                "@DataTypeId={2}," +
                                                                "@UserPreferenceKeyId={3}," +
                                                                "@ApplicationId={4}," +
                                                                "@AuditId={5}," +
                                                                "@UserPreferenceCategoryId={6}",
                                                                "NULL", applicationUserId, "NULL", "NULL", "NULL", auditId, userPreferenceCategoryId);

                var oDT = new DataAccess.DBDataTable("EXEC dbo.UserPreferenceSearch", sql, DataStoreKey);

                if (oDT.DBTable != null && oDT.DBTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in oDT.DBTable.Rows)
                    {
                        if (!objDictionary.Keys.Contains(Convert.ToString(dr["UserPreferenceKey"])))
                        {
                            objDictionary.Add(Convert.ToString(dr["UserPreferenceKey"]), Convert.ToString(dr["Value"]));
                        }
                    }
                }

                return objDictionary;
            }
            catch (Exception ex)
            {
                Log4Net.LogError(sql, MethodBase.GetCurrentMethod().DeclaringType.ToString(), ApplicationId, ex);
                throw ex;
            }
        }

        public static DataTable GetTopNUserPreferences(Data data, int topN, int auditId)
        {

            var sql = String.Empty;
            try
            {
                // formulate SQL
                sql = "EXEC dbo.UserPreferenceTopNPreference " +
               " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
               ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
               ", " + data.ToSQLParameter(DataColumns.Value) +
               ", " + data.ToSQLParameter(DataColumns.UserPreferenceKeyId) +
               ", @TopN = " + Convert.ToString(topN);

                var oDT = new DataAccess.DBDataTable("EXEC dbo.UserPreferenceTopNPreference", sql, DataStoreKey);

                if (oDT.DBTable != null && oDT.DBTable.Rows.Count > topN) // check if the total no. of records are greater then TopN
                {
                    if (Convert.ToString(oDT.DBTable.Rows[topN + 1]["Value"]) == data.Value) // check the last row's Value. IF it matches the current value passed then remove the 2nd last row.
                    {
                        oDT.DBTable.Rows.RemoveAt(topN);
                    }
                    else
                    {
                        oDT.DBTable.Rows.RemoveAt(topN + 1); //if not then remove the last row.
                    }
                }
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


