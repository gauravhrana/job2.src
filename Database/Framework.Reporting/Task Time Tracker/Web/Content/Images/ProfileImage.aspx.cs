using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.Images
{
    public partial class ProfileImage : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/Content/images/ProfilePics/Male.ico");
        }

        
    }
}