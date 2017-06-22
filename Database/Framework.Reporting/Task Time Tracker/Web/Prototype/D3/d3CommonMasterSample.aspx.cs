using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Web.UI.HtmlControls;
using System.Web.WebPages;
using System.Configuration;
using System.Text;
using System.IO;

namespace ApplicationContainer.UI.Web.Prototype.D3
{
	public partial class d3CommonMasterSample : Framework.UI.Web.BaseClasses.PageBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//var tabControl = ApplicationCommon.GetNewDetailTabControl();

			//tabControl.Setup("d3CommonMasterSample");

			//var d3SampleControl = (ApplicationContainer.UI.Web.Prototype.D3.Controls.d3SampleControl)Page.LoadControl("~/Prototype/D3/Controls/d3SampleControl.ascx");
			
			//tabControl.AddTab("d3SampleControl", d3SampleControl,string.Empty,true);
			

			//var d3PieSampleControl = (ApplicationContainer.UI.Web.Prototype.D3.Controls.d3PieSampleControl)Page.LoadControl("~/Prototype/D3/Controls/d3PieSampleControl.ascx");
			
			//tabControl.AddTab("d3PieSampleControl", d3PieSampleControl);

			//testParent.Controls.Add(tabControl);
		}

		[System.Web.Services.WebMethod]
		public static string AjaxGetBarCtrl(string controlName)
		{
			Page page = new Page();

			UserControl control = (UserControl)page.LoadControl(controlName);
			
			page.Controls.Add(control);

			String htmlContent = "";

			using (var textWriter = new StringWriter())
			{
				
				HttpContext.Current.Server.Execute(page, textWriter, false);
				htmlContent = textWriter.ToString();
			}
			return htmlContent;
			
		}
		
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			var SettingCategory = "d3CommonMasterSampleDefaultView";
			var sbm = this.Master.SubMenuObject;

			sbm.SettingCategory = SettingCategory + "SubMenuControl";
			sbm.Setup();
			sbm.GenerateMenu();

			var bcControl = this.Master.BreadCrumbObject;
			bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			bcControl.Setup("");
			bcControl.GenerateMenu();
	    }
	}
}