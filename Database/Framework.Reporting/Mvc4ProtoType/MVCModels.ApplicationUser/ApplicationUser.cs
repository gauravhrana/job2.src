using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;

namespace MVCModels.ApplicationUser
{
    public partial class ApplicationUser : BaseClass
    {

        static readonly string DataStoreKey = "";
        static readonly int ApplicationId;

        static ApplicationUser()
        {
            ApplicationId = SetupConfiguration.ApplicationId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationUser");
        }

        #region GetList

        static public DataTable GetList(int auditId)
        {
            var sql = "EXEC dbo.ApplicationUserList" +
                " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                ", " + BaseColumns.ToSQLParameter(BaseColumns.ApplicationId, ApplicationId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("ApplicationUser.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        static public DataTable GetDetails(Data data, int auditId)
        {
            var sql = "EXEC dbo.ApplicationUserDetails " +
                " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                ", " + data.ToSQLParameter(DataColumns.ApplicationUserId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("ApplicationUser.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Create

        static public void Create(Data data, int auditId)
        {
            var sql = Save(data, auditId, "Create");
            Framework.Components.DataAccess.DBDML.RunSQL("ApplicationUser.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        static public void Update(Data data, int auditId)
        {
            var sql = Save(data, auditId, "Update");
            Framework.Components.DataAccess.DBDML.RunSQL("ApplicationUser.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        static public void Delete(Data data, int auditId)
        {
            var sql = "EXEC dbo.ApplicationUserDelete " +
                " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                ", " + data.ToSQLParameter(DataColumns.ApplicationUserId);

            Framework.Components.DataAccess.DBDML.RunSQL("ApplicationUser.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        static public DataTable Search(Data data, int auditId)
        {
            // formulate SQL
            var sql = "EXEC dbo.ApplicationUserSearch " +
                " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                ", " + data.ToSQLParameter(DataColumns.ApplicationId) +
                ", " + data.ToSQLParameter(DataColumns.ApplicationUserId) +
                ", " + data.ToSQLParameter(DataColumns.ApplicationUserName) +
                ", " + data.ToSQLParameter(DataColumns.FirstName) +
                ", " + data.ToSQLParameter(DataColumns.MiddleName) +
                ", " + data.ToSQLParameter(DataColumns.LastName) +
                ", " + data.ToSQLParameter(DataColumns.ApplicationUserTitleId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("ApplicationUser.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        static public string GetFullName(Data data, int auditId)
        {
            var dr = GetDetails(data, auditId).Rows[0];
            var value = dr["FirstName"]
                        + " " + dr["MiddleName"]
                        + " " + dr["LastName"];

            return value;
        }

        #endregion

        #region Save

        static private string Save(Data data, int auditId, string sqlcmd)
        {
            var sql = "EXEC ";

            switch (sqlcmd)
            {
                case "Create":
                    sql += "dbo.ApplicationUserInsert  " +
                        " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                        ", " + data.ToSQLParameter(DataColumns.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ApplicationUserUpdate  " +
                        " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                        ", " + data.ToSQLParameter(DataColumns.ApplicationId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + data.ToSQLParameter(DataColumns.ApplicationUserId) +
                        ", " + data.ToSQLParameter(DataColumns.ApplicationUserName) +
                        ", " + data.ToSQLParameter(DataColumns.FirstName) +
                        ", " + data.ToSQLParameter(DataColumns.LastName) +
                        ", " + data.ToSQLParameter(DataColumns.MiddleName) +
                        ", " + data.ToSQLParameter(DataColumns.ApplicationUserTitleId);
            return sql;
        }

        #endregion

        #region DoesExist

        static public DataTable DoesExist(Data data, int auditId)
        {
            var sql = "EXEC dbo.ApplicationUserDoesExist " +
                " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                ", " + BaseColumns.ToSQLParameter(BaseColumns.ApplicationId, ApplicationId) +
                ", " + data.ToSQLParameter(DataColumns.ApplicationUserName) +
                ", " + data.ToSQLParameter(DataColumns.ApplicationUserId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("ApplicationUser.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetChildren

        static private DataSet GetChildren(Data data, int auditId)
        {
            var sql = "EXEC dbo.ApplicationUserChildrenGet " +
                            " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                            ", " + data.ToSQLParameter(DataColumns.ApplicationUserId);

            var oDT = new Framework.Components.DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        static public bool IsDeletable(Data data, int auditId)
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
