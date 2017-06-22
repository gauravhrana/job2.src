using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker;
using Dapper;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public partial class ClientXProjectDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static ClientXProjectDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ClientXProject");
        }

        public static string ToSQLParameter(ClientXProjectDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case ClientXProjectDataModel.DataColumns.ClientXProjectId:
                    if (data.ClientXProjectId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ClientXProjectDataModel.DataColumns.ClientXProjectId, data.ClientXProjectId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClientXProjectDataModel.DataColumns.ClientXProjectId);
                    }
                    break;

                case ClientXProjectDataModel.DataColumns.ClientId:
                    if (data.ClientId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ClientXProjectDataModel.DataColumns.ClientId, data.ClientId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClientXProjectDataModel.DataColumns.ClientId);
                    }
                    break;

                case ClientXProjectDataModel.DataColumns.ProjectId:
                    if (data.ProjectId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ClientXProjectDataModel.DataColumns.ProjectId, data.ProjectId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClientXProjectDataModel.DataColumns.ProjectId);
                    }
                    break;

                case ClientXProjectDataModel.DataColumns.Client:
                    if (!string.IsNullOrEmpty(data.Client))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ClientXProjectDataModel.DataColumns.Client, data.Client);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClientXProjectDataModel.DataColumns.Client);
                    }
                    break;

                case ClientXProjectDataModel.DataColumns.Project:
                    if (!string.IsNullOrEmpty(data.Project))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ClientXProjectDataModel.DataColumns.Project, data.Project);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ClientXProjectDataModel.DataColumns.Project);
                    }
                    break;
            }

            return returnValue;
        }

        #region Create By Client

        public static void CreateByClient(int clientId, int[] projectIds, RequestProfile requestProfile)
        {
            foreach (int projectId in projectIds)
            {
                var sql = "EXEC ClientXProjectInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @ClientId					=   " + clientId +
                            ",      @ProjectId				=   " + projectId;

                DBDML.RunSQL("ClientXProjectInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Create By Project

        public static void CreateByProject(int projectId, int[] clientIds, RequestProfile requestProfile)
        {
            foreach (int clientId in clientIds)
            {
                var sql = "EXEC ClientXProjectInsert " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ",   @ClientId					=   " + clientId +
                            ",      @ProjectId				=   " + projectId;
                DBDML.RunSQL("ClientXProjectInsert", sql, DataStoreKey);
            }
        }

        #endregion

        #region Get By Project

        public static DataTable GetByProject(int projectId, int auditId)
        {
            var sql = "EXEC ClientXProjectSearch @ProjectId     =" + projectId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);

            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion

        #region Get By Client

        public static DataTable GetByClient(int clientId, int auditId)
        {
            var sql = "EXEC ClientXProjectSearch @ClientId       =" + clientId + ", " +
                         " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);

            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion

        #region Delete

        public static void Delete(ClientXProjectDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.ClientXProjectDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, ClientXProjectDataModel.DataColumns.ClientXProjectId);

            DBDML.RunSQL("ClientXProject.Delete", sql, DataStoreKey);
        }

        #endregion

        #region GetList

        public static List<ClientXProjectDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ClientXProjectDataModel.Empty, requestProfile, 0);
        }

        #endregion

        #region GetDetails

        public static ClientXProjectDataModel GetDetails(ClientXProjectDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        #endregion

        #region Search

        public static DataTable Search(ClientXProjectDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }

        public static List<ClientXProjectDataModel> GetEntityDetails(ClientXProjectDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.ClientXProjectSearch ";

            var parameters =
            new
            {
                    AuditId                 = requestProfile.AuditId
                ,   ApplicationId           = requestProfile.ApplicationId
                ,   ReturnAuditInfo         = returnAuditInfo
                ,   ClientXProjectId             = dataQuery.ClientXProjectId
                ,   ClientId                    = dataQuery.ClientId
                ,   ProjectId               = dataQuery.ProjectId
            };

            List<ClientXProjectDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<ClientXProjectDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        #endregion

        #region Create

        public static void Create(ClientXProjectDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DBDML.RunSQL("ClientXProject.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(ClientXProjectDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DBDML.RunSQL("ClientXProject.Update", sql, DataStoreKey);
        }

        #endregion

        #region Save

        private static string Save(ClientXProjectDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.ClientXProjectInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ClientXProjectUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, ClientXProjectDataModel.DataColumns.ClientXProjectId) +
                        ", " + ToSQLParameter(data, ClientXProjectDataModel.DataColumns.ProjectId) +
                        ", " + ToSQLParameter(data, ClientXProjectDataModel.DataColumns.ClientId);
            return sql;
        }

        #endregion

        #region Delete By Project

        public static void DeleteByProject(int projectId, int auditId)
        {
            var sql = "EXEC ClientXProjectDelete @projectId       =" + projectId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

        #region Delete By Client

        public static void DeleteByClient(int ClientId, int auditId)
        {
            var sql = "EXEC ClientXProjectDelete @ClientId		=" + ClientId + ", " +
                          " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId);
            DBDML.RunSQL("Delete Details", sql, DataStoreKey);
        }

        #endregion

    }
}
