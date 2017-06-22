using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;

namespace MVCModels.ApplicationUser
{
    public partial class ApplicationRole : BaseClass
    {

        static readonly string DataStoreKey = "";
        static readonly int ApplicationId;

        static ApplicationRole()
        {
            ApplicationId = SetupConfiguration.ApplicationId;
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ApplicationRole");
        }

        #region GetList

        static public DataTable GetList(int auditId)
        {
            var sql = "EXEC dbo.ApplicationRoleList" +
                " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                ", " + BaseColumns.ToSQLParameter(BaseColumns.ApplicationId, ApplicationId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("ApplicationRole.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetDetails

        static public DataTable GetDetails(Data data, int auditId)
        {
            var sql = "EXEC dbo.ApplicationRoleDetails " +
                " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                ", " + data.ToSQLParameter(DataColumns.ApplicationRoleId);

            var oDT = new Framework.Components.DataAccess.DBDataTable("ApplicationRole.Details", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Create

        static public int Create(Data data, int auditId)
        {
            var sql = Save(data, auditId, "Create");
            var Id = Framework.Components.DataAccess.DBDML.RunScalarSQL("ApplicationRole.Insert", sql, DataStoreKey);
            return Convert.ToInt32(Id);
        }

        #endregion

        #region Update

        static public void Update(Data data, int auditId)
        {
            var sql = Save(data, auditId, "Update");
            Framework.Components.DataAccess.DBDML.RunSQL("ApplicationRole.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        static public void Delete(Data data, int auditId)
        {
            var sql = "EXEC dbo.ApplicationRoleDelete " +
                " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                ", " + data.ToSQLParameter(DataColumns.ApplicationRoleId);

            Framework.Components.DataAccess.DBDML.RunSQL("ApplicationRole.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        static public DataTable Search(Data data, int auditId)
        {
            // formulate SQL
            var sql = "EXEC dbo.ApplicationRoleSearch " +
                " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                ", " + data.ToSQLParameter(DataColumns.ApplicationId) +
                ", " + data.ToSQLParameter(DataColumns.ApplicationRoleId) +
                ", " + data.ToSQLParameter(DataColumns.Name);

            var oDT = new Framework.Components.DataAccess.DBDataTable("ApplicationRole.Search", sql, DataStoreKey);
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
                    sql += "dbo.ApplicationRoleInsert  " + " " + BaseColumns.ToSQLParameter(BaseColumns.ApplicationId, ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ApplicationRoleUpdate  " + " " + BaseColumns.ToSQLParameter(BaseColumns.ApplicationId, ApplicationId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                        ", " + data.ToSQLParameter(DataColumns.ApplicationRoleId) +
                        ", " + data.ToSQLParameter(DataColumns.Name) +
                        ", " + data.ToSQLParameter(DataColumns.Description) +
                        ", " + data.ToSQLParameter(DataColumns.SortOrder);// + 
            //", " + data.ToSQLParameter(DataColumns.ApplicationId) ;
            return sql;
        }

        #endregion

        #region DoesExist

        static public DataTable DoesExist(Data data, int auditId)
        {
            var sql = "EXEC dbo.ApplicationRoleDoesExist " +
            " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
            ", " + BaseColumns.ToSQLParameter(BaseColumns.ApplicationId, ApplicationId) +
                ", " + data.ToSQLParameter(DataColumns.ApplicationRoleId) +
                ", " + data.ToSQLParameter(DataColumns.Name);

            var oDT = new Framework.Components.DataAccess.DBDataTable("ApplicationRole.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region GetChildren

        static private DataSet GetChildren(Data data, int auditId)
        {
            var sql = "EXEC dbo.ApplicationRoleChildrenGet " +
                            " " + BaseColumns.ToSQLParameter(BaseColumns.AuditId, auditId) +
                            ", " + data.ToSQLParameter(DataColumns.ApplicationRoleId);

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
