using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Task;

namespace TaskTimeTracker.Components.BusinessLayer.Task
{
    public partial class TaskFormulationDataManager : BaseDataManager
    {
		static string DataStoreKey = "";
	
        static TaskFormulationDataManager()
        {
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskFormulation");
        }

        #region GetList

        public static DataTable GetList(RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskFormulationList " +
                    " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId)+
                    ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
            var oDT = new DBDataTable("Get List", sql, DataStoreKey);

            return oDT.DBTable;
        }
        #endregion GetList		

        #region Search
		
        public static DataTable Search(TaskFormulationDataModel data, RequestProfile requestProfile)
        {		

			// formulate SQL  
            var sql = "EXEC dbo.TaskFormulationSearch " +
                 " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, requestProfile.ApplicationModeId) +
				 ", " + ToSQLParameter(data, TaskFormulationDataModel.DataColumns.ProjectId) +
				 ", " + ToSQLParameter(data, TaskFormulationDataModel.DataColumns.TaskId) +
				 ", " + ToSQLParameter(data, TaskFormulationDataModel.DataColumns.FeatureId) +							
				 ", " + ToSQLParameter(data, TaskFormulationDataModel.DataColumns.TaskFormulationId);	
  

            var oDT = new DBDataTable("Get List", sql, DataStoreKey);

            return oDT.DBTable;

        }

