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
	public class PerformanceKeyController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<PerformanceKeyDataModel> GetList(string value, string value1)
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

			var dataQuery = JsonConvert.DeserializeObject<PerformanceKeyDataModel>(searchString);
			return PerformanceKeyDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public PerformanceKeyDataModel GetById(string value)
		{
			var dataQuery = new PerformanceKeyDataModel();
			dataQuery.PerformanceKeyId = int.Parse(value);

			var result = PerformanceKeyDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);
			return result[0];
		}

		[HttpPost]
		public void Create([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataCreate = JsonConvert.DeserializeObject<PerformanceKeyDataModel>(jsonString);

			dataCreate.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

			PerformanceKeyDataManager.Create(dataCreate, SessionVariables.RequestProfile);
		}

		[HttpPost]
		public void Update([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataUpdate = JsonConvert.DeserializeObject<PerformanceKeyDataModel>(jsonString);
			PerformanceKeyDataManager.Update(dataUpdate, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataDelete = new PerformanceKeyDataModel();
			dataDelete.PerformanceKeyId = int.Parse(value);
			PerformanceKeyDataManager.Delete(dataDelete, SessionVariables.RequestProfile);
		}
	}
}
