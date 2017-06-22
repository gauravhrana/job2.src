using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shared.WebCommon.UI.Web;
using Newtonsoft.Json;
using DataModel.Legal;
using Legal.Components.BusinessLayer;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;

namespace Web.Api.Controllers
{
	//[Authorize]
	public class CaseTypeController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<CaseTypeDataModel> GetList(string value, string value1)
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

			var dataQuery = JsonConvert.DeserializeObject<CaseTypeDataModel>(searchString);
			return CaseTypeDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public CaseTypeDataModel GetById(string value)
		{
			var dataQuery = new CaseTypeDataModel();
			dataQuery.CaseTypeId = int.Parse(value);

			var result = CaseTypeDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);
			return result[0];
		}

		[HttpPost]
		public void Create([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataCreate = JsonConvert.DeserializeObject<CaseTypeDataModel>(jsonString);

			dataCreate.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

			CaseTypeDataManager.Create(dataCreate, SessionVariables.RequestProfile);
		}

		[HttpPost]
		public void Update([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataUpdate = JsonConvert.DeserializeObject<CaseTypeDataModel>(jsonString);
			CaseTypeDataManager.Update(dataUpdate, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataDelete = new CaseTypeDataModel();
			dataDelete.CaseTypeId = int.Parse(value);
			CaseTypeDataManager.Delete(dataDelete, SessionVariables.RequestProfile);
		}
	}
}
