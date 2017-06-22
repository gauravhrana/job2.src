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
	public class FundXLegalEntityController : ApiController
	{

		public IEnumerable<JObject> GetSourceEntityList(string value)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
			var listResult = new List<JObject>();

			if (sourceEntity == "Fund")
			{
				var dataQuery = new FundDataModel();
				var entityItems = FundDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

				foreach (var obj in entityItems)
				{
					var jObject = new JObject();

					jObject["Text"] = obj.Name;
					jObject["EntityKey"] = obj.FundId;

					listResult.Add(jObject);
				}
			}
			else if(sourceEntity == "LegalEntity")
			{
				var dataQuery = new LegalEntityDataModel();
				var entityItems = LegalEntityDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

				foreach (var obj in entityItems)
				{
					var jObject = new JObject();

					jObject["Text"] = obj.Name;
					jObject["EntityKey"] = obj.LegalEntityId;

					listResult.Add(jObject);
				}
			}

			return listResult;
		}

		public IEnumerable<FundXLegalEntityDataModel> GetEntityRecords(string value, string value1)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
			var dataQuery = new FundXLegalEntityDataModel();

			if (sourceEntity == "Fund")
			{
				var FundId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.FundId = int.Parse(FundId);
			}
			else if(sourceEntity == "LegalEntity")
			{
				var LegalEntityId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.LegalEntityId = int.Parse(LegalEntityId);
			}

			return FundXLegalEntityDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 0);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public JObject AddEntityRecords(string value1, string value2, string value3)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value1);
			var sourceEntityId = new JavaScriptSerializer().Deserialize<string>(value2);
			var targetEntityList = new JavaScriptSerializer().Deserialize<List<string>>(value3);

			foreach (var tmpValue in targetEntityList)
			{
				var data = new FundXLegalEntityDataModel();

				if (sourceEntity == "Fund")
				{
					data.FundId = int.Parse(sourceEntityId);
					data.LegalEntityId = int.Parse(tmpValue);
				}
				else if(sourceEntity == "LegalEntity")
				{
					data.LegalEntityId = int.Parse(sourceEntityId);
					data.FundId = int.Parse(tmpValue);
				}

				FundXLegalEntityDataManager.Create(data, SessionVariables.RequestProfile);
			}

			var jObject = new JObject();
			jObject["Result"] = true;
			return jObject;
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public JObject RemoveEntityRecords(string value, string value1)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
			var dataQuery = new FundXLegalEntityDataModel();

			if (sourceEntity == "Fund")
			{
				var FundId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.FundId = int.Parse(FundId);
			}
			else if (sourceEntity == "LegalEntity")
			{
				var LegalEntityId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.LegalEntityId = int.Parse(LegalEntityId);
			}
			else
			{
				dataQuery.FundId = -1;
				dataQuery.LegalEntityId = -1;
			}

			FundXLegalEntityDataManager.Delete(dataQuery, SessionVariables.RequestProfile);

			var jObject = new JObject();
			jObject["Result"] = true;
			return jObject;
		}
	}
}
