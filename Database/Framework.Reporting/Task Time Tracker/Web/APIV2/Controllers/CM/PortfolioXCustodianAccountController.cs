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
	public class PortfolioXCustodianAccountController : ApiController
	{

		public IEnumerable<JObject> GetSourceEntityList(string value)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
			var listResult = new List<JObject>();

			if (sourceEntity == "Portfolio")
			{
				var dataQuery = new PortfolioDataModel();
				var entityItems = PortfolioDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

				foreach (var obj in entityItems)
				{
					var jObject = new JObject();

					jObject["Text"] = obj.Name;
					jObject["EntityKey"] = obj.PortfolioId;

					listResult.Add(jObject);
				}
			}
			else if(sourceEntity == "CustodianAccount")
			{
				var dataQuery = new CustodianAccountDataModel();
				var entityItems = CustodianAccountDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);

				foreach (var obj in entityItems)
				{
					var jObject = new JObject();

					jObject["Text"] = obj.Name;
					jObject["EntityKey"] = obj.CustodianAccountId;

					listResult.Add(jObject);
				}
			}

			return listResult;
		}

		public IEnumerable<PortfolioXCustodianAccountDataModel> GetEntityRecords(string value, string value1)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
			var dataQuery = new PortfolioXCustodianAccountDataModel();

			if (sourceEntity == "Portfolio")
			{
				var PortfolioId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.PortfolioId = int.Parse(PortfolioId);
			}
			else if(sourceEntity == "CustodianAccount")
			{
				var CustodianAccountId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.CustodianAccountId = int.Parse(CustodianAccountId);
			}

			return PortfolioXCustodianAccountDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 0);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public JObject AddEntityRecords(string value1, string value2, string value3)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value1);
			var sourceEntityId = new JavaScriptSerializer().Deserialize<string>(value2);
			var targetEntityList = new JavaScriptSerializer().Deserialize<List<string>>(value3);

			foreach (var tmpValue in targetEntityList)
			{
				var data = new PortfolioXCustodianAccountDataModel();

				if (sourceEntity == "Portfolio")
				{
					data.PortfolioId = int.Parse(sourceEntityId);
					data.CustodianAccountId = int.Parse(tmpValue);
				}
				else if(sourceEntity == "CustodianAccount")
				{
					data.CustodianAccountId = int.Parse(sourceEntityId);
					data.PortfolioId = int.Parse(tmpValue);
				}

				PortfolioXCustodianAccountDataManager.Create(data, SessionVariables.RequestProfile);
			}

			var jObject = new JObject();
			jObject["Result"] = true;
			return jObject;
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public JObject RemoveEntityRecords(string value, string value1)
		{
			var sourceEntity = new JavaScriptSerializer().Deserialize<string>(value);
			var dataQuery = new PortfolioXCustodianAccountDataModel();

			if (sourceEntity == "Portfolio")
			{
				var PortfolioId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.PortfolioId = int.Parse(PortfolioId);
			}
			else if (sourceEntity == "CustodianAccount")
			{
				var CustodianAccountId = new JavaScriptSerializer().Deserialize<string>(value1);
				dataQuery.CustodianAccountId = int.Parse(CustodianAccountId);
			}
			else
			{
				dataQuery.PortfolioId = -1;
				dataQuery.CustodianAccountId = -1;
			}

			PortfolioXCustodianAccountDataManager.Delete(dataQuery, SessionVariables.RequestProfile);

			var jObject = new JObject();
			jObject["Result"] = true;
			return jObject;
		}
	}
}
