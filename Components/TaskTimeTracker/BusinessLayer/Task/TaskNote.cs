using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer.Task
{

	public partial class TaskNoteDataManager : StandardDataManager
	{
		
		private static string DataStoreKey = "";
		
		static TaskNoteDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("TaskNote");			
		}

		public TaskNoteDataManager()
		{
			EntityName = "TaskNote";
		}

		#region GetList

        public static DataTable GetList(RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskNoteSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId)
                + ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
			var oDT = new DBDataTable("TaskNote.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion GetList

		#region Search
		public static string ToSQLParameter(TaskNoteDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case TaskNoteDataModel.DataColumns.TaskNoteId:
					if (data.TaskNoteId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, TaskNoteDataModel.DataColumns.TaskNoteId, data.TaskNoteId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, TaskNoteDataModel.DataColumns.TaskNoteId);
					}
					break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}

			return returnValue;
		}

        public static DataTable Search(TaskNoteDataModel data, RequestProfile requestProfile)
		{
			// formulate SQL  
			var sql = "EXEC dbo.TaskNoteSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationMode, requestProfile.ApplicationModeId) +
				", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +				
				", " + ToSQLParameter(data, TaskNoteDataModel.DataColumns.TaskNoteId);
				

			var oDT = new DBDataTable("TaskNote.GetList", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion Search

		#region GetDetails

        public static DataTable GetDetails(TaskNoteDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);

            var table = list.ToDataTable();

            return table;
        }

        public static List<TaskNoteDataModel> GetEntityDetails(TaskNoteDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.TaskNoteSearch ";

            var parameters =
            new
            {
                    AuditId              = requestProfile.AuditId
                ,   ApplicationId        = requestProfile.ApplicationId
                ,   ReturnAuditInfo      = returnAuditInfo
                ,   TaskNoteId           = dataQuery.TaskNoteId
                ,   Name                 = dataQuery.Name
              
            };

            List<TaskNoteDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                

                result = dataAccess.Connection.Query<TaskNoteDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();

                
            }

            return result;
        }

        //public static List<TaskNoteDataModel> GetEntityDetails(TaskNoteDataModel data, int auditId)
        //{
        //    var sql = "EXEC dbo.TaskNoteSearch " +
        //        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
        //        ", " + ToSQLParameter(data, TaskNoteDataModel.DataColumns.TaskNoteId);
				

        //    var result = new List<TaskNoteDataModel>();

        //    using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
        //    {
        //        var dbReader = reader.DBReader;

        //        while (dbReader.Read())
        //        {
        //            var dataItem = new TaskNoteDataModel();

        //            dataItem.TaskNoteId = (int)dbReader[TaskNoteDataModel.DataColumns.TaskNoteId];

        //            SetStandardInfo(dataItem, dbReader);

        //            SetBaseInfo(dataItem, dbReader);

        //            result.Add(dataItem);
        //        }
        //    }

        //    return result;
		//}

		#endregion GetDetails

		#region CreateOrUpdate

        private static string CreateOrUpdate(TaskNoteDataModel data, RequestProfile requestProfile, string action)
		{
            var traceId = TraceDataManager.GetNextTraceId(requestProfile);
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.TaskNoteInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.TraceId, traceId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.TaskNoteUpdate  " + "\r\n" +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.TraceId, traceId);
					break;

				default:
					break;
			}

			sql = sql + ", " + ToSQLParameter(data, TaskNoteDataModel.DataColumns.TaskNoteId) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
						", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

			return sql;
		}

		#endregion CreateOrUpdate

        #region Create

        public static int Create(TaskNoteDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");

            var taskNoteId = DBDML.RunScalarSQL("TaskNote.Insert", sql, DataStoreKey);
            return Convert.ToInt32(taskNoteId);
        }

        #endregion Create

		#region Update

        public static void Update(TaskNoteDataModel data, RequestProfile requestProfile)
		{
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("Clent.Update", sql, DataStoreKey);
		}

		#endregion Update

		#region Renumber
        public static void Renumber(int seed, int increment, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskNoteRenumber " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
					  ",@Seed = " + seed +
					  ",@Increment = " + increment;

			DBDML.RunSQL("TaskNote.Renumber", sql, DataStoreKey);
		}
		#endregion Renumber

		#region Delete

        public static void Delete(TaskNoteDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.TaskNoteDelete ";

            var parameters =
            new
            {
                    AuditId      = requestProfile.AuditId
                ,   TaskNoteId   = dataQuery.TaskNoteId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                

                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

                
            }
        }

		#endregion Delete

		#region DoesExist

        public static DataTable DoesExist(TaskNoteDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskNoteSearch " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
							", " + ToSQLParameter(data, TaskNoteDataModel.DataColumns.TaskNoteId) +
							", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name);

			var oDT = new DBDataTable("TaskNote.DoesExist", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion DoesExist

		#region GetChildren

		private static DataSet GetChildren(TaskNoteDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskNoteChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskNoteDataModel.DataColumns.TaskNoteId);

			var oDT = new DBDataSet("TaskNote.GetChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region DeleteChildren

        public static DataSet DeleteChildren(TaskNoteDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.TaskNoteChildrenDelete " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, TaskNoteDataModel.DataColumns.TaskNoteId);

			var oDT = new DBDataSet("TaskNote.DeleteChildren", sql, DataStoreKey);

			return oDT.DBDataset;
		}

		#endregion

		#region IsDeletable

        public static bool IsDeletable(TaskNoteDataModel data, RequestProfile requestProfile)
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

		

		public virtual string GetInsertProcedureScript()
		{
			var obj = new TaskNoteDataModel();
			return base.GetInsertProcedureScript(obj.GetType());
		}

		public virtual string GetUpdateProcedureScript()
		{
			var obj = new TaskNoteDataModel();
			return base.GetUpdateProcedureScript(obj.GetType());
		}

		public virtual string GetSearchProcedureScript(List<string> searchableColumns)
		{
			var obj = new TaskNoteDataModel();
			return base.GetSearchProcedureScript(searchableColumns, obj.GetType());
		}

	}
}
