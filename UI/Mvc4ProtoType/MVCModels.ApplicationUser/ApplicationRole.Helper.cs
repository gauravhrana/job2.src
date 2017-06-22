﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace MVCModels.ApplicationUser
{
    public partial class ApplicationRole
    {
        public static class DataColumns
        {
            public const string ApplicationRoleId = "ApplicationRoleId";
            public const string Name = "Name";
            public const string Description = "Description";
            public const string SortOrder = "SortOrder";
            public const string ApplicationId = "ApplicationId";
            public const string Application = "Application";
        }

        public class Data
        {
            public int? ApplicationRoleId { get; set; }
            public int? ApplicationId { get; set; }
            public string Application { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int? SortOrder { get; set; }

            public string ToURLQuery()
            {
                return string.Empty; //"ApplicationRoleId=" + ApplicationRoleId
            }

            public string ToSQLParameter(string dataColumnName)
            {
                var returnValue = "NULL";

                switch (dataColumnName)
                {

                    case DataColumns.ApplicationRoleId:
                        if (ApplicationRoleId != null)
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, DataColumns.ApplicationRoleId, ApplicationRoleId);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.ApplicationRoleId);

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
                        if (!string.IsNullOrEmpty(Application))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Application, Application);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Application);

                        }
                        break;

                    case DataColumns.Name:
                        if (!string.IsNullOrEmpty(Name))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Name, Name);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Name);

                        }
                        break;

                    case DataColumns.Description:
                        if (!string.IsNullOrEmpty(Description))
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, DataColumns.Description, Description);

                        }
                        else
                        {
                            returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, DataColumns.Description);

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

                }
                return returnValue;
            }

        }

    }
}
