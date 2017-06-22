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

	public class PageUpdateInline : PageCommon
	{	
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			ViewName = "Details";
		}
	}
}