using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shared.WebCommon.UI.Web;
using Newtonsoft.Json;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Framework.Components.DataAccess;

namespace Web.Api.Controllers
{
	//[Authorize]
	public class FieldConfigurationBaseController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<FieldConfigurationBaseDataModel> GetList(string value, string value1)
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

			var dataQuery = JsonConvert.DeserializeObject<FieldConfigurationBaseDataModel>(searchString);
			return FieldConfigurationBaseDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public FieldConfigurationBaseDataModel GetById(string value)
		{
			var dataQuery = new FieldConfigurationBaseDataModel();
			dataQuery.FieldConfigurationBaseId = int.Parse(value);

			var result = FieldConfigurationBaseDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);
			return result[0];
		}

		[HttpPost]
		public void Create([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataCreate = JsonConvert.DeserializeObject<FieldConfigurationBaseDataModel>(jsonString);

			dataCreate.ApplicationId = SessionVariables.RequestProfile.ApplicationId;

			FieldConfigurationBaseDataManager.Create(dataCreate, SessionVariables.RequestProfile);
		}

		[HttpPost]
		public void Update([FromBody]dynamic data)
		{
			var jsonString = JsonConvert.SerializeObject(data);
			var dataUpdate = JsonConvert.DeserializeObject<FieldConfigurationBaseDataModel>(jsonString);
			FieldConfigurationBaseDataManager.Update(dataUpdate, SessionVariables.RequestProfile);
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataDelete = new FieldConfigurationBaseDataModel();
			dataDelete.FieldConfigurationBaseId = int.Parse(value);
			FieldConfigurationBaseDataManager.Delete(dataDelete, SessionVariables.RequestProfile);
		}
	}
}
