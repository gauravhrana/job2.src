using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shared.WebCommon.UI.Web;
using Newtonsoft.Json;
using DataModel.DayCare;
using DayCare.Components.BusinessLayer;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;

namespace Web.Api.Controllers
{
	//[Authorize]
	public class MealDetailController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<MealDetailDataModel> GetList(string value, string value1)
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

			var dataQuery = JsonConvert.DeserializeObject<MealDetailDataModel>(searchString);
			return MealDetailDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public MealDetailDataModel GetById(string value)
		{

			var dataQuery = new MealDetailDataModel();
			dataQuery.MealDetailId = int.Parse(value);
			var result = MealDetailDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);
			return result[0];

		}

		public void Create([FromBody]MealDetailDataModel data)
		{
			MealDetailDataManager.Create(data, SessionVariables.RequestProfile);
		}

		public void Update([FromBody]MealDetailDataModel data)
		{
			MealDetailDataManager.Update(data, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataQuery = new MealDetailDataModel();
			dataQuery.MealDetailId = int.Parse(value);
			MealDetailDataManager.Delete(dataQuery, SessionVariables.RequestProfile);
		}
	}
}
