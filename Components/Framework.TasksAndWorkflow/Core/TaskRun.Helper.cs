using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace Framework.Components.TasksAndWorkflow
{
    public partial class TaskRun
    {
        public class DataColumns : DataModel.Framework.DataAccess.BaseDataModel.BaseDataColumns
        {
            public const string TaskRunId      = "TaskRunId";
            public const string TaskScheduleId = "TaskScheduleId";
            public const string TaskEntityId   = "TaskEntityId";
            public const string BusinessDate   = "BusinessDate";
            public const string StartTime      = "StartTime";
            public const string EndTime        = "EndTime";
            public const string RunBy          = "RunBy";
            public const string TaskEntity     = "TaskEntity";
        }

        public class Data : DataModel.Framework.DataAccess.BaseDataModel
        {
			public int?			TaskRunId		{ get; set; }
			public int?			TaskScheduleId  { get; set; }
			public int?			TaskEntityId	{ get; set; }
			public int?			BusinessDate	{ get; set; }
			public DateTime?	StartTime		{ get; set; }
            public DateTime?    EndTime         { get; set; }
			public string		RunBy			{ get; set; }
			public string		TaskEntity		{ get; set; }

			public string ToURLQuery()
			{
				return String.Empty; //"TaskRunId=" + TaskRunId
			}

			public string ToSQLParameter(string dataColumnName)
			{
				var returnValue = "NULL";
				switch (dataColumnName)
                {
                    case DataColumns.TaskRunId:
                        if (TaskRunId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.TaskRunId, TaskRunId);
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.TaskRunId);
                        }
                        break;

					case DataColumns.TaskScheduleId:
                        if (TaskScheduleId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.TaskScheduleId, TaskScheduleId);
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.TaskScheduleId);
                        }
                        break;

					case DataColumns.TaskEntityId:
                        if (TaskEntityId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.TaskEntityId, TaskEntityId);
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.TaskEntityId);
                        }
                        break;

					case DataColumns.BusinessDate:
                        if (BusinessDate != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.BusinessDate, BusinessDate);
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.BusinessDate);
                        }
                        break;

					case DataColumns.StartTime:
                        if (StartTime != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.StartTime, StartTime);
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.StartTime);
                        }
                        break;

					case DataColumns.EndTime:
                        if (EndTime != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.EndTime, EndTime);
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.EndTime);
                        }
                        break;

                    case DataColumns.RunBy:
                        if (!string.IsNullOrEmpty(RunBy))
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.RunBy, RunBy);
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.RunBy);
                        }
                        break;

					case DataColumns.TaskEntity:
						if (!string.IsNullOrEmpty(TaskEntity))
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.TaskEntity, TaskEntity);
						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.TaskEntity);
						}
						break;

                }
                return returnValue;
            }

        }

    }
}
