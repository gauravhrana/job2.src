using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using System.Data;

namespace MVCModels.ApplicationUser
{
    public partial class Application : BaseClass
    {

        static readonly string DataStoreKey = "";

        static Application()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("Application");
        }

        #region GetList

        static public DataTable GetList(int auditId)
        {
            var sql = "EXEC dbo.ApplicationList" +
                " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("Application.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        static public DataTable GetDetails(Data data, int auditId)
        {
            var sql = "EXEC dbo.ApplicationDetails " +
                " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                ", " + data.ToSQLParameter(DataColumns.ApplicationId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("Application.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Create

        static public void Create(Data data, int auditId)
        {
            var sql = Save(data, auditId, "Create");
            Framework.Components.DataAccess.DBDML.RunSQL("Application.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        static public void Update(Data data, int auditId)
        {
            var sql = Save(data, auditId, "Update");
            Framework.Components.DataAccess.DBDML.RunSQL("Application.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        static public void Delete(Data data, int auditId)
        {
            var sql = "EXEC dbo.ApplicationDelete " +
                " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                ", " + data.ToSQLParameter(DataColumns.ApplicationId);

            Framework.Components.DataAccess.DBDML.RunSQL("Application.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        static public DataTable Search(Data data, int auditId)
        {
            // formulate SQL
            var sql = "EXEC dbo.ApplicationSearch " +
                " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                ", " + data.ToSQLParameter(DataColumns.ApplicationId) +
                ", " + data.ToSQLParameter(DataColumns.Name);

            var oDT = new Framework.Components.DataAccess.DBDataTable("Application.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        static private string Save(Data data, int auditId, string sqlcmd)
        {
            var sql = "EXEC ";

            switch (sqlcmd)
            {
                case "Create":
                    sql += "dbo.ApplicationInsert  " +
                        " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId);
                    break;

                case "Update":
                    sql += "dbo.ApplicationUpdate  " +
                        " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + data.ToSQLParameter(DataColumns.ApplicationId) +
                        ", " + data.ToSQLParameter(DataColumns.Name) +
                        ", " + data.ToSQLParameter(DataColumns.Description) +
                        ", " + data.ToSQLParameter(DataColumns.SortOrder);
            return sql;
        }

        #endregion

        #region DoesExist

        static public DataTable DoesExist(Data data, int auditId)
        {
            var sql = "EXEC dbo.ApplicationDoesExist " +
            " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                ", " + data.ToSQLParameter(DataColumns.ApplicationId) +
                ", " + data.ToSQLParameter(DataColumns.Name);

            var oDT = new Framework.Components.DataAccess.DBDataTable("Application.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

    }
}
