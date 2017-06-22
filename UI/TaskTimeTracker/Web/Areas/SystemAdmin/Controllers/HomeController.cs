using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApplicationContainer.UI.Web.Areas.SystemAdminArea.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /SystemAdmin/Home/
        public ActionResult Index()
        {
            return View();
        }
	}
}