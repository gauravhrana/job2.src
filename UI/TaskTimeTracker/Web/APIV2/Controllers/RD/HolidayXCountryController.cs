using Newtonsoft.Json.Linq;
using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using DataModel.ReferenceData;
using ReferenceData.Components.BusinessLayer;

namespace Web.Api.Controllers
{
	//[Authorize]
	public class HolidayXCountryController : ApiController
	{

		public IEnumerable<JObject> GetSourceEntityList(string value)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
			var listResult = new List<JObject>();

			if (sourceEntity == "Holiday")
			{
				var dataQuery = new HolidayDataModel();
				var entityItems = HolidayDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

				foreach (var obj in entityItems)
				{
					var jObject = new JObject();

					jObject["Text"] = obj.Name;
					jObject["EntityKey"] = obj.HolidayId;

					listResult.Add(jObject);
				}
			}
			else if(sourceEntity == "Country")
			{
				var dataQuery = new CountryDataModel();
				var entityItems = CountryDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

				foreach (var obj in entityItems)
				{
					var jObject = new JObject();

					jObject["Text"] = obj.Name;
					jObject["EntityKey"] = obj.CountryId;

					listResult.Add(jObject);
				}
			}

			return listResult;
		}

		public IEnumerable<HolidayXCountryDataModel> GetEntityRecords(string value, string value1)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
			var dataQuery = new HolidayXCountryDataModel();

			if (sourceEntity == "Holiday")
			{
				var HolidayId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.HolidayId = int.Parse(HolidayId);
			}
			else if(sourceEntity == "Country")
			{
				var CountryId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.CountryId = int.Parse(CountryId);
			}

			return HolidayXCountryDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 0);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public JObject AddEntityRecords(string value1, string value2, string value3)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value1);
			var sourceEntityId = new JavaScriptSerializer().Deserialize<string>(value2);
			var targetEntityList = new JavaScriptSerializer().Deserialize<List<string>>(value3);

			foreach (var tmpValue in targetEntityList)
			{
				var data = new HolidayXCountryDataModel();

				if (sourceEntity == "Holiday")
				{
					data.HolidayId = int.Parse(sourceEntityId);
					data.CountryId = int.Parse(tmpValue);
				}
				else if(sourceEntity == "Country")
				{
					data.CountryId = int.Parse(sourceEntityId);
					data.HolidayId = int.Parse(tmpValue);
				}

				HolidayXCountryDataManager.Create(data, SessionVariables.RequestProfile);
			}

			var jObject = new JObject();
			jObject["Result"] = true;
			return jObject;
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public JObject RemoveEntityRecords(string value, string value1)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
			var dataQuery = new HolidayXCountryDataModel();

			if (sourceEntity == "Holiday")
			{
				var HolidayId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.HolidayId = int.Parse(HolidayId);
			}
			else if (sourceEntity == "Country")
			{
				var CountryId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.CountryId = int.Parse(CountryId);
			}
			else
			{
				dataQuery.HolidayId = -1;
				dataQuery.CountryId = -1;
			}

			HolidayXCountryDataManager.Delete(dataQuery, SessionVariables.RequestProfile);

			var jObject = new JObject();
			jObject["Result"] = true;
			return jObject;
		}
	}
}
