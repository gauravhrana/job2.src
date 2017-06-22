using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.Framework.TasksAndWorkFlow;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using System.Runtime.CompilerServices;
using Dapper;
using Framework.CommonServices.BusinessDomain.Utils;

namespace Framework.Components.TasksAndWorkflow
{
    public partial class TaskSchedule : BaseDataManager
    {

        static readonly string DataStoreKey = "";        

        static TaskSchedule()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskSchedule");
        }

        #region GetList

		public static DataTable GetList(RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskScheduleSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId);

            var oDT = new DataAccess.DBDataTable("TaskSchedule.List", sql, DataStoreKey);
            return oDT.DBTable;
        }

		public static List<TaskScheduleDataModel> GetTaskScheduleList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskScheduleSearch" +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);

			var result = new List<TaskScheduleDataModel>();

			using (var reader = new DBDataReader("Get List", sql, DataStoreKey))
			{
				var dbReader = reader.DBReader;

				while (dbReader.Read())
				{
					var dataItem = new TaskScheduleDataModel();

					dataItem.TaskScheduleId = (int)dbReader[TaskScheduleDataModel.DataColumns.TaskScheduleId];
					dataItem.TaskScheduleType = dbReader[TaskScheduleDataModel.DataColumns.TaskScheduleType].ToString();
					//SetStandardInfo(dataItem, dbReader);

					result.Add(dataItem);
				}

			}

			return result;
		}


		public static string ToSQLParameter(TaskScheduleDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TaskScheduleDataModel.DataColumns.TaskScheduleId:
					if (data.TaskScheduleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskScheduleDataModel.DataColumns.TaskScheduleId, data.TaskScheduleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskScheduleDataModel.DataColumns.TaskScheduleId);
					}
					break;

				case TaskScheduleDataModel.DataColumns.TaskEntityId:
					if (data.TaskEntityId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskScheduleDataModel.DataColumns.TaskEntityId, data.TaskEntityId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskScheduleDataModel.DataColumns.TaskEntityId);
					}
					break;

				case TaskScheduleDataModel.DataColumns.TaskScheduleTypeId:
					if (data.TaskScheduleTypeId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskScheduleDataModel.DataColumns.TaskScheduleTypeId, data.TaskScheduleTypeId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskScheduleDataModel.DataColumns.TaskScheduleTypeId);
					}
					break;
				
				case TaskScheduleDataModel.DataColumns.TaskScheduleType:
					if (data.TaskScheduleType != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskScheduleDataModel.DataColumns.TaskScheduleType, data.TaskScheduleType);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskScheduleDataModel.DataColumns.TaskScheduleType);
					}
					break;

				case TaskScheduleDataModel.DataColumns.TaskEntity:
					if (data.TaskEntity != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskScheduleDataModel.DataColumns.TaskEntity, data.TaskEntity);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskScheduleDataModel.DataColumns.TaskEntity);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

        #endregion


        #region GetDetails

		public static DataTable GetDetails(TaskScheduleDataModel data, RequestProfile requestProfile)
        {
			var list = GetEntityDetails(data, requestProfile);

			var table = list.ToDataTable();

			return table;
        }

		#endregion

		#region GetEntitySearch

		public static List<TaskScheduleDataModel> GetEntityDetails(TaskScheduleDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.TaskScheduleSearch ";

			var parameters =
			new
			{
					AuditId						= requestProfile.AuditId
				,	ApplicationId				= dataQuery.ApplicationId
				,	TaskScheduleId				= dataQuery.TaskScheduleId
				,	Name						= dataQuery.Name
				,	ApplicationMode				= requestProfile.ApplicationModeId
				,	ReturnAuditInfo				= returnAuditInfo
				,	TaskScheduleTypeId			= dataQuery.TaskScheduleTypeId 
				,	TaskEntityId				= dataQuery.TaskEntityId 

			};

			List<TaskScheduleDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<TaskScheduleDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		

        #endregion

        #region Create

		public static void Create(TaskScheduleDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DataAccess.DBDML.RunSQL("TaskSchedule.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

		public static void Update(TaskScheduleDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DataAccess.DBDML.RunSQL("TaskSchedule.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

		public static void Delete(TaskScheduleDataModel dataQuery, RequestProfile requestProfile)
        {
			const string sql = @"dbo.TaskScheduleDelete ";

			var parameters =
			new
			{
					AuditId			= requestProfile.AuditId
				,	TaskScheduleId	= dataQuery.TaskScheduleId

			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				

				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

				
			}
        }

        #endregion

        #region Search

		public static DataTable Search(TaskScheduleDataModel data, RequestProfile requestProfile)
        {
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
        }

        #endregion

        #region Save

		private static string Save(TaskScheduleDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.TaskScheduleInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId,requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.TaskScheduleUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, TaskScheduleDataModel.DataColumns.TaskScheduleId) +
				", " + ToSQLParameter(data, TaskScheduleDataModel.DataColumns.TaskScheduleTypeId) +
				", " + ToSQLParameter(data, TaskScheduleDataModel.DataColumns.TaskEntityId);
            return sql;
        }

        #endregion

        #region DoesExist

		public static DataTable DoesExist(TaskScheduleDataModel data, RequestProfile requestProfile)
        {
			var doesExistRequest = new TaskScheduleDataModel();
			doesExistRequest.Name = data.Name;

			return Search(doesExistRequest, requestProfile);
        }

        #endregion

        #region GetChildren

		private static DataSet GetChildren(TaskScheduleDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.TaskScheduleChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId,requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskScheduleDataModel.DataColumns.TaskScheduleId);

            var oDT = new DataAccess.DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

		public static bool IsDeletable(TaskScheduleDataModel data, RequestProfile requestProfile)
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
