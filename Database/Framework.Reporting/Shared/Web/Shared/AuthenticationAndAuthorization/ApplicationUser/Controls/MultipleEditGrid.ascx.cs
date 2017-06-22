using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser.Controls
{
	public partial class MultipleEditGrid : Shared.UI.WebFramework.BaseControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				rptrApplicationUser.DataSource = GetData(String.Empty);
				rptrApplicationUser.DataBind();
			}
		}

		private DataTable GetData(string name)
		{
			var data = new ApplicationUserDataModel();
			var AuditId = SessionVariables.RequestProfile.AuditId;
			data.LastName = name;


			var dt = Framework.Components.ApplicationUser.ApplicationUserDataManager.Search(data, SessionVariables.RequestProfile);
			return dt;
		}
		protected void OnItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item ||
				e.Item.ItemType == ListItemType.AlternatingItem)
			{
				var lblApplicationUserId = e.Item.FindControl("lblApplicationUserId") as Label;
				lblApplicationUserId.Text = ((ApplicationUserDataModel)e.Item.DataItem).ApplicationUserId.ToString();

                var txtApplicationUserName = e.Item.FindControl("txtApplicationUserName") as TextBox;
                txtApplicationUserName.Text = ((ApplicationUserDataModel)e.Item.DataItem).ApplicationUserName;

				var txtFirstName = e.Item.FindControl("txtFirstName") as TextBox;
				txtFirstName.Text = ((ApplicationUserDataModel)e.Item.DataItem).FirstName;

				var txtLastName = e.Item.FindControl("txtLastNam") as TextBox;
				txtLastName.Text = ((ApplicationUserDataModel)e.Item.DataItem).LastName;


			}
		}
		protected void lnkSave_Click(object sender, EventArgs e)
		{
			for (var i = 0; i < rptrApplicationUser.Items.Count; i++)
			{
				if (rptrApplicationUser.Items[i].ItemType == ListItemType.Item || rptrApplicationUser.Items[i].ItemType == ListItemType.AlternatingItem)
				{
					Label lblApplicationUserId = rptrApplicationUser.Items[i].FindControl("lblApplicationUserId") as Label;
					TextBox txtFirstName = rptrApplicationUser.Items[i].FindControl("txtFirstName") as TextBox;
					TextBox txtLastName = rptrApplicationUser.Items[i].FindControl("txtLastName") as TextBox;
                    var txtApplicationUserName = rptrApplicationUser.Items[i].FindControl("txtApplicationUserName") as TextBox;
					if (lblApplicationUserId != null)
					{
						var data = new ApplicationUserDataModel();
						var AuditId = SessionVariables.RequestProfile.AuditId;
						data.ApplicationUserId = int.Parse(lblApplicationUserId.Text);
                        data.ApplicationUserName = txtApplicationUserName.Text.Trim();
						data.LastName = txtLastName.Text;
						data.FirstName = txtFirstName.Text;

						Framework.Components.ApplicationUser.ApplicationUserDataManager.Update(data, SessionVariables.RequestProfile);
					}
				}
			}
			Response.Redirect("Default.aspx?Added=true", false);
		}
	}
}