using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;


namespace Web.Api.Controllers
{
	//[Authorize]
	public class FieldConfigurationModeController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<FieldConfigurationModeDataModel> GetList()
		{
			var dataQuery = new FieldConfigurationModeDataModel();

			return FieldConfigurationModeDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public void Create([FromBody]FieldConfigurationModeDataModel data)
		{
			FieldConfigurationModeDataManager.Create(data, SessionVariables.RequestProfile);
		}

		public void Update([FromBody]FieldConfigurationModeDataModel data)
		{
			FieldConfigurationModeDataManager.Update(data, SessionVariables.RequestProfile);
		}

		public FieldConfigurationModeDataModel GetById(string value)
		{
			var dataQuery = new FieldConfigurationModeDataModel();

			dataQuery.FieldConfigurationModeId = int.Parse(value);

			var result = FieldConfigurationModeDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);

			return result[0];
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataQuery = new FieldConfigurationModeDataModel();
			dataQuery.FieldConfigurationModeId = int.Parse(value);
			FieldConfigurationModeDataManager.Delete(dataQuery, SessionVariables.RequestProfile);
		}

	}
}
