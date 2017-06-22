using System;
using System.Data;
using System.Web.UI;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.PMO.Client
{
	public partial class Details : PageDetails
    {
        #region Methods

        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

			detailsControl.ID = "Details";

            tabControl.Setup("ClientDetailsView");

			tabControl.AddTab("Client", detailsControl, String.Empty, true);

            var listControl = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

			tabControl.AddTab("Project", listControl);

			listControl.Setup("Project", String.Empty, "ProjectId", setId, true, GetData, GetProjectColumns, "Client");
            listControl.SetSession("true");

            return tabControl;
        }

		private DetailTab1Control GetVerticalTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetail1TabControl();

			tabControl.AddTab("Client", detailsControl, "Client", false);

            var listControl = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

			tabControl.AddTab("Project", listControl, "Project");

            listControl.Setup("Project", String.Empty, "ProjectId", setId, true, GetData, GetProjectColumns, "Project");
            listControl.SetSession("true");

            //tabControl.AddTab("Project", "", 2, "ProjectId", setId, true, GetData, GetProjectColumns);
            //tabControl.AddLastTab();
            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetProjectData(int.Parse(key));
		}

        private DataTable GetProjectData(int clientId)
        {
			var dt = ClientXProjectDataManager.GetByClient(clientId, SessionVariables.RequestProfile.AuditId);
            var projectdt = ProjectDataManager.GetList(SessionVariables.RequestProfile);
            var resultdt = projectdt.Clone();

			foreach (DataRow row in dt.Rows)
            {
                var rows = projectdt.Select("ProjectId = " + row[ProjectDataModel.DataColumns.ProjectId]);
                resultdt.ImportRow(rows[0]);
            }

            return resultdt;
        }

        private static string[] GetProjectColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(SystemEntity.Project, "DBColumns", SessionVariables.RequestProfile);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			PrimaryEntity		   = SystemEntity.Client;
	        PrimaryEntityKey	   = "Client";
            DetailsControlPath	   = ApplicationCommon.GetControlPath("Client", ControlType.DetailsControl);
            PrimaryPlaceHolder     = oDetailsControl.PlaceHolderDetails;
			BreadCrumbObject	   = Master.BreadCrumbObject;
        }

        #endregion

    }
}