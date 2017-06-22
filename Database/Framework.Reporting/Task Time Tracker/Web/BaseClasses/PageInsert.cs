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
    [ToolboxData("<{0}:PageInsert runat=server></{0}:PageInsert>")]

    public class PageInsert : PageCommon
    {

        #region Variables

        protected ControlGeneric PrimaryGenericControl { get; set; }

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
			ViewName = "Insert";
		}

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                var entityKey = PrimaryGenericControl.Save("Insert");
				if (PrimaryEntityKey == "Schedule")
				{
					Response.Redirect(Page.GetRouteUrl(PrimaryEntityKey + "EntityRoute", new { Action = "Update", SetId = entityKey.ToString() }), false);
				}
				else
				{
					Response.Redirect(Page.GetRouteUrl(PrimaryEntityKey + "EntityRoute", new { Action = "Default", SetId = true }), false);
				}
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected virtual void btnClone_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.GetRouteUrl(PrimaryEntityKey + "EntityRoute", new { Action = "Clone", SetId = SetId }), false);
        }

		

        #endregion

    }
}