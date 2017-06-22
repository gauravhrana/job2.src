using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.ApplicationEntityFieldLabel
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
			var data = new Framework.Components.UserPreference.ApplicationEntityFieldLabel.Data();
			
			data.ApplicationEntityFieldLabelId	   = myGenericControl.ApplicationEntityFieldLabelId; 
			data.Name							   = myGenericControl.Name;
			data.Value							   = myGenericControl.Value;
			data.SystemEntityTypeId				   = myGenericControl.SystemEntityTypeId;
			data.Width							   = myGenericControl.Width;
			data.Formatting						   = myGenericControl.Formatting;
			data.ControlType					   = myGenericControl.ControlType;
			data.HorizontalAlignment			   = myGenericControl.HorizontalAlignment;
			data.GridViewPriority				   = myGenericControl.GridViewPriority;
			data.DetailsViewPriority			   = myGenericControl.DetailsViewPriority;
			data.ApplicationEntityFieldLabelModeId = myGenericControl.ApplicationEntityFieldLabelModeId;

			Framework.Components.UserPreference.ApplicationEntityFieldLabel.Create(data, AuditId);
		}

		protected void btnInsert_Click(object sender, EventArgs e)
		{
			try
			{
				InsertData();

                Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabel", Action = "Default", SetId = true }), false);
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
            Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabel", Action = "Default" }), false);
		}
	}
}