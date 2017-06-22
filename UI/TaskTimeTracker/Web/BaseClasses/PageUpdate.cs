using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;

namespace Framework.UI.Web.BaseClasses
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:PageUpdate runat=server></{0}:PageUpdate>")]
    public class PageUpdate : PageCommon
    {

        #region Properties & Variables

        protected LinkButton BtnCancel { get; set; }
        protected LinkButton BtnUpdate { get; set; }
        protected LinkButton BtnClone { get; set; }

        #endregion

        #region Events

        protected override void OnPreInit(EventArgs e)
        {
            base.SetSiteMasterPagePath();

            base.OnPreInit(e);
        }

        protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			// done here, because its not in view state
			ViewName = "Update";
		}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SuperKey = ApplicationCommon.GetSuperKey();

			// should be client side ... maybe
            if (!string.IsNullOrEmpty(SuperKey))
            {
                BtnCancel.Visible = true;
                BtnUpdate.Visible = true;
                BtnClone.Visible = false;

				var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(PrimaryEntity.Value(), SuperKey);

                foreach (var entityKey in lstEntityKeys)
                {
                    AddUpdateControl(false, entityKey);
                }

	            return;
            }

			SetId = ApplicationCommon.GetSetId();
            if (SetId != 0)
            {
                BtnUpdate.Visible = true;
                BtnCancel.Visible = true;
                BtnClone.Visible = false;

                AddUpdateControl(true, SetId);
            }
        }

        protected override void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            var isVisible = ((CheckBox)sender).Checked;

            foreach (var control in PrimaryPlaceHolder.Controls)
            {
                try
                {
                    if (control is Shared.UI.Web.Controls.DetailTabControl)
                    {
                        var cntrl = (Shared.UI.Web.Controls.DetailTabControl)control;
                        ((ControlCommon)cntrl.GenericControl).IsHistoryVisible = isVisible;
                    }
                    else
                    {
                        ((ControlCommon)control).IsHistoryVisible = isVisible;
                    }
                }
                catch { }
            }
        }

        override protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (PrimaryPlaceHolder.Controls.Count > 1)
            {
                for (var i = 0; i < PrimaryPlaceHolder.Controls.Count; i++)
                {
                    ((ControlGeneric)PrimaryPlaceHolder.Controls[i]).Save("Update");
                }
            }
            else if (PrimaryPlaceHolder.Controls.Count > 0)
            {
                if (PrimaryPlaceHolder.Controls[0] is Shared.UI.Web.Controls.DetailTabControl)
                {
                    var tabControl = (Shared.UI.Web.Controls.DetailTabControl)PrimaryPlaceHolder.Controls[0];
                    if (tabControl.GenericControl != null)
                    {
                        ((ControlGeneric)tabControl.GenericControl).Save("Update");
                    }
                }
                else
                {
                    ((ControlGeneric)PrimaryPlaceHolder.Controls[0]).Save("Update");
                }
            }

            // To refresh values in the default page on an update.
            Response.Redirect(Page.GetRouteUrl(PrimaryEntityKey + "EntityRoute", new { Action = "Default", SetId = true }), false);
        }

        protected virtual void btnClone_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.GetRouteUrl(PrimaryEntityKey + "EntityRoute", new { Action = "Clone", SetId = SetId }), false);
        }

        #endregion

    }
}