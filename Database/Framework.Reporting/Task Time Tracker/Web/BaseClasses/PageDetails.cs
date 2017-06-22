using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;

namespace Framework.UI.Web.BaseClasses
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:PageDetails runat=server></{0}:PageDetails>")]
	public class PageDetails : PageCommon
    {
		protected override void OnPreInit(EventArgs e)
        {
            base.SetSiteMasterPagePath();

			base.OnPreInit(e);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			// done here, because its not in view state
			ViewName = "Details";
		}

        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            var isVisible = ((CheckBox)sender).Checked;

            foreach (var control in PrimaryPlaceHolder.Controls)
            {
                try
                {
	                var tabControl = control as DetailTabControl;

	                if (tabControl != null)
                    {
                        ((ControlCommon)tabControl.FindControl("Details")).IsHistoryVisible = isVisible;
                    }
                    else
                    {
                        ((ControlCommon)control).IsHistoryVisible = isVisible;
                    }
                }
                catch { }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

			SuperKey = ApplicationCommon.GetSuperKey();

            if (!string.IsNullOrEmpty(SuperKey))
            {
                var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(PrimaryEntity.Value(), SuperKey);

	            foreach (var entityKey in lstEntityKeys)
	            {
                    AddDetailControl(false, entityKey);
                }

	            return;
            }

			SetId = ApplicationCommon.GetSetId();
            if (SetId != 0)
            {
                AddDetailControl(true, SetId);
            }
        }

		protected virtual void btnDelete_Click(object sender, EventArgs e)
		{
			Redirect("Delete");
		}

		protected virtual void btnBack_Click(object sender, EventArgs e)
		{
			Response.Redirect(Page.GetRouteUrl(PrimaryEntityKey + "EntityRoute", new { Action = "Default" }), false);
		}

		protected virtual void btnUpdate_Click(object sender, EventArgs e)
		{
			Redirect("Update");
		}

	    protected virtual void btnClone_Click(object sender, EventArgs e)
		{
			Response.Redirect(Page.GetRouteUrl(PrimaryEntityKey + "EntityRoute", new { Action = "Clone", SetId = SetId }), false);
        }

    }
}
