using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shared.WebCommon.UI.Web;
using Newtonsoft.Json;
using DataModel.CapitalMarkets;
using CapitalMarkets.Components.BusinessLayer;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;

namespace Web.Api.Controllers
{
	//[Authorize]
	public class PortfolioGroupRulesController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<PortfolioGroupRulesDataModel> GetList(string value, string value1)
		{
			var settingCategory = value1;
			var searchString = value;

			var dictionaryObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(searchString);

			// save search filter parameters in user preference
			if (dictionaryObject != null)
			{
				foreach (var searchFilterColumnName in dictionaryObject.Keys)
				{
					var searchFilterValue = dictionaryObject[searchFilterColumnName];
					PreferenceUtility.UpdateUserPreference(settingCategory, searchFilterColumnName, dictionaryObject[searchFilterColumnName]);
				}
			}

			var dataQuery = JsonConvert.DeserializeObject<PortfolioGroupRulesDataModel>(searchString);
			return PortfolioGroupRulesDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public PortfolioGroupRulesDataModel GetById(string value)
		{
			var dataQuery = new PortfolioGroupRulesDataModel();
			dataQuery.PortfolioGroupRulesId = int.Parse(value);

			var result = PortfolioGroupRulesDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);
			return result[0];
		}

		[HttpPost]
		public void Create([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataCreate = JsonConvert.DeserializeObject<PortfolioGroupRulesDataModel>(jsonString);

			dataCreate.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

			PortfolioGroupRulesDataManager.Create(dataCreate, SessionVariables.RequestProfile);
		}

		[HttpPost]
		public void Update([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataUpdate = JsonConvert.DeserializeObject<PortfolioGroupRulesDataModel>(jsonString);
			PortfolioGroupRulesDataManager.Update(dataUpdate, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataDelete = new PortfolioGroupRulesDataModel();
			dataDelete.PortfolioGroupRulesId = int.Parse(value);
			PortfolioGroupRulesDataManager.Delete(dataDelete, SessionVariables.RequestProfile);
		}
	}
}
