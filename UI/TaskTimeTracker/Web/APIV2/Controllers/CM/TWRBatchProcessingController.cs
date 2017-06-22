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
	public class TWRBatchProcessingController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<TWRBatchProcessingDataModel> GetList(string value, string value1)
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

			var dataQuery = JsonConvert.DeserializeObject<TWRBatchProcessingDataModel>(searchString);
			return TWRBatchProcessingDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public TWRBatchProcessingDataModel GetById(string value)
		{
			var dataQuery = new TWRBatchProcessingDataModel();
			dataQuery.TWRBatchProcessingId = int.Parse(value);

			var result = TWRBatchProcessingDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);
			return result[0];
		}

		[HttpPost]
		public void Create([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataCreate = JsonConvert.DeserializeObject<TWRBatchProcessingDataModel>(jsonString);

			dataCreate.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

			TWRBatchProcessingDataManager.Create(dataCreate, SessionVariables.RequestProfile);
		}

		[HttpPost]
		public void Update([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataUpdate = JsonConvert.DeserializeObject<TWRBatchProcessingDataModel>(jsonString);
			TWRBatchProcessingDataManager.Update(dataUpdate, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataDelete = new TWRBatchProcessingDataModel();
			dataDelete.TWRBatchProcessingId = int.Parse(value);
			TWRBatchProcessingDataManager.Delete(dataDelete, SessionVariables.RequestProfile);
		}
	}
}
