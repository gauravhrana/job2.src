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
	public class UserLoginHistoryController : ApiController
	{
		// GET api/summary/GetList
		public List<UserLoginHistoryDataModel> GetList()
		{
            var items = UserLoginHistoryMongoDbDataManager.GetList();
            return items;
		}

		[System.Web.Http.AcceptVerbs("GET")]
        public List<UserLoginHistoryDataModel> GetListByUserName(string value, string value1)
		{			
			var dataQuery = new UserLoginHistoryDataModel();

			dataQuery.UserName = JsonConvert.DeserializeObject(value).ToString();
			dataQuery.ServerName = JsonConvert.DeserializeObject(value1).ToString();

			return UserLoginHistoryMongoDbDataManager.GetList(dataQuery, SessionVariables.RequestProfile);
		}
	}
}