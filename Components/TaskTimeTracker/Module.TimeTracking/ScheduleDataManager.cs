using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Framework.Components.DataAccess;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.TimeTracking;
using Dapper;

namespace TaskTimeTracker.Components.Module.TimeTracking
{
	public partial class ScheduleDataManager : BaseDataManager
	{
		static readonly string DataStoreKey = "";

		static ScheduleDataManager()
		{
			DataStoreKey = SetupConfiguration.GetDataStoreKey("Schedule");
		}

		#region GetList

        public static List<ScheduleDataModel> GetList(RequestProfile requestProfile)
        {
            return GetEntityDetails(ScheduleDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region GetScheduleList

		public static List<ScheduleDataModel> GetScheduleList(RequestProfile requestProfile)
		{
			return GetEntityDetails(ScheduleDataModel.Empty, requestProfile, 0);
		}

		#endregion

		#region ToSQLParameter

		public static string ToSQLParameter(ScheduleDataModel data, string dataColumnName)
		{
			var returnValue = "NULL";

			switch (dataColumnName)
			{
				case ScheduleDataModel.DataColumns.ScheduleId:
					if (data.ScheduleId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleDataModel.DataColumns.ScheduleId, data.ScheduleId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDataModel.DataColumns.ScheduleId);
					}
					break;

				case ScheduleDataModel.DataColumns.PersonId:
					if (data.PersonId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleDataModel.DataColumns.PersonId, data.PersonId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDataModel.DataColumns.PersonId);
					}
					break;

				case ScheduleDataModel.DataColumns.Person:
					if (data.Person != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDataModel.DataColumns.Person, data.Person);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDataModel.DataColumns.Person);
					}
					break;
				case ScheduleDataModel.DataColumns.ScheduleStateId:
					if (data.ScheduleStateId != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleDataModel.DataColumns.ScheduleStateId, data.ScheduleStateId);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDataModel.DataColumns.ScheduleStateId);
					}
					break;

				case ScheduleDataModel.DataColumns.ExcludeItems:
					if (data.ExcludeItems != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleDataModel.DataColumns.ExcludeItems, data.ExcludeItems);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDataModel.DataColumns.ExcludeItems);
					}
					break;

				case ScheduleDataModel.DataColumns.ScheduleStateName:
					if (data.ScheduleStateName != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDataModel.DataColumns.ScheduleStateName, data.ScheduleStateName);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDataModel.DataColumns.ScheduleStateName);
					}
					break;

				case ScheduleDataModel.DataColumns.WorkDate:
					if (data.WorkDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDataModel.DataColumns.WorkDate, data.WorkDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDataModel.DataColumns.WorkDate);
					}
					break;

				case ScheduleDataModel.DataColumns.StartTime:
					if (data.StartTime != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDataModel.DataColumns.StartTime, data.StartTime);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDataModel.DataColumns.StartTime);
					}
					break;

				case ScheduleDataModel.DataColumns.EndTime:
					if (data.EndTime != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDataModel.DataColumns.EndTime, data.EndTime);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDataModel.DataColumns.EndTime);
					}
					break;

				case ScheduleDataModel.DataColumns.TotalHoursWorked:
					if (data.TotalHoursWorked != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleDataModel.DataColumns.TotalHoursWorked, data.TotalHoursWorked);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDataModel.DataColumns.TotalHoursWorked);
					}
					break;

				case ScheduleDataModel.DataColumns.PlannedHours:
					if (data.PlannedHours != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleDataModel.DataColumns.PlannedHours, data.PlannedHours);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDataModel.DataColumns.PlannedHours);
					}
					break;

				case ScheduleDataModel.DataColumns.ComputedHours:
					if (data.ComputedHours != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NUMBER, ScheduleDataModel.DataColumns.ComputedHours, data.ComputedHours);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDataModel.DataColumns.ComputedHours);
					}
					break;

				case ScheduleDataModel.DataColumns.NextWorkDate:
					if (data.NextWorkDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDataModel.DataColumns.NextWorkDate, data.NextWorkDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDataModel.DataColumns.NextWorkDate);
					}
					break;

				case ScheduleDataModel.DataColumns.NextWorkTime:
					if (data.NextWorkTime != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDataModel.DataColumns.NextWorkTime, data.NextWorkTime);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDataModel.DataColumns.NextWorkTime);
					}
					break;

				case ScheduleDataModel.DataColumns.FromSearchDate:
					if (data.FromSearchDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDataModel.DataColumns.FromSearchDate, data.FromSearchDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDataModel.DataColumns.FromSearchDate);
					}
					break;

				case ScheduleDataModel.DataColumns.ToSearchDate:
					if (data.ToSearchDate != null)
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_STRING_OR_DATE, ScheduleDataModel.DataColumns.ToSearchDate, data.ToSearchDate);
					}
					else
					{
						returnValue = string.Format(SQL_TEMPLATE_PARAMETER_NULL, ScheduleDataModel.DataColumns.ToSearchDate);
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

        public static ScheduleDataModel GetDetails(ScheduleDataModel data, RequestProfile requestProfile)
		{
            var list = GetEntityDetails(data, requestProfile, 1);
            return list.FirstOrDefault();
		}

		public static DataTable GetSampleSearch()
		{
			var sql = "EXEC dbo.ScheduleSampleSearch ";

			var oDt = new DBDataTable("Schedule.Search", sql, DataStoreKey);
			return oDt.DBTable;
		}

		static public List<ScheduleDataModel> GetEntityDetails(ScheduleDataModel dataQuery, RequestProfile requestProfile, int returnAuditInfo = BaseDataManager.ReturnAuditInfoOnDetails)
		{
			const string sql = @"dbo.ScheduleSearch ";

			var parameters =
			new
			{
				    AuditId             = requestProfile.AuditId
				,   ReturnAuditInfo     = returnAuditInfo
				,	ApplicationId		= requestProfile.ApplicationId
				,	ApplicationMode		= requestProfile.ApplicationModeId
				,   ScheduleId          = dataQuery.ScheduleId
				,   ScheduleStateId     = dataQuery.ScheduleStateId
				,   FromSearchDate      = dataQuery.FromSearchDate
				,   ToSearchDate        = dataQuery.ToSearchDate
				,   Person				= dataQuery.PersonId
			};

			List<ScheduleDataModel> result;

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{
				result = dataAccess.Connection.Query<ScheduleDataModel>(sql, parameters, commandType: CommandType.StoredProcedure).ToList();
			}

			return result;
		}


		//public static List<ScheduleDataModel> GetEntityDetails(ScheduleDataModel data, int auditId)
		//{
		//	var sql = "EXEC dbo.ScheduleSearch " +
		//			  " " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, auditId) +
		//			  ", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ReturnAuditInfo, BaseDataManager.ReturnAuditInfoOnDetails) +
		//			  ", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.ScheduleId);

		//	var result = new List<ScheduleDataModel>();

		//	using (var reader = new DBDataReader("Get Details", sql, DataStoreKey))
		//	{
		//		var dbReader                     = reader.DBReader;

		//		while (dbReader.Read())
		//		{
		//			var dataItem                 = new ScheduleDataModel();

		//			dataItem.ScheduleId		     = (int?)dbReader[ScheduleDataModel.DataColumns.ScheduleId];
		//			dataItem.Person			     = (string)dbReader[ScheduleDataModel.DataColumns.Person];
		//			dataItem.PersonId		     = (int?)dbReader[ScheduleDataModel.DataColumns.PersonId];
		//			dataItem.ScheduleStateId     = (int?)dbReader[ScheduleDataModel.DataColumns.ScheduleStateId];
		//			dataItem.WorkDate		     = (DateTime?)dbReader[ScheduleDataModel.DataColumns.WorkDate];
		//			dataItem.StartTime		     = (DateTime?)dbReader[ScheduleDataModel.DataColumns.StartTime];
		//			dataItem.EndTime		     = (DateTime?)dbReader[ScheduleDataModel.DataColumns.EndTime];
		//			dataItem.NextWorkDate        = (DateTime?)dbReader[ScheduleDataModel.DataColumns.NextWorkDate];
		//			dataItem.NextWorkTime	     = (DateTime?)dbReader[ScheduleDataModel.DataColumns.NextWorkTime];
		//			dataItem.TotalHoursWorked    = (decimal?)dbReader[ScheduleDataModel.DataColumns.TotalHoursWorked];

		//			if (dbReader[ScheduleDataModel.DataColumns.ComputedHours] != DBNull.Value)
		//			{
		//				dataItem.ComputedHours   = Convert.ToDecimal(dbReader[ScheduleDataModel.DataColumns.ComputedHours]);
		//			}

		//			dataItem.ScheduleStateName   = (string)dbReader[ScheduleDataModel.DataColumns.ScheduleStateName];

		//			dataItem.CreatedDate	     = (DateTime)dbReader[BaseDataModel.BaseDataColumns.CreatedDate];
		//			dataItem.ModifiedDate	     = (DateTime)dbReader[BaseDataModel.BaseDataColumns.ModifiedDate];
		//			dataItem.CreatedByAuditId    = (int?)dbReader[BaseDataModel.BaseDataColumns.CreatedByAuditId];
		//			dataItem.ModifiedByAuditId   = (int?)dbReader[BaseDataModel.BaseDataColumns.ModifiedByAuditId];
		//			//dataItem.CreatedDate	     = (DateTime)dbReader[ScheduleDataModel.DataColumns.CreatedDate];
		//			//dataItem.ModifiedDate	     = (DateTime)dbReader[ScheduleDataModel.DataColumns.ModifiedDate];
		//			//dataItem.CreatedByAuditId  = (int?)dbReader[ScheduleDataModel.DataColumns.CreatedByAuditId];
		//			//dataItem.ModifiedByAuditId = (int?)dbReader[ScheduleDataModel.DataColumns.ModifiedByAuditId];


		//			SetBaseInfo(dataItem, dbReader);

		//			result.Add(dataItem);
		//		}
		//	}

		//	return result;
		//}

		#endregion

		#region GroupByPerson

		public static DataTable GroupByPerson(ScheduleDataModel data)
		{
			var sql = "EXEC dbo.ScheduleGroupByPerson " + ToSQLParameter(data, ScheduleDataModel.DataColumns.PersonId);

			var oDt = new DBDataTable("Schedule.GroupByPerson", sql, DataStoreKey);
			return oDt.DBTable;
		}

		#endregion

		#region Create

		public static int Create(ScheduleDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Create");
			var id = DBDML.RunScalarSQL("Schedule.Insert", sql, DataStoreKey);
			return Convert.ToInt32(id);
	
		}

		
		#endregion

		#region Update

		public static void Update(ScheduleDataModel data, RequestProfile requestProfile)
		{
			var sql = Save(data, requestProfile, "Update");
			DBDML.RunSQL("Schedule.Update", sql, DataStoreKey);
		}

		#endregion

		#region Delete

		public static void Delete(ScheduleDataModel dataQuery, RequestProfile requestProfile)
		{
			const string sql = @"dbo.ScheduleDelete ";

			var parameters =
			new
			{
				AuditId = requestProfile.AuditId
				,
				ScheduleId = dataQuery.ScheduleId
			};

			using (var dataAccess = new DataAccessBase(DataStoreKey))
			{


				dataAccess.Connection.Execute(sql, parameters, commandType: CommandType.StoredProcedure);


			}
		}

		#endregion

		#region Search

		public static DataTable Search(ScheduleDataModel data, RequestProfile requestProfile)
		{
			var list = GetEntityDetails(data, requestProfile, 0);

			var table = list.ToDataTable();

			return table;
		}

		public static DataSet SearchView(ScheduleDataModel data, RequestProfile requestProfile)
		{

			// formulate SQL
			var sql = "EXEC dbo.ScheduleViewSearch " +
				" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
				", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
				", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.ScheduleId) +
				", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.ExcludeItems) +
				", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.PersonId) +
				", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.ScheduleStateId) +
				", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.FromSearchDate) +
				", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.ToSearchDate);

			var oDs = new DBDataSet("Schedule.Search", sql, DataStoreKey);
			return oDs.DBDataset;
		}

		#endregion

		#region Save

		private static string Save(ScheduleDataModel data, RequestProfile requestProfile, string action)
		{
			var sql = "EXEC ";

			switch (action)
			{
				case "Create":
					sql += "dbo.ScheduleInsert  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId);
					break;

				case "Update":
					sql += "dbo.ScheduleUpdate  " +
						" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.ApplicationId, requestProfile.ApplicationId) +
						", " + ToSQLParameter(BaseDataModel.BaseDataColumns.CreatedDate, data.CreatedDate);
					break;

				default:
					break;

			}

			sql = sql + ", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.ScheduleId) +
						", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.PersonId) +
						", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.WorkDate) +
						", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.StartTime) +
						", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.EndTime) +
						", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.TotalHoursWorked) +
						", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.PlannedHours) +
						", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.NextWorkDate) +
						", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.NextWorkTime) +
						", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.ScheduleStateId);


			return sql;
		}

		#endregion

		#region DoesExist

		public static bool DoesExist(ScheduleDataModel data, RequestProfile requestProfile)
		{
			var doesExistRequest = new ScheduleDataModel();
			doesExistRequest.FromSearchDate = data.FromSearchDate;
			doesExistRequest.ToSearchDate = data.ToSearchDate;
			doesExistRequest.PersonId = data.PersonId;

            var list = GetEntityDetails(doesExistRequest, requestProfile, 0);
            return list.Count > 0;
		}

		#endregion

		#region GetChildren

		private static DataSet GetChildren(ScheduleDataModel data, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.ScheduleChildrenGet " +
							" " + ToSQLParameter(BaseDataModel.BaseDataColumns.AuditId, requestProfile.AuditId) +
							", " + ToSQLParameter(data, ScheduleDataModel.DataColumns.ScheduleId);

			var oDt = new DBDataSet("Get Children", sql, DataStoreKey);
			return oDt.DBDataset;
		}

		#endregion

		#region IsDeletable

		public static bool IsDeletable(ScheduleDataModel data, RequestProfile requestProfile)
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

		#region GetStatisticData

		public static decimal GetTotalTimeSpent(DataTable dt, int scheduleTimeSpentConstant)
		{
			var series = new decimal[dt.Rows.Count];
			var i = 0;

			foreach (DataRow item in dt.Rows)
			{
				var timeSpent = item[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString();

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

			// totalTimeSpent1 calculates the time of 'UnKnown' value based on the ReleaseNotesTimeSpentConstant
			//decimal totalTimeSpent1 = scheduleData
			//							 .Where(z => (string.IsNullOrEmpty(z[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString()) || z[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == scheduleStatisticUnknown))
			//							 .Count() * scheduleTimeSpentConstant;

			//// totalTimeSpent2 calculates the time of TimeSpent column <> 'UnKnown'
			//decimal totalTimeSpent2 = scheduleData
			//							 .Where(x => x[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != scheduleStatisticUnknown)
			//							 .Sum(x => Convert.ToDecimal(x[ScheduleDataModel.DataColumns.TotalHoursWorked]));

			//// calculates the count of records whose TimeSpent <> 'UnKnown'
			//var count = scheduleData
			//							 .Where(x => x[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != scheduleStatisticUnknown)
			//							 .Select(x => x[ScheduleDataModel.DataColumns.TotalHoursWorked]).Count();

			//// calculates the total count of records
			//var totalCount = scheduleData
			//							 .Select(x => x[ScheduleDataModel.DataColumns.TotalHoursWorked]).Count();

			dataItem.Total = GetTotalTimeSpent(scheduleData, scheduleTimeSpentConstant);

			//dataItem.Total = totalTimeSpent1 + totalTimeSpent2;

			//dataItem.Count = totalCount;		
			var totalCount = scheduleData.Rows.Count;
			dataItem.Count = totalCount;


			var dt = new DataTable();
			dt.Columns.Add(ScheduleDataModel.DataColumns.TotalHoursWorked);
			dt.AcceptChanges();

			var rowT = dt.NewRow();

			var list = scheduleData.AsEnumerable()
					  .Where(x => x[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != scheduleStatisticUnknown);

			var list1 = scheduleData.AsEnumerable()
						.Where(x => x[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == scheduleStatisticUnknown);

			var rows = from row in list1.AsEnumerable()
					   select row;
			// takes care of the logic of TimeSpent column with UnKnown value to calculate average and median
			foreach (var row in rows)
			{
				if (Convert.ToInt16(row[ScheduleDataModel.DataColumns.TotalHoursWorked]) != 0)
				{
					if (row[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == scheduleStatisticUnknown)
					{
						rowT = dt.NewRow();
						rowT[ScheduleDataModel.DataColumns.TotalHoursWorked] = scheduleTimeSpentConstant;
						dt.Rows.Add(rowT);
					}
				}
			}

			var list2 = list.Concat(dt.AsEnumerable());

			var dataRows = list2 as DataRow[] ?? list2.ToArray();
			var rowItem = from row in dataRows.AsEnumerable() select row;

			if (rowItem.Any())
			{				
				var excludeZero = dataRows.AsEnumerable().Where(c => c.Field<Decimal>(ScheduleDataModel.DataColumns.TotalHoursWorked) != new Decimal(0.00));

                dataItem.Average = 0;
                if (excludeZero.Any())
                {
                    //calculates the average value				
                    dataItem.Average = excludeZero
                                          .Where(x => x[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != scheduleStatisticUnknown)
                                          .Select(x => Convert.ToDecimal(x[ScheduleDataModel.DataColumns.TotalHoursWorked])).Average();
                }

				//calculates the max and min values 
				dataItem.Max = dataRows.Max(x => Convert.ToDecimal(x[ScheduleDataModel.DataColumns.TotalHoursWorked]));
				dataItem.Min = dataRows.Min(x => Convert.ToDecimal(x[ScheduleDataModel.DataColumns.TotalHoursWorked]));

				// gets the ordered list to find the median
				var orderedList = dataRows.OrderBy(p => Convert.ToDecimal(p[ScheduleDataModel.DataColumns.TotalHoursWorked]));

				// calculates median for even number list
				if ((totalCount % 2) == 0)
				{
					dataItem.Median = Convert.ToDecimal(orderedList.ElementAt(totalCount / 2)[ScheduleDataModel.DataColumns.TotalHoursWorked]) + Convert.ToDecimal(orderedList.ElementAt((totalCount - 1) / 2)[ScheduleDataModel.DataColumns.TotalHoursWorked]);
					dataItem.Median /= 2;
				}
				else
				{
					// calculating median for odd number list
					if (totalCount == 1)
					{
						dataItem.Median = Convert.ToDecimal(orderedList.ElementAt(totalCount - 1)[ScheduleDataModel.DataColumns.TotalHoursWorked]);
					}
					else
					{
						dataItem.Median = Convert.ToDecimal(orderedList.ElementAt(totalCount / 2)[ScheduleDataModel.DataColumns.TotalHoursWorked]);
					}
				}
			}

			//calculates variance
			var dataSD = scheduleData.AsEnumerable()
								.Select(x => Convert.ToDouble(x[ScheduleDataModel.DataColumns.TotalHoursWorked])).AsList();

			dataItem.StandardDeviation = CalculateStdDeviation(dataSD);
			return dataItem;
		}

		public static double CalculateStdDeviation(IEnumerable<double> values)
		{
			double result = 0;
			if (values.Count() > 0)
			{
				//Compute the Average      
				double avg = values.Average();

				//Calculate Variance
				double sum = values.Sum(d => Math.Pow(d - avg, 2))/values.Count();

				//Calculate Standard Deviation
				result = Math.Sqrt(sum);
			}
			return result;
		}

		public static Dictionary<string, decimal> GetStatisticDataSummary(DataTable scheduleData, int scheduleTimeSpentConstant, string scheduleStatisticUnknown)
		{
			var lstResult = new Dictionary<string, decimal>();

			DataTable dt = new DataTable();
			dt.Clear();
			dt.Columns.Add(ScheduleDataModel.DataColumns.TotalHoursWorked);
			DataRow rowT = dt.NewRow();
			decimal average = 0;

			//var totalTimeSpent1 = scheduleData
			//					.Where(z => (string.IsNullOrEmpty(z[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString()) || z[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == scheduleStatisticUnknown)).Count() * scheduleTimeSpentConstant;

			//// totalTimeSpent2 calculates the time of TimeSpent column <> 'UnKnown'
			//var totalTimeSpent2 = scheduleData
			//						.Where(x => x[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != scheduleStatisticUnknown)
			//						.Sum(x => Convert.ToDecimal(x[ScheduleDataModel.DataColumns.TotalHoursWorked]));

			var totalTimeSpent = GetTotalTimeSpent(scheduleData, scheduleTimeSpentConstant);
			//lstResult.Add("TotalHoursWorked", totalTimeSpent1 + totalTimeSpent2);
			lstResult.Add("TotalTimeSpent", totalTimeSpent);

			//finding total number o records
			var count = scheduleData.AsEnumerable()
				.Select(x => x[ScheduleDataModel.DataColumns.TotalHoursWorked]).Count();

			lstResult.Add("Count", count);

			if (count != 0)
				average = totalTimeSpent / count;
				//average = (totalTimeSpent1 + totalTimeSpent2) / count;

			var list = scheduleData.AsEnumerable()
				.Where(x => x[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() != scheduleStatisticUnknown);

			var list1 = scheduleData.AsEnumerable()
				.Where(x => x[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == scheduleStatisticUnknown);


			var rows = from row in list1.AsEnumerable()
					   select row;

			foreach (DataRow row in rows)
			{
				if (row[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString().ToLower() == scheduleStatisticUnknown)
				{
					rowT = dt.NewRow();
					rowT[ScheduleDataModel.DataColumns.TotalHoursWorked] = scheduleTimeSpentConstant;
					dt.Rows.Add(rowT);
				}
			}

			var listMedian = list.Concat(dt.AsEnumerable());

			var orderedList = listMedian.OrderBy(p => (Decimal)(p[ScheduleDataModel.DataColumns.TotalHoursWorked]));
			decimal median = 0;

			if (count != 0)
			{
				// calculating median for even number list
				if ((count % 2) == 0)
				{
					median = (Decimal)(orderedList.ElementAt(count / 2)[ScheduleDataModel.DataColumns.TotalHoursWorked]) + (Decimal)(orderedList.ElementAt((count - 1) / 2)[ScheduleDataModel.DataColumns.TotalHoursWorked]);
					median /= 2;
				}
				else
				{
					// calculating median for odd number list
					if (count == 1)
					{
						median = (Decimal)(orderedList.ElementAt(count - 1)[ScheduleDataModel.DataColumns.TotalHoursWorked]);
					}
					else
					{
						median = (Decimal)(orderedList.ElementAt(count / 2)[ScheduleDataModel.DataColumns.TotalHoursWorked]);
					}
				}
			}

			lstResult.Add("Average", average);
			lstResult.Add("Median", median);

			return lstResult;
		}

		#endregion  

		#region GetMinutes

		public static string GetTicketHierarchy(double hour)
		{
			TimeSpan ts = new TimeSpan(Convert.ToDateTime(TimeSpan.FromHours(hour).ToString("h\\:mm")).Ticks);

			var diffMinutes = DateTime.MinValue.AddSeconds(ts.TotalSeconds);

			return diffMinutes.TimeOfDay.TotalMinutes.ToString();
		}

		#endregion 
      
		#region GetMinutes

		public static string GetMinutes(double hour)
		{			
			TimeSpan ts = new TimeSpan(Convert.ToDateTime(TimeSpan.FromHours(hour).ToString("h\\:mm")).Ticks);
			
			var diffMinutes = DateTime.MinValue.AddSeconds(ts.TotalSeconds);

			return diffMinutes.TimeOfDay.TotalMinutes.ToString();
		}

		#endregion 

		#region GetHierarchyDetails

		public static DataTable GetHierarchyList(string parentWorkTicket, string workTicket)
		{			
			var sql = "EXEC dbo.WorkTicketHierarchySearch " +
						" @ParentWorkTicket = '" + parentWorkTicket + "'" +
						", @WorkTicket = '" + workTicket + "'";

			var oDT = new DBDataTable("Get List", sql, DataStoreKey);
			
			return oDT.DBTable;
		}

		#endregion 
      
		#region SaveHierarchyDetails

		public static DataTable SaveHierarchyList(string parentWorkTicket, string workTicket, RequestProfile requestProfile)
		{
			var sql = "EXEC dbo.WorkTicketHierarchyInsert " +
						" @ApplicationId = " + requestProfile.ApplicationId + 
						", @ParentWorkTicket = '" + parentWorkTicket + "'" +
						", @WorkTicket = '" + workTicket + "'";

			var oDT = new DBDataTable("Get List", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion 
      
		#region DeleteHierarchyDetails

		public static DataTable DeleteHierarchyList(int workTicketHierarchyId)
		{
			var sql = "EXEC dbo.WorkTicketHierarchyDelete " +
						" @WorkTicketHierarchyId = " + workTicketHierarchyId;

			var oDT = new DBDataTable("Get List", sql, DataStoreKey);

			return oDT.DBTable;
		}

		#endregion 
      
	}
}
