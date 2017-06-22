using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
    public partial class ApplicationModeXFieldConfigurationMode
    {
        public class DataColumns : Framework.Components.DataAccess.DomainModel.BaseModel.BaseDataColumns
        {
            public const string ApplicationModeXFieldConfigurationModeId = "ApplicationModeXFieldConfigurationModeId";
            public const string ApplicationModeId = "ApplicationModeId";
            public const string FieldConfigurationModeId = "FieldConfigurationModeId";
        }

        public class Data : Framework.Components.DataAccess.DomainModel.BaseModel
        {
            public int? ApplicationModeXFieldConfigurationModeId { get; set; }
            public int? ApplicationModeId { get; set; }
            public int? FieldConfigurationModeId { get; set; }

            public string ToURLQuery()
            {
                return String.Empty; //"ApplicationModeXFieldConfigurationModeId=" + ApplicationModeXFieldConfigurationModeId
            }

            public string ToSQLParameter(string dataColumnName)
            {
                var returnValue = "NULL";

                switch (dataColumnName)
                {

                    case DataColumns.ApplicationModeXFieldConfigurationModeId:
                        if (ApplicationModeXFieldConfigurationModeId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.ApplicationModeXFieldConfigurationModeId, ApplicationModeXFieldConfigurationModeId);


                        }

                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ApplicationModeXFieldConfigurationModeId);

                        }
                        break;

                    case DataColumns.ApplicationModeId:
                        if (ApplicationModeId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.ApplicationModeId, ApplicationModeId);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ApplicationModeId);

                        }
                        break;

                    case DataColumns.FieldConfigurationModeId:
                        if (FieldConfigurationModeId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.FieldConfigurationModeId, FieldConfigurationModeId);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.FieldConfigurationModeId);

                        }
                        break;

                }
                return returnValue;
            }

        }
    }
}
