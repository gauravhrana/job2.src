using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace Framework.Components.DataAccess
{
    public abstract class StandardManager : BaseDataManager
    {
        public string EntityName;

        public static string ToSQLParameter(StandardModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case StandardModel.StandardColumns.Name:
                    if (!string.IsNullOrEmpty(data.Name))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, StandardModel.StandardColumns.Name,
                            data.Name);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StandardModel.StandardColumns.Name);
                    }

                    break;

                case StandardModel.StandardColumns.Description:
                    if (!string.IsNullOrEmpty(data.Description))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE,
                            StandardModel.StandardColumns.Description, data.Description);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StandardModel.StandardColumns.Description);
                    }

                    break;

                case StandardModel.StandardColumns.SortOrder:
                    if (data.SortOrder != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, StandardModel.StandardColumns.SortOrder,
                            data.SortOrder);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, StandardModel.StandardColumns.SortOrder);
                    }

                    break;

                case BaseDataModel.BaseDataColumns.ApplicationId:
                    if (data.ApplicationId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, BaseDataModel.BaseDataColumns.ApplicationId, data.ApplicationId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, BaseDataModel.BaseDataColumns.ApplicationId);
                    }

                    break;
            }

            return returnValue;
        }

        protected static void SetStandardInfo(StandardModel dataItem, SqlDataReader dbReader)
        {
            dataItem.Name        = dbReader[StandardModel.StandardColumns.Name].ToString();
            dataItem.Description = dbReader[StandardModel.StandardColumns.Description].ToString();
            dataItem.SortOrder   = (int)dbReader[StandardModel.StandardColumns.SortOrder];
            dataItem.EntityKey   = (int)dbReader[StandardModel.StandardColumns.EntityKey];
        }

        protected static void SetStandardInfo(StandardModel dataItem, DataRow dataRow)
        {
            dataItem.Name        = dataRow[StandardModel.StandardColumns.Name].ToString();
            dataItem.Description = dataRow[StandardModel.StandardColumns.Description].ToString();
            dataItem.SortOrder   = (int)dataRow[StandardModel.StandardColumns.SortOrder];
            dataItem.EntityKey   = (int)dataRow[StandardModel.StandardColumns.EntityKey];
        }        

    }
}
