using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer
{
    public partial class MilestoneXFeatureDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static MilestoneXFeatureDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("MilestoneXFeature");
        }

        #region GetList

        public static List<MilestoneXFeatureDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityList(MilestoneXFeatureDataModel.Empty, requestProfile);
        }

        #endregion

        #region GetDetails

        public static MilestoneXFeatureDataModel GetDetails(MilestoneXFeatureDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityList(data, requestProfile);
            return list.FirstOrDefault();
        }

        public static List<MilestoneXFeatureDataModel> GetEntityList(MilestoneXFeatureDataModel data, RequestProfile requestProfile)
        {
            var oDT = GetDetails(data, requestProfile);

            var dataList = new List<MilestoneXFeatureDataModel>();

            if (oDT != null)
            {
                var oData = new MilestoneXFeatureDataModel();

                oData.MilestoneXFeatureId   = oDT.MilestoneXFeatureId;
                oData.Feature               = oDT.Feature;
                oData.Milestone             = oDT.Milestone;
                oData.MilestoneFeatureState = oDT.MilestoneFeatureState;
                oData.Memo                  = oDT.Memo;

                //SetBaseInfo(oData, oDT);

                dataList.Add(oData);
            }

            return dataList;
        }

        #endregion

        #region Create

        public static void Create(MilestoneXFeatureDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            DBDML.RunSQL("MilestoneXFeature.Insert", sql, DataStoreKey);
        }

        #endregion

        #region Update

        public static void Update(MilestoneXFeatureDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DBDML.RunSQL("MilestoneXFeature.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(MilestoneXFeatureDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.MilestoneXFeatureDelete " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureDataModel.DataColumns.MilestoneXFeatureId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureDataModel.DataColumns.MilestoneId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureDataModel.DataColumns.MilestoneFeatureStateId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureDataModel.DataColumns.FeatureId);

            DBDML.RunSQL("MilestoneXFeature.Delete", sql, DataStoreKey);
        }

        #endregion

        #region Search

        public static string ToSQLParameter(MilestoneXFeatureDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";
            switch (dataColumnName)
            {
                case MilestoneXFeatureDataModel.DataColumns.MilestoneXFeatureId:
                    if (data.MilestoneXFeatureId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MilestoneXFeatureDataModel.DataColumns.MilestoneXFeatureId, data.MilestoneXFeatureId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureDataModel.DataColumns.MilestoneXFeatureId);
                    }
                    break;

                case MilestoneXFeatureDataModel.DataColumns.FeatureId:
                    if (data.FeatureId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MilestoneXFeatureDataModel.DataColumns.FeatureId, data.FeatureId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureDataModel.DataColumns.FeatureId);
                    }
                    break;

                case MilestoneXFeatureDataModel.DataColumns.MilestoneId:
                    if (data.MilestoneId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MilestoneXFeatureDataModel.DataColumns.MilestoneId, data.MilestoneId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureDataModel.DataColumns.MilestoneId);
                    }
                    break;

                case MilestoneXFeatureDataModel.DataColumns.MilestoneFeatureStateId:
                    if (data.MilestoneFeatureStateId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, MilestoneXFeatureDataModel.DataColumns.MilestoneFeatureStateId, data.MilestoneFeatureStateId);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureDataModel.DataColumns.MilestoneFeatureStateId);

                    }
                    break;

                case MilestoneXFeatureDataModel.DataColumns.Feature:
                    if (!string.IsNullOrEmpty(data.Feature))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MilestoneXFeatureDataModel.DataColumns.Feature, data.Feature);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureDataModel.DataColumns.Feature);
                    }
                    break;

                case MilestoneXFeatureDataModel.DataColumns.Milestone:
                    if (!string.IsNullOrEmpty(data.Milestone))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MilestoneXFeatureDataModel.DataColumns.Milestone, data.Milestone);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureDataModel.DataColumns.Milestone);
                    }
                    break;

                case MilestoneXFeatureDataModel.DataColumns.MilestoneFeatureState:
                    if (!string.IsNullOrEmpty(data.MilestoneFeatureState))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MilestoneXFeatureDataModel.DataColumns.MilestoneFeatureState, data.MilestoneFeatureState);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureDataModel.DataColumns.MilestoneFeatureState);

                    }
                    break;

                case MilestoneXFeatureDataModel.DataColumns.Memo:
                    if (!string.IsNullOrEmpty(data.Memo))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, MilestoneXFeatureDataModel.DataColumns.Memo, data.Memo);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, MilestoneXFeatureDataModel.DataColumns.Memo);

                    }
                    break;
            }
            return returnValue;
        }

        public static DataTable Search(MilestoneXFeatureDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.MilestoneXFeatureSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureDataModel.DataColumns.MilestoneXFeatureId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureDataModel.DataColumns.MilestoneId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureDataModel.DataColumns.MilestoneFeatureStateId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureDataModel.DataColumns.FeatureId);

            var oDT = new DBDataTable("MilestoneXFeature.Search", sql, DataStoreKey);
            return oDT.DBTable;
        }

        #endregion

        #region Save

        private static string Save(MilestoneXFeatureDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.MilestoneXFeatureInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.MilestoneXFeatureUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                default:
                    break;
            }

            sql = sql + ", " + ToSQLParameter(data, MilestoneXFeatureDataModel.DataColumns.MilestoneXFeatureId) +
                        ", " + ToSQLParameter(data, MilestoneXFeatureDataModel.DataColumns.MilestoneId) +
                        ", " + ToSQLParameter(data, MilestoneXFeatureDataModel.DataColumns.MilestoneFeatureStateId) +
                        ", " + ToSQLParameter(data, MilestoneXFeatureDataModel.DataColumns.Memo) +
                        ", " + ToSQLParameter(data, MilestoneXFeatureDataModel.DataColumns.FeatureId);

            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(MilestoneXFeatureDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.MilestoneXFeatureSearch " +
            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureDataModel.DataColumns.MilestoneXFeatureId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureDataModel.DataColumns.FeatureId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureDataModel.DataColumns.MilestoneId) +
                ", " + ToSQLParameter(data, MilestoneXFeatureDataModel.DataColumns.MilestoneFeatureStateId);

            var oDT = new DBDataTable("MilestoneXFeature.DoesExist", sql, DataStoreKey);
            return oDT.DBTable.Rows.Count > 0;
        }

        #endregion
    }
}
