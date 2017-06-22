using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.ApplicationEntityFieldLabel
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
			var setId = 0;

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
			var data = new Framework.Components.UserPreference.ApplicationEntityFieldLabel.Data();

			data.ApplicationEntityFieldLabelId = (int?)myGenericControl.ApplicationEntityFieldLabelId;
			data.Name = myGenericControl.Name;
			data.Value = myGenericControl.Value;
			data.SystemEntityTypeId = myGenericControl.SystemEntityTypeId;
			data.Width = myGenericControl.Width;
			data.Formatting = myGenericControl.Formatting;
			data.ControlType = myGenericControl.ControlType;
			data.HorizontalAlignment = myGenericControl.HorizontalAlignment;           

			Framework.Components.UserPreference.ApplicationEntityFieldLabel.Create(data, AuditId);
		}

		protected void lnkSave_Click(object sender, EventArgs e)
		{

			//Framework.Components.ApplicationUser.Person.Create(int.Parse(txtPersonId.Text), txtLastName.Text, txtFirstName.Text,txtMiddleName.Text);
			InsertData();
            Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabel", Action = "Default", SetId = true }), false);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabel", Action = "Default" }), false);
        }


	}
}