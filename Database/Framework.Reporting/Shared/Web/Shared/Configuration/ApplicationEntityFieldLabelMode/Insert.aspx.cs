using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.ApplicationEntityFieldLabelMode
{
	public partial class Insert : Shared.UI.WebFramework.BasePage
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			// RegularExpressionValidator1.ValidationExpression = Shared.Components.RegularExpressionConstants.ValidateAlphaWithLengthLimit(1,40);
			// RegularExpressionValidator3.ValidationExpression = Shared.Components.RegularExpressionConstants.ValidateAlphaWithLengthLimit(1,200);

		}

		private void InsertData()
		{
			var data = new Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.Data();
			
			data.ApplicationEntityFieldLabelModeId	 = myGenericControl.ApplicationEntityFieldLabelModeId; 
			data.Name								 = myGenericControl.Name;
			data.Description						 = myGenericControl.Description;
			data.SortOrder							 = myGenericControl.SortOrder;

			Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.Create(data, AuditId);
		}

		protected void btnInsert_Click(object sender, EventArgs e)
		{
			try
			{
				InsertData();

                Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabelMode", Action = "Default", SetId = true }), false);
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
            Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabelMode", Action = "Default" }), false);
		}
	}
}