using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserRoleMapping
{
	public partial class ApplicationUserView : Shared.UI.WebFramework.BasePage
	{

		#region properties

		public int ApplicationUserId
		{
			get
			{
				return int.Parse(drpApplicationUser.SelectedItem.Value);
			}
		}

		public int ApplicationRoleId
		{
			get
			{
				return int.Parse(drpApplicationRole.SelectedItem.Value);
			}
		}

		#endregion

		#region private methods

		private void PopulateDropDownList()
		{
			//parent

            var applicationEntryData = Framework.Components.ApplicationUser.ApplicationDataManager.GetList(SessionVariables.RequestProfile);
			drpApplication.DataSource = applicationEntryData;
			drpApplication.DataTextField = "Name";
			drpApplication.DataValueField = "ApplicationId";
			drpApplication.DataBind();

			ApplicationUserDataModel data = new ApplicationUserDataModel();
			//data.ApplicationId = Convert.ToInt32(drpApplication.SelectedItem.Value);
			var ApplicationUserEntry = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

			drpApplicationUser.DataSource = ApplicationUserEntry;
			drpApplicationUser.DataTextField = "Name";
			drpApplicationUser.DataValueField = "ApplicationUserId";
			drpApplicationUser.DataBind();


			if (drpApplicationUser.Items.Count == 0)
			{
				drpApplicationUser.Items.Add(new ListItem("None", "-1"));
			}


			//Source List
            var applicationRoleEntryData = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
			lstSourceApplicationRole.DataSource = applicationRoleEntryData;
			lstSourceApplicationRole.DataTextField = "Name";
			lstSourceApplicationRole.DataValueField = "ApplicationRoleId";
			lstSourceApplicationRole.DataBind();

			PopulateTargetApplicationRole();
			CleanUpApplicationRole();

            drpApplicationRole.DataSource = applicationRoleEntryData;
			drpApplicationRole.DataTextField = "Name";
			drpApplicationRole.DataValueField = "ApplicationRoleId";
			drpApplicationRole.DataBind();

			ApplicationUserEntry = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetEntityDetails(ApplicationUserDataModel.Empty, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
			lstSourceApplicationUser.DataSource = ApplicationUserEntry;
			lstSourceApplicationUser.DataTextField = "Name";
			lstSourceApplicationUser.DataValueField = "ApplicationUserId";
			lstSourceApplicationUser.DataBind();

			PopulateTargetApplicationUser();
			CleanUpApplicationUser();
		}

		private void PopulateTargetApplicationUser()
		{
			// Current Target List
			var ApplicationRoleId = int.Parse(drpApplicationRole.SelectedItem.Value);
			var CurrentAssignment = Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.GetByApplicationRole(ApplicationRoleId, SessionVariables.RequestProfile);
			lstTargetApplicationUser.DataSource = CurrentAssignment.DefaultView;
			lstTargetApplicationUser.DataTextField = "ApplicationUser";
			lstTargetApplicationUser.DataValueField = "ApplicationUserId";
			lstTargetApplicationUser.DataBind();
		}

		private void PopulateTargetApplicationRole()
		{
			//Current Target List
			var ApplicationUserId = int.Parse(drpApplicationUser.SelectedItem.Value);
			var CurrentAssignment = Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.GetByApplicationUser(ApplicationUserId, SessionVariables.RequestProfile);
			lstTargetApplicationRole.DataSource = CurrentAssignment.DefaultView;
			lstTargetApplicationRole.DataTextField = "ApplicationRole";
			lstTargetApplicationRole.DataValueField = "ApplicationRoleId";
			lstTargetApplicationRole.DataBind();
		}

		private void SwitchValues(ListBox source, ListBox target)
		{
			var listRemoval = new System.Collections.ArrayList();

			// Find the number of items selected in the List and items selected
			// Call the move function equal to the number of items selected
			// Remove from Source list, The items moved

			// iterate through source list
			foreach (ListItem itemCurrent in source.Items)
			{
				if (itemCurrent.Selected == true)
				{
					// 1. DETERIMNE - find out which item(s) was selected of SOURCE LIST
					//Response.Write(itemCurrent.ToString());

					// 2. MOVE / COPY - Add it to TARGET LIST
					var copy = new ListItem(itemCurrent.Text, itemCurrent.Value);
					target.Items.Add(copy);

					// 3. REMOVE - Add to external list so we can remove afterwards from the source
					listRemoval.Add(itemCurrent);

					// 4. Set the moved selection as selected, so quickly can move back
					// avoiding the user from reselecting items, disable any preveiously selected items     
					if (target.SelectedItem != null)
					{
						target.SelectedItem.Selected = false;
					}
					target.Items.FindByValue(copy.Value).Selected = true;
				}
			}

			foreach (ListItem itemToRemove in listRemoval)
			{
				source.Items.Remove(itemToRemove);
			}
		}

		// Popluate the complete list for the source side
		private void ResetSourceApplicationRole()
		{
            var roleEntryData = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
			lstSourceApplicationRole.DataSource = roleEntryData;
			lstSourceApplicationRole.DataTextField = "Name";
			lstSourceApplicationRole.DataValueField = "ApplicationRoleId";
			lstSourceApplicationRole.DataBind();
		}

		// Popluate the complete list for the source side
		private void ResetSourceApplicationUser()
		{
			var applicationUserEntryData = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
			lstSourceApplicationUser.DataSource = applicationUserEntryData;
			lstSourceApplicationUser.DataTextField = "Name";
			lstSourceApplicationUser.DataValueField = "ApplicationUserId";
			lstSourceApplicationUser.DataBind();
		}

		/// <summary>
		/// 1. the left side should not have any of the values that are on the right side
		// and simlar right should not have any that is on left
		/// </summary>
		private void CleanUpApplicationRole()
		{
			foreach (ListItem item in lstTargetApplicationRole.Items)
			{
				ListItem newItem = new ListItem();
				newItem.Text = item.Text;
				newItem.Value = item.Value;
				lstSourceApplicationRole.Items.Remove(newItem);
			}
		}

		private void CleanUpApplicationUser()
		{

			var ApplicationRoleId = int.Parse(drpApplicationRole.SelectedItem.Value);
			var CurrentAssignment = Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.GetByApplicationRole(ApplicationRoleId, SessionVariables.RequestProfile);

			foreach (DataRow row in CurrentAssignment.Rows)
			{
				var item = new ListItem();

				var data = new ApplicationUserDataModel();

				data.ApplicationUserId = int.Parse(row["ApplicationUserId"].ToString());

				var obj = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetDetails(data, SessionVariables.RequestProfile);
				item.Value = obj.ApplicationUserId.ToString();
				item.Text = obj.FullName;
				lstSourceApplicationUser.Items.Remove(item);
			}
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

				PopulateDropDownList();
			}

		}

		protected void btnLeftApplicationRole_Click(object sender, EventArgs e)
		{
			SwitchValues(lstSourceApplicationRole, lstTargetApplicationRole);
		}

		protected void btnRightApplicationRole_Click(object sender, EventArgs e)
		{
			SwitchValues(lstTargetApplicationRole, lstSourceApplicationRole);
		}

		protected void btnSaveApplicationRole_Click(object sender, EventArgs e)
		{
			//Gather items
			var i = 0;
			var finalList = new int[lstTargetApplicationRole.Items.Count];
			foreach (ListItem itemCurrent in lstTargetApplicationRole.Items)
			{
				finalList[i++] = int.Parse(itemCurrent.Value);
			}

			if (ApplicationUserId != -1)
			{
				//  Delete all that are previously stored in database
				Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.DeleteByApplicationUser(ApplicationUserId, SessionVariables.RequestProfile);

				//Save final list
				Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.CreateByApplicationUser(ApplicationUserId, finalList, SessionVariables.RequestProfile);
			}
		}

		protected void drpApplicationUser_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			ResetSourceApplicationRole();
			PopulateTargetApplicationRole();
			CleanUpApplicationRole();
		}

		protected void btnLeftApplicationUser_Click(object sender, EventArgs e)
		{
			SwitchValues(lstSourceApplicationUser, lstTargetApplicationUser);
		}

		protected void btnRightApplicationUser_Click(object sender, EventArgs e)
		{
			SwitchValues(lstTargetApplicationUser, lstSourceApplicationUser);
		}

		protected void btnSaveApplicationUser_Click(object sender, EventArgs e)
		{
			// Gather items
			var i = 0;
			var finalList = new int[lstTargetApplicationUser.Items.Count];
			foreach (ListItem itemCurrent in lstTargetApplicationUser.Items)
			{
				finalList[i++] = int.Parse(itemCurrent.Value);
			}

			if (ApplicationRoleId != -1)
			{
				//  Delete all that are previously stored in database
				Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.DeleteByApplicationRole(ApplicationRoleId, SessionVariables.RequestProfile);

				// Save final list
				Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.CreateByApplicationRole(ApplicationRoleId, finalList, SessionVariables.RequestProfile);
			}
		}

		protected void drpApplicationRole_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			ResetSourceApplicationUser();
			PopulateTargetApplicationUser();
			CleanUpApplicationUser();
		}

		protected void drpApplication_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			ApplicationUserDataModel data = new ApplicationUserDataModel();
			//data.ApplicationId = Convert.ToInt32(drpApplication.SelectedItem.Value);
			var ApplicationUserEntry = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

			drpApplicationUser.DataSource = ApplicationUserEntry;
			drpApplicationUser.DataTextField = "Name";
			drpApplicationUser.DataValueField = "ApplicationUserId";
			drpApplicationUser.DataBind();

			if (drpApplicationUser.Items.Count == 0)
			{
				drpApplicationUser.Items.Add(new ListItem("None", "-1"));
			}

			ResetSourceApplicationRole();
			PopulateTargetApplicationRole();
			CleanUpApplicationRole();
		}

		protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (drpSelection.SelectedValue == "ByApplicationUser")
			{
				dynApplicationUser.Visible = true;
				dynApplicationRole.Visible = false;

				ResetSourceApplicationRole();
				PopulateTargetApplicationRole();
				CleanUpApplicationRole();
			}
			else if (drpSelection.SelectedValue == "ByApplicationRoles")
			{
				dynApplicationUser.Visible = false;
				dynApplicationRole.Visible = true;

				ResetSourceApplicationUser();
				PopulateTargetApplicationUser();
				CleanUpApplicationUser();
			}

		}

		#endregion

	}
}