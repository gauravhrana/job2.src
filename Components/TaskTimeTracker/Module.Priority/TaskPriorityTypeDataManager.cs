using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.TaskTimeTracker.Priority;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.Module.Priority
{
    public partial class TaskPriorityTypeDataManager : StandardDataManager
    {
        private static readonly string DataStoreKey = "";

        static TaskPriorityTypeDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskPriorityType");
        }

        #region GetList

        public static List<TaskPriorityTypeDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(TaskPriorityTypeDataModel.Empty, requestProfile, 1);
        }

        #endregion GetList

        #region Search
        public static string ToSQLParameter(TaskPriorityTypeDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case TaskPriorityTypeDataModel.DataColumns.TaskPriorityTypeId:
                    if (data.TaskPriorityTypeId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskPriorityTypeDataModel.DataColumns.TaskPriorityTypeId, data.TaskPriorityTypeId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskPriorityTypeDataModel.DataColumns.TaskPriorityTypeId);
                    }
                    break;
                case TaskPriorityTypeDataModel.DataColumns.Weight:
                    if (data.Weight != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskPriorityTypeDataModel.DataColumns.Weight, data.Weight);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskPriorityTypeDataModel.DataColumns.Weight);
                    }
                    break;

                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        public static DataTable Search(TaskPriorityTypeDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }

        #endregion Search

        #region GetDetails

		public static TaskPriorityTypeDataModel GetDetails(TaskPriorityTypeDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);

            return list.FirstOrDefault();
        }

        public static List<TaskPriorityTypeDataModel> GetEntityDetails(TaskPriorityTypeDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.TaskPriorityTypeSearch ";

            var parameters =
            new
            {
                    AuditId             = requestProfile.AuditId
                ,   ApplicationMode     = requestProfile.ApplicationModeId
                ,   ApplicationId       = requestProfile.ApplicationId
                ,   ReturnAuditInfo     = returnAuditInfo
                ,   TaskPriorityTypeId  = dataQuery.TaskPriorityTypeId
                ,   Name                = dataQuery.Name
            };

            List<TaskPriorityTypeDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<TaskPriorityTypeDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        //public static List<TaskPriorityTypeDataModel> GetEntityDetails(TaskPriorityTypeDataModel data, int auditId)
        //{
        //    var sql = "EXEC dbo.TaskPriorityTypeSearch " +
        //              " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
        //               ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, BaseDataManager.ReturnAuditInfoOnDetails) +
        //              ", " + ToSQLParameter(data, TaskPriorityTypeDataModel.DataColumns.TaskPriorityTypeId);

        //    var result = new List<TaskPriorityTypeDataModel>();

        //    using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
        //    {
        //        var dbReader = reader.DBReader;

        //        while (dbReader.Read())
        //        {
        //            var dataItem = new TaskPriorityTypeDataModel();

        //            dataItem.TaskPriorityTypeId  = (int)dbReader[TaskPriorityTypeDataModel.DataColumns.TaskPriorityTypeId];
        //            dataItem.Weight				 = (decimal?)dbReader[TaskPriorityTypeDataModel.DataColumns.Weight];

        //            SetStandardInfo(dataItem, dbReader);

        //            SetBaseInfo(dataItem, dbReader);

        //            result.Add(dataItem);
        //        }
        //    }

        //    return result;
        //}

        #endregion GetDetails

        #region CreateOrUpdate
        private static string CreateOrUpdate(TaskPriorityTypeDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.TaskPriorityTypeInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.TaskPriorityTypeUpdate  " + "\r\n" +
                           " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;
            }

            sql = sql + ", " + ToSQLParameter(data, TaskPriorityTypeDataModel.DataColumns.TaskPriorityTypeId) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder) +
                        ", " + ToSQLParameter(data, TaskPriorityTypeDataModel.DataColumns.Weight);

            return sql;
        }

        #endregion CreateOrUpdate

        #region Create

        public static int Create(TaskPriorityTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");

            var taskPriorityTypeId = DBDML.RunScalarSQL("TaskPriorityType.Insert", sql, DataStoreKey);
            return Convert.ToInt32(taskPriorityTypeId);
        }

        #endregion Create

        #region Update

        public static void Update(TaskPriorityTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("TaskPriorityType.Update", sql, DataStoreKey);
        }

        #endregion Update

        #region Renumber
        public static void Renumber(int seed, int increment, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskPriorityTypeRenumber " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                      ",@Seed = " + seed +
                      ",@Increment = " + increment;

            DBDML.RunSQL("TaskPriorityType.Renumber", sql, DataStoreKey);
        }
        #endregion Renumber

        #region Delete

        public static void Delete(TaskPriorityTypeDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.TaskPriorityTypeDelete ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                TaskPriorityTypeId = dataQuery.TaskPriorityTypeId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion Delete

        #region DoesExist

        public static bool DoesExist(TaskPriorityTypeDataModel data, RequestProfile requestProfile)
        {
            var doesExistRequest = new TaskPriorityTypeDataModel();
            doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
        }

        #endregion DoesExist

        #region GetChildren

        private static DataSet GetChildren(TaskPriorityTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskPriorityTypeChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, TaskPriorityTypeDataModel.DataColumns.TaskPriorityTypeId);

            var oDT = new DBDataSet("TaskPriorityType.GetChildren", sql, DataStoreKey);

            return oDT.DBDataset;
        }

        #endregion

        #region DeleteChildren

        public static DataSet DeleteChildren(TaskPriorityTypeDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskPriorityTypeChildrenDelete " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, TaskPriorityTypeDataModel.DataColumns.TaskPriorityTypeId);

            var oDT = new DBDataSet("TaskPriorityType.DeleteChildren", sql, DataStoreKey);

            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(TaskPriorityTypeDataModel data, RequestProfile requestProfile)
        {
            var isDeletable = true;

            var ds = GetChildren(data, requestProfile);

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables.Cast<DataTable>().Any(dt => dt.Rows.Count > 0))
                {
                    isDeletable = false;
                }
            }

            return isDeletable;
        }

        #endregion
    }
}
