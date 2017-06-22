using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationRole.Controls
{
    public partial class Relation : Shared.UI.WebFramework.BaseControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        //Shows the ApplicationRoles associated with the Person with the given PersonId
        //
        #region ShowRelationship
        public void ShowRelation(int PersonId, bool showParentInfo)
        {
            // most parent pages taht will use this contorl already know
            // parent details, no need to show explictly again
            if(showParentInfo)
            {
            	var oData = new ApplicationUserDataModel();

            	oData.ApplicationUserId = PersonId;
                var personItem = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetDetails(oData, SessionVariables.RequestProfile);
				if (personItem != null)
				{
					var name = personItem.FirstName + " " + personItem.LastName;
					lblName.Text = name;
				}
            }
            
            // convert to table and bind or explicitly fill in some how.
			var ApplicationRolesTable = Framework.Components.ApplicationUser.ApplicationUserXApplicationRoleDataManager.GetByApplicationUser(PersonId, SessionVariables.RequestProfile);

            foreach(DataRow row in ApplicationRolesTable.Rows)
            {
               var newRow = new TableRow();
               var newCell = new TableCell();
               //newCell.Width = 100;
               newCell.Text = row["RoleName"].ToString();
               newRow.Cells.Add(newCell);
               tblMain2.Rows.Add(newRow);
            }
        }
        #endregion ShowRelationship
    }
}