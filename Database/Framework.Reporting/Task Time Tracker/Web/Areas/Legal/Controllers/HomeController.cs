using Framework.Components.DataAccess;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.LegalArea.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /V45/Home/

        public ActionResult Index()
        {
            ApplicationCommon.ResetApplicationCache("LE");
            return View();
        }

    }
}
