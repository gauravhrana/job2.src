using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Web
{
    public partial class About : System.Web.UI.Page
    {
        protected DateTime date;

        public string GetLink(string JiraID)
        {
            string str = @"http://ivr-app-jra-01:8080/browse/";
            return str + JiraID;
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //date = DateTime.Now;
            //DataBind();

        }
    }
}
