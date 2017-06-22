using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker.Feature;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer.Feature
{
    public partial class FeatureXFeatureRuleDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static FeatureXFeatureRuleDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("FeatureXFeatureRule");
        }

        #region GetList

        public static List<FeatureXFeatureRuleDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FeatureXFeatureRuleDataModel.Empty, requestProfile);
        }

        #endregion

        #region GetDetails

        public static FeatureXFeatureRuleDataModel GetDetails(FeatureXFeatureRuleDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
        }

        public static List<FeatureXFeatureRuleDataModel> GetEntityDetails(FeatureXFeatureRuleDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.FeatureXFeatureRuleSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureXFeatureRuleId);

            var result = new List<FeatureXFeatureRuleDataModel>();

            using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
            {
                var dbReader = reader.DBReader;

                while (dbReader.Read())
                {
                    var dataItem = new FeatureXFeatureRuleDataModel();

                    dataItem.FeatureXFeatureRuleId = (int?)dbReader[FeatureXFeatureRuleDataModel.DataColumns.FeatureXFeatureRuleId];

                    dataItem.FeatureId = (int?)dbReader[FeatureXFeatureRuleDataModel.DataColumns.FeatureId];
                    dataItem.FeatureRuleId = (int?)dbReader[FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleId];
                    dataItem.FeatureRuleStatusId = (int?)dbReader[FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatusId];

                    dataItem.Feature = (string)dbReader[FeatureXFeatureRuleDataModel.DataColumns.Feature];
                    dataItem.FeatureRule = (string)dbReader[FeatureXFeatureRuleDataModel.DataColumns.FeatureRule];
                    dataItem.FeatureRuleStatus = (string)dbReader[FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatus];

                    SetBaseInfo(dataItem, dbReader);

                    result.Add(dataItem);
                }
            }

            return result;
        }

        #endregion

        #region Create

        public static void Create(FeatureXFeatureRuleDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DBDML.RunSQL("FeatureXFeatureRule.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(FeatureXFeatureRuleDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DBDML.RunSQL("FeatureXFeatureRule.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(FeatureXFeatureRuleDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.FeatureXFeatureRuleDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureXFeatureRuleId) +
                ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureId) +
                ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatusId) +
                ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleId);

            DBDML.RunSQL("FeatureXFeatureRule.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static string ToSQLParameter(FeatureXFeatureRuleDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case FeatureXFeatureRuleDataModel.DataColumns.FeatureXFeatureRuleId:
                    if (data.FeatureXFeatureRuleId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FeatureXFeatureRuleDataModel.DataColumns.FeatureXFeatureRuleId, data.FeatureXFeatureRuleId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FeatureXFeatureRuleDataModel.DataColumns.FeatureXFeatureRuleId);
                    }
                    break;

                case FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleId:
                    if (data.FeatureRuleId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleId, data.FeatureRuleId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleId);
                    }
                    break;

                case FeatureXFeatureRuleDataModel.DataColumns.FeatureId:
                    if (data.FeatureId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FeatureXFeatureRuleDataModel.DataColumns.FeatureId, data.FeatureId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FeatureXFeatureRuleDataModel.DataColumns.FeatureId);
                    }
                    break;
                case FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatusId:
                    if (data.FeatureRuleStatusId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatusId, data.FeatureRuleStatusId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatusId);

                    }
                    break;

                case FeatureXFeatureRuleDataModel.DataColumns.FeatureRule:
                    if (!string.IsNullOrEmpty(data.FeatureRule))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FeatureXFeatureRuleDataModel.DataColumns.FeatureRule, data.FeatureRule);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FeatureXFeatureRuleDataModel.DataColumns.FeatureRule);
                    }
                    break;

                case FeatureXFeatureRuleDataModel.DataColumns.Feature:
                    if (!string.IsNullOrEmpty(data.Feature))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FeatureXFeatureRuleDataModel.DataColumns.Feature, data.Feature);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FeatureXFeatureRuleDataModel.DataColumns.Feature);
                    }
                    break;
                case FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatus:
                    if (!string.IsNullOrEmpty(data.FeatureRuleStatus))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatus, data.FeatureRuleStatus);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatus);

                    }
                    break;
            }
            return returnValue;
        }

        public static DataTable Search(FeatureXFeatureRuleDataModel data, RequestProfile requestProfile)
        {
            var mergeddt = new DataTable();

            if (data.FeatureRuleStatusIds != null && data.FeatureRuleStatusIds.Length >= 1)
            {
                for (int i = 0; i < data.FeatureRuleStatusIds.Length; i++)
                {
                    var sql = "EXEC dbo.FeatureXFeatureRuleSearch " +
                    " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                    ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                    ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureXFeatureRuleId) +
                    ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureId) +
                    ", @FeatureRuleStatusId = " + data.FeatureRuleStatusIds[i] +
                    ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleId);

                    var oDT = new DBDataTable("FeatureXFeatureRule.Search", sql, DataStoreKey);
                    var dt = new DataTable();
                    dt = oDT.DBTable;
                    if (mergeddt.Rows.Count == 0)
                    {
                        mergeddt = dt.Clone();
                    }
                    foreach (DataRow row in dt.Rows)
                    {
                        mergeddt.ImportRow(row);
                    }
                }
                return mergeddt;
            }
            else
            {
                // formulate SQL
                var sql = "EXEC dbo.FeatureXFeatureRuleSearch " +
                    " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                    ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                    ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureXFeatureRuleId) +
                    ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureId) +
                    ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatusId) +
                    ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleId);

                var oDT = new DBDataTable("FeatureXFeatureRule.Search", sql, DataStoreKey);
                return oDT.DBTable;
            }


        }

        #endregion

        #region Save

        private static string Save(FeatureXFeatureRuleDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.FeatureXFeatureRuleInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.FeatureXFeatureRuleUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureXFeatureRuleId) +
                        ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureId) +
                        ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatusId) +
                        ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleId);
            return sql;
        }

        #endregion

        #region DoesExist

        public static DataTable DoesExist(FeatureXFeatureRuleDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.FeatureXFeatureRuleSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
            ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureId) +
            ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleId) +
            ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatusId) +
            ", " + ToSQLParameter(data, FeatureXFeatureRuleDataModel.DataColumns.FeatureXFeatureRuleId);

            var oDT = new DBDataTable("FeatureXFeatureRule.DoesExist", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion
    }
}
