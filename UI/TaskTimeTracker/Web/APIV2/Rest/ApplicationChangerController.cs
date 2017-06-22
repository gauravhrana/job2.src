using DataModel.Framework.AuthenticationAndAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApplicationContainer.UI.Web.APIV2.Rest
{
    public class ApplicationChangerController : ApiController
    {
        public void Put(int id, ApplicationDataModel obj)
        {
            Shared.WebCommon.UI.Web.ApplicationCommon.ResetApplicationCache(obj.Code);
        }
    }
}
