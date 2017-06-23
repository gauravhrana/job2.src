using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.Controls
{
    public partial class ControlDetails : BaseControl
    {

        #region Variables

        public string EntityName
        {
            get;
            set;
        }

        public PlaceHolder PlaceHolderDetails
        {
            get
            {
                return plcHolderDetails;
            }
            set
            {
                plcHolderDetails = value;
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            lblEntityName.Text = EntityName + " Details";
        }

        protected virtual void btnDelete_Click(object sender, EventArgs e)
        {
            //Redirect("Delete");
        }

        protected virtual void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.GetRouteUrl(PrimaryEntityKey + "EntityRoute", new { Action = "Default" }), false);
        }

        protected virtual void btnUpdate_Click(object sender, EventArgs e)
        {
            //Redirect("Update");
        }

        protected virtual void btnClone_Click(object sender, EventArgs e)
        {
            //Response.Redirect(Page.GetRouteUrl(PrimaryEntityKey + "EntityRoute", new { Action = "Clone", SetId = SetId }), false);
        }

        #endregion

    }
}