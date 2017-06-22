using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using System.Globalization;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public partial class MilestoneXFeatureArchiveDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static MilestoneXFeatureArchiveDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("MilestoneXFeatureArchive");
        }

        #region GetDetails

        public static MilestoneXFeatureArchiveDataModel GetDetails(MilestoneXFeatureArchiveDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);
            return list.FirstOrDefault();
        }

        public static List<MilestoneXFeatureArchiveDataModel> GetEntityDetails(MilestoneXFeatureArchiveDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.MilestoneXFeatureArchiveSearch " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                      ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureArchiveId);

            var result = new List<MilestoneXFeatureArchiveDataModel>();

            using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
            {
                var dbReader = reader.DBReader;

                while (dbReader.Read())
                {
                    var dataItem = new MilestoneXFeatureArchiveDataModel();

                    dataItem.MilestoneXFeatureArchiveId = Convert.ToInt32(dbReader[MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureArchiveId]);
                    dataItem.Milestone = Convert.ToString(dbReader[MilestoneXFeatureArchiveDataModel.DataColumns.Milestone]);
                    dataItem.Feature = Convert.ToString(dbReader[MilestoneXFeatureArchiveDataModel.DataColumns.Feature]);
                    dataItem.MilestoneFeatureState = Convert.ToString(dbReader[MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneFeatureState]);
                    dataItem.Memo = Convert.ToString(dbReader[MilestoneXFeatureArchiveDataModel.DataColumns.Memo]);
                    //dataItem.KnowledgeDate = Convert.ToDateTime(dbReader[DataColumns.KnowledgeDate]);					
                    dataItem.AcknowledgedBy = Convert.ToString(dbReader[MilestoneXFeatureArchiveDataModel.DataColumns.AcknowledgedBy]);
                    dataItem.AcknowledgedById = (int?)dbReader[MilestoneXFeatureArchiveDataModel.DataColumns.AcknowledgedById];
                    dataItem.MilestoneXFeatureId = (int?)dbReader[MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureId];
                    /*dataItem.UpdatedDate = Convert.ToDateTime(dbReader[BaseColumns.UpdatedDate]);
                    dataItem.UpdatedBy = Convert.ToString(dbReader[BaseColumns.UpdatedBy]);
                    dataItem.LastAction = Convert.ToString(dbReader[BaseColumns.LastAction]);
                    */
                    result.Add(dataItem);
                }
            }

            return result;
        }

        #endregion

        #region Create

        public static void Create(MilestoneXFeatureArchiveDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");
            DBDML.RunSQL("MilestoneXFeatureArchive.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(MilestoneXFeatureArchiveDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("MilestoneXFeatureArchive.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(MilestoneXFeatureArchiveDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.MilestoneXFeatureArchiveDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureArchiveId);

            DBDML.RunSQL("MilestoneXFeatureArchive.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static string ToSQLParameter(MilestoneXFeatureArchiveDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {

                case MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureArchiveId:
                    if (data.MilestoneXFeatureArchiveId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureArchiveId, data.MilestoneXFeatureArchiveId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureArchiveId);

                    }
                    break;

                case MilestoneXFeatureArchiveDataModel.DataColumns.RecordDate:
                    if (data.RecordDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MilestoneXFeatureArchiveDataModel.DataColumns.RecordDate, data.RecordDate);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureArchiveDataModel.DataColumns.RecordDate);

                    }
                    break;

                case MilestoneXFeatureArchiveDataModel.DataColumns.Milestone:
                    if (!string.IsNullOrEmpty(data.Milestone))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MilestoneXFeatureArchiveDataModel.DataColumns.Milestone, data.Milestone);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureArchiveDataModel.DataColumns.Milestone);

                    }
                    break;

                case MilestoneXFeatureArchiveDataModel.DataColumns.Feature:
                    if (!string.IsNullOrEmpty(data.Feature))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MilestoneXFeatureArchiveDataModel.DataColumns.Feature, data.Feature);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureArchiveDataModel.DataColumns.Feature);

                    }
                    break;

                case MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneFeatureState:
                    if (!string.IsNullOrEmpty(data.MilestoneFeatureState))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneFeatureState, data.MilestoneFeatureState);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneFeatureState);

                    }
                    break;

                case MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureId:
                    if (data.MilestoneXFeatureId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureId, data.MilestoneXFeatureId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureId);

                    }
                    break;

                case MilestoneXFeatureArchiveDataModel.DataColumns.Memo:
                    if (!string.IsNullOrEmpty(data.Memo))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MilestoneXFeatureArchiveDataModel.DataColumns.Memo, data.Memo);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureArchiveDataModel.DataColumns.Memo);

                    }
                    break;

                case MilestoneXFeatureArchiveDataModel.DataColumns.KnowledgeDate:
                    if (data.KnowledgeDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MilestoneXFeatureArchiveDataModel.DataColumns.KnowledgeDate, data.KnowledgeDate);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureArchiveDataModel.DataColumns.KnowledgeDate);

                    }
                    break;

                case MilestoneXFeatureArchiveDataModel.DataColumns.AcknowledgedById:
                    if (data.AcknowledgedById != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MilestoneXFeatureArchiveDataModel.DataColumns.AcknowledgedById, data.AcknowledgedById);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureArchiveDataModel.DataColumns.AcknowledgedById);

                    }
                    break;

                case MilestoneXFeatureArchiveDataModel.DataColumns.AcknowledgedBy:
                    if (!string.IsNullOrEmpty(data.AcknowledgedBy))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MilestoneXFeatureArchiveDataModel.DataColumns.AcknowledgedBy, data.AcknowledgedBy);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureArchiveDataModel.DataColumns.AcknowledgedBy);

                    }
                    break;

            }
            return returnValue;
        }

        public static DataTable Search(MilestoneXFeatureArchiveDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.MilestoneXFeatureArchiveSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureArchiveId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.Milestone) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.Feature) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneFeatureState) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureId);

            var oDT = new DBDataTable("MilestoneXFeatureArchive.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        public static DataTable SearchHistory(MilestoneXFeatureArchiveDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.MilestoneXFeatureArchiveSearchHistory " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureArchiveId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.Milestone) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.Feature) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneFeatureState) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureId);

            var oDT = new DBDataTable("MilestoneXFeatureArchive.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region CreateOrUpdate

        private static string CreateOrUpdate(MilestoneXFeatureArchiveDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.MilestoneXFeatureArchiveInsert  ";
                    break;

                case "Update":
                    sql += "dbo.MilestoneXFeatureArchiveUpdate  ";
                    break;

                default:
                    break;

            }

            sql = sql + " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureArchiveId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.RecordDate) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.Milestone) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.Feature) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneFeatureState) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.Memo) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.KnowledgeDate) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.AcknowledgedById) +
                ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.AcknowledgedBy);
            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(MilestoneXFeatureArchiveDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.MilestoneXFeatureArchiveSearch " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                        ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureArchiveId) +
                        ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.Milestone) +
                        ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.Feature) +
                        ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneFeatureState) +
                        ", " + ToSQLParameter(data, MilestoneXFeatureArchiveDataModel.DataColumns.MilestoneXFeatureId);

            var oDT = new DBDataTable("MilestoneXFeatureArchive.DoesExist", sql, DataStoreKey);
            return oDT.DBTable.Rows.Count > 0;
        }

        #endregion
    }

}
