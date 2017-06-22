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
	public class ManagerController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<ManagerDataModel> GetList(string value, string value1)
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
					PerferenceUtility.UpdateUserPreference(settingCategory, searchFilterColumnName, dictionaryObject[searchFilterColumnName]);
				}
			}

			var dataQuery = JsonConvert.DeserializeObject<ManagerDataModel>(searchString);
			return ManagerDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public ManagerDataModel GetById(string value)
		{

			var dataQuery = new ManagerDataModel();
			dataQuery.ManagerId = int.Parse(value);
			var result = ManagerDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);
			return result[0];

		}

		public void Create([FromBody]ManagerDataModel data)
		{
			ManagerDataManager.Create(data, SessionVariables.RequestProfile);
		}

		public void Update([FromBody]ManagerDataModel data)
		{
			ManagerDataManager.Update(data, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataQuery = new ManagerDataModel();
			dataQuery.ManagerId = int.Parse(value);
			ManagerDataManager.Delete(dataQuery, SessionVariables.RequestProfile);
		}
	}
}
