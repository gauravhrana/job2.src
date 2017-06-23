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
using System.Web.Services;
using System.Web.Script.Services;
using System.Text;
using System.IO;

namespace ApplicationContainer.UI.Web.Prototype.Prototype.D3
{
	public partial class CommonWebMethod : Framework.UI.Web.BaseClasses.PageBasePage
	{

		[WebMethod]
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
		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}