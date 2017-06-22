using System;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationManagement.Development.BucketExample
{
	public partial class SampleByPerson : Shared.UI.WebFramework.BasePage
    {
        private void PopulateDropDownList()
        {
            //parent
            var PersonEntry = Framework.Components.ApplicationUser.ApplicationUser.GetList(SessionVariables.AuditId);
			//drpParent.DataSource = PersonEntry.DefaultView;
			//drpParent.DataTextField = "FirstName";
			//drpParent.DataValueField = "PersonId";
			//drpParent.DataBind();

			////Source List
			//var ApplicationRoleEntry = Framework.Components.ApplicationUser.ApplicationRole.GetList(SessionVariables.AuditId);
			//lstSource.DataSource = ApplicationRoleEntry.DefaultView;
			//lstSource.DataTextField = "Name";
			//lstSource.DataValueField = "ApplicationRoleId";
			//lstSource.DataBind();

            PopulateTarget();

            CleanUp();
        }

        private void PopulateTarget()
        {
            //Current Target List
			//var PersonId = int.Parse(drpParent.SelectedItem.Value);
			//var CurrentAssignment = Framework.Components.ApplicationUser.ApplicationUserXApplicationRole.GetByApplicationUser(PersonId, AuditId);
			//lstTarget.DataSource = CurrentAssignment.DefaultView;
			//lstTarget.DataTextField = "RoleName";
			//lstTarget.DataValueField = "ApplicationRoleId";
			//lstTarget.DataBind();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateDropDownList();
            }

        }

        protected void btnLeft_Click(object sender, EventArgs e)
        {
            //SwitchValues(lstSource, lstTarget);
        }

        protected void btnRight_Click(object sender, EventArgs e)
        {
            //SwitchValues(lstTarget, lstSource);
        }

        public int ParentId
        {
            get
            {
                //return int.Parse(drpParent.SelectedItem.Value);
	            return 01;
			}
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
			////Gather items
			//var i = 0;
            
			//var finalList = new int[lstTarget.Items.Count];
            
			//foreach (ListItem itemCurrent in lstTarget.Items)
			//{
			//    finalList[i++] = int.Parse(itemCurrent.Value);
			//}

			////  Delete all that are previously stored in database
			//Framework.Components.ApplicationUser.ApplicationUserXApplicationRole.DeleteByApplicationUser(ParentId, AuditId);

			////Save final list
			//Framework.Components.ApplicationUser.ApplicationUserXApplicationRole.CreateByApplicationUser(ParentId, finalList, AuditId);
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
                    Response.Write(itemCurrent.ToString());

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

        protected void drpParent_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ResetSource();
            PopulateTarget();
            CleanUp();
        }

        // Popluate the complete list for the source side
        private void ResetSource()
        {
			//var RoleEntry = Framework.Components.ApplicationUser.ApplicationRole.GetList(SessionVariables.AuditId);
			//lstSource.DataSource = RoleEntry.DefaultView;
			//lstSource.DataTextField = "Name";
			//lstSource.DataValueField = "ApplicationRoleId";
			//lstSource.DataBind();
        }

        /// <summary>
        /// 1. the left side should not have any of the values that are on the right side
        // and simlar right should not have any that is on left
        /// </summary>
        private void CleanUp()
        {
			//foreach (ListItem item in lstTarget.Items)
			//{
			//    ListItem newItem = new ListItem();
			//    newItem.Text = item.Text;
			//    newItem.Value = item.Value;
			//    lstSource.Items.Remove(newItem);
			//}
        }

    }
}