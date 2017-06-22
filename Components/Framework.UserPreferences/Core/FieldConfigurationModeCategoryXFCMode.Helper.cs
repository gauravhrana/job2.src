using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
    public partial class FieldConfigurationModeCategoryXFCMode : BaseClass
    {
        public class DataColumns : Framework.Components.DataAccess.DomainModel.BaseModel.BaseDataColumns
        {
            public const string FieldConfigurationModeCategoryXFCModeId = "FieldConfigurationModeCategoryXFCModeId";
            public const string FieldConfigurationModeCategoryId = "FieldConfigurationModeCategoryId";
            public const string FieldConfigurationModeId = "FieldConfigurationModeId";
        }

        public class Data : Framework.Components.DataAccess.DomainModel.BaseModel
        {
            public int? FieldConfigurationModeCategoryXFCModeId { get; set; }
            public int? FieldConfigurationModeCategoryId { get; set; }
            public int? FieldConfigurationModeId { get; set; }

            public string ToURLQuery()
            {
                return String.Empty; //"FieldConfigurationModeCategoryXFCModeId=" + FieldConfigurationModeCategoryXFCModeId
            }

            public string ToSQLParameter(string dataColumnName)
            {
                var returnValue = "NULL";

                switch (dataColumnName)
                {

                    case DataColumns.FieldConfigurationModeCategoryXFCModeId:
                        if (FieldConfigurationModeCategoryXFCModeId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.FieldConfigurationModeCategoryXFCModeId, FieldConfigurationModeCategoryXFCModeId);


                        }

                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.FieldConfigurationModeCategoryXFCModeId);

                        }
                        break;

                    case DataColumns.FieldConfigurationModeCategoryId:
                        if (FieldConfigurationModeCategoryId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.FieldConfigurationModeCategoryId, FieldConfigurationModeCategoryId);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.FieldConfigurationModeCategoryId);

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
