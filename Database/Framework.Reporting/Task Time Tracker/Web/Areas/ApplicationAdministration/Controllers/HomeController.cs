using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.ApplicationAdministrationArea.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /V45/Home/

        public ActionResult Index()
        {
            ApplicationCommon.ResetApplicationCache("AA");

            return View();
        }

    }
}
