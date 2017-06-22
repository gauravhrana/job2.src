using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataModel.Framework.Configuration;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;


namespace Web.Api.Controllers
{
    //[Authorize]
    public class ApplicationModeController : ApiController
    {

        // GET api/summary/GetList
        public IEnumerable<ApplicationModeDataModel> GetList()
        {
			var dataQuery = new ApplicationModeDataModel();

			return ApplicationModeDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile);
        }

		public void Create([FromBody]ApplicationModeDataModel data)
		{
			ApplicationModeDataManager.Create(data, SessionVariables.RequestProfile);
		}

		public void Update([FromBody]ApplicationModeDataModel data)
		{
			ApplicationModeDataManager.Update(data, SessionVariables.RequestProfile);
		}

		public ApplicationModeDataModel GetById(string value)
		{
			var dataQuery = new ApplicationModeDataModel();

			dataQuery.ApplicationModeId = int.Parse(value);

			var result = ApplicationModeDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, 1);

			return result[0];
		}

		[System.Web.Http.AcceptVerbs("DELETE", "GET", "POST")]
		public void Delete(string value)
		{
			var dataQuery = new ApplicationModeDataModel();
			dataQuery.ApplicationModeId = int.Parse(value);
			ApplicationModeDataManager.Delete(dataQuery, SessionVariables.RequestProfile);
		}

    }
}
