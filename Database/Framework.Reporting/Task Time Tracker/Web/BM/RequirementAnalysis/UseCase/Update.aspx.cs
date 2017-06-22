using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.TaskTimeTracker.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.UseCase
{
	public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {
        #region Methods

        private DataTable GetUseCaseStepList()
        {
            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseStepDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedUseCaseSteps(int useCaseId)
        {
			var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStepDataManager.GetByUseCase(useCaseId, SessionVariables.RequestProfile.AuditId);
            return dt;
        }

        private void SaveUseCaseXUseCaseStep(int useCaseId, List<int> useCaseStepIds)
        {
			TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStepDataManager.DeleteByUseCase(useCaseId, SessionVariables.RequestProfile.AuditId);
            TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStepDataManager.Create(useCaseId, useCaseStepIds.ToArray(), SessionVariables.RequestProfile);
        }

        private string[] GetUseCaseStepColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UseCaseStep, "DBColumns", SessionVariables.RequestProfile);
        }

        private DataTable GetUseCaseStepData(int useCaseId)
        {
			var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseStepDataManager.GetByUseCase(useCaseId, SessionVariables.RequestProfile.AuditId);
            var fdt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseStepDataManager.GetList(SessionVariables.RequestProfile);
            var resultdt = fdt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                var rows = fdt.Select("UseCaseStepId = " + row[UseCaseStepDataModel.DataColumns.UseCaseStepId]);
                resultdt.ImportRow(rows[0]);
            }
            return resultdt;
        }

       
        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();
			tabControl.AddTab("Use Case", detailsControl, String.Empty, true);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

			tabControl.AddTab("Actors", listControl);

			listControl.Setup("UseCaseActorXUseCase", "UseCaseActorXUseCase", "UseCaseActorXUseCaseId", setId, true, GetData, GetUseCaseActorXUseCaseColumns, "UseCaseActorXUseCase");
            listControl.SetSession("true");

            var listControlUA = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

			tabControl.AddTab("Steps", listControlUA);

            listControlUA.Setup("UseCaseXUseCaseStep", "UseCaseXUseCaseStep", "UseCaseXUseCaseStepId", setId, true, GetUseCaseXUseCaseStepData, GetUseCaseXUseCaseStepColumns, "UseCaseXUseCaseStep");
            listControlUA.SetSession("true");

            tabControl.Setup("UseCaseUpdateView");      
                       

            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetUseCaseActorXUseCaseData(int.Parse(key));
		}

		private DataTable GetUseCaseXUseCaseStepData(string key)
		{
			return GetUseCaseXUseCaseStepData(int.Parse(key));
		}

        private DataTable GetUseCaseActorXUseCaseData(int useCaseId)
        {
			var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorXUseCaseDataManager.GetByUseCase(useCaseId, SessionVariables.RequestProfile.AuditId);
            var fdt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorXUseCaseDataManager.GetList(SessionVariables.RequestProfile);
            var resultdt = fdt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                var rows = fdt.Select("UseCaseId = " + row[UseCaseDataModel.DataColumns.UseCaseId]);
                resultdt.ImportRow(rows[0]);
            }
            return resultdt;
        }

        private string[] GetUseCaseActorXUseCaseColumns()
        {  
             return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UseCaseActorXUseCase, "DBColumns", SessionVariables.RequestProfile);
        }

        private string[] GetUseCaseXUseCaseStepColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UseCaseXUseCaseStep, "DBColumns", SessionVariables.RequestProfile);
        }

        private DataTable GetUseCaseXUseCaseStepData(int useCaseId)
        {
			var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStepDataManager.GetByUseCase(useCaseId, SessionVariables.RequestProfile.AuditId);
            var fdt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStepDataManager.GetList(SessionVariables.RequestProfile);
            var resultdt = fdt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                var rows = fdt.Select("UseCaseStepId = " + row[UseCaseStepDataModel.DataColumns.UseCaseStepId]);
                resultdt.ImportRow(rows[0]);
            }
            return resultdt;
		}  
       

        #endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCase;

			GenericControlPath = ApplicationCommon.GetControlPath("UseCase", ControlType.GenericControl);
			PrimaryPlaceHolder = plcUpdateList;
			PrimaryEntityKey   = "UseCase";
			BreadCrumbObject   = Master.BreadCrumbObject;

			BtnUpdate          = btnUpdate;
			BtnClone           = btnClone;
			BtnCancel          = btnCancel;

		}

		#endregion

    }
}