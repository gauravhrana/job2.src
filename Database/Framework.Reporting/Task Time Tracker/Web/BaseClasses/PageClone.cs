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
    [ToolboxData("<{0}:WebCustomControl1 runat=server></{0}:WebCustomControl1>")]

	
	public class PageClone : PageCommon
	{
		protected PlaceHolder PlaceHolderCore								{ get; set; }
		public ControlGeneric PrimaryGenericControl							{ get; set; }

		protected virtual void InsertData()
	    {
		    ;
	    }

		protected virtual void lnkSave_Click(object sender, EventArgs e)
		{
            PrimaryGenericControl.Save("Insert");
			Response.Redirect(Page.GetRouteUrl(PrimaryEntityKey + "EntityRoute", new { Action = "Default", SetId = true }), false);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			ViewName = "Clone";
		}

        protected override void OnPreInit(EventArgs e)
        {
            base.SetSiteMasterPagePath();

            base.OnPreInit(e);
        }

		protected virtual void Page_Load(object sender, EventArgs e)
		{	
			if (!IsPostBack)
			{
				SetId = ApplicationCommon.GetSetId();
				PrimaryGenericControl.SetId(SetId, true);
			}
		}
	}
}