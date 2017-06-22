using Newtonsoft.Json.Linq;
using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Api.Controllers
{
    public class UserPreferenceUtilityController : ApiController
    {
        [System.Web.Http.AcceptVerbs("GET")]
        public JObject GetUPData(string value, string value1)
        {
            var jObject = new JObject();

            var upKey = value;
            var settingCategoryName = value1;

            jObject["Result"] = PerferenceUtility.GetUserPreferenceByKey(upKey, settingCategoryName);
            return jObject;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public void SetUPData(string value1, string value2, string value3)
        {
            var upKey = value1;
            var settingCategoryName = value2;
            var upValue = value3;

            PerferenceUtility.UpdateUserPreference(settingCategoryName, upKey, upValue);

        }
    }
}
