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
	public class GeographicRegionController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<GeographicRegionDataModel> GetList(string value, string value1)
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

			var dataQuery = JsonConvert.DeserializeObject<GeographicRegionDataModel>(searchString);
			return GeographicRegionDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public GeographicRegionDataModel GetById(string value)
		{

			var dataQuery = new GeographicRegionDataModel();
			dataQuery.GeographicRegionId = int.Parse(value);
			var result = GeographicRegionDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);
			return result[0];

		}

		public void Create([FromBody]GeographicRegionDataModel data)
		{
			GeographicRegionDataManager.Create(data, SessionVariables.RequestProfile);
		}

		public void Update([FromBody]GeographicRegionDataModel data)
		{
			GeographicRegionDataManager.Update(data, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataQuery = new GeographicRegionDataModel();
			dataQuery.GeographicRegionId = int.Parse(value);
			GeographicRegionDataManager.Delete(dataQuery, SessionVariables.RequestProfile);
		}
	}
}
