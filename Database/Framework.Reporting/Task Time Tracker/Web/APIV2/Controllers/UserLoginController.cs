using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data;
using Framework.Components.LogAndTrace;
using Shared.WebCommon.UI.Web;
using Newtonsoft.Json;

namespace Web.Api.Controllers
{
	//[Authorize]
	public class UserLoginController : ApiController
	{
		// GET api/summary/GetList
        public List<UserLoginDataModel> GetList()
		{
			return UserLoginMongoDbDataManager.GetList();
		}

		[System.Web.Http.AcceptVerbs("GET")]
        public List<UserLoginDataModel> GetListByUserName(string value)
		{
			var dataQuery = new UserLoginDataModel();
            dataQuery.UserName = JsonConvert.DeserializeObject(value).ToString();

			return UserLoginMongoDbDataManager.GetList(dataQuery, SessionVariables.RequestProfile);
		}
	}
}