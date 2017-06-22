using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data;
using Framework.Components.LogAndTrace;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.LogAndTrace;
using Newtonsoft.Json;

namespace Web.Api.Controllers
{
	//[Authorize]
	public class UserLoginStatusController : ApiController
	{
		// GET api/summary/GetList
        public List<UserLoginStatusDataModel> GetList()
		{
			return UserLoginStatusMongoDbDataManager.GetList();
		}

		[System.Web.Http.AcceptVerbs("GET")]
        public List<UserLoginStatusDataModel> GetListByUserName(string value)
		{
			var dataQuery = new DataModel.Framework.LogAndTrace.UserLoginStatusDataModel();
            dataQuery.Name = JsonConvert.DeserializeObject(value).ToString();

			return UserLoginStatusMongoDbDataManager.GetList(dataQuery, SessionVariables.RequestProfile);
		}
	}
}