using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer.Task
{
    public partial class TaskXTaskNote
    {


		public class DataColumns : BaseDataModel.BaseDataColumns
        {
            public const string TaskXTaskNoteId = "TaskXTaskNoteId";
            public const string TaskId = "TaskId";
            public const string TaskNoteId = "TaskNoteId";

            public const string Task = "Task";
            public const string TaskNote = "TaskNote";
        }

		public class Data : BaseDataModel
        {
            public int? TaskXTaskNoteId { get; set; }
            public int? TaskId { get; set; }
            public int? TaskNoteId { get; set; }

            public string Task { get; set; }
            public string TaskNote { get; set; }

            public string ToSQLParameter(string dataColumnName)
            {
                var returnValue = "NULL";
                switch (dataColumnName)
                {
                    case DataColumns.TaskXTaskNoteId:
                        if (TaskXTaskNoteId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.TaskXTaskNoteId, TaskXTaskNoteId);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.TaskXTaskNoteId);
                        }
                        break;

                    case DataColumns.TaskId:
                        if (TaskId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.TaskId, TaskId);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.TaskId);
                        }
                        break;

                    case DataColumns.TaskNoteId:
                        if (TaskNoteId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.TaskNoteId, TaskNoteId);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.TaskNoteId);
                        }
                        break;

                    case DataColumns.Task:
                        if (!string.IsNullOrEmpty(Task))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Task, Task);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Task);
                        }
                        break;

                    case DataColumns.TaskNote:
                        if (!string.IsNullOrEmpty(TaskNote))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.TaskNote, TaskNote);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.TaskNote);
                        }
                        break;
                }
                return returnValue;
            }

        }
    }
}
