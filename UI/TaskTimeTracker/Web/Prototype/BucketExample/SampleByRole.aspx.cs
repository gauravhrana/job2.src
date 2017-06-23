using System;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.ApplicationManagement.Development.BucketExample
{
	public partial class SampleByRole : Framework.UI.Web.BaseClasses.PageBasePage
    {
        private void PopulateDropDownList()
        {
            // parent
			var applicationRoleEntry = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
            drpParent.DataSource = applicationRoleEntry;
            drpParent.DataTextField = "Name";
            drpParent.DataValueField = "ApplicationRoleId";
            drpParent.DataBind();

            // Source List
			var personEntry = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            lstSource.DataSource = personEntry;
            lstSource.DataTextField = "FirstName";
            lstSource.DataValueField = "PersonId";
            lstSource.DataBind();

            PopulateTarget();

            CleanUp();
        }
        
        private void PopulateTarget()
        {
            // Current Target List
            var applicationRoleId = int.Parse(drpParent.SelectedItem.Value);
			var currentAssignment = Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.GetByApplicationRole(applicationRoleId, SessionVariables.RequestProfile);
            lstTarget.DataSource = currentAssignment.DefaultView;            
            lstTarget.DataTextField = "FullName";
            lstTarget.DataValueField = "PersonId";
            lstTarget.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                PopulateDropDownList();
            }
        }        

        protected void btnLeft_Click(object sender, EventArgs e)
        {             
            SwitchValues(lstSource, lstTarget);
        }

        protected void btnRight_Click(object sender, EventArgs e)
        {            
            SwitchValues(lstTarget, lstSource);
        }

        public int ParentId
        {
            get
            {
                return int.Parse(drpParent.SelectedItem.Value);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {                                    
            // Gather items
            var i = 0;
            var finalList = new int[lstTarget.Items.Count];
            foreach(ListItem itemCurrent in lstTarget.Items)
            {               
                finalList[i++] = int.Parse(itemCurrent.Value); 
            }

            //  Delete all that are previously stored in database
			Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.DeleteByApplicationRole(ParentId, SessionVariables.RequestProfile);

            // Save final list
			Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.CreateByApplicationRole(ParentId, finalList, SessionVariables.RequestProfile);             
        }

        private void SwitchValues(ListBox source, ListBox target)
        {
            var listRemoval = new System.Collections.ArrayList();

            // Find the number of items selected in the List and items selected
            // Call the move function equal to the number of items selected
            // Remove from Source list, The items moved

            // iterate through source list
            foreach(ListItem itemCurrent in source.Items)
            {
                if(itemCurrent.Selected == true)
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
                    if(target.SelectedItem !=  null)
                    {
                        target.SelectedItem.Selected = false;
                    }            
                    target.Items.FindByValue(copy.Value).Selected = true; 
                }
            }            

            foreach(ListItem itemToRemove in listRemoval)
            {
                source.Items.Remove(itemToRemove);
            }            
        }

        // Need full description of what the purpose of this funtion is
        private void ApplicationRoleListToNormal()
        {
			var ApplicationRoleEntry = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
            lstSource.DataSource = ApplicationRoleEntry;
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
			var PersonEntry = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            lstSource.DataSource = PersonEntry;
            lstSource.DataTextField = "FirstName";
            lstSource.DataValueField = "PersonId";
            lstSource.DataBind();
        }

        /// <summary>
        /// 1. the left side should not have any of the values that are on the right side
        // and simlar right should not have any that is on left
        /// </summary>
        private void CleanUp()
        {

            var ApplicationRoleId = int.Parse(drpParent.SelectedItem.Value);
			var CurrentAssignment = Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.GetByApplicationRole(ApplicationRoleId, SessionVariables.RequestProfile);

            foreach(DataRow row in CurrentAssignment.Rows)
            {
               ListItem item = new ListItem();

            	var data = new ApplicationUserDataModel();
            	data.ApplicationUserId = int.Parse(row["PersonId"].ToString());

				var oAppUser = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetDetails(data, SessionVariables.RequestProfile);

                item.Value = oAppUser.ApplicationUserId.ToString();
                item.Text = oAppUser.FirstName;
               lstSource.Items.Remove(item); 
            }
        }

    }
}