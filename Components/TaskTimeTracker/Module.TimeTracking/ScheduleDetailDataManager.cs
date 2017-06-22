using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.TimeTracking;
using Dapper;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Configuration;
using TaskTimeTracker.Components.Module.TimeTracking.DomainModel;
using System.Collections;
using System.Globalization;

namespace TaskTimeTracker.Components.Module.TimeTracking
{
    public partial class ScheduleDetailDataManager : BaseDataManager
    {
        static readonly string DataStoreKey = "";

        static ScheduleDetailDataManager()
        {
            DataStoreKey = SetupConfiguration.GetDataStoreKey("ScheduleDetail");
        }

        #region GetList

        public static List<ScheduleDetailDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ScheduleDetailDataModel.Empty, requestProfile, 0);
        }

        #endregion

		#region GetScheduleList

		public static List<ScheduleDetailDataModel> GetScheduleDetailList(RequestProfile requestProfile)
		{
			return GetEntityDetails(ScheduleDetailDataModel.Empty, requestProfile, 0);
		}

		#endregion

        #region Get By Schedule

        public static DataTable GetBySchedule(int scheduleId, RequestProfile requestProfile)
        {
            var sql = "EXEC ScheduleDetailSearch @ScheduleId       =" + scheduleId + ", " +
                         " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId);

            var oDT = new DBDataTable("GetDetails", sql, DataStoreKey);

            return oDT.DBTable;
        }

        #endregion

        #region ToSQLParameter

        public static string ToSQLParameter(ScheduleDetailDataModel data, string dataColumnName)
        {
            var returnValue = "NULL";

            switch (dataColumnName)
            {
                case ScheduleDetailDataModel.DataColumns.ScheduleDetailId:
                    if (data.ScheduleDetailId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleDetailDataModel.DataColumns.ScheduleDetailId, data.ScheduleDetailId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDetailDataModel.DataColumns.ScheduleDetailId);
                    }
                    break;

                case ScheduleDetailDataModel.DataColumns.ScheduleDetailActivityCategoryId:
                    if (data.ScheduleDetailActivityCategoryId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleDetailDataModel.DataColumns.ScheduleDetailActivityCategoryId, data.ScheduleDetailActivityCategoryId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDetailDataModel.DataColumns.ScheduleDetailActivityCategoryId);
                    }
                    break;

                case ScheduleDetailDataModel.DataColumns.ScheduleId:
                    if (data.ScheduleId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleDetailDataModel.DataColumns.ScheduleId, data.ScheduleId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDetailDataModel.DataColumns.ScheduleId);
                    }
                    break;

                case ScheduleDetailDataModel.DataColumns.InTime:
                    if (data.InTime != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDetailDataModel.DataColumns.InTime, data.InTime);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDetailDataModel.DataColumns.InTime);
                    }
                    break;

                case ScheduleDetailDataModel.DataColumns.OutTime:
                    if (data.OutTime != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDetailDataModel.DataColumns.OutTime, data.OutTime);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDetailDataModel.DataColumns.OutTime);
                    }
                    break;

                case ScheduleDetailDataModel.DataColumns.Message:
                    if (!string.IsNullOrEmpty(data.Message))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDetailDataModel.DataColumns.Message, data.Message);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDetailDataModel.DataColumns.Message);
                    }
                    break;

                case ScheduleDetailDataModel.DataColumns.WorkTicket:
                    if (!string.IsNullOrEmpty(data.WorkTicket))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDetailDataModel.DataColumns.WorkTicket, data.WorkTicket);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDetailDataModel.DataColumns.WorkTicket);
                    }
                    break;

                case ScheduleDetailDataModel.DataColumns.ScheduleDetailActivityCategory:
                    if (!string.IsNullOrEmpty(data.ScheduleDetailActivityCategory))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDetailDataModel.DataColumns.ScheduleDetailActivityCategory, data.ScheduleDetailActivityCategory);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDetailDataModel.DataColumns.ScheduleDetailActivityCategory);
                    }
                    break;

                case ScheduleDetailDataModel.DataColumns.PersonId:
                    if (data.PersonId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleDetailDataModel.DataColumns.PersonId, data.PersonId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDetailDataModel.DataColumns.PersonId);
                    }
                    break;

                case ScheduleDetailDataModel.DataColumns.DateDiffHrs:
                    if (data.DateDiffHrs != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleDetailDataModel.DataColumns.DateDiffHrs, data.DateDiffHrs);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDetailDataModel.DataColumns.DateDiffHrs);
                    }
                    break;

                case ScheduleDetailDataModel.DataColumns.FromSearchDate:
                    if (data.FromSearchDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDetailDataModel.DataColumns.FromSearchDate, data.FromSearchDate);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDetailDataModel.DataColumns.FromSearchDate);
                    }
                    break;

                case ScheduleDetailDataModel.DataColumns.ToSearchDate:
                    if (data.ToSearchDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDetailDataModel.DataColumns.ToSearchDate, data.ToSearchDate);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDetailDataModel.DataColumns.ToSearchDate);
                    }
                    break;

                case ScheduleDetailDataModel.DataColumns.Person:
                    if (data.Person != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDetailDataModel.DataColumns.Person, data.Person);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDetailDataModel.DataColumns.Person);
                    }
                    break;

                case ScheduleDetailDataModel.DataColumns.CreatedDate:
                    if (data.CreatedDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDetailDataModel.DataColumns.CreatedDate, data.CreatedDate);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDetailDataModel.DataColumns.CreatedDate);

                    }
                    break;

                case ScheduleDetailDataModel.DataColumns.ModifiedDate:
                    if (data.ModifiedDate != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDetailDataModel.DataColumns.ModifiedDate, data.ModifiedDate);

                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDetailDataModel.DataColumns.ModifiedDate);

                    }
                    break;

                case ScheduleDetailDataModel.DataColumns.CreatedByAuditId:
                    if (data.CreatedByAuditId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleDetailDataModel.DataColumns.CreatedByAuditId, data.CreatedByAuditId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDetailDataModel.DataColumns.CreatedByAuditId);
                    }
                    break;


                case ScheduleDetailDataModel.DataColumns.ModifiedByAuditId:
                    if (data.ModifiedByAuditId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleDetailDataModel.DataColumns.ModifiedByAuditId, data.ModifiedByAuditId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDetailDataModel.DataColumns.ModifiedByAuditId);
                    }
                    break;
                default:
                    returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
                    break;

            }

            return returnValue;
        }

        #endregion

        #region GetDetails

        public static ScheduleDetailDataModel GetDetails(ScheduleDetailDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
        }

        public static List<ScheduleDetailDataModel> GetEntityDetails(ScheduleDetailDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
        {
            const string sql = @"dbo.ScheduleDetailSearch ";

            var parameters =
            new
            {
                    AuditId = requestProfile.AuditId
                ,   ApplicationId = requestProfile.ApplicationId
                ,   ScheduleDetailId = dataQuery.ScheduleDetailId
                ,   ScheduleId = dataQuery.ScheduleId
                ,   PersonId = dataQuery.PersonId
                ,   FromSearchDate = dataQuery.FromSearchDate
                ,   ToSearchDate = dataQuery.ToSearchDate
                ,   Message = dataQuery.Message
                ,   ScheduleDetailActivityCategoryId = dataQuery.ScheduleDetailActivityCategoryId
                ,   WorkTicket = dataQuery.WorkTicket
                ,   CreatedDate = dataQuery.CreatedDate
                ,   ModifiedDate = dataQuery.ModifiedDate
                ,   CreatedByAuditId = dataQuery.CreatedByAuditId
                ,   ModifiedByAuditId = dataQuery.ModifiedByAuditId
                ,
                ReturnAuditInfo = returnAuditInfo
            };

            List<ScheduleDetailDataModel> result;

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                result = dataAccess.Connection.Query<ScheduleDetailDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
            }

            return result;
        }

        #endregion

        #region Create

        public static int Create(ScheduleDetailDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Create");
            var id = DBDML.RunScalarSQL("ScheduleDetail.Insert", sql, DataStoreKey);
            return Convert.ToInt32(id);
        }

        #endregion

        #region Update

        public static void Update(ScheduleDetailDataModel data, RequestProfile requestProfile)
        {
            var sql = Save(data, requestProfile, "Update");
            DBDML.RunSQL("ScheduleDetail.Update", sql, DataStoreKey);
        }

        #endregion

        #region Delete

        public static void Delete(ScheduleDetailDataModel dataQuery, RequestProfile requestProfile)
        {
            const string sql = @"dbo.ScheduleDetailDelete ";

            var parameters =
            new
            {
                AuditId = requestProfile.AuditId
                ,
                ScheduleDetailId = dataQuery.ScheduleDetailId
            };

            using (var dataAccess = new DataAccessBase(DataStoreKey))
            {
                dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion

        #region Search

        public static DataTable Search(ScheduleDetailDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);

            var table = list.ToDataTable();

            return table;
        }



        public static List<string> GetJIRAS(ScheduleDetailDataModel data, RequestProfile requestProfile)
        {
            var list = GetEntityDetails(data, requestProfile, 0);
            var jiras = new List<string>();
            for (var i = 0; i < list.Count(); i++)
            {
                var item = list[i];
                if (item.WorkTicket == null)
                    continue;
                else
                    jiras.Add(item.WorkTicket.ToString());
            }
            return jiras;
        }

        public static DataSet SearchView(ScheduleDetailDataModel data, RequestProfile requestProfile)
        {
            // formulate SQL
            var sql = "EXEC dbo.ScheduleDetailViewSearch " +
                " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
                ", " + ToSQLParameter(data, ScheduleDetailDataModel.DataColumns.ScheduleId) +
                ", " + ToSQLParameter(data, ScheduleDetailDataModel.DataColumns.ScheduleDetailId) +
                ", " + ToSQLParameter(data, ScheduleDetailDataModel.DataColumns.PersonId) +
				", " + ToSQLParameter(data, ScheduleDetailDataModel.DataColumns.ScheduleDetailActivityCategory) +
                ", " + ToSQLParameter(data, ScheduleDetailDataModel.DataColumns.FromSearchDate) +
                ", " + ToSQLParameter(data, ScheduleDetailDataModel.DataColumns.ToSearchDate);

            var oDs = new DBDataSet("Schedule.Search", sql, DataStoreKey);
            return oDs.DBDataset;
        }

        #endregion

        #region Save

        private static void FormatData(ScheduleDetailDataModel data)
        {
            // JIRA #3831
            data.Message = Regex.Replace(data.Message, "jira", "JIRA", RegexOptions.IgnoreCase);
			data.WorkTicket = data.WorkTicket.ToUpper();
        }

        private static string Save(ScheduleDetailDataModel data, RequestProfile requestProfile, string action)
        {
            var sql = "EXEC ";

            FormatData(data);

            switch (action)
            {
                case "Create":
                    sql += "dbo.ScheduleDetailInsert  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
                    break;

                case "Update":
                    sql += "dbo.ScheduleDetailUpdate  " +
                        " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
                        ", " + ToSQLParameter(data, ScheduleDetailDataModel.DataColumns.ScheduleDetailId);
                    break;

                default:
                    break;

            }

            sql = sql + ", " + ToSQLParameter(data, ScheduleDetailDataModel.DataColumns.ScheduleId) +
                        ", " + ToSQLParameter(data, ScheduleDetailDataModel.DataColumns.Message) +
                        ", " + ToSQLParameter(data, ScheduleDetailDataModel.DataColumns.ScheduleDetailActivityCategoryId) +
                        ", " + ToSQLParameter(data, ScheduleDetailDataModel.DataColumns.WorkTicket) +
                        ", " + ToSQLParameter(data, ScheduleDetailDataModel.DataColumns.InTime) +
                        ", " + ToSQLParameter(data, ScheduleDetailDataModel.DataColumns.OutTime);

            return sql;
        }

        #endregion

        #region DoesExist

        public static bool DoesExist(ScheduleDetailDataModel data, RequestProfile requestProfile)
        {
            var doesExistRequest = new ScheduleDetailDataModel();
            doesExistRequest.ScheduleId = data.ScheduleId;
            doesExistRequest.InTime = data.InTime;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
        }

        #endregion

        #region SendEmail

        public static DataTable GetQuestionData(int? scheduleId, RequestProfile requestProfile)
        {
            var data = new ScheduleQuestionDataModel();
            data.ScheduleId = scheduleId;
            var scheduleQuestiondt = ScheduleQuestionDataManager.Search(data, requestProfile);
            return scheduleQuestiondt;
        }
        #endregion

        #region GetStatisticData

        public static decimal GetTotalTimeSpent(DataTable dt, int scheduleTimeSpentConstant)
        {
            var series = new decimal[dt.Rows.Count];
            var i = 0;

            foreach (DataRow item in dt.Rows)
            {
                var timeSpent = item[ScheduleDetailDataModel.DataColumns.DateDiffHrs].ToString();

                var timeSpentValue = 0m;

                if (!Decimal.TryParse(timeSpent, out timeSpentValue))
                {
                    timeSpentValue = scheduleTimeSpentConstant;
                }

                series[i++] = timeSpentValue;
            }

            var timeSpentForGroup = series.Sum();
            return timeSpentForGroup;
        }

        public static Statistic GetStatisticData(DataTable scheduleData, int scheduleTimeSpentConstant, string scheduleStatisticUnknown)
        {
            var dataItem = new Statistic();

            dataItem.Total = GetTotalTimeSpent(scheduleData, scheduleTimeSpentConstant);

            //dataItem.Total = totalTimeSpent1 + totalTimeSpent2;

            //dataItem.Count = totalCount;		
            var totalCount = scheduleData.Rows.Count;
            dataItem.Count = totalCount;


            var dt = new DataTable();
            dt.Columns.Add(ScheduleDetailDataModel.DataColumns.DateDiffHrs);
            dt.AcceptChanges();

            var rowT = dt.NewRow();

            var list = scheduleData.AsEnumerable()
                      .Where(x => x[ScheduleDetailDataModel.DataColumns.DateDiffHrs].ToString().ToLower() != scheduleStatisticUnknown);

            var list1 = scheduleData.AsEnumerable()
                        .Where(x => x[ScheduleDetailDataModel.DataColumns.DateDiffHrs].ToString().ToLower() == scheduleStatisticUnknown);

            var rows = from row in list1.AsEnumerable()
                       select row;
            // takes care of the logic of TimeSpent column with UnKnown value to calculate average and median
            foreach (var row in rows)
            {
                if (Convert.ToInt16(row[ScheduleDetailDataModel.DataColumns.DateDiffHrs]) != 0)
                {
                    if (row[ScheduleDetailDataModel.DataColumns.DateDiffHrs].ToString().ToLower() == scheduleStatisticUnknown)
                    {
                        rowT = dt.NewRow();
                        rowT[ScheduleDetailDataModel.DataColumns.DateDiffHrs] = scheduleTimeSpentConstant;
                        dt.Rows.Add(rowT);
                    }
                }
            }

            var list2 = list.Concat(dt.AsEnumerable());

            var dataRows = list2 as DataRow[] ?? list2.ToArray();
            var rowItem = from row in dataRows.AsEnumerable() select row;

            if (rowItem.Any())
            {
                //var excludeZero = 
                //	dataRows.AsEnumerable()
                //	.Where(c => c.Field<Decimal>(ScheduleDetailDataModel.DataColumns.DateDiffHrs) != new Decimal(0.00));

                ////calculates the average value				
                //dataItem.Average = excludeZero
                //					  .Where(x => x[ScheduleDetailDataModel.DataColumns.DateDiffHrs].ToString().ToLower() != scheduleStatisticUnknown)
                //					  .Select(x => Convert.ToDecimal(x[ScheduleDetailDataModel.DataColumns.DateDiffHrs])).Average();
                dataItem.Average = dataRows.AsEnumerable()
                                      .Where(x => x[ScheduleDetailDataModel.DataColumns.DateDiffHrs].ToString().ToLower() != scheduleStatisticUnknown)
                                      .Select(x => Convert.ToDecimal(x[ScheduleDetailDataModel.DataColumns.DateDiffHrs])).Average();

                //calculates the max and min values 
                dataItem.Max = dataRows.Max(x => Convert.ToDecimal(x[ScheduleDetailDataModel.DataColumns.DateDiffHrs]));
                dataItem.Min = dataRows.Min(x => Convert.ToDecimal(x[ScheduleDetailDataModel.DataColumns.DateDiffHrs]));

                // gets the ordered list to find the median
                var orderedList = dataRows.OrderBy(p => Convert.ToDecimal(p[ScheduleDetailDataModel.DataColumns.DateDiffHrs]));

                // calculates median for even number list
                if ((totalCount % 2) == 0)
                {
                    dataItem.Median = Convert.ToDecimal(orderedList.ElementAt(totalCount / 2)[ScheduleDetailDataModel.DataColumns.DateDiffHrs]) + Convert.ToDecimal(orderedList.ElementAt((totalCount - 1) / 2)[ScheduleDetailDataModel.DataColumns.DateDiffHrs]);
                    dataItem.Median /= 2;
                }
                else
                {
                    // calculating median for odd number list
                    if (totalCount == 1)
                    {
                        dataItem.Median = Convert.ToDecimal(orderedList.ElementAt(totalCount - 1)[ScheduleDetailDataModel.DataColumns.DateDiffHrs]);
                    }
                    else
                    {
                        dataItem.Median = Convert.ToDecimal(orderedList.ElementAt(totalCount / 2)[ScheduleDetailDataModel.DataColumns.DateDiffHrs]);
                    }
                }
            }

            return dataItem;
        }

        public static Dictionary<string, decimal> GetStatisticDataSummary(DataTable scheduleData, int scheduleTimeSpentConstant, string scheduleStatisticUnknown)
        {
            var lstResult = new Dictionary<string, decimal>();

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(ScheduleDetailDataModel.DataColumns.DateDiffHrs);
            DataRow rowT = dt.NewRow();
            decimal average = 0;

            //var totalTimeSpent1 = scheduleData
            //					.Where(z => (string.IsNullOrEmpty(z[ScheduleDetailDataModel.DataColumns.DateDiffHrs].ToString()) || z[ScheduleDetailDataModel.DataColumns.DateDiffHrs].ToString().ToLower() == scheduleStatisticUnknown)).Count() * scheduleTimeSpentConstant;

            //// totalTimeSpent2 calculates the time of TimeSpent column <> 'UnKnown'
            //var totalTimeSpent2 = scheduleData
            //						.Where(x => x[ScheduleDetailDataModel.DataColumns.DateDiffHrs].ToString().ToLower() != scheduleStatisticUnknown)
            //						.Sum(x => Convert.ToDecimal(x[ScheduleDetailDataModel.DataColumns.DateDiffHrs]));

            var totalTimeSpent = GetTotalTimeSpent(scheduleData, scheduleTimeSpentConstant);
            //lstResult.Add("TotalHoursWorked", totalTimeSpent1 + totalTimeSpent2);
            lstResult.Add("TotalTimeSpent", totalTimeSpent);

            //finding total number o records
            var count = scheduleData.AsEnumerable()
                .Select(x => x[ScheduleDetailDataModel.DataColumns.DateDiffHrs]).Count();

            lstResult.Add("Count", count);

            if (count != 0)
                average = totalTimeSpent / count;
            //average = (totalTimeSpent1 + totalTimeSpent2) / count;

            var list = scheduleData.AsEnumerable()
                .Where(x => x[ScheduleDetailDataModel.DataColumns.DateDiffHrs].ToString().ToLower() != scheduleStatisticUnknown);

            var list1 = scheduleData.AsEnumerable()
                .Where(x => x[ScheduleDetailDataModel.DataColumns.DateDiffHrs].ToString().ToLower() == scheduleStatisticUnknown);


            var rows = from row in list1.AsEnumerable()
                       select row;

            foreach (DataRow row in rows)
            {
                if (row[ScheduleDetailDataModel.DataColumns.DateDiffHrs].ToString().ToLower() == scheduleStatisticUnknown)
                {
                    rowT = dt.NewRow();
                    rowT[ScheduleDetailDataModel.DataColumns.DateDiffHrs] = scheduleTimeSpentConstant;
                    dt.Rows.Add(rowT);
                }
            }

            var listMedian = list.Concat(dt.AsEnumerable());

            //var orderedList = listMedian.OrderBy(p => (Decimal)(p[ScheduleDetailDataModel.DataColumns.DateDiffHrs]));
            var orderedList = listMedian.OrderBy(p => p[ScheduleDetailDataModel.DataColumns.DateDiffHrs]);
            decimal median = 0;

            if (count != 0)
            {
                // calculating median for even number list
                if ((count % 2) == 0)
                {
                    //median = (Decimal)(orderedList.ElementAt(count / 2)[ScheduleDetailDataModel.DataColumns.DateDiffHrs]) + (Decimal)(orderedList.ElementAt((count - 1) / 2)[ScheduleDetailDataModel.DataColumns.DateDiffHrs]);					
                    median = Convert.ToDecimal(orderedList.ElementAt(count / 2)[ScheduleDetailDataModel.DataColumns.DateDiffHrs]);
                    median /= 2;
                }
                else
                {
                    // calculating median for odd number list
                    if (count == 1)
                    {
                        median = (Decimal)(orderedList.ElementAt(count - 1)[ScheduleDetailDataModel.DataColumns.DateDiffHrs]);
                    }
                    else
                    {
                        median = Convert.ToDecimal(orderedList.ElementAt(count / 2)[ScheduleDetailDataModel.DataColumns.DateDiffHrs]);
                    }
                }
            }

            lstResult.Add("Average", average);
            lstResult.Add("Median", median);

            return lstResult;
        }

        #endregion

		#region GetHierarchyDetails

		public static DataTable GetHierarchyList(string sourceColumn, string workTicket)
		{
			var sql = "EXEC dbo.WorkTicketHierarchySearch " +
						" @SourceColumn = '" + sourceColumn + "'" +
						", @WorkTicket = '" + workTicket + "'";

			var oDT = new DBDataTable("Get List", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion 

		#region Update Hierarchy

		public static void CreateWorkTicketHierarchy(string parentWorkTicket, string workTicket, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.WorkTicketHierarchyInsert " +
									" @ParentWorkTicket = '" + parentWorkTicket + "'" +
									", @WorkTicket = '" + workTicket + "'"+
			                        ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);


			DBDML.RunSQL("ScheduleDetailHierarchy.Insert", sql, DataStoreKey);
		}

		#endregion

		#region Delete Hierarchy

		public static void DeleteWorkTicketHierarchy(string parentWorkTicket, string workTicket)
		{
			var sql = "EXEC dbo.WorkTicketHierarchyDelete " +
									" @ParentWorkTicket = '" + parentWorkTicket + "'" +
									", @WorkTicket = '" + workTicket + "'";

			DBDML.RunSQL("ScheduleDetailHierarchy.Insert", sql, DataStoreKey);
		}

		#endregion



		#region GetHierarchySourceDetails

		public static DataTable GetHierarchySourceList(string sourceColumn)
		{
			var sql = "EXEC dbo.WorkTicketSourceSearch " +
						" @SourceColumn = '" + sourceColumn + "'";

			var oDT = new DBDataTable("Get List", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion 

		#region GetScheduleDetailDistinctList

		public static DataTable GetScheduleDetailDistinctList()
		{
			var sql = "EXEC dbo.ScheduleDetailDistinctSearch ";

			var oDT = new DBDataTable("Get List", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion 



        public static List<WorkCategoryRecord> GetWorkCategoryData(ScheduleDetailDataModel data, RequestProfile requestProfile)
        {
            var lstResult = new List<WorkCategoryRecord>();
            var columnTotals = new List<int>();

            var resultList = GetEntityDetails(data, requestProfile);

            // perform Grouping
            var resultGrouping = from l in resultList
                                 group l by new
                                 {
                                     Week = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(l.WorkDate.Value, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday), //+ "(" + l.WorkDate.Value.Year + ")",
                                     l.ScheduleDetailActivityCategory
                                 }
                                     into gcs
                                     select new
                                     {
                                         gcs.Key.Week,
                                         gcs.Key.ScheduleDetailActivityCategory,
                                         Count = gcs.Sum(x => ((TimeSpan)(x.OutTime - x.InTime)).Hours)
                                     };

            var dt = resultGrouping.ToList().ToDataTable();

            //currently assuming both will not be null
            var startingWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(data.FromSearchDate.Value, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
            var endingWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(data.ToSearchDate.Value, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

            var lstWeeks = new List<string>();

            var counter = startingWeek;
            while (counter != endingWeek)
            {
                lstWeeks.Add(counter.ToString());
                if (startingWeek > endingWeek && counter == 52)
                {
                    counter = 0;
                }
                counter++;
            }
            lstWeeks.Add(endingWeek.ToString());

            var distCategories = (from row in dt.AsEnumerable()
                                  select row["ScheduleDetailActivityCategory"].ToString().Trim())
                                    .Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();



            // add header row
            var objWorkCategoryRecordHeader = new WorkCategoryRecord();

            objWorkCategoryRecordHeader.Name = "Work Category";
            if (lstWeeks.Count > 0)
            {
                objWorkCategoryRecordHeader.Info = new SpecialData[lstWeeks.Count];
                for (int i = 0; i < lstWeeks.Count; i++)
                {
                    var week = lstWeeks[i];
                    var specialData = new SpecialData();
                    specialData.WeekNo = week;

                    objWorkCategoryRecordHeader.Info[i] = specialData;

                    columnTotals.Add(0);
                }
            }
            lstResult.Add(objWorkCategoryRecordHeader);

            for (int j = 0; j < distCategories.Count; j++)
            {
                var category = distCategories[j];
                var objWorkCategoryRecord = new WorkCategoryRecord();

                var rowTotal = 0;

                objWorkCategoryRecord.Name = category;

                if (lstWeeks.Count > 0)
                {

                    objWorkCategoryRecord.Info = new SpecialData[lstWeeks.Count];

                    for (int i = 0; i < lstWeeks.Count; i++)
                    {
                        var week = lstWeeks[i];

                        var specialData = new SpecialData();
                        specialData.WeekNo = week;

                        var weekCount = 0;

                        try
                        {
                            weekCount = (from row in dt.AsEnumerable()
                                         where row["ScheduleDetailActivityCategory"].ToString() == category && row["Week"].ToString() == week
                                         select Convert.ToInt32(row["Count"])).FirstOrDefault();
                        }
                        catch { }

                        specialData.Value = weekCount.ToString("0.00");
                        rowTotal += weekCount;
                        columnTotals[i] += weekCount;

                        objWorkCategoryRecord.Info[i] = specialData;
                    }
                }

                objWorkCategoryRecord.Total = rowTotal.ToString("0.00");

                lstResult.Add(objWorkCategoryRecord);

            }

            // count grand total
            var grandTotal = columnTotals.Sum();

            // add total row
            var objWorkCategoryRecordTotal = new WorkCategoryRecord();
            objWorkCategoryRecordTotal.Name = "Total";

            // add Percentage row
            var objWorkCategoryRecordPercentage = new WorkCategoryRecord();
            objWorkCategoryRecordPercentage.Name = "Percentage";

            if (lstWeeks.Count > 0)
            {

                objWorkCategoryRecordTotal.Info = new SpecialData[lstWeeks.Count];
                objWorkCategoryRecordPercentage.Info = new SpecialData[lstWeeks.Count];
                for (int i = 0; i < lstWeeks.Count; i++)
                {
                    var week                           = lstWeeks[i];

                    var specialData                    = new SpecialData();
                    specialData.WeekNo                 = week;
                    specialData.Value                  = columnTotals[i].ToString("0.00");
                    objWorkCategoryRecordTotal.Info[i] = specialData;

                    var specialDataPercentage = new SpecialData();
                    specialDataPercentage.WeekNo = week;
                    if (grandTotal != 0)
                    {
                        specialDataPercentage.Value = String.Format("{0:P2}", (((float)columnTotals[i]) / ((float)grandTotal)));                        
                    }
                    else
                    {
                        specialDataPercentage.Value = "0.00%";
                    }
                    objWorkCategoryRecordPercentage.Info[i] = specialDataPercentage;

                }
            }

            objWorkCategoryRecordTotal.Total = grandTotal.ToString("0.00");
            objWorkCategoryRecordPercentage.Total = grandTotal.ToString("0.00");

            lstResult.Add(objWorkCategoryRecordTotal);
            lstResult.Add(objWorkCategoryRecordPercentage);

            return lstResult;

        }


    }
}
