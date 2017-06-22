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
	public class AdjustmentCategoryController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<AdjustmentCategoryDataModel> GetList(string value, string value1)
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

			var dataQuery = JsonConvert.DeserializeObject<AdjustmentCategoryDataModel>(searchString);
			return AdjustmentCategoryDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public AdjustmentCategoryDataModel GetById(string value)
		{
			var dataQuery = new AdjustmentCategoryDataModel();
			dataQuery.AdjustmentCategoryId = int.Parse(value);

			var result = AdjustmentCategoryDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);
			return result[0];
		}

		[HttpPost]
		public void Create([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataCreate = JsonConvert.DeserializeObject<AdjustmentCategoryDataModel>(jsonString);

			dataCreate.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

			AdjustmentCategoryDataManager.Create(dataCreate, SessionVariables.RequestProfile);
		}

		[HttpPost]
		public void Update([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataUpdate = JsonConvert.DeserializeObject<AdjustmentCategoryDataModel>(jsonString);
			AdjustmentCategoryDataManager.Update(dataUpdate, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataDelete = new AdjustmentCategoryDataModel();
			dataDelete.AdjustmentCategoryId = int.Parse(value);
			AdjustmentCategoryDataManager.Delete(dataDelete, SessionVariables.RequestProfile);
		}
	}
}
