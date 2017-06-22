using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;


namespace Web.Api.Controllers
{
	//[Authorize]
	public class LanguageController : ApiController
	{

		// GET api/summary/GetList
		public IEnumerable<LanguageDataModel> GetList()
		{
			var dataQuery = new LanguageDataModel();

			return LanguageDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
		}

		public void Create([FromBody]LanguageDataModel data)
		{
			LanguageDataManager.Create(data, SessionVariables.RequestProfile);
		}

		public void Update([FromBody]LanguageDataModel data)
		{
			LanguageDataManager.Update(data, SessionVariables.RequestProfile);
		}

		public LanguageDataModel GetById(string value)
		{
			var dataQuery = new LanguageDataModel();

			dataQuery.LanguageId = int.Parse(value);

			var result = LanguageDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);

			return result[0];
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataQuery = new LanguageDataModel();
			dataQuery.LanguageId = int.Parse(value);
			LanguageDataManager.Delete(dataQuery, SessionVariables.RequestProfile);
		}

	}
}
