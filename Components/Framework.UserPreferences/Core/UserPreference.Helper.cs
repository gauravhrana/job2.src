using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference
{
    public partial class UserPreference
    {
        public class DataColumns : Framework.Components.DataAccess.DomainModel.BaseModel.BaseDataColumns
        {
            public const string UserPreferenceId         = "UserPreferenceId";
            public const string UserPreferenceKeyId      = "UserPreferenceKeyId";
            public const string Value                    = "Value";
            public const string DataTypeId               = "DataTypeId";
            public const string UserPreferenceCategoryId = "UserPreferenceCategoryId";
            public const string ApplicationUserId        = "ApplicationUserId";

            public const string Application              = "Application";
            public const string UserPreferenceCategory   = "UserPreferenceCategory";
            public const string UserPreferenceDataType   = "UserPreferenceDataType";
            public const string ApplicationUser          = "ApplicationUser";
            public const string UserPreferenceKey        = "UserPreferenceKey";
        }

        public class Data : Framework.Components.DataAccess.DomainModel.BaseModel
        {
			public int?		UserPreferenceId			{ get; set; }
			public int?		UserPreferenceKeyId			{ get; set; }
			public string	Value						{ get; set; }
			public int?		DataTypeId					{ get; set; }
			public int?		ApplicationId				{ get; set; }
			public int?		UserPreferenceCategoryId	{ get; set; }
			public int?		ApplicationUserId			{ get; set; }
			public string	Application					{ get; set; }
			public string	UserPreferenceCategory		{ get; set; }
			public string	 UserPreferenceDataType		{ get; set; }
			public string	ApplicationUser				{ get; set; }
			public string	UserPreferenceKey			{ get; set; }

            public string ToURLQuery()
            {
                return String.Empty; //"UserPreferenceId=" + UserPreferenceId
            }

            public string ToSQLParameter(string dataColumnName)
            {
                var returnValue = "NULL";

                switch (dataColumnName)
                {
                    case DataColumns.UserPreferenceId:
                        if (UserPreferenceId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.UserPreferenceId, UserPreferenceId);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.UserPreferenceId);
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

                    case DataColumns.DataTypeId:
                        if (DataTypeId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.DataTypeId, DataTypeId);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.DataTypeId);

                        }
                        break;

                    case DataColumns.UserPreferenceCategoryId:
                        if (UserPreferenceCategoryId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.UserPreferenceCategoryId, UserPreferenceCategoryId);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.UserPreferenceCategoryId);

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

                    case DataColumns.Application:
                        if (!string.IsNullOrEmpty(Application))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Application, Application);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Application);

                        }
                        break;

                    case DataColumns.UserPreferenceCategory:
                        if (!string.IsNullOrEmpty(Value))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.UserPreferenceCategory, UserPreferenceCategory);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.UserPreferenceCategory);

                        }
                        break;

                    case DataColumns.UserPreferenceDataType:
                        if (!string.IsNullOrEmpty(Value))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.UserPreferenceDataType, UserPreferenceDataType);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.UserPreferenceDataType);

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
