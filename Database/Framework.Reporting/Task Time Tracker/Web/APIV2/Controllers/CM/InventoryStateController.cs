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
	public class InventoryStateController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<InventoryStateDataModel> GetList(string value, string value1)
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

			var dataQuery = JsonConvert.DeserializeObject<InventoryStateDataModel>(searchString);
			return InventoryStateDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public InventoryStateDataModel GetById(string value)
		{
			var dataQuery = new InventoryStateDataModel();
			dataQuery.InventoryStateId = int.Parse(value);

			var result = InventoryStateDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);
			return result[0];
		}

		[HttpPost]
		public void Create([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataCreate = JsonConvert.DeserializeObject<InventoryStateDataModel>(jsonString);

			dataCreate.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

			InventoryStateDataManager.Create(dataCreate, SessionVariables.RequestProfile);
		}

		[HttpPost]
		public void Update([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataUpdate = JsonConvert.DeserializeObject<InventoryStateDataModel>(jsonString);
			InventoryStateDataManager.Update(dataUpdate, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataDelete = new InventoryStateDataModel();
			dataDelete.InventoryStateId = int.Parse(value);
			InventoryStateDataManager.Delete(dataDelete, SessionVariables.RequestProfile);
		}
	}
}
