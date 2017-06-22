using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.TimeTracking;
using System.Data;
using System.Text;
using System.Collections;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Web.Api.Controllers
{
	//[Authorize]
	public class ScheduleController : ApiController
	{
		#region variable

		DataTable AllDataRows;

		List<Statistic> lstStat = new List<Statistic>();
		List<string> lstKey		= new List<string>();

		string groupOn			= string.Empty;
		string groupOnDirection  = string.Empty;

		decimal totalHoursWorkedForGroup;
		decimal totalCount;

		#endregion		
		
		[System.Web.Http.AcceptVerbs("GET")]
		public List<Statistic> GetStatisticDisplayData(string value, string value1)
		{				
			DataTable dt = null;
			
			var distinctFieldValues = GetData(value, value1);		

			foreach (var key in distinctFieldValues)
			{
				var dataView = AllDataRows.DefaultView;
				if(key != "All")
				dataView.RowFilter = groupOn + " = '" + key + "'";

				dt = dataView.ToTable();

				var statData = ScheduleDataManager.GetStatisticData(dt, ScheduleDataModel.DataColumns.ScheduleTimeSpentConstant, ApplicationCommon.ScheduleStatisticUnknown);

				statData.CountPercentage = (statData.Count / totalCount) * 100;

				statData.TotalPercentage = (statData.Total / totalHoursWorkedForGroup) * 100;
				
				lstStat.Add(statData);
			}
			Statistic summaryStat = new Statistic();
			summaryStat = GetSummaryLine(lstStat);
			if (summaryStat != null)
			{
				summaryStat.Name = "Summary";
				lstStat.Add(summaryStat);
			}
			return lstStat;
		}

		private Statistic GetSummaryLine(List<Statistic> lstStat)
		{
			var lstData1 = new Statistic();
			if (lstStat.Count == 0)
				return null;
			decimal? Average = 0, Median, Max = 0, Min = lstStat[0].Min, Total = 0, TotalPercentage = 0, CountPercentage = 0;
			int? Count = 0;
			decimal?[] array = new decimal?[lstStat.Count];
			for (int i = 0; i < lstStat.Count; i++)
			{
				Total += lstStat[i].Total;
				TotalPercentage += lstStat[i].TotalPercentage;
				Average += lstStat[i].Average;
				CountPercentage += lstStat[i].CountPercentage;
				Count += lstStat[i].Count;
				if (Max < lstStat[i].Max)
					Max = lstStat[i].Max;
				if (!(Min < lstStat[i].Min))
					Min = lstStat[i].Min;
				array[i] = lstStat[i].Median;
			}
			Array.Sort(array);
			if (array.Length % 2 == 0)
			{
				var a = array[(array.Length / 2) - 1];
				var b = array[(array.Length / 2)];
				Median = (a + b) / 2;
			}
			else
			{
				Median = array[(array.Length / 2)];
			}
			
			Average = Average / lstStat.Count;
			lstData1.Total = Total;
			lstData1.CountPercentage = CountPercentage;
			lstData1.TotalPercentage = TotalPercentage;
			lstData1.Average = Average;
			lstData1.Median = Median;
			lstData1.Count = Count;
			lstData1.Max = Max;
			lstData1.Min = Min;
			return lstData1;
		}

		

		[System.Web.Http.AcceptVerbs("GET")]
		public List<string> GetStatisticKeyData(string value, string value1)
		{		
			var distinctFieldValues = GetData(value, value1);
			
			foreach (var key in distinctFieldValues)
			{
				lstKey.Add(key);
			}

			return lstKey;
		}

		public IEnumerable<string> GetData(string value, string value1)
		{
			var data = new ScheduleDataModel();

			dynamic jData = JsonConvert.DeserializeObject(value);
			data.Person			    = jData[0].Value;
			data.ScheduleStateId	= jData[1].Value;		
			data.ExcludeItems		= jData[2].Value;

			if (!string.IsNullOrEmpty(value1))
			{
				var dates = value1.Split('&');
				if (Boolean.Parse(dates[2]))
				{
					data.FromSearchDate = DateTimeHelper.FromApplicationDateFormatToDate(dates[0]);
					data.ToSearchDate = DateTimeHelper.FromApplicationDateFormatToDate(dates[1]);
				}
			}

			groupOn = jData[3].Value;
			var ds = ScheduleDataManager.SearchView(data, SessionVariables.RequestProfile);

			AllDataRows = ds.Tables[1];

			totalCount = AllDataRows.Rows.Count;

			var series = new decimal[AllDataRows.Rows.Count];
			var i = 0;

			foreach (DataRow item in AllDataRows.Rows)
			{
				var timeSpent = item[ScheduleDataModel.DataColumns.TotalHoursWorked].ToString();

				var timeSpentValue = 0m;

				Decimal.TryParse(timeSpent, out timeSpentValue);

				series[i++] = timeSpentValue;
			}

			totalHoursWorkedForGroup = series.Sum();
			DataTable dt = null;
			if (string.IsNullOrEmpty(groupOn) || groupOn == "-1" || groupOn == "All")
			{
				if (ds.Tables.Count > 1)
				{
					var tblKeyDescription = new DataTable();

					tblKeyDescription.AcceptChanges();
					tblKeyDescription.Columns.Add("Person");
					tblKeyDescription.Columns.Add("PersonId");

					var row = tblKeyDescription.NewRow();
					row["Person"] = "All";
					row["PersonId"] = "All";
					tblKeyDescription.Rows.Add(row);

					var dataView = tblKeyDescription.DefaultView;

					dt = dataView.ToTable();

					var distinctFieldValues = (from rowDisp in dt.AsEnumerable() select rowDisp.Field<string>("PersonId"))
												 .Distinct(StringComparer.CurrentCultureIgnoreCase);
					return distinctFieldValues;
				}
			}
			else
			{
				var distinctFieldValues = (from row in AllDataRows.AsEnumerable()
											  .Where(row => row[groupOn].ToString().Trim() != "")
										   orderby row[groupOn].ToString().Trim() descending
										   select row[groupOn].ToString().Trim())
										   .Distinct(StringComparer.CurrentCultureIgnoreCase);


				return distinctFieldValues;
			}

			var values = from products in ds.Tables[0].AsEnumerable() select products.Field<string>("Person");
			return values;
		}

	}
}