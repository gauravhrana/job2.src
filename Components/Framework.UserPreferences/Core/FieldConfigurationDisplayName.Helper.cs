using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Components.UserPreference
{

    public partial class FieldConfigurationDisplayName
    {
        public class DataColumns : Framework.Components.DataAccess.DomainModel.BaseModel.BaseDataColumns
        {
            public const string FieldConfigurationDisplayNameId = "FieldConfigurationDisplayNameId";
            public const string LanguageId = "LanguageId";
            public const string FieldConfigurationId = "FieldConfigurationId";
            public const string FieldConfiguration = "FieldConfiguration";
            public const string Value = "Value";
            public const string IsDefault = "IsDefault";
            public const string Language = "Language";
        }

        public class Data : Framework.Components.DataAccess.DomainModel.BaseModel
        {
            public int? FieldConfigurationDisplayNameId { get; set; }
            public string Value { get; set; }
            public int? LanguageId { get; set; }
            public int? IsDefault { get; set; }
            public int? FieldConfigurationId { get; set; }
            public string FieldConfiguration { get; set; }
            public string Language { get; set; }

            public string ToURLQuery()
            {
                return String.Empty; //"FieldConfigurationDisplayNameId=" + FieldConfigurationDisplayNameId
            }

            public string ToSQLParameter(string dataColumnName)
            {
                var returnValue = "NULL";

                switch (dataColumnName)
                {

                    case DataColumns.FieldConfigurationDisplayNameId:
                        if (FieldConfigurationDisplayNameId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.FieldConfigurationDisplayNameId, FieldConfigurationDisplayNameId);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.FieldConfigurationDisplayNameId);

                        }
                        break;

                    case DataColumns.FieldConfigurationId:
                        if (FieldConfigurationId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.FieldConfigurationId, FieldConfigurationId);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.FieldConfigurationId);

                        }
                        break;

                    case DataColumns.IsDefault:
                        if (IsDefault != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.IsDefault, IsDefault);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.IsDefault);

                        }
                        break;

                    case DataColumns.Value:
                        if (!string.IsNullOrEmpty(Value))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Value, Value.Trim());

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Value);

                        }
                        break;

                    case DataColumns.FieldConfiguration:
                        if (!string.IsNullOrEmpty(FieldConfiguration))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.FieldConfiguration, FieldConfiguration.Trim());

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.FieldConfiguration);

                        }
                        break;

                    case DataColumns.Language:
                        if (!string.IsNullOrEmpty(Language))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Language, Language.Trim());

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Language);

                        }
                        break;

                    case DataColumns.LanguageId:
                        if (LanguageId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.LanguageId, LanguageId);

                        }
                        else
                        {
                            returnValue = returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.LanguageId);

                        }
                        break;

                }
                return returnValue;
            }

        }

    }


}
