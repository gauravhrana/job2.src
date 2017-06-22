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
using Shared.UI.Web;

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

			decimal average_value = 0;
            decimal median_value = 0;
            decimal max_value = 0; 
            decimal min_value = lstStat[0].Min;
            decimal total_value = 0;
            decimal totalPercentage_value = 0;
            decimal countPercentage_value = 0;
			int     count_value = 0;

			decimal[] array = new decimal[lstStat.Count];

			for (int i = 0; i < lstStat.Count; i++)
			{
                total_value += lstStat[i].Total;
                totalPercentage_value += lstStat[i].TotalPercentage;
				average_value += lstStat[i].Average;
				countPercentage_value += lstStat[i].CountPercentage;
				count_value += lstStat[i].Count;
				if (max_value < lstStat[i].Max)
                    max_value = lstStat[i].Max;
				if (!(min_value < lstStat[i].Min))
					min_value = lstStat[i].Min;
				array[i] = lstStat[i].Median;
			}

			Array.Sort(array);

			if (array.Length % 2 == 0)
			{
				var a = array[(array.Length / 2) - 1];
				var b = array[(array.Length / 2)];
				median_value = (a + b) / 2;
			}
			else
			{
				median_value = array[(array.Length / 2)];
			}
			
			average_value = average_value / lstStat.Count;
			lstData1.Total = total_value;
			lstData1.CountPercentage = countPercentage_value;
			lstData1.TotalPercentage = totalPercentage_value;
			lstData1.Average = average_value;
			lstData1.Median = median_value;
			lstData1.Count = count_value;
			lstData1.Max = max_value;
			lstData1.Min = min_value;

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

			//Newtonsoft.Json.

			var jData = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);

			if (jData.ContainsKey("ExcludeItems") && !string.IsNullOrEmpty(jData["ExcludeItems"]) != null && jData["ExcludeItems"] != "All")
			{
				data.ExcludeItems = int.Parse(jData["ExcludeItems"]);
			}

			if (jData.ContainsKey("Person") && !string.IsNullOrEmpty(jData["Person"]) != null && jData["Person"] != "All")
			{
				data.PersonId = int.Parse(jData["Person"]);
			}

			if (jData.ContainsKey("ScheduleStateName") && !string.IsNullOrEmpty(jData["ScheduleStateName"]) != null && jData["ScheduleStateName"] != "All")
			{
				data.ScheduleStateId = int.Parse(jData["ScheduleStateName"]);
			}
			
			//data.PersonId			= jData[0].Value;
			//data.ScheduleStateId	= jData[1].Value;		
			//data.ExcludeItems		= jData[2].Value;
			//groupOn = jData[3].Value;

			if (!string.IsNullOrEmpty(value1))
			{
				var dates = value1.Split('&');
				if (Boolean.Parse(dates[2]))
				{
                    try
                    {
                        data.FromSearchDate = DateTimeHelper.FromApplicationDateFormatToDate(dates[0]);
                        data.ToSearchDate = DateTimeHelper.FromApplicationDateFormatToDate(dates[1]);
                    }
                    catch
                    {
                        data.FromSearchDate = DateTimeHelper.FromUserDateFormatToDate(dates[0]);
                        data.ToSearchDate = DateTimeHelper.FromUserDateFormatToDate(dates[1]);
                    }
				}
			}

			
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

		// GET api/summary/GetList

		[System.Web.Http.AcceptVerbs("GET")]
		public IEnumerable<ScheduleDataModel> GetListByApplication()
		{
			var dataQuery = new ScheduleDataModel();
			
			dataQuery.ApplicationId = 100047;

			return ScheduleDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public IEnumerable<ScheduleDataModel> GetList()
		{
			return ScheduleDataManager.GetScheduleList(SessionVariables.RequestProfile);
		}

		public IEnumerable<ScheduleDataModel> GetList(string value, string value1)
		{
			var settingCategory = value1;
			var searchString = value;
            var workDateValue = string.Empty;
            
			var dictionaryObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(searchString);
			//(searchString);

            if(dictionaryObject.Keys.Contains("WorkDate"))
            {
                workDateValue = dictionaryObject["WorkDate"];
                dictionaryObject["WorkDate"] = string.Empty;

                value = JsonConvert.SerializeObject(dictionaryObject);
            }
            
            var dataQuery = JsonConvert.DeserializeObject<ScheduleDataModel>(value);
            
            if (dictionaryObject.Keys.Contains("WorkDate"))
            {
                var tmpArr = workDateValue.Split(new string[] { "&" }, StringSplitOptions.None);

                var fromDateValue = tmpArr[0];
                var toDateValue = tmpArr[1];
                var preFilledItem = tmpArr[2];

                if (preFilledItem != "Custom" && string.IsNullOrEmpty(fromDateValue) && string.IsNullOrEmpty(toDateValue))
                {
                    var ranges = DateRangeHelper.FillUpDate(value, SessionVariables.UserDateFormat);
                    fromDateValue = ranges[0];
                    toDateValue = ranges[1];
                }

                dataQuery.FromSearchDate = DateTimeHelper.FromUserDateFormatToDate(fromDateValue);
                dataQuery.ToSearchDate = DateTimeHelper.FromUserDateFormatToDate(toDateValue);

                dictionaryObject["WorkDate"] =  DateTimeHelper.FromUserDateFormatToApplicationDateFormat(fromDateValue) + "&"
                                                + DateTimeHelper.FromUserDateFormatToApplicationDateFormat(toDateValue) + "&"
                                                + preFilledItem + "&";
            }            
				
			// save search filter parameters in user preference
			if (dictionaryObject != null)
			{
				foreach (var searchFilterColumnName in dictionaryObject.Keys)
				{
					var searchFilterValue = dictionaryObject[searchFilterColumnName];
					PreferenceUtility.UpdateUserPreference(settingCategory, searchFilterColumnName, dictionaryObject[searchFilterColumnName]);
				}
			}            
				
			return ScheduleDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public ScheduleDataModel GetById(string value)
		{
			var dataQuery = new ScheduleDataModel();

			dataQuery.ScheduleId = int.Parse(value);

			var result = ScheduleDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);

			return result[0];
		}

		[HttpPost]
		public void Create([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataCreate = JsonConvert.DeserializeObject<ScheduleDataModel>(jsonString);

			dataCreate.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

			ScheduleDataManager.Create(dataCreate, SessionVariables.RequestProfile);
		}

		[HttpPost]
		public void Update([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataUpdate = JsonConvert.DeserializeObject<ScheduleDataModel>(jsonString);
			ScheduleDataManager.Update(dataUpdate, SessionVariables.RequestProfile);
		}


		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataQuery = new ScheduleDataModel();
			dataQuery.ScheduleId = int.Parse(value);
			ScheduleDataManager.Delete(dataQuery, SessionVariables.RequestProfile);
		}

	}
}