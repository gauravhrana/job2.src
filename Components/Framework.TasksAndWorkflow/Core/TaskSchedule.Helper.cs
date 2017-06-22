using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace Framework.Components.TasksAndWorkflow
{
    public partial class TaskSchedule
    {
        public class DataColumns : DataModel.Framework.DataAccess.BaseDataModel.BaseDataColumns
        {
            public const string TaskScheduleId     = "TaskScheduleId";
            public const string TaskScheduleTypeId = "TaskScheduleTypeId";
            public const string TaskEntityId       = "TaskEntityId";
            public const string TaskScheduleType   = "TaskScheduleType";
            public const string TaskEntity         = "TaskEntity";            
        }

        public class Data : DataModel.Framework.DataAccess.BaseDataModel
        {
			public int?		 TaskScheduleId		{ get; set; }
			public int?		 TaskScheduleTypeId { get; set; }
			public int?		 TaskEntityId		{ get; set; }
			public string	 TaskScheduleType	{ get; set; }
			public string	 TaskEntity			{ get; set; }

            public string ToURLQuery()
			{
				return String.Empty; //"TaskScheduleId=" + TaskScheduleId
			}

			public string ToSQLParameter(string dataColumnName)
			{
				var returnValue = "NULL";

				switch (dataColumnName)
                {

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

					case DataColumns.TaskScheduleTypeId:
                        if (TaskScheduleTypeId != null)
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.TaskScheduleTypeId, TaskScheduleTypeId);
														
                        }
                        else
                        {
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.TaskScheduleTypeId);
														
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

					case DataColumns.TaskScheduleType:
						if (!string.IsNullOrEmpty(TaskScheduleType))
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.TaskScheduleType, TaskScheduleType);
						}
						else
						{
							returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.TaskScheduleType);
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
