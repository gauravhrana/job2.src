using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
    public partial class UserPreferenceDataType : BaseClass
    {

        static readonly string DataStoreKey = "";
        static readonly int ApplicationId;

        static UserPreferenceDataType()
        {
            ApplicationId = SetupConfiguration.ApplicationId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("UserPreferenceDataType");
        }

        #region GetList

        public static DataTable GetList(int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceDataTypeList" +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);

            var oDT = new DataAccess.DBDataTable("UserPreferenceDataType.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        public static DataTable GetDetails(Data data, int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceDataTypeDetails " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + data.ToSQLParameter(DataColumns.UserPreferenceDataTypeId);

            var oDT = new DataAccess.DBDataTable("UserPreferenceDataType.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }


        public static List<Data> GetEntityDetails(Data data, int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceDataTypeDetails " +
                      " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                      ", " + data.ToSQLParameter(DataColumns.UserPreferenceDataTypeId);

            var result = new List<Data>();

            using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
            {
                var dbReader = reader.DBReader;

                while (dbReader.Read())
                {
                    var dataItem = new Data();

                    dataItem.UserPreferenceDataTypeId   = (int)dbReader[DataColumns.UserPreferenceDataTypeId];
                    dataItem.Name                       = dbReader[DataColumns.Name].ToString();
                    dataItem.Description                = dbReader[DataColumns.Description].ToString();
                    dataItem.SortOrder                  = (int)dbReader[DataColumns.SortOrder];

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
            var sql = Save(data, auditId, "Create");
            DataAccess.DBDML.RunSQL("UserPreferenceDataType.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(Data data, int auditId)
        {
            var sql = Save(data, auditId, "Update");
            DataAccess.DBDML.RunSQL("UserPreferenceDataType.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(Data data, int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceDataTypeDelete " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + data.ToSQLParameter(DataColumns.UserPreferenceDataTypeId);

            DataAccess.DBDML.RunSQL("UserPreferenceDataType.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static DataTable Search(Data data, int auditId)
        {
            // formulate SQL
            var sql = "EXEC dbo.UserPreferenceDataTypeSearch " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + data.ToSQLParameter(DataColumns.UserPreferenceDataTypeId) +
                ", " + data.ToSQLParameter(DataColumns.Name);

            var oDT = new DataAccess.DBDataTable("UserPreferenceDataType.Search", sql, DataStoreKey);
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
                    sql += "dbo.UserPreferenceDataTypeInsert  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.UserPreferenceDataTypeUpdate  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + data.ToSQLParameter(DataColumns.UserPreferenceDataTypeId) +
                        ", " + data.ToSQLParameter(DataColumns.Name) +
                        ", " + data.ToSQLParameter(DataColumns.Description) +
                        ", " + data.ToSQLParameter(DataColumns.SortOrder);
            return sql;
        }

        #endregion

        #region DoesExist

        public static DataTable DoesExist(Data data, int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceDataTypeDoesExist " +
            " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
            ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + data.ToSQLParameter(DataColumns.UserPreferenceDataTypeId);

            var oDT = new DataAccess.DBDataTable("UserPreferenceDataType.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetChildren

        private static DataSet GetChildren(Data data, int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceDataTypeChildrenGet " +
                            " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                            ", " + data.ToSQLParameter(DataColumns.UserPreferenceDataTypeId);

            var oDT = new DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(Data data, int auditId)
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
