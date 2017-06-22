using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace MVCModels.ApplicationUser
{
    public partial class ApplicationUser
    {
        public static class DataColumns
        {
            public const string ApplicationUserId = "ApplicationUserId";
            public const string ApplicationUserName = "ApplicationUserName";
            public const string FirstName = "FirstName";
            public const string LastName = "LastName";
            public const string MiddleName = "MiddleName";
            public const string FullName = "FullName";
            public const string ApplicationUserTitleId = "ApplicationUserTitleId";
            public const string ApplicationUserTitle = "ApplicationUserTitle";
            public const string ApplicationId = "ApplicationId";
            public const string Application = "Application";

        }

        public class Data
        {
            public int? ApplicationUserId { get; set; }
            public string ApplicationUserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public string FullName { get; set; }
            public int? ApplicationUserTitleId { get; set; }
            public string ApplicationUserTitle { get; set; }
            public int? ApplicationId { get; set; }
            public string Application { get; set; }

            public string ToURLQuery()
            {
                return string.Empty; //"ApplicationUserId=" + ApplicationUserId
            }

            public string ToSQLParameter(string dataColumnName)
            {
                var returnValue = "NULL";

                switch (dataColumnName)
                {

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

                    case DataColumns.ApplicationUserName:
                        if (!string.IsNullOrEmpty(ApplicationUserName))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.ApplicationUserName, ApplicationUserName);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ApplicationUserName);
                        }
                        break;

                    case DataColumns.FirstName:
                        if (!string.IsNullOrEmpty(FirstName))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.FirstName, FirstName);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.FirstName);
                        }
                        break;

                    case DataColumns.LastName:
                        if (!string.IsNullOrEmpty(LastName))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.LastName, LastName); ;
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.LastName);
                        }
                        break;

                    case DataColumns.MiddleName:
                        if (!string.IsNullOrEmpty(MiddleName))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.MiddleName, MiddleName);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.MiddleName);
                        }
                        break;

                    case DataColumns.FullName:
                        if (!string.IsNullOrEmpty(FullName))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.FullName, FullName);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.FullName);
                        }
                        break;

                    case DataColumns.ApplicationUserTitleId:
                        if (ApplicationUserTitleId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.ApplicationUserTitleId, ApplicationUserTitleId);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ApplicationUserTitleId);
                        }
                        break;

                    case DataColumns.ApplicationUserTitle:
                        if (!string.IsNullOrEmpty(FullName))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.ApplicationUserTitle, ApplicationUserTitle);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ApplicationUserTitle);
                        }
                        break;

                    case DataColumns.ApplicationId:
                        if (ApplicationId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.ApplicationId, ApplicationId);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ApplicationId);
                        }
                        break;

                    case DataColumns.Application:
                        if (Application != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.Application, Application);
                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Application);
                        }
                        break;

                }
                return returnValue;
            }

        }

    }
}