		public static string ToSQLParameter(TaskFormulationDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TaskFormulationDataModel.DataColumns.TaskFormulationId:
					if (data.TaskFormulationId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskFormulationDataModel.DataColumns.TaskFormulationId, data.TaskFormulationId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskFormulationDataModel.DataColumns.TaskFormulationId);
					}
					break;
				case TaskFormulationDataModel.DataColumns.TaskId:
					if (data.TaskId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskFormulationDataModel.DataColumns.TaskId, data.TaskId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskFormulationDataModel.DataColumns.TaskId);
					}
					break;
				case TaskFormulationDataModel.DataColumns.ProjectId:
					if (data.ProjectId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskFormulationDataModel.DataColumns.ProjectId, data.ProjectId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskFormulationDataModel.DataColumns.ProjectId);
					}
					break;

				case TaskFormulationDataModel.DataColumns.FeatureId:
					if (data.FeatureId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskFormulationDataModel.DataColumns.FeatureId, data.FeatureId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskFormulationDataModel.DataColumns.FeatureId);
					}
					break;

				case TaskFormulationDataModel.DataColumns.SortOrder:
					if (data.SortOrder != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskFormulationDataModel.DataColumns.SortOrder, data.SortOrder);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskFormulationDataModel.DataColumns.SortOrder);
					}
					break;

				case TaskFormulationDataModel.DataColumns.TaskName:
					if (!string.IsNullOrEmpty(data.TaskName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskFormulationDataModel.DataColumns.TaskName, data.TaskName);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskFormulationDataModel.DataColumns.TaskName);
					}
					break;

				case TaskFormulationDataModel.DataColumns.ProjectName:
					if (!string.IsNullOrEmpty(data.ProjectName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskFormulationDataModel.DataColumns.ProjectName, data.ProjectName);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskFormulationDataModel.DataColumns.ProjectName);
					}
					break;

				case TaskFormulationDataModel.DataColumns.FeatureName:
					if (!string.IsNullOrEmpty(data.FeatureName))
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, TaskFormulationDataModel.DataColumns.FeatureName, data.FeatureName);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskFormulationDataModel.DataColumns.FeatureName);
					}
					break;


				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

        #endregion Search

        #region GetDetails

        public static DataTable GetDetails(TaskFormulationDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);

            var table = list.ToDataTable();

            return table;
        }
        public static List<TaskFormulationDataModel> GetEntityDetails(TaskFormulationDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.TaskFormulationSearch ";

            var parameters =
            new
            {
                    AuditId              = requestProfile.AuditId
                ,   ApplicationId        = requestProfile.ApplicationId
                ,   ReturnAuditInfo      = returnAuditInfo               
                ,   TaskFormulationId    = dataQuery.TaskFormulationId

            };

            List<TaskFormulationDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                

                result = dataAccess.Connection.Query<TaskFormulationDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();

                
            }

            return result;
        }

        //public static List<TaskFormulationDataModel> GetEntityDetails(TaskFormulationDataModel data, int auditId)
        //{
        //    var sql = "EXEC dbo.TaskFormulationSearch " +
        //              " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
        //               ", " + ToSQLParameter(data, TaskFormulationDataModel.DataColumns.TaskFormulationId);

        //    var result = new List<TaskFormulationDataModel>();

        //    using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
        //    {
        //        var dbReader = reader.DBReader;

        //        while (dbReader.Read())
        //        {
        //            var dataItem = new TaskFormulationDataModel();

        //            dataItem.TaskFormulationId = (int?)dbReader[TaskFormulationDataModel.DataColumns.TaskFormulationId];
        //            dataItem.TaskId = (int?)dbReader[TaskFormulationDataModel.DataColumns.TaskId];
        //            dataItem.ProjectId = (int?)dbReader[TaskFormulationDataModel.DataColumns.ProjectId];
        //            dataItem.FeatureId = (int?)dbReader[TaskFormulationDataModel.DataColumns.FeatureId];
        //            dataItem.SortOrder = (int?)dbReader[TaskFormulationDataModel.DataColumns.SortOrder];

        //            SetBaseInfo(dataItem, dbReader);

        //            result.Add(dataItem);
        //        }
        //    }

        //    return result;
        //}		

        #endregion GetDetails

		#region CreateOrUpdate

        private static string CreateOrUpdate(TaskFormulationDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
                    sql += "dbo.TaskFormulationInsert  " + "\r\n" +
                       " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +                       
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
                    sql += "dbo.TaskFormulationUpdate  " + "\r\n" +
                         " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                       
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, TaskFormulationDataModel.DataColumns.ProjectId) +
				 ", " + ToSQLParameter(data, TaskFormulationDataModel.DataColumns.TaskId) +
				 ", " + ToSQLParameter(data, TaskFormulationDataModel.DataColumns.FeatureId) +
				 ", " + ToSQLParameter(data, TaskFormulationDataModel.DataColumns.SortOrder) +
				 ", " + ToSQLParameter(data, TaskFormulationDataModel.DataColumns.TaskFormulationId);

			return sql;

		}
		#endregion CreateOrUpdate

        public static int Create(TaskFormulationDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");

            var taskFormulationId = DBDML.RunScalarSQL("TaskFormulation.Insert", sql, DataStoreKey);
            return Convert.ToInt32(taskFormulationId);
        }

		#region Update

        public static void Update(TaskFormulationDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("TaskFormulation.Update", sql, DataStoreKey);
		}
		#endregion Update

        #region Delete

        public static void Delete(TaskFormulationDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.TaskFormulationDelete ";

            var parameters =
            new
            {
                    AuditId             = requestProfile.AuditId
                ,   TaskFormulationId   = dataQuery.TaskFormulationId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                

                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

                
            }
        }

        #endregion Delete

		#region DoesExist

        public static DataTable DoesExist(TaskFormulationDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskFormulationSearch " +
                           " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +                            
							", " + ToSQLParameter(data, TaskFormulationDataModel.DataColumns.ProjectId) +
							", " + ToSQLParameter(data, TaskFormulationDataModel.DataColumns.TaskId) +
							", " + ToSQLParameter(data, TaskFormulationDataModel.DataColumns.FeatureId) +
							", " + ToSQLParameter(data, TaskFormulationDataModel.DataColumns.TaskFormulationId);	
  

			var oDT = new DBDataTable("Task.DoesExist", sql, DataStoreKey);

			return oDT.DBTable;
		}


        #endregion DoesExist

        #region GetChildren

        private static DataSet GetChildren(TaskFormulationDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskFormulationChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskFormulationDataModel.DataColumns.TaskFormulationId);	

            var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(TaskFormulationDataModel data, RequestProfile requestProfile)
        {
            var isDeletable = true;
            var ds = GetChildren(data, requestProfile);
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




