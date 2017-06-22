using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dapper;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;

namespace TaskTimeTracker.Components.Module.TimeTracking
{
    public partial class VacationPlanDataManager : StandardDataManager
    {
		static readonly string DataStoreKey = string.Empty;

        static VacationPlanDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("VacationPlan");
        }

        #region GetList

        public static List<VacationPlanDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(VacationPlanDataModel.Empty, requestProfile, 0);
        }

        #endregion GetList

        //#region GetGroupbyList

        //public static DataTable GetAggregateList(string groupbyField, int startDate1, int startDate2, int auditId)
        //{
        //	var sql = "EXEC dbo.FunctionalityEntityStatusAggregateSearch " +
        //		" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
        //		", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, ApplicationId) +
        //		", @GroupByFieldName = " + groupbyField +
        //		", @Date1 = " + startDate1 +
        //		", @Date2 = " + startDate2;

        //	var oDT = new Framework.Components.DataAccess.DBDataTable("FunctionalityEntityStatus.AggregateList", sql, DataStoreKey);
        //	return oDT.DBTable;
        //}

        //#endregion 

        //#region GetDateRangeList

        //public static DataTable GetDateRangeList(int startDate1, int endDate)
        //{
        //	var sql = "EXEC dbo.FunctionalityEntityStatusArchiveSearchDateRange" +

        //		", @Date = " + startDate1 +
        //		", @Date2 = " + endDate;


        //	var oDT = new Framework.Components.DataAccess.DBDataTable("VacationPlan.DateRangeList", sql, DataStoreKey);
        //	return oDT.DBTable;
        //}

        //#endregion

        #region Search

        public static string ToSQLParameter(VacationPlanDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case VacationPlanDataModel.DataColumns.VacationPlanId:
                    if (data.VacationPlanId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, VacationPlanDataModel.DataColumns.VacationPlanId, data.VacationPlanId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, VacationPlanDataModel.DataColumns.VacationPlanId);
                    }
                    break;
                case VacationPlanDataModel.DataColumns.ApplicationUserId:
                    if (data.ApplicationUserId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, VacationPlanDataModel.DataColumns.ApplicationUserId, data.ApplicationUserId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, VacationPlanDataModel.DataColumns.ApplicationUserId);
                    }
                    break;


                case VacationPlanDataModel.DataColumns.StartDate:
                    if (data.StartDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, VacationPlanDataModel.DataColumns.StartDate, data.StartDate);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, VacationPlanDataModel.DataColumns.StartDate);

                    }
                    break;

                case VacationPlanDataModel.DataColumns.StartDate2:
                    if (data.StartDateR2 != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, VacationPlanDataModel.DataColumns.StartDate2, data.StartDateR2);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, VacationPlanDataModel.DataColumns.StartDate2);
                    }
                    break;

                case VacationPlanDataModel.DataColumns.EndDate:
                    if (data.EndDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, VacationPlanDataModel.DataColumns.EndDate, data.EndDate);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, VacationPlanDataModel.DataColumns.EndDate);

                    }
                    break;

                case VacationPlanDataModel.DataColumns.EndDate2:
                    if (data.EndDateR2 != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, VacationPlanDataModel.DataColumns.EndDate2, data.EndDateR2);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, VacationPlanDataModel.DataColumns.EndDate2);
                    }
                    break;


                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        public static DataTable Search(VacationPlanDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;

        }

        #endregion Search

        #region GetDetails

        public static VacationPlanDataModel GetDetails(VacationPlanDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        public static List<VacationPlanDataModel> GetEntityDetails(VacationPlanDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.VacationPlanSearch ";

            var parameters =
            new
            {
                    AuditId                     = requestProfile.AuditId
                ,   ApplicationId               = requestProfile.ApplicationId
                ,   ApplicationMode             = requestProfile.ApplicationModeId
                ,   ReturnAuditInfo             = returnAuditInfo
                ,   VacationPlanId              = dataQuery.VacationPlanId
                ,   ApplicationUserId           = dataQuery.ApplicationUserId
                ,   Name                        = dataQuery.Name
                ,   Description                 = dataQuery.Description
                ,   StartDate                   = dataQuery.StartDate
                ,   EndDate                     = dataQuery.EndDate
                ,   StartDate2                  = dataQuery.StartDateR2
                ,   EndDate2                    = dataQuery.EndDateR2
                ,   CreatedDate                 = dataQuery.CreatedDate
                ,   ModifiedDate                = dataQuery.ModifiedDate
                ,   CreatedByAuditId            = dataQuery.CreatedByAuditId
                ,   ModifiedByAuditId           = dataQuery.ModifiedByAuditId
            };

            List<VacationPlanDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<VacationPlanDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        //public static List<VacationPlanDataModel> GetEntityDetails(VacationPlanDataModel data, int auditId)
        //{
        //    var sql = "EXEC dbo.VacationPlanSearch " +
        //              " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
        //              ", " + ToSQLParameter(data, VacationPlanDataModel.DataColumns.VacationPlanId);

        //    var result = new List<VacationPlanDataModel>();

        //    using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
        //    {
        //        var dbReader = reader.DBReader;

        //        while (dbReader.Read())
        //        {
        //            var dataItem = new VacationPlanDataModel();

        //            dataItem.VacationPlanId		= (int?)dbReader[VacationPlanDataModel.DataColumns.VacationPlanId];
        //            dataItem.ApplicationUser    = (string)dbReader[VacationPlanDataModel.DataColumns.ApplicationUser];     
        //            dataItem.ApplicationUserId  = (int?)dbReader[VacationPlanDataModel.DataColumns.ApplicationUserId];                    
        //            dataItem.StartDate			= (DateTime)dbReader[VacationPlanDataModel.DataColumns.StartDate];
        //            dataItem.EndDate			= (DateTime)dbReader[VacationPlanDataModel.DataColumns.EndDate];
        //            dataItem.CreatedDate		= (DateTime)dbReader[VacationPlanDataModel.DataColumns.CreatedDate];
        //            dataItem.ModifiedDate		= (DateTime)dbReader[VacationPlanDataModel.DataColumns.ModifiedDate];
        //            dataItem.CreatedByAuditId	= (int?)dbReader[VacationPlanDataModel.DataColumns.CreatedByAuditId];
        //            dataItem.ModifiedByAuditId  = (int?)dbReader[VacationPlanDataModel.DataColumns.ModifiedByAuditId];

        //            SetStandardInfo(dataItem, dbReader);

        //            SetBaseInfo(dataItem, dbReader);

        //            result.Add(dataItem);
        //        }
        //    }

        //    return result;
        //}

        #endregion GetDetails

		#region CreateOrUpdate

		private static string CreateOrUpdate(VacationPlanDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            switch (action)
            {
                case "Create":
                    sql += "dbo.VacationPlanInsert  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.VacationPlanUpdate  " + "\r\n" +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                default:
                    break;
            }

            sql = sql + ", " + ToSQLParameter(data, VacationPlanDataModel.DataColumns.VacationPlanId) +
                ", " + ToSQLParameter(data, VacationPlanDataModel.DataColumns.ApplicationUserId) +
                ", " + ToSQLParameter(data, VacationPlanDataModel.DataColumns.StartDate) +
                ", " + ToSQLParameter(data, VacationPlanDataModel.DataColumns.EndDate) +
                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Name) +
                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.Description) +
                ", " + ToSQLParameter(data, StandardDataModel.StandardDataColumns.SortOrder);

            return sql;

        }
        #endregion CreateOrUpdate

        #region Create

        public static int Create(VacationPlanDataModel data, RequestProfile requestProfile)
        {
			var sql = CreateOrUpdate(data, requestProfile, "Create");

            var id = DBDML.RunScalarSQL("VacationPlan.Insert", sql, DataStoreKey);
            return Convert.ToInt32(id);
        }

        #endregion Create


        #region Update

        public static void Update(VacationPlanDataModel data, RequestProfile requestProfile)
        {
            var sql = CreateOrUpdate(data, requestProfile, "Update");
            DBDML.RunSQL("VacationPlan.Update", sql, DataStoreKey);
        }

        #endregion Update

        #region Delete

        public static void Delete(VacationPlanDataModel dataQuery, RequestProfile requestprofile)
        {
            const string sql = @"dbo.VacationPlanDelete ";

            var parameters =
            new
            {
                AuditId = requestprofile.AuditId
                ,
                VacationPlanId = dataQuery.VacationPlanId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {


                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


            }
        }

        #endregion Delete

        #region DoesExist

        public static bool DoesExist(VacationPlanDataModel data, RequestProfile requestProfile)
        {
            var doesExistRequest = new VacationPlanDataModel();
            doesExistRequest.Name = data.Name;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
        }


        #endregion DoesExist

        #region GetChildren

        private static DataSet GetChildren(VacationPlanDataModel data, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.VacationPlanChildrenGet " +
                            " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                            ", " + ToSQLParameter(data, VacationPlanDataModel.DataColumns.VacationPlanId);

            var oDT = new DBDataSet("Get Children", sql, DataStoreKey);
            return oDT.DBDataset;
        }

        #endregion

        #region IsDeletable

        public static bool IsDeletable(VacationPlanDataModel data, RequestProfile requestProfile)
        {
            var isDeletable = true;
            var ds = GetChildren(data, requestProfile);
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.Rows.Count > 0)
                    {
                        isDeletable = false;
                        break;
                    }
                }
            }
            return isDeletable;
        }

        #endregion

        #region Renumber
        public static void Renumber(int seed, int increment, RequestProfile requestProfile)
        {
            var sql = "EXEC dbo.VacationPlanRenumber " +
                      " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                      ",@Seed = " + seed +
                      ",@Increment = " + increment;

            DBDML.RunSQL("VacationPlan.Renumber", sql, DataStoreKey);
        }
        #endregion Renumber

    }
}
