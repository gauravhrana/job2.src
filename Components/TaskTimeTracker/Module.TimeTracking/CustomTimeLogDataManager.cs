using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.TimeTracking;
using Dapper;
using TaskTimeTracker.Components.Module.TimeTracking.DomainModel;
using System.Globalization;
using System.Collections;

namespace TaskTimeTracker.Components.Module.TimeTracking
{
	public partial class CustomTimeLogDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static CustomTimeLogDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("CustomTimeLog");
		}

		#region GetList

        public static List<CustomTimeLogDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(CustomTimeLogDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region ToSQLParameter

		public static string ToSQLParameter(CustomTimeLogDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case CustomTimeLogDataModel.DataColumns.CustomTimeLogId:
					if (data.CustomTimeLogId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CustomTimeLogDataModel.DataColumns.CustomTimeLogId, data.CustomTimeLogId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustomTimeLogDataModel.DataColumns.CustomTimeLogId);
					}
					break;

				case CustomTimeLogDataModel.DataColumns.PromotedDate:
					if (data.PromotedDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CustomTimeLogDataModel.DataColumns.PromotedDate, data.PromotedDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustomTimeLogDataModel.DataColumns.PromotedDate);
					}
					break;

				case CustomTimeLogDataModel.DataColumns.PersonId:
					if (data.PersonId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CustomTimeLogDataModel.DataColumns.PersonId, data.PersonId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustomTimeLogDataModel.DataColumns.PersonId);
					}
					break;

				case CustomTimeLogDataModel.DataColumns.CustomTimeCategoryId:
					if (data.CustomTimeCategoryId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CustomTimeLogDataModel.DataColumns.CustomTimeCategoryId, data.CustomTimeCategoryId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustomTimeLogDataModel.DataColumns.CustomTimeCategoryId);
					}
					break;

				case CustomTimeLogDataModel.DataColumns.CustomTimeLogKey:
					if (data.CustomTimeLogKey != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CustomTimeLogDataModel.DataColumns.CustomTimeLogKey, data.CustomTimeLogKey);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustomTimeLogDataModel.DataColumns.CustomTimeLogKey);
					}
					break;

				case CustomTimeLogDataModel.DataColumns.CustomTimeCategory:
					if (data.CustomTimeCategory != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CustomTimeLogDataModel.DataColumns.CustomTimeCategory, data.CustomTimeCategory);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustomTimeLogDataModel.DataColumns.CustomTimeCategory);
					}
					break;


				case CustomTimeLogDataModel.DataColumns.Value:
					if (data.Value != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CustomTimeLogDataModel.DataColumns.Value, data.Value);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustomTimeLogDataModel.DataColumns.Value);
					}
					break;



				case CustomTimeLogDataModel.DataColumns.FromSearchDate:
					if (data.FromSearchDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CustomTimeLogDataModel.DataColumns.FromSearchDate, data.FromSearchDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustomTimeLogDataModel.DataColumns.FromSearchDate);
					}
					break;

				case CustomTimeLogDataModel.DataColumns.ToSearchDate:
					if (data.ToSearchDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CustomTimeLogDataModel.DataColumns.ToSearchDate, data.ToSearchDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustomTimeLogDataModel.DataColumns.ToSearchDate);
					}
					break;

				case CustomTimeLogDataModel.DataColumns.Person:
					if (data.Person != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CustomTimeLogDataModel.DataColumns.Person, data.Person);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustomTimeLogDataModel.DataColumns.Person);
					}
					break;

                case CustomTimeLogDataModel.DataColumns.ApplicationId:
                    if (data.ApplicationId != null)
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, CustomTimeLogDataModel.DataColumns.ApplicationId, data.ApplicationId);
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustomTimeLogDataModel.DataColumns.ApplicationId);
                    }
                    break;

                case CustomTimeLogDataModel.DataColumns.Application:
                    if (!string.IsNullOrEmpty(data.Application))
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, CustomTimeLogDataModel.DataColumns.Application, data.Application.Trim());
                    }
                    else
                    {
                        returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, CustomTimeLogDataModel.DataColumns.Application);
                    }
                    break;

				default:
					returnValue = StandardDataManager.ToSQLParameter(data, dataColumnName);
					break;

			}
			 
			return returnValue;
		}

		public static List<WorkCategoryDataModel> GetWorkCategoryList(CustomTimeLogDataModel data, RequestProfile requestProfile)
		{
			
			var list = GetEntityDetails(data, requestProfile);
			
			var result = new  List<WorkCategoryDataModel>();			
			for (var i = 0; i < list.Count; i++)
			{
				var workData = new WorkCategoryDataModel();
				workData.Category = list[i].CustomTimeLogKey;
				workData.Week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(Convert.ToDateTime(list[i].PromotedDate), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);				
				workData.Value = (int)list[i].Value;
				result.Add(workData);
				
			}

			return result;	
			
			
		}

		#endregion

		#region GetDetails

        public static CustomTimeLogDataModel GetDetails(CustomTimeLogDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}



		static public List<CustomTimeLogDataModel> GetEntityDetails(CustomTimeLogDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.CustomTimeLogSearch ";

			var parameters =
			new
			{
					AuditId					= requestProfile.AuditId
				,	ReturnAuditInfo			= returnAuditInfo
				,	ApplicationId			= dataQuery.ApplicationId
				,	CustomTimeLogId			= dataQuery.CustomTimeLogId
				,	CustomTimeCategoryId	= dataQuery.CustomTimeCategoryId
				,	FromSearchDate			= dataQuery.FromSearchDate
				,	ToSearchDate			= dataQuery.ToSearchDate
				,	PersonId				= dataQuery.PersonId
				,	CustomTimeLogKey		= dataQuery.CustomTimeLogKey
				,	PromotedDate			= dataQuery.PromotedDate				
			};

			List<CustomTimeLogDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<CustomTimeLogDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}

		#endregion

		#region GroupByPerson

		public static DataTable GroupByPerson(CustomTimeLogDataModel data)
		{
			var sql = "EXEC dbo.CustomTimeLogGroupByPerson " + ToSQLParameter(data, CustomTimeLogDataModel.DataColumns.PersonId);

			var oDt = new DBDataTable("CustomTimeLog.GroupByPerson", sql, DataStoreKey);
			return oDt.DBTable;
		}

		public static DataTable GroupByKey(CustomTimeLogDataModel data)
		{
			var sql = "EXEC dbo.CustomTimeLogGroupByKey " + ToSQLParameter(data, CustomTimeLogDataModel.DataColumns.CustomTimeLogId);

			var oDt = new DBDataTable("CustomTimeLog.GroupByKey", sql, DataStoreKey);
			return oDt.DBTable;
		}

		#endregion

		#region Create

		public static int Create(CustomTimeLogDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			var id = DBDML.RunScalarSQL("CustomTimeLog.Insert", sql, DataStoreKey);
			return Convert.ToInt32(id);
		}

		#endregion

		#region Update

		public static void Update(CustomTimeLogDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("CustomTimeLog.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(CustomTimeLogDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.CustomTimeLogDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				CustomTimeLogId = dataQuery.CustomTimeLogId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);

			}
		}

		#endregion

		#region Search

		public static DataTable Search(CustomTimeLogDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		//public static DataSet SearchView(CustomTimeLogDataModel data, RequestProfile requestProfile)
		//{

		//	// formulate SQL
		//	var sql = "EXEC dbo.CustomTimeLogViewSearch " +
		//		" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
		//		", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
		//		", " + ToSQLParameter(data, CustomTimeLogDataModel.DataColumns.CustomTimeLogId) +
		//		", " + ToSQLParameter(data, CustomTimeLogDataModel.DataColumns.ExcludeItems) +
		//		", " + ToSQLParameter(data, CustomTimeLogDataModel.DataColumns.PersonId) +
		//		", " + ToSQLParameter(data, CustomTimeLogDataModel.DataColumns.CustomTimeLogStateId) +
		//		", " + ToSQLParameter(data, CustomTimeLogDataModel.DataColumns.FromSearchDate) +
		//		", " + ToSQLParameter(data, CustomTimeLogDataModel.DataColumns.ToSearchDate);

		//	var oDs = new DBDataSet("CustomTimeLog.Search", sql, DataStoreKey);
		//	return oDs.DBDataset;
		//}

		#endregion

		#region Save

		private static string Save(CustomTimeLogDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.CustomTimeLogInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, data.ApplicationId);
					break;

				case "Update":
					sql += "dbo.CustomTimeLogUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, data.ApplicationId);// +
					//", " + ToSQLParameter(BaseDataModel.BaseDataColumns.CreatedDate, data.CreatedDate);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, CustomTimeLogDataModel.DataColumns.CustomTimeLogId) +
						", " + ToSQLParameter(data, CustomTimeLogDataModel.DataColumns.PersonId) +
						", " + ToSQLParameter(data, CustomTimeLogDataModel.DataColumns.PromotedDate) +
						", " + ToSQLParameter(data, CustomTimeLogDataModel.DataColumns.CustomTimeLogKey) +
				//", " + ToSQLParameter(data, CustomTimeLogDataModel.DataColumns.StartDate) +
				//", " + ToSQLParameter(data, CustomTimeLogDataModel.DataColumns.EndDate) +
						", " + ToSQLParameter(data, CustomTimeLogDataModel.DataColumns.Value) +
						", " + ToSQLParameter(data, CustomTimeLogDataModel.DataColumns.CustomTimeCategoryId);


			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(CustomTimeLogDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new CustomTimeLogDataModel();
			doesExistRequest.CustomTimeLogKey = data.CustomTimeLogKey;
			doesExistRequest.ApplicationId = data.ApplicationId;
			doesExistRequest.PromotedDate = data.PromotedDate;
			doesExistRequest.PersonId = data.PersonId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

		//#region GetChildren

		//private static DataSet GetChildren(CustomTimeLogDataModel data, RequestProfile requestProfile)
		//{
		//	var sql = "EXEC dbo.CustomTimeLogChildrenGet " +
		//					" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
		//					", " + ToSQLParameter(data, CustomTimeLogDataModel.DataColumns.CustomTimeLogId);

		//	var oDt = new DBDataSet("Get Children", sql, DataStoreKey);
		//	return oDt.DBDataset;
		//}

		//#endregion

		//#region IsDeletable

		//public static bool IsDeletable(CustomTimeLogDataModel data, RequestProfile requestProfile)
		//{
		//	var isDeletable = true;
		//	var ds = GetChildren(data, requestProfile);
		//	if (ds != null && ds.Tables.Count > 0)
		//	{
		//		foreach (DataTable dt in ds.Tables)
		//		{
		//			if (dt.Rows.Count > 0)
		//			{
		//				isDeletable = false;
		//				break;
		//			}
		//		}
		//	}
		//	return isDeletable;
		//}

		//#endregion

		//#region GetStatisticData

		//public static decimal GetTotalTimeSpent(DataTable dt, int CustomTimeLogTimeSpentConstant)
		//{
		//	var series = new decimal[dt.Rows.Count];
		//	var i = 0;

		//	foreach (DataRow item in dt.Rows)
		//	{
		//		var timeSpent = item[CustomTimeLogDataModel.DataColumns.TotalHoursWorked].ToString();

		//		var timeSpentValue = 0m;

		//		if (!Decimal.TryParse(timeSpent, out timeSpentValue))
		//		{
		//			timeSpentValue = CustomTimeLogTimeSpentConstant;
		//		}

		//		series[i++] = timeSpentValue;
		//	}

		//	var timeSpentForGroup = series.Sum();
		//	return timeSpentForGroup;
		//}

		//public static Statistic GetStatisticData(DataTable CustomTimeLogData, int CustomTimeLogTimeSpentConstant, string CustomTimeLogStatisticUnknown)
		//{
		//	var dataItem = new Statistic();

		//	// totalTimeSpent1 calculates the time of 'UnKnown' value based on the ReleaseNotesTimeSpentConstant
		//	//decimal totalTimeSpent1 = CustomTimeLogData
		//	//							 .Where(z => (string.IsNullOrEmpty(z[CustomTimeLogDataModel.DataColumns.TotalHoursWorked].ToString()) || z[CustomTimeLogDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == CustomTimeLogStatisticUnknown))
		//	//							 .Count() * CustomTimeLogTimeSpentConstant;

		//	//// totalTimeSpent2 calculates the time of TimeSpent column <> 'UnKnown'
		//	//decimal totalTimeSpent2 = CustomTimeLogData
		//	//							 .Where(x => x[CustomTimeLogDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != CustomTimeLogStatisticUnknown)
		//	//							 .Sum(x => Convert.ToDecimal(x[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]));

		//	//// calculates the count of records whose TimeSpent <> 'UnKnown'
		//	//var count = CustomTimeLogData
		//	//							 .Where(x => x[CustomTimeLogDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != CustomTimeLogStatisticUnknown)
		//	//							 .Select(x => x[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]).Count();

		//	//// calculates the total count of records
		//	//var totalCount = CustomTimeLogData
		//	//							 .Select(x => x[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]).Count();

		//	dataItem.Total = GetTotalTimeSpent(CustomTimeLogData, CustomTimeLogTimeSpentConstant);

		//	//dataItem.Total = totalTimeSpent1 + totalTimeSpent2;

		//	//dataItem.Count = totalCount;		
		//	var totalCount = CustomTimeLogData.Rows.Count;
		//	dataItem.Count = totalCount;


		//	var dt = new DataTable();
		//	dt.Columns.Add(CustomTimeLogDataModel.DataColumns.TotalHoursWorked);
		//	dt.AcceptChanges();

		//	var rowT = dt.NewRow();

		//	var list = CustomTimeLogData.AsEnumerable()
		//			  .Where(x => x[CustomTimeLogDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != CustomTimeLogStatisticUnknown);

		//	var list1 = CustomTimeLogData.AsEnumerable()
		//				.Where(x => x[CustomTimeLogDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == CustomTimeLogStatisticUnknown);

		//	var rows = from row in list1.AsEnumerable()
		//			   select row;
		//	// takes care of the logic of TimeSpent column with UnKnown value to calculate average and median
		//	foreach (var row in rows)
		//	{
		//		if (Convert.ToInt16(row[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]) != 0)
		//		{
		//			if (row[CustomTimeLogDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == CustomTimeLogStatisticUnknown)
		//			{
		//				rowT = dt.NewRow();
		//				rowT[CustomTimeLogDataModel.DataColumns.TotalHoursWorked] = CustomTimeLogTimeSpentConstant;
		//				dt.Rows.Add(rowT);
		//			}
		//		}
		//	}

		//	var list2 = list.Concat(dt.AsEnumerable());

		//	var dataRows = list2 as DataRow[] ?? list2.ToArray();
		//	var rowItem = from row in dataRows.AsEnumerable() select row;

		//	if (rowItem.Any())
		//	{
		//		var excludeZero = dataRows.AsEnumerable().Where(c => c.Field<Decimal>(CustomTimeLogDataModel.DataColumns.TotalHoursWorked) != new Decimal(0.00));
		//		//calculates the average value				
		//		dataItem.Average = excludeZero
		//							  .Where(x => x[CustomTimeLogDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != CustomTimeLogStatisticUnknown)
		//							  .Select(x => Convert.ToDecimal(x[CustomTimeLogDataModel.DataColumns.TotalHoursWorked])).Average();

		//		//calculates the max and min values 
		//		dataItem.Max = dataRows.Max(x => Convert.ToDecimal(x[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]));
		//		dataItem.Min = dataRows.Min(x => Convert.ToDecimal(x[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]));

		//		// gets the ordered list to find the median
		//		var orderedList = dataRows.OrderBy(p => Convert.ToDecimal(p[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]));

		//		// calculates median for even number list
		//		if ((totalCount % 2) == 0)
		//		{
		//			dataItem.Median = Convert.ToDecimal(orderedList.ElementAt(totalCount / 2)[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]) + Convert.ToDecimal(orderedList.ElementAt((totalCount - 1) / 2)[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]);
		//			dataItem.Median /= 2;
		//		}
		//		else
		//		{
		//			// calculating median for odd number list
		//			if (totalCount == 1)
		//			{
		//				dataItem.Median = Convert.ToDecimal(orderedList.ElementAt(totalCount - 1)[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]);
		//			}
		//			else
		//			{
		//				dataItem.Median = Convert.ToDecimal(orderedList.ElementAt(totalCount / 2)[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]);
		//			}
		//		}
		//	}

		//	return dataItem;
		//}

		//public static Dictionary<string, decimal> GetStatisticDataSummary(DataTable CustomTimeLogData, int CustomTimeLogTimeSpentConstant, string CustomTimeLogStatisticUnknown)
		//{
		//	var lstResult = new Dictionary<string, decimal>();

		//	DataTable dt = new DataTable();
		//	dt.Clear();
		//	dt.Columns.Add(CustomTimeLogDataModel.DataColumns.TotalHoursWorked);
		//	DataRow rowT = dt.NewRow();
		//	decimal average = 0;

		//	//var totalTimeSpent1 = CustomTimeLogData
		//	//					.Where(z => (string.IsNullOrEmpty(z[CustomTimeLogDataModel.DataColumns.TotalHoursWorked].ToString()) || z[CustomTimeLogDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == CustomTimeLogStatisticUnknown)).Count() * CustomTimeLogTimeSpentConstant;

		//	//// totalTimeSpent2 calculates the time of TimeSpent column <> 'UnKnown'
		//	//var totalTimeSpent2 = CustomTimeLogData
		//	//						.Where(x => x[CustomTimeLogDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != CustomTimeLogStatisticUnknown)
		//	//						.Sum(x => Convert.ToDecimal(x[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]));

		//	var totalTimeSpent = GetTotalTimeSpent(CustomTimeLogData, CustomTimeLogTimeSpentConstant);
		//	//lstResult.Add("TotalHoursWorked", totalTimeSpent1 + totalTimeSpent2);
		//	lstResult.Add("TotalTimeSpent", totalTimeSpent);

		//	//finding total number o records
		//	var count = CustomTimeLogData.AsEnumerable()
		//		.Select(x => x[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]).Count();

		//	lstResult.Add("Count", count);

		//	if (count != 0)
		//		average = totalTimeSpent / count;
		//	//average = (totalTimeSpent1 + totalTimeSpent2) / count;

		//	var list = CustomTimeLogData.AsEnumerable()
		//		.Where(x => x[CustomTimeLogDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != CustomTimeLogStatisticUnknown);

		//	var list1 = CustomTimeLogData.AsEnumerable()
		//		.Where(x => x[CustomTimeLogDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == CustomTimeLogStatisticUnknown);


		//	var rows = from row in list1.AsEnumerable()
		//			   select row;

		//	foreach (DataRow row in rows)
		//	{
		//		if (row[CustomTimeLogDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == CustomTimeLogStatisticUnknown)
		//		{
		//			rowT = dt.NewRow();
		//			rowT[CustomTimeLogDataModel.DataColumns.TotalHoursWorked] = CustomTimeLogTimeSpentConstant;
		//			dt.Rows.Add(rowT);
		//		}
		//	}

		//	var listMedian = list.Concat(dt.AsEnumerable());

		//	var orderedList = listMedian.OrderBy(p => (Decimal)(p[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]));
		//	decimal median = 0;

		//	if (count != 0)
		//	{
		//		// calculating median for even number list
		//		if ((count % 2) == 0)
		//		{
		//			median = (Decimal)(orderedList.ElementAt(count / 2)[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]) + (Decimal)(orderedList.ElementAt((count - 1) / 2)[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]);
		//			median /= 2;
		//		}
		//		else
		//		{
		//			// calculating median for odd number list
		//			if (count == 1)
		//			{
		//				median = (Decimal)(orderedList.ElementAt(count - 1)[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]);
		//			}
		//			else
		//			{
		//				median = (Decimal)(orderedList.ElementAt(count / 2)[CustomTimeLogDataModel.DataColumns.TotalHoursWorked]);
		//			}
		//		}
		//	}

		//	lstResult.Add("Average", average);
		//	lstResult.Add("Median", median);

		//	return lstResult;
		//}

		//#endregion

        public static List<BranchRecord> GetBranchSummaryReportData(CustomTimeLogDataModel data, RequestProfile requestProfile)
        {
            var lstResult = new List<BranchRecord>();
            var columnTotals = new List<int>();

            var resultList = GetEntityDetails(data, requestProfile);

            // perform Grouping
            var resultGrouping = from l in resultList
                                 group l by new
                                 {
                                     Week = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(l.PromotedDate.Value, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday),
                                     l.CustomTimeLogKey
                                 }
                                     into gcs
                                     select new
                                     {
                                         gcs.Key.Week,
                                         gcs.Key.CustomTimeLogKey,
                                         Count = gcs.Sum(x => x.Value)
                                     };

            var dt = resultGrouping.ToList().ToDataTable();

            //currently assuming both will not be null
            var startingWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(data.FromSearchDate.Value, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
            var endingWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(data.ToSearchDate.Value, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);

            var lstWeeks = new List<string>();

            // case of multiple year
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

            var distBranches = (from row in dt.AsEnumerable()
                                select row["CustomTimeLogKey"].ToString().Trim())
                                    .Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();

            // add header row
            var objBranchRecordHeader = new BranchRecord();

            objBranchRecordHeader.Name = "Branch";
            if (lstWeeks.Count > 0)
            {
                objBranchRecordHeader.Info = new SpecialData[lstWeeks.Count];
                for (int i = 0; i < lstWeeks.Count; i++)
                {
                    var week = lstWeeks[i];
                    var specialData = new SpecialData();
                    specialData.WeekNo = week;

                    objBranchRecordHeader.Info[i] = specialData;

                    columnTotals.Add(0);
                }
            }
            lstResult.Add(objBranchRecordHeader);

            for (int j = 0; j < distBranches.Count; j++)
            {
                var branch = distBranches[j];
                var objBranchRecord = new BranchRecord();

                var rowTotal = 0;

                objBranchRecord.Name = branch;

                if (lstWeeks.Count > 0) 
                {

                    objBranchRecord.Info = new SpecialData[lstWeeks.Count];

                    for (int i = 0; i < lstWeeks.Count; i++)
                    {
                        var week = lstWeeks[i];

                        var specialData = new SpecialData();
                        specialData.WeekNo = week;
                        
                        var weekCount = 0;

                        try
                        {
                            weekCount = (from row in dt.AsEnumerable()
                                         where row["CustomTimeLogKey"].ToString() == branch && row["Week"].ToString() == week
                                         select Convert.ToInt32(row["Count"])).FirstOrDefault();
                        }
                        catch { }

                        specialData.Value = weekCount.ToString("0.00");
                        rowTotal += weekCount;
                        columnTotals[i] += weekCount;

                        objBranchRecord.Info[i] = specialData;
                    }
                }

                objBranchRecord.Total = rowTotal.ToString("0.00");

                lstResult.Add(objBranchRecord);

            }

            // count grand total
            var grandTotal = columnTotals.Sum();

            // add total row
            var objBranchRecordTotal = new BranchRecord();
            objBranchRecordTotal.Name = "Total";

            // add Percentage row
            var objBranchRecordPercentage = new BranchRecord();
            objBranchRecordPercentage.Name = "Percentage";

            if (lstWeeks.Count > 0)
            {
                objBranchRecordTotal.Info = new SpecialData[lstWeeks.Count];
                objBranchRecordPercentage.Info = new SpecialData[lstWeeks.Count];
                for (int i = 0; i < lstWeeks.Count; i++)
                {
                    var week                     = lstWeeks[i];

                    var specialData              = new SpecialData();
                    specialData.WeekNo           = week;
                    specialData.Value            = columnTotals[i].ToString("0.00");
                    objBranchRecordTotal.Info[i] = specialData;

                    var specialDataPercentage         = new SpecialData();
                    specialDataPercentage.WeekNo      = week;
                    if (grandTotal != 0)
                    {
                        specialDataPercentage.Value = String.Format("{0:P2}", (((float)columnTotals[i]) / ((float)grandTotal)));
                    }
                    else
                    {
                        specialDataPercentage.Value = "0.00%";
                    }
                    objBranchRecordPercentage.Info[i] = specialDataPercentage;
                }
            }
            
            objBranchRecordTotal.Total = grandTotal.ToString("0.00");
            objBranchRecordPercentage.Total = grandTotal.ToString("0.00");

            lstResult.Add(objBranchRecordTotal);
            lstResult.Add(objBranchRecordPercentage);

            return lstResult;

        }

	}
}
