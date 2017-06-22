using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.About
{
    public partial class Default : System.Web.UI.Page
    {
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {

                Response.Redirect("Insert.aspx", false);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}