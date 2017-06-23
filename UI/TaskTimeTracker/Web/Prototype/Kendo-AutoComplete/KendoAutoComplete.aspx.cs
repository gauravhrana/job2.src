using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationContainer.UI.Web.Prototype.Sample
{
	public partial class KendoAutoComplete : Framework.UI.Web.BaseClasses.PageBasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		protected void Submit(object sender, EventArgs e)
		{
			string customerName = Request.Form[autoComplete.UniqueID];
			string customerId = Request.Form[hfCustomerId.UniqueID];
		}
	}
}