using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
    public partial class UserPreferenceSelectedItem
    {
        public class DataColumns : Framework.Components.DataAccess.DomainModel.BaseModel.BaseDataColumns
        {
            public const string UserPreferenceSelectedItemId         = "UserPreferenceSelectedItemId";
            public const string UserPreferenceKeyId                  = "UserPreferenceKeyId";
            public const string ApplicationUserId                    = "ApplicationUserId";
            public const string ParentKey                            = "ParentKey";
            public const string Value                                = "Value";
            public const string ApplicationUser                      = "ApplicationUser";
            public const string UserPreferenceKey                    = "UserPreferenceKey";
            public const string SortOrder                            = "SortOrder";
        }

        public class Data : Framework.Components.DataAccess.DomainModel.BaseModel
        {
            public int?		UserPreferenceSelectedItemId	{ get; set; }
			public int?		UserPreferenceKeyId				{ get; set; }
			public string	Value							{ get; set; }
			public string	ParentKey						{ get; set; }
            public int?		SortOrder						{ get; set;}
			public int?		ApplicationUserId				{ get; set; }

			public string	ApplicationUser					{ get; set; }
			public string	UserPreferenceKey				{ get; set; }

            public string ToURLQuery()
            {
                return String.Empty; 
            }

            public string ToSQLParameter(string dataColumnName)
            {
                var returnValue = "NULL";

                switch (dataColumnName)
                {
                    case DataColumns.UserPreferenceSelectedItemId:
                        if (UserPreferenceSelectedItemId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.UserPreferenceSelectedItemId, UserPreferenceSelectedItemId);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.UserPreferenceSelectedItemId);
                        }
                        break;


                    case DataColumns.UserPreferenceKeyId:
                        if (UserPreferenceKeyId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.UserPreferenceKeyId, UserPreferenceKeyId);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.UserPreferenceKeyId);

                        }
                        break;

                    case DataColumns.Value:
                        if (Value != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Value, Value);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Value);

                        }
                        break;

                    case DataColumns.ParentKey:
                        if (ParentKey != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.ParentKey, ParentKey);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ParentKey);

                        }
                        break;

                    case DataColumns.SortOrder:
                        if (SortOrder != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.SortOrder, SortOrder);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.SortOrder);

                        }
                        break;

                    case DataColumns.ApplicationUserId:
                        if (ApplicationUserId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.ApplicationUserId, ApplicationUserId);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ApplicationUserId);

                        }
                        break;

                    case DataColumns.ApplicationUser:
                        if (!string.IsNullOrEmpty(Value))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.ApplicationUser, ApplicationUser);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ApplicationUser);

                        }
                        break;

                    case DataColumns.UserPreferenceKey:
                        if (!string.IsNullOrEmpty(UserPreferenceKey))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.UserPreferenceKey, UserPreferenceKey);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.UserPreferenceKey);

                        }
                        break;

                }
                return returnValue;
            }

        }

    }
}
