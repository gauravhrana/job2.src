using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Framework.Components.DataAccess;

namespace MvcWebAPISample.Controllers
{
    public class UserLoginsController : ApiController
    {

        // GET api/<controller>
        public IEnumerable<Framework.Components.LogAndTrace.UserLoginHistoryDataModel> Get(string userId)
        {
            var auditId = 0;
            if (!string.IsNullOrEmpty(userId))
            {
                auditId = Convert.ToInt32(userId);
            }

			var requestProfile = new RequestProfile();
			requestProfile.AuditId = auditId;
			requestProfile.ApplicationId = Convert.ToInt32(ConfigurationManager.AppSettings["StartupApplicationId"]);

            var data = new Framework.Components.LogAndTrace.UserLoginHistoryDataModel();
            data.UserId = auditId;
			var list = Framework.Components.LogAndTrace.UserLoginHistoryDataManager.GetLastUrls(data, 7, requestProfile);
            return list;
        }

    }
}
