using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.TaskTimeTracker.RequirementAnalysis;


namespace ApplicationContainer.UI.Web.ProjectUseCaseStatus
{
	public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {
        #region Methods  
      
        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.Setup("ProjectUseCaseStatusUpdateView");

            tabControl.AddTab("ProjectUseCaseStatus", detailsControl, "Project Use Case Status", true);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("ProjectXUseCase", listControl);

			listControl.Setup("ProjectXUseCase", "ProjectXUseCase", "ProjectXUseCaseId", setId, true, GetData, GetProjectXUseCaseColumns, "ProjectXUseCase");
            listControl.SetSession("true");


            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetProjectXUseCaseData(int.Parse(key));
		}

        private DataTable GetProjectXUseCaseData(int projectUseCaseStatusId)
        {
			var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectXUseCaseDataManager.GetByProjectUseCaseStatus(projectUseCaseStatusId, SessionVariables.RequestProfile.AuditId);
            var projectXUseCasedt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectXUseCaseDataManager.GetList(SessionVariables.RequestProfile);
            var resultdt = projectXUseCasedt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                var rows = projectXUseCasedt.Select("ProjectXUseCaseId = " + row[ProjectXUseCaseDataModel.DataColumns.ProjectXUseCaseId]);
                resultdt.ImportRow(rows[0]);
            }
            return resultdt;
        }     
        
        private string[] GetProjectXUseCaseColumns()
        {

            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ProjectXUseCase, "DBColumns", SessionVariables.RequestProfile);
        }

        #endregion


        #region

        protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProjectUseCaseStatus;

			GenericControlPath = ApplicationCommon.GetControlPath("ProjectUseCaseStatus", ControlType.GenericControl);
			PrimaryPlaceHolder = plcUpdateList;  
			PrimaryEntityKey = "ProjectUseCaseStatus";
			BreadCrumbObject = Master.BreadCrumbObject;

			BtnUpdate = btnUpdate;
			BtnClone = btnClone;
			BtnCancel = btnCancel;

        }

        #endregion

    }
}