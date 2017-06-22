using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.ApplicationEntityFieldLabelMode
{
	public partial class Clone : Shared.UI.WebFramework.BasePage
	{

        private int SetId
        {
            get
            {
                return Convert.ToInt32(ViewState["SetId"]);
            }
            set
            {
                ViewState["SetId"] = value;
            }
        }

		protected void Page_Load(object sender, EventArgs e)
		{
			

			// load on first direct loading of this page
			// don't want to reload everttime, as it would 
			// reset the values the user had put.
			if (!IsPostBack)
			{
				try
				{

                    SetId = ApplicationCommon.GetSetId();
                    myGenericControl.SetId(SetId, true);
				}
				catch (Exception ex)
				{

					System.Diagnostics.Debug.WriteLine(ex.Message);
					//throw
				}

				//LoadData(setId);
			}

		}

		private void InsertData()
		{
			var data = new Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.Data();

			data.ApplicationEntityFieldLabelModeId = (int?)myGenericControl.ApplicationEntityFieldLabelModeId;
			data.Name = myGenericControl.Name;
			data.Description = myGenericControl.Description;
			data.SortOrder = myGenericControl.SortOrder;

			Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.Create(data, AuditId);
		}

		protected void lnkSave_Click(object sender, EventArgs e)
		{

			//Framework.Components.ApplicationUser.Person.Create(int.Parse(txtPersonId.Text), txtLastName.Text, txtFirstName.Text,txtMiddleName.Text);
			InsertData();
            Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabelMode", Action = "Default", SetId = true }), false);
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
            Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabelMode", Action = "Default" }), false);
		}


	}
}