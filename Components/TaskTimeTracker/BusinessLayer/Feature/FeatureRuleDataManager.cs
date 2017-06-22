using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.TaskTimeTracker.Feature;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.BusinessLayer.Feature
{
    public partial class FeatureRuleDataManager : StandardDataManager
    {
        static readonly string DataStoreKey = "";

        static FeatureRuleDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("FeatureRule");
        }

        #region GetList

        public static List<FeatureRuleDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FeatureRuleDataModel.Empty, requestProfile, 1);
        }

        public static List<FeatureRuleDataModel> GetFeatureRuleList(RequestProfile requestProfile)
        {
            return GetEntityDetails(FeatureRuleDataModel.Empty, requestProfile, 0);
        } 

        #endregion GetList

        #region Search

        public static string ToSQLParameter(FeatureRuleDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case FeatureRuleDataModel.DataColumns.FeatureRuleId:
                    if (data.FeatureRuleId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FeatureRuleDataModel.DataColumns.FeatureRuleId, data.FeatureRuleId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FeatureRuleDataModel.DataColumns.FeatureRuleId);
                    }
                    break;

                case FeatureRuleDataModel.DataColumns.FeatureRuleCategoryId:
                    if (data.FeatureRuleCategoryId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, FeatureRuleDataModel.DataColumns.FeatureRuleCategoryId, data.FeatureRuleCategoryId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, FeatureRuleDataModel.DataColumns.FeatureRuleCategoryId);
                    }
                    break;

                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        public static DataTable Search(FeatureRuleDataModel data, RequestProfile requestProfile)
        {


            // formulate SQL  
            var sql = "EXEC dbo.FeatureRuleSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +                
                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
                ", " + ToSQLParameter(data, FeatureRuleDataModel.DataColumns.FeatureRuleId) +
                ", " + ToSQLParameter(data, FeatureRuleDataModel.DataColumns.FeatureRuleCategoryId) +
                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);


            var oDT = new DBDataTable("Get List", sql, DataStoreKey);

            return oDT.DBTable;

        }

        #endregion Search

        #region GetDetails

		public static FeatureRuleDataModel GetDetails(FeatureRuleDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile);

            return list.FirstOrDefault();
        }

        public static List<FeatureRuleDataModel> GetEntityDetails(FeatureRuleDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.FeatureRuleSearch ";

            var parameters =
            new
            {
                    AuditId              = requestProfile.AuditId
                ,   ApplicationId        = requestProfile.ApplicationId               
                ,   FeatureRuleId        = dataQuery.FeatureRuleId
                ,   Name                 = dataQuery.Name
            };

            List<FeatureRuleDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {

                result = dataAccess.Connection.Query<FeatureRuleDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        //public static List<FeatureRuleDataModel> GetEntityDetails(FeatureRuleDataModel data, int auditId)
        //{
        //    var sql = "EXEC dbo.FeatureRuleSearch " +
        //              " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
        //               ", " + ToSQLParameter(data, FeatureRuleDataModel.DataColumns.FeatureRuleId);		

        //    var result = new List<FeatureRuleDataModel>();

        //    using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
        //    {
        //        var dbReader = reader.DBReader;

        //        while (dbReader.Read())
        //        {
        //            var dataItem = new FeatureRuleDataModel();

        //            dataItem.FeatureRuleId = (int)dbReader[FeatureRuleDataModel.DataColumns.FeatureRuleId];
        //            dataItem.FeatureRuleCategoryId = (int)dbReader[FeatureRuleDataModel.DataColumns.FeatureRuleCategoryId];

        //            SetStandardInfo(dataItem, dbReader);

        //            SetBaseInfo(dataItem, dbReader);

        //            result.Add(dataItem);
        //        }
        //    }

        //    return result;
        //}

        #endregion GetDetails

        #region CreateOrUpdate
        private static string CreateOrUpdate(FeatureRuleDataModel data, RequestProfile requestProfile, string action)
        {
            var traceId = TraceDataManager.GetNextTraceId(requestProfile);
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.FeatureRuleInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +                       
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.FeatureRuleUpdate  " + "\r\n" +
                           " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);
                           
                    break;

                default:
                    break;
            }

            sql = sql + ", " + ToSQLParameter(data, FeatureRuleDataModel.DataColumns.FeatureRuleId) +
                        ", " + ToSQLParameter(data, FeatureRuleDataModel.DataColumns.FeatureRuleCategoryId) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
                        ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

            return sql;
        }

        #endregion CreateOrUpdate

        #region Create

        public static int Create(FeatureRuleDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Create");

            var featureRuleId = DBDML.RunScalarSQL("FeatureRule.Insert", sql, DataStoreKey);
            return Convert.ToInt32(featureRuleId);
        }
        #endregion Create

        #region Update

        public static void Update(FeatureRuleDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("FeatureRule.Update", sql, DataStoreKey);
        }


        #endregion Update

        #region Renumber

        public static void Renumber(int seed, int increment, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.FeatureRuleRenumber " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                      ",@Seed = " + seed +
                      ",@Increment = " + increment;

            DBDML.RunSQL("FeatureRule.Renumber", sql, DataStoreKey);
        }

        #endregion Renumber

        #region Delete

        public static void Delete(FeatureRuleDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.FeatureRuleDelete ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                FeatureRuleId = dataQuery.FeatureRuleId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion Delete

        #region DoesExist

        public static DataTable DoesExist(FeatureRuleDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.FeatureRuleSearch " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                            ", " + ToSQLParameter(data, FeatureRuleDataModel.DataColumns.FeatureRuleId) +
                            ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name);

            var oDT = new DBDataTable("FeatureRule.DoesExist", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion DoesExist

        #region GetChildren

        private static DataSet GetChildren(FeatureRuleDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.FeatureRuleChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, FeatureRuleDataModel.DataColumns.FeatureRuleId);

            var oDT = new DBDataSet("FeatureRule.GetChildren", sql, DataStoreKey);

            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(FeatureRuleDataModel data, RequestProfile requestProfile)
        {
            var isDeletable = true;

            var ds = GetChildren(data, requestProfile);

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables.Cast<DataTable>().Any(dt => dt.Rows.Count > 0))
                {
                    isDeletable = false;
                }
            }

            return isDeletable;
        }

        #endregion


    }
}
