using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shared.WebCommon.UI.Web;
using Newtonsoft.Json;
using DataModel.ReferenceData;
using DayCare.Components.BusinessLayer;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;

namespace Web.Api.Controllers
{
	//[Authorize]
	public class MallController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<MallDataModel> GetList(string value, string value1)
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

			var dataQuery = JsonConvert.DeserializeObject<MallDataModel>(searchString);
			return MallDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public MallDataModel GetById(string value)
		{

			var dataQuery = new MallDataModel();
			dataQuery.MallId = int.Parse(value);
			var result = MallDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);
			return result[0];

		}

		public void Create([FromBody]MallDataModel data)
		{
			MallDataManager.Create(data, SessionVariables.RequestProfile);
		}

		public void Update([FromBody]MallDataModel data)
		{
			MallDataManager.Update(data, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataQuery = new MallDataModel();
			dataQuery.MallId = int.Parse(value);
			MallDataManager.Delete(dataQuery, SessionVariables.RequestProfile);
		}
	}
}
