using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using System.Data;
using DataModel.TaskTimeTracker.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.ProjectUseCaseStatus
{
	public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
	{

        #region Methods

        protected override Control GetTabControl(int setId, Control detailsControl)
        {

            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			listControl.Setup("ProjectXUseCase", String.Empty, "ProjectXUseCaseId", setId, true, GetData,
                GetProjectXUseCaseColumns, "ProjectXUseCase");
            listControl.SetSession("true");

            tabControl.Setup("ProjectUseCaseStatusDetailsView");
            tabControl.AddTab("ProjectUseCaseStatus", detailsControl, String.Empty, true);
            tabControl.AddTab("ProjectXUseCase", listControl);

            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetProjectXUseCaseData(int.Parse(key));
		}

        private DataTable GetProjectXUseCaseData(int projectUseCaseStatusId)
        {
            var data = new ProjectXUseCaseDataModel();
            data.ProjectUseCaseStatusId = projectUseCaseStatusId;
            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectXUseCaseDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        private string[] GetProjectXUseCaseColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ProjectXUseCase, "DBColumns", SessionVariables.RequestProfile);
        }


        #endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProjectUseCaseStatus;
			PrimaryEntityKey = "ProjectUseCaseStatus";
			DetailsControlPath = ApplicationCommon.GetControlPath("ProjectUseCaseStatus", ControlType.DetailsControl);
			PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

	}
}