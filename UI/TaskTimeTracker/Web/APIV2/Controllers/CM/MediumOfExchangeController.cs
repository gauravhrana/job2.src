using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Shared.WebCommon.UI.Web;
using Newtonsoft.Json;
using DataModel.CapitalMarkets;
using CapitalMarkets.Components.BusinessLayer;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Shared.UI.Web;
using System.Web.Http;

namespace Web.Api.Controllers
{
	//[Authorize]
	public class MediumOfExchangeController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<MediumOfExchangeDataModel> GetList(string value, string value1)
		{
			var settingCategory = value1;
			var searchString = value;

			var dictionaryObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(searchString);

			var dataQuery = JsonConvert.DeserializeObject<MediumOfExchangeDataModel>(value);

			// save search filter parameters in user preference
			if (dictionaryObject != null)
			{
				foreach (var searchFilterColumnName in dictionaryObject.Keys)
				{
					var searchFilterValue = dictionaryObject[searchFilterColumnName];
					PreferenceUtility.UpdateUserPreference(settingCategory, searchFilterColumnName, dictionaryObject[searchFilterColumnName]);
				}
			}

			return MediumOfExchangeDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public MediumOfExchangeDataModel GetById(string value)
		{
			var dataQuery = new MediumOfExchangeDataModel();
			dataQuery.MediumOfExchangeId = int.Parse(value);

			var result = MediumOfExchangeDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);
			return result[0];
		}

		[HttpPost]
		public void Create([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataCreate = JsonConvert.DeserializeObject<MediumOfExchangeDataModel>(jsonString);

			dataCreate.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

			MediumOfExchangeDataManager.Create(dataCreate, SessionVariables.RequestProfile);
		}

		[HttpPost]
		public void Update([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataUpdate = JsonConvert.DeserializeObject<MediumOfExchangeDataModel>(jsonString);
			MediumOfExchangeDataManager.Update(dataUpdate, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataDelete = new MediumOfExchangeDataModel();
			dataDelete.MediumOfExchangeId = int.Parse(value);
			MediumOfExchangeDataManager.Delete(dataDelete, SessionVariables.RequestProfile);
		}
	}
}