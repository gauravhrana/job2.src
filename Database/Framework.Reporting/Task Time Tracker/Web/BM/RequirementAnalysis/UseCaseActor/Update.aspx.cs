using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.TaskTimeTracker.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.UseCaseActor
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
			var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetByUseCaseActorXUseCase(useCaseId, SessionVariables.RequestProfile.AuditId);
            return dt;
        }

        private string[] GetUseCaseColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UseCase, "DBColumns", SessionVariables.RequestProfile);
        }

        private DataTable GetUseCaseData(int useCaseId)
        {
			var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetByUseCaseActorXUseCase(useCaseId, SessionVariables.RequestProfile.AuditId);
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

            tabControl.Setup("UseCaseActorUpdateView");

            tabControl.AddTab("UseCaseActor", detailsControl, "Use Case Actor", true);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("UseCaseActorXUseCase", listControl);

			listControl.Setup("UseCaseActorXUseCase", "UseCaseActorXUseCase", "UseCaseActorXUseCaseId", setId, true, GetData, GetUseCaseActorXUseCaseColumns, "UseCaseActorXUseCase");
            listControl.SetSession("true");


            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetUseCaseActorXUseCaseData(int.Parse(key));
		}

        private DataTable GetUseCaseActorXUseCaseData(int UseCaseActorXUseCaseId)
        {           
            var data = new UseCaseActorXUseCaseDataModel();
            data.UseCaseActorId = UseCaseActorXUseCaseId;

            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorXUseCaseDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        private string[] GetUseCaseActorXUseCaseColumns()
        {

            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UseCaseActorXUseCase, "DBColumns", SessionVariables.RequestProfile);
        }

        #endregion

        #region

        protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseActor;

			GenericControlPath = ApplicationCommon.GetControlPath("UseCaseActor", ControlType.GenericControl);
			PrimaryPlaceHolder = plcUpdateList;
			PrimaryEntityKey = "UseCaseActor";
			BreadCrumbObject = Master.BreadCrumbObject;

			BtnUpdate = btnUpdate;
			BtnClone = btnClone;
			BtnCancel = btnCancel;

        }

        #endregion

    }
}