using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//ApplicationContainer.UI.Web.Areas.DayCareArea.Controllers
namespace ApplicationContainer.UI.Web.Areas.TimeEntryArea.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /TimeEntry/Home/
        public ActionResult Index()
        {
            return View();
        }
	}
}