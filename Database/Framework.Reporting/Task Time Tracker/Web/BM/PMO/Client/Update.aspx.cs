using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Client
{
	public partial class Update : PageUpdate
    {

		#region Methods

        protected override Control GetTabControl(int setId, Control updateControl)
        {
			var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.Setup("ClientUpdateView");

            var selected = false;

            if (string.IsNullOrEmpty(Request.QueryString["tab"]) || Request.QueryString["tab"] == "1")
            {
                selected = true;
            }

            tabControl.AddTab("Client", updateControl, String.Empty, selected);
            
			// not making sense ?
			selected = false;

            if (Request.QueryString["tab"] == "2")
            {
                selected = true;
            }

            var bucketControl = ApplicationCommon.GetNewBucketControl();
            bucketControl.ConfigureBucket("Project", setId, GetProjectList, GetAssociatedProjects, SaveClientXProject);
            tabControl.AddTab("Project", bucketControl, String.Empty, selected);

            return tabControl;
        }

		private DataTable GetProjectList()
		{
            var dt = ProjectDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedProjects(int clientId)
		{
			var dt = ClientXProjectDataManager.GetByClient(clientId, SessionVariables.RequestProfile.AuditId);
			return dt;
		}

		private void SaveClientXProject(int clientId, List<int> projectIds)
		{
			ClientXProjectDataManager.DeleteByClient(clientId, SessionVariables.RequestProfile.AuditId);
            ClientXProjectDataManager.CreateByClient(clientId, projectIds.ToArray(), SessionVariables.RequestProfile);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity		= SystemEntity.Client;
			
			GenericControlPath	= ApplicationCommon.GetControlPath("Client", ControlType.GenericControl);
			PrimaryPlaceHolder	= plcUpdateList;
			PrimaryEntityKey	= "Client";
			BreadCrumbObject	= Master.BreadCrumbObject;

            BtnUpdate			= btnUpdate;
            BtnClone			= btnClone;
            BtnCancel			= btnCancel;
		}
		
        #endregion

    }
}