using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.LogAndTrace;

namespace ApplicationContainer.UI.Web.Prototype.Mongodb
{
	public partial class Test : Framework.UI.Web.BaseClasses.PageBasePage
	{

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			SampleMongoDbDataManager.Test2();
		}
	}
}