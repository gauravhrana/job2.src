using DataModel.Framework.Core;
using Framework.Components.Core;
using Newtonsoft.Json.Linq;
using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Web.Api.Controllers
{
    public class MenuController : ApiController
    {
        [System.Web.Http.AcceptVerbs("GET")]
        public IEnumerable<MenuDataModel> GetUserPreferenceMenu()
        {
            var data = new MenuDataModel();

            var listUPMenu = SessionVariables.UserPreferedMenuData;

            return listUPMenu;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public JObject GetApplicationCode()
        {
            var jObject = new JObject();

            jObject["Result"] = SessionVariables.CurrentApplicationCode;

            return jObject;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public JObject SetApplicationModule(string value)
        {
            var newApplicationModule = new JavaScriptSerializer().Deserialize<string>(value);
            SessionVariables.CurrentApplicationModuleCode = newApplicationModule;

            var jObject = new JObject();

            jObject["Result"] = true;

            return jObject;
        }
    }
}
