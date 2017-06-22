using Newtonsoft.Json.Linq;
using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using DataModel.CapitalMarkets;
using CapitalMarkets.Components.BusinessLayer;

namespace Web.Api.Controllers
{
	//[Authorize]
	public class PriceScheduleXPriceListController : ApiController
	{

		public IEnumerable<JObject> GetSourceEntityList(string value)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
			var listResult = new List<JObject>();

			if (sourceEntity == "PriceSchedule")
			{
				var dataQuery = new PriceScheduleDataModel();
				var entityItems = PriceScheduleDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

				foreach (var obj in entityItems)
				{
					var jObject = new JObject();

					jObject["Text"] = obj.Name;
					jObject["EntityKey"] = obj.PriceScheduleId;

					listResult.Add(jObject);
				}
			}
			else if(sourceEntity == "PriceList")
			{
				var dataQuery = new PriceListDataModel();
				var entityItems = PriceListDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

				foreach (var obj in entityItems)
				{
					var jObject = new JObject();

					jObject["Text"] = obj.Name;
					jObject["EntityKey"] = obj.PriceListId;

					listResult.Add(jObject);
				}
			}

			return listResult;
		}

		public IEnumerable<PriceScheduleXPriceListDataModel> GetEntityRecords(string value, string value1)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
			var dataQuery = new PriceScheduleXPriceListDataModel();

			if (sourceEntity == "PriceSchedule")
			{
				var PriceScheduleId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.PriceScheduleId = int.Parse(PriceScheduleId);
			}
			else if(sourceEntity == "PriceList")
			{
				var PriceListId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.PriceListId = int.Parse(PriceListId);
			}

			return PriceScheduleXPriceListDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 0);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public JObject AddEntityRecords(string value1, string value2, string value3)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value1);
			var sourceEntityId = new JavaScriptSerializer().Deserialize<string>(value2);
			var targetEntityList = new JavaScriptSerializer().Deserialize<List<string>>(value3);

			foreach (var tmpValue in targetEntityList)
			{
				var data = new PriceScheduleXPriceListDataModel();

				if (sourceEntity == "PriceSchedule")
				{
					data.PriceScheduleId = int.Parse(sourceEntityId);
					data.PriceListId = int.Parse(tmpValue);
				}
				else if(sourceEntity == "PriceList")
				{
					data.PriceListId = int.Parse(sourceEntityId);
					data.PriceScheduleId = int.Parse(tmpValue);
				}

				PriceScheduleXPriceListDataManager.Create(data, SessionVariables.RequestProfile);
			}

			var jObject = new JObject();
			jObject["Result"] = true;
			return jObject;
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public JObject RemoveEntityRecords(string value, string value1)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
			var dataQuery = new PriceScheduleXPriceListDataModel();

			if (sourceEntity == "PriceSchedule")
			{
				var PriceScheduleId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.PriceScheduleId = int.Parse(PriceScheduleId);
			}
			else if (sourceEntity == "PriceList")
			{
				var PriceListId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.PriceListId = int.Parse(PriceListId);
			}
			else
			{
				dataQuery.PriceScheduleId = -1;
				dataQuery.PriceListId = -1;
			}

			PriceScheduleXPriceListDataManager.Delete(dataQuery, SessionVariables.RequestProfile);

			var jObject = new JObject();
			jObject["Result"] = true;
			return jObject;
		}
	}
}
