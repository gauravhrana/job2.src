using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApplicationContainer.UI.Web.APIV2.Rest
{
    public class LoginController : ApiController
    {
        public bool Get(string username, string password)
        {
            if (username == "test" && password == "test")
            {
                return true;
            }
            return false;
        }
    }
}
