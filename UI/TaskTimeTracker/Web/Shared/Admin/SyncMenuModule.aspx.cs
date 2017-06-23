using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.ApplicationUser;
using Framework.Components.Core;
using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shared.UI.Web.Admin
{
	public partial class SyncMenuModule : Framework.UI.Web.BaseClasses.PageBasePage
    {
        private void BindApplications()
        {
            var applications = ApplicationDataManager.GetList(SessionVariables.RequestProfile);

            UIHelper.LoadDropDown(applications, drpSourceApplication,
                ApplicationDataModel.DataColumns.Name, ApplicationDataModel.DataColumns.ApplicationId);

            UIHelper.LoadDropDown(applications, drpTargetApplication,
                ApplicationDataModel.DataColumns.Name, ApplicationDataModel.DataColumns.ApplicationId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindApplications();
            }
        }

        protected void btnSync_Click(object sender, EventArgs e)
        {
            var sourceModule = txtSourceModule.Text.Trim();
            var targetModule = txtTargetModule.Text.Trim();

            if (!string.IsNullOrEmpty(sourceModule) && !string.IsNullOrEmpty(targetModule))
            {
                var sourceApplicationId = int.Parse(drpSourceApplication.SelectedValue);
                var targetApplicationId = int.Parse(drpTargetApplication.SelectedValue);

                if (sourceApplicationId != targetApplicationId)
                {
                    MenuDataManager.SyncModule(targetModule, targetApplicationId, sourceModule, sourceApplicationId, SessionVariables.RequestProfile);

                    Response.Write("Menu Module Sync Complete."); 
                }
                else
                {
                    Response.Write("Source And Target Application can not be same.");
                }
            }
            else 
            { 
                Response.Write("Source or Target Module can not be Empty."); 
            }
        }
    }
}