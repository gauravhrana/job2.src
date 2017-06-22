using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using System.Data;
using Shared.UI.Web;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationRole.Controls
{
	public partial class RelationshipTool : Shared.UI.WebFramework.BaseControl
    {        
        // use viewstate so value is presisted across page
        // postback without reintializing
        public int ParentId
        {
            get
            {
                return (int)ViewState["ParentId"];
            }
            set
            {
                ViewState["ParentId"] = value;
            }
        }


        private bool ValidateSaveRule()
        {
            return (ParentId > 0 );
        }

        // should be called by page that holds this control
        public void Show(int parentId, bool showParentInfo, bool showSaveOption)
        {        
            ParentId  = parentId;

            if(showParentInfo)
            {
				var data = new ApplicationUserDataModel();
            	data.ApplicationUserId = ParentId;

				lblPerson.Text = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetFullName(data, SessionVariables.RequestProfile);                
            }

            btnSave.Visible = showSaveOption;
            
            ResetSource();            
            
            PopulateTarget();   
            
            CleanUp();
        }

        public void Save()
        {                            
            if(!ValidateSaveRule())
            {
                throw new Exception("ParentId not Set.");
            }

            // Gather items
            var i = 0;
            var finalList = new int[lstTarget.Items.Count];
            foreach(ListItem itemCurrent in lstTarget.Items)
            {               
                finalList[i++] = int.Parse(itemCurrent.Value); 
            }

            //  Delete all that are previously stored in database
			Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.DeleteByApplicationUser(ParentId, SessionVariables.RequestProfile);

            // Save final list
			Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.CreateByApplicationUser(ParentId, finalList, SessionVariables.RequestProfile);             
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
                    // Response.Write(itemCurrent.ToString());
            
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
       
        private void PopulateTarget()
        {
            // Current Target List
			var CurrentAssignment = Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.GetByApplicationUser(ParentId, SessionVariables.RequestProfile);
            lstTarget.DataSource = CurrentAssignment.DefaultView;            
            lstTarget.DataTextField = "RoleName";
            lstTarget.DataValueField = "ApplicationRoleId";
            lstTarget.DataBind();
        }

        // Popluate the complete list for the source side
        private void ResetSource()
        {
			var ApplicationRoleEntry = Framework.Components.ApplicationUser.ApplicationRoleDataManager.GetList(SessionVariables.RequestProfile);
            lstSource.DataSource = ApplicationRoleEntry.DefaultView;
            lstSource.DataTextField = "Name";
            lstSource.DataValueField = "ApplicationRoleId";
            lstSource.DataBind();
        }

        /// <summary>
        /// the left side should not have any of the values that are on the right side
        // and simlar right should not have any that is on left
        /// </summary>
        private void CleanUp()
        {
            // we don't want get from database again
            // we already have in traget list, so use that
            // var CurrentAssignment = Framework.Components.ApplicationUser.ApplicationUserXApplicationRole.GetByApplicationUser(ParentId);

            foreach(ListItem listItem in lstTarget.Items)
            {               
                ListItem item = new ListItem();
                item.Value = listItem.Value;
                item.Text = listItem.Text;
                lstSource.Items.Remove(item); 
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

        protected void btnSave_Click(object sender, EventArgs e)
        {            
            Save();
        }

    }
}