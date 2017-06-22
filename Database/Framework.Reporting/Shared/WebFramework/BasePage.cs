using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shared.UI.WebFramework
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:WebCustomControl1 runat=server></{0}:WebCustomControl1>")]
	public class BasePage : System.Web.UI.Page
	{
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				var s = (String)ViewState["Text"];  
				return ((s == null) ? String.Empty : s);
			}

			set
			{
				ViewState["Text"] = value;
			}
		}

        public string SuperKey
        {
            get
            {
				//Convert.ToString is used so that if the object is null it will not raise any exception.
				return Convert.ToString(ViewState["SuperKey"]);	 
            }
            set
            {
                ViewState["SuperKey"] = value;
            }
        }

        public int SetId 
        {
            get
            {
				//Convert changed to cast TTT-3893
				var s = (int)ViewState["SetId"];
				return s;
            }
            set
            {
                ViewState["SetId"] = value;
            }
        }

		protected void SetStatusLabelVisibility()
		{
			var lblStatus = ((Label) Master.FindControl("lblStatus"));
			var isTesting = Convert.ToBoolean(HttpContext.Current.Session["IsTesting"]);

			if (lblStatus != null && !(isTesting))
			{
				lblStatus.Visible = false;
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			SetStatusLabelVisibility();
		}

		public string SettingCategory
		{
			get
			{
				return Convert.ToString(ViewState["SettingCategory"]);
			}
			set
			{
				ViewState["SettingCategory"] = value;
			}
		}
	}
}
