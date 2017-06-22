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
	public class CountryXReligionController : ApiController
	{

		public IEnumerable<JObject> GetSourceEntityList(string value)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
			var listResult = new List<JObject>();

			if (sourceEntity == "Country")
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
			else if(sourceEntity == "Religion")
			{
				var dataQuery = new ReligionDataModel();
				var entityItems = ReligionDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

				foreach (var obj in entityItems)
				{
					var jObject = new JObject();

					jObject["Text"] = obj.Name;
					jObject["EntityKey"] = obj.ReligionId;

					listResult.Add(jObject);
				}
			}

			return listResult;
		}

		public IEnumerable<CountryXReligionDataModel> GetEntityRecords(string value, string value1)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
			var dataQuery = new CountryXReligionDataModel();

			if (sourceEntity == "Country")
			{
				var CountryId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.CountryId = int.Parse(CountryId);
			}
			else if(sourceEntity == "Religion")
			{
				var ReligionId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.ReligionId = int.Parse(ReligionId);
			}

			return CountryXReligionDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 0);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public JObject AddEntityRecords(string value1, string value2, string value3)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value1);
			var sourceEntityId = new JavaScriptSerializer().Deserialize<string>(value2);
			var targetEntityList = new JavaScriptSerializer().Deserialize<List<string>>(value3);

			foreach (var tmpValue in targetEntityList)
			{
				var data = new CountryXReligionDataModel();

				if (sourceEntity == "Country")
				{
					data.CountryId = int.Parse(sourceEntityId);
					data.ReligionId = int.Parse(tmpValue);
				}
				else if(sourceEntity == "Religion")
				{
					data.ReligionId = int.Parse(sourceEntityId);
					data.CountryId = int.Parse(tmpValue);
				}

				CountryXReligionDataManager.Create(data, SessionVariables.RequestProfile);
			}

			var jObject = new JObject();
			jObject["Result"] = true;
			return jObject;
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public JObject RemoveEntityRecords(string value, string value1)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
			var dataQuery = new CountryXReligionDataModel();

			if (sourceEntity == "Country")
			{
				var CountryId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.CountryId = int.Parse(CountryId);
			}
			else if (sourceEntity == "Religion")
			{
				var ReligionId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.ReligionId = int.Parse(ReligionId);
			}
			else
			{
				dataQuery.CountryId = -1;
				dataQuery.ReligionId = -1;
			}

			CountryXReligionDataManager.Delete(dataQuery, SessionVariables.RequestProfile);

			var jObject = new JObject();
			jObject["Result"] = true;
			return jObject;
		}
	}
}
