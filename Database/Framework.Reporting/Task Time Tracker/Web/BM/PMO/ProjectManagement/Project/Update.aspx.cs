using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.Components.DataAccess;
using Dapper;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Project
{
	public partial class Update : PageUpdate
    {

        #region Methods

		protected override Control GetTabControl(int setId, Control updateControl)
		{
			var tabControl = ApplicationCommon.GetNewDetailTabControl();

			tabControl.Setup("ProjectUpdateView");

			var selected = false;
			if (string.IsNullOrEmpty(Request.QueryString["tab"]) || Request.QueryString["tab"] == "1")
			{
				selected = true;
			}
			tabControl.AddTab("Project", updateControl, String.Empty, selected);

			selected = false;
			if (Request.QueryString["tab"] == "2")
			{
				selected = true;
			}

			//swecond tab
			//second tab needs bucket control configuration and  special methods
            var bucketControl = ApplicationCommon.GetNewBucketControl();
			bucketControl.ConfigureBucket("Client", ApplicationCommon.GetSetId(), GetClientList, GetAssociatedClients, SaveClientXProject);

			// tabControl.AddTab("Client", String.Empty, selected, bucketControl();
            tabControl.AddTab("Client", bucketControl, String.Empty, selected);

			//third tab
            var bucketControl2 = ApplicationCommon.GetNewBucketControl();
			bucketControl2.ConfigureBucket("Need", ApplicationCommon.GetSetId(), GetNeedList, GetAssociatedNeeds, SaveNeedXProject);


            if (Request.QueryString["tab"] == "3")
            {
                selected = true;
            }
            tabControl.AddTab("Need", bucketControl2, String.Empty, selected);

			return tabControl;


		}

        //Tab Control supporting methods
        private DataTable GetClientList()
        {
            var list = ClientDataManager.GetList(SessionVariables.RequestProfile);
            return list;
        }

		private DataTable GetNeedList()
		{
            var dt = NeedDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

        private DataTable GetAssociatedClients(int projectId)
        {
			var dt = ClientXProjectDataManager.GetByProject(projectId, SessionVariables.RequestProfile.AuditId);
            return dt;
        }

		private DataTable GetAssociatedNeeds(int projectId)
		{
			var dt = ProjectXNeedDataManager.GetByProject(projectId, SessionVariables.RequestProfile.AuditId);
			return dt;
		}

        private void SaveClientXProject(int projectId, List<int> ClientIds)
        {
			ClientXProjectDataManager.DeleteByProject(projectId, SessionVariables.RequestProfile.AuditId);
            ClientXProjectDataManager.CreateByProject(projectId, ClientIds.ToArray(), SessionVariables.RequestProfile);
        }

		private void SaveNeedXProject(int projectId, List<int> NeedIds)
		{
			ProjectXNeedDataManager.DeleteByProject(projectId, SessionVariables.RequestProfile);
            ProjectXNeedDataManager.CreateByProject(projectId, NeedIds.ToArray(), SessionVariables.RequestProfile);
		}

        #endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity      = SystemEntity.Project;

			GenericControlPath = ApplicationCommon.GetControlPath("Project", ControlType.GenericControl);
			PrimaryPlaceHolder = plcUpdateList;
			PrimaryEntityKey   = "Project";
			BreadCrumbObject   = Master.BreadCrumbObject;

			BtnUpdate          = btnUpdate;
			BtnClone           = btnClone;
			BtnCancel          = btnCancel;
		}

		#endregion

    }
}