//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Framework.Components.DataAccess;
//using Framework.Components.DataAccess.DomainModel;

//namespace Framework.Components.TasksAndWorkflow
//{
//    public partial class TaskScheduleType
//    {
//        public class DataColumns : Framework.Components.DataAccess.DomainModel.BaseModel.BaseDataColumns
//        {
//            public const string TaskScheduleTypeId = "TaskScheduleTypeId";
//            public const string Name               = "Name";
//            public const string Description        = "Description";
//            public const string Active             = "Active";
//            public const string SortOrder          = "SortOrder";
//        }

//        public struct Data
//        {
//            public int?		TaskScheduleTypeId	{ get; set; }
//            public string	Name				{ get; set; }
//            public string	Description			{ get; set; }
//            public int?		Active				{ get; set; }
//            public int?		SortOrder			{ get; set; }

//            public string ToURLQuery()
//            {
//                return string.Empty; //"TaskScheduleTypeId=" + TaskScheduleTypeId
//            }

//            public string ToSQLParameter(string dataColumnName)
//            {
//                var returnValue = "NULL";
//                switch (dataColumnName)
//                {

//                    case DataColumns.TaskScheduleTypeId:
//                        if (TaskScheduleTypeId != null)
//                        {
//                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.TaskScheduleTypeId, TaskScheduleTypeId);

//                        }
//                        else
//                        {
//                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.TaskScheduleTypeId);

//                        }
//                        break;

//                    case DataColumns.Name:
//                        if (!string.IsNullOrEmpty(Name))
//                        {
//                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Name, Name);

//                        }
//                        else
//                        {
//                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Name);

//                        }
//                        break;

//                    case DataColumns.Description:
//                        if (!string.IsNullOrEmpty(Description))
//                        {
//                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Description, Description);

//                        }
//                        else
//                        {
//                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Description);

//                        }
//                        break;

//                    case DataColumns.Active:
//                        if (Active != null)
//                        {
//                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.Active, Active);

//                        }
//                        else
//                        {
//                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Active);

//                        }
//                        break;

//                    case DataColumns.SortOrder:
//                        if (SortOrder != null)
//                        {
//                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.SortOrder, SortOrder);

//                        }
//                        else
//                        {
//                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.SortOrder);

//                        }
//                        break;

//                }
//                return returnValue;
//            }

//        }

//    }
//}
