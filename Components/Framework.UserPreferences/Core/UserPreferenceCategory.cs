using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
    public partial class UserPreferenceCategory : BaseClass
    {
		private static Dictionary<string, int> UserPreferenceCategoryCache;

		static readonly string DataStoreKey = "";
        static readonly int ApplicationId;

        static UserPreferenceCategory()
        {
            ApplicationId = SetupConfiguration.ApplicationId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("UserPreferenceCategory");

			UserPreferenceCategoryCache = new Dictionary<string, int>();
        }

        #region GetList

        public static DataTable GetList(int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceCategoryList" +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);

            var oDT = new DataAccess.DBDataTable("UserPreferenceCategory.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        public static DataTable GetDetails(DomainModel.UserPreferenceCategory data, int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceCategoryDetails " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(data, DomainModel.UserPreferenceCategory.DataColumns.UserPreferenceCategoryId);

            var oDT = new DataAccess.DBDataTable("UserPreferenceCategory.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static List<DomainModel.UserPreferenceCategory> GetEntityDetails(DomainModel.UserPreferenceCategory data, int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceCategoryDetails " +
                      " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                      ", " + ToSQLParameter(data, DomainModel.UserPreferenceCategory.DataColumns.UserPreferenceCategoryId);

            var result = new List<DomainModel.UserPreferenceCategory>();

            using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
            {
                var dbReader = reader.DBReader;

                while (dbReader.Read())
                {
                    var dataItem = new DomainModel.UserPreferenceCategory();

                    dataItem.UserPreferenceCategoryId   = (int)dbReader[DomainModel.UserPreferenceCategory.DataColumns.UserPreferenceCategoryId];
                    dataItem.Name                       = dbReader[DomainModel.UserPreferenceCategory.DataColumns.Name].ToString();
                    dataItem.Description                = dbReader[DomainModel.UserPreferenceCategory.DataColumns.Description].ToString();
                    dataItem.SortOrder                  = (int)dbReader[DomainModel.UserPreferenceCategory.DataColumns.SortOrder];

					SetBaseInfo(dataItem, dbReader);

                    result.Add(dataItem);
                }
            }

            return result;
        }


        #endregion

        #region Create

        public static int Create(DomainModel.UserPreferenceCategory data, int auditId)
        {
            var sql = Save(data, auditId, "Create");
            var id = DataAccess.DBDML.RunScalarSQL("UserPreferenceCategory.Insert", sql, DataStoreKey);
            return Convert.ToInt32(id);
        }

        #endregion

        #region Update

        public static void Update(DomainModel.UserPreferenceCategory data, int auditId)
        {
            var sql = Save(data, auditId, "Update");
            DataAccess.DBDML.RunSQL("UserPreferenceCategory.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(DomainModel.UserPreferenceCategory data, int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceCategoryDelete " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(data, DomainModel.UserPreferenceCategory.DataColumns.UserPreferenceCategoryId);

            DataAccess.DBDML.RunSQL("UserPreferenceCategory.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static DataTable Search(DomainModel.UserPreferenceCategory data, int auditId)
        {
            // formulate SQL
            var sql = "EXEC dbo.UserPreferenceCategorySearch " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + ToSQLParameter(data, DomainModel.UserPreferenceCategory.DataColumns.UserPreferenceCategoryId) +
                ", " + ToSQLParameter(data, DomainModel.UserPreferenceCategory.DataColumns.Name);

            var oDT = new DataAccess.DBDataTable("UserPreferenceCategory.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        private static string Save(DomainModel.UserPreferenceCategory data, int auditId, string sqlcmd)
        {
            var sql = "EXEC ";

            switch (sqlcmd)
            {
                case "Create":
                    sql += "dbo.UserPreferenceCategoryInsert  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                        ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.UserPreferenceCategoryUpdate  " +
                        " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, DomainModel.UserPreferenceCategory.DataColumns.UserPreferenceCategoryId) +
                        ", " + ToSQLParameter(data, DomainModel.UserPreferenceCategory.DataColumns.Name) +
                        ", " + ToSQLParameter(data, DomainModel.UserPreferenceCategory.DataColumns.Description) +
                        ", " + ToSQLParameter(data, DomainModel.UserPreferenceCategory.DataColumns.SortOrder);
            return sql;
        }

        #endregion

        #region DoesExist

        public static int? DoesExist(DomainModel.UserPreferenceCategory data, int auditId)
        {
			var key = auditId + ", " + data.ToURLQuery();

			if (UserPreferenceCategoryCache.ContainsKey(key))
			{
				return UserPreferenceCategoryCache[key];
			}

	        var sql = "EXEC dbo.UserPreferenceCategoryDoesExist " +
                " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                ", " + ToSQLParameter(BaseModel.BaseDataColumns.ApplicationId, ApplicationId) +
                ", " + ToSQLParameter(data, DomainModel.UserPreferenceCategory.DataColumns.Name) +
                ", " + ToSQLParameter(data, DomainModel.UserPreferenceCategory.DataColumns.UserPreferenceCategoryId);

            var oDT = new DataAccess.DBDataTable("UserPreferenceCategory.DoesExist", sql, DataStoreKey);
            var dt = oDT.DBTable;

            if (dt != null && dt.Rows.Count > 0)

            {
                var id = (int)dt.Rows[0][DomainModel.UserPreferenceCategory.DataColumns.UserPreferenceCategoryId];
			    UserPreferenceCategoryCache.Add(key, id);
                return id;
            }

	        return null;
        }

        #endregion

        #region GetChildren

        private static DataSet GetChildren(DomainModel.UserPreferenceCategory data, int auditId)
        {
            var sql = "EXEC dbo.UserPreferenceCategoryChildrenGet " +
                            " " + ToSQLParameter(BaseModel.BaseDataColumns.AuditId, auditId) +
                            ", " + ToSQLParameter(data, DomainModel.UserPreferenceCategory.DataColumns.UserPreferenceCategoryId);

            var oDT = new DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(DomainModel.UserPreferenceCategory data, int auditId)
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

        #region ToSQLParameter

        public static string ToSQLParameter(DomainModel.UserPreferenceCategory data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case DomainModel.UserPreferenceCategory.DataColumns.UserPreferenceCategoryId:
                    if (data.UserPreferenceCategoryId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DomainModel.UserPreferenceCategory.DataColumns.UserPreferenceCategoryId, data.UserPreferenceCategoryId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DomainModel.UserPreferenceCategory.DataColumns.UserPreferenceCategoryId);
                    }
                    break;

                default:
                    returnValue = StandardClass.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        #endregion

    }
}
