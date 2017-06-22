using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.TaskTimeTracker.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.UseCaseStep
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {

        #region Methods

        private DataTable GetUseCaseList()
        {
            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedUseCases(int useCaseId)
        {
			var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetByUseCaseXUseCaseStep(useCaseId, SessionVariables.RequestProfile.AuditId);
            return dt;
        }

        private string[] GetUseCaseColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UseCase, "DBColumns", SessionVariables.RequestProfile);
        }

        private DataTable GetUseCaseData(int useCaseId)
        {
			var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetByUseCaseXUseCaseStep(useCaseId, SessionVariables.RequestProfile.AuditId);
            var fdt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetList(SessionVariables.RequestProfile);
            var resultdt = fdt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                var rows = fdt.Select("UseCaseId = " + row[UseCaseDataModel.DataColumns.UseCaseId]);
                resultdt.ImportRow(rows[0]);
            }
            return resultdt;

            
        }


        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.Setup("UseCaseStepUpdateView");

            tabControl.AddTab("UseCaseStep", detailsControl, "Use Case Step", true);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("UseCaseXUseCaseStep", listControl);

			listControl.Setup("UseCaseXUseCaseStep", "UseCaseXUseCaseStep", "UseCaseXUseCaseStepId", setId, true, GetData, GetUseCaseXUseCaseStepColumns, "UseCaseXUseCaseStep");
            listControl.SetSession("true");


            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetUseCaseXUseCaseStepData(int.Parse(key));
		}

        private DataTable GetUseCaseXUseCaseStepData(int useCaseXUseCaseStepId)
        {
            //var data = new UseCaseXUseCaseStepDataModel();
            //data.UseCaseXUseCaseStepId = useCaseXUseCaseStepId;
            //var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStep.Search(data, AuditId);
            //var fdt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStep.GetList(AuditId);
            //var resultdt = fdt.Clone();
            //foreach (DataRow row in dt.Rows)
            //{
            //    var rows = fdt.Select(UseCaseXUseCaseStepDataModel.DataColumns.UseCaseXUseCaseStepId + " = " + row[UseCaseXUseCaseStepDataModel.DataColumns.UseCaseXUseCaseStepId]);
            //    resultdt.ImportRow(rows[0]);
            //}
            //return resultdt;
            var data = new UseCaseXUseCaseStepDataModel();
            data.UseCaseStepId = useCaseXUseCaseStepId;

            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseXUseCaseStepDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        private string[] GetUseCaseXUseCaseStepColumns()
        {

            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UseCaseXUseCaseStep, "DBColumns", SessionVariables.RequestProfile);
        }

        #endregion

        #region

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseStep;

            GenericControlPath = ApplicationCommon.GetControlPath("UseCaseStep", ControlType.GenericControl);
            PrimaryPlaceHolder = plcUpdateList;
            PrimaryEntityKey = "UseCaseStep";
            BreadCrumbObject = Master.BreadCrumbObject;

            BtnUpdate = btnUpdate;
            BtnClone = btnClone;
            BtnCancel = btnCancel;

        }

        #endregion

    }
}