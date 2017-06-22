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
	public class PositionController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<PositionDataModel> GetList(string value, string value1)
		{
			var settingCategory = value1;
			var searchString = value;

			var dictionaryObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(searchString);

			var dataQuery = JsonConvert.DeserializeObject<PositionDataModel>(value);

			// save search filter parameters in user preference
			if (dictionaryObject != null)
			{
				foreach (var searchFilterColumnName in dictionaryObject.Keys)
				{
					var searchFilterValue = dictionaryObject[searchFilterColumnName];
					PreferenceUtility.UpdateUserPreference(settingCategory, searchFilterColumnName, dictionaryObject[searchFilterColumnName]);
				}
			}

			return PositionDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public PositionDataModel GetById(string value)
		{
			var dataQuery = new PositionDataModel();
			dataQuery.PositionId = int.Parse(value);

			var result = PositionDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);
			return result[0];
		}

		[HttpPost]
		public void Create([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataCreate = JsonConvert.DeserializeObject<PositionDataModel>(jsonString);

			dataCreate.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

			PositionDataManager.Create(dataCreate, SessionVariables.RequestProfile);
		}

		[HttpPost]
		public void Update([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataUpdate = JsonConvert.DeserializeObject<PositionDataModel>(jsonString);
			PositionDataManager.Update(dataUpdate, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataDelete = new PositionDataModel();
			dataDelete.PositionId = int.Parse(value);
			PositionDataManager.Delete(dataDelete, SessionVariables.RequestProfile);
		}
	}
}
