using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.TaskTimeTracker.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.UseCasePackage
{
	public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
	{


        #region Methods

		//private DataTable GetUseCasePackageList()
		//{
		//	var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackage.GetList(AuditId);
		//	return dt;
		//}

		//private DataTable GetAssociatedUseCasePackages(int useCaseId)
		//{
		//	var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCase.GetByUseCase(useCaseId, AuditId);
		//	return dt;
		//}

		//private void SaveUseCasePackageXUseCase(int useCaseId, List<int> UseCasePackageIds)
		//{
		//	TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCase.DeleteByUseCase(useCaseId, AuditId);
		//	TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCase.CreateByUseCase(useCaseId, UseCasePackageIds.ToArray(), AuditId);
		//}

		//private string[] GetUseCasePackageColumns()
		//{
		//	return Components.BusinessLayer.ApplicationSecurity.GetUseCasePackageColumns("DBColumns", SessionVariables.RequestProfile);
		//}

		//private DataTable GetUseCasePackageData(int useCaseId)
		//{
		//	var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCase.GetByUseCase(useCaseId, AuditId);
		//	var fdt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCase.GetList(AuditId);
		//	var resultdt = fdt.Clone();
		//	foreach (DataRow row in dt.Rows)
		//	{
		//		var rows = fdt.Select("UseCasePackageId = " + row[UseCasePackageDataModel.DataColumns.UseCasePackageId]);
		//		resultdt.ImportRow(rows[0]);
		//	}
		//	return resultdt;
		//}


        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();
            tabControl.AddTab("Use Case Package", detailsControl, String.Empty, true);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("Use Case", listControl);

            listControl.Setup("UseCase", "UseCase", "UseCaseId", setId, true, GetUseCaseData, GetUseCaseColumns, "UseCase");
            listControl.SetSession("true");

			//var listControlUP = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

			//tabControl.AddTab("UseCasePackageXUseCase", listControlUP);

			//listControlUP.Setup("UseCasePackageXUseCase", "UseCasePackageXUseCase", "UseCasePackageXUseCaseId", setId, true, GetData, GetUseCasePackageXUseCaseColumns, "UseCasePackageXUseCase");
			//listControlUP.SetSession("true");

            tabControl.Setup("UseCaseUpdateView");


            return tabControl;
        }

		private System.Data.DataTable GetUseCaseData(string key)
		{
			return GetUseCaseData(int.Parse(key));
		}

		//private System.Data.DataTable GetData(string key)
		//{
		//	return GetUseCasePackageXUseCaseData(int.Parse(key));
		//}

		private DataTable GetUseCaseData(int useCasePackageId)
		{
			var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.GetByUseCasePackage(useCasePackageId, SessionVariables.RequestProfile.AuditId);
            var fdt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetList(SessionVariables.RequestProfile);
			var resultdt = fdt.Clone();
			foreach (DataRow row in dt.Rows)
			{
				var rows = fdt.Select("UseCaseId = " + row[UseCaseDataModel.DataColumns.UseCaseId]);
				resultdt.ImportRow(rows[0]);
			}
			return resultdt;
		}

		//private System.Data.DataTable GetUseCaseData(int useCaseId)
		//{
		//	var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCase.GetByUseCase(useCaseId, AuditId);
		//	var fdt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCase.GetList(AuditId);
		//	var resultdt = fdt.Clone();
		//	foreach (DataRow row in dt.Rows)
		//	{
		//		var rows = fdt.Select("UseCaseId = " + row[UseCaseDataModel.DataColumns.UseCaseId]);
		//		resultdt.ImportRow(rows[0]);
		//	}
		//	return resultdt;
		//}

        private string[] GetUseCaseColumns()
        {

            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UseCase, "DBColumns", SessionVariables.RequestProfile);
        }

		//private string[] GetUseCasePackageXUseCaseColumns()
		//{
		//	return Components.BusinessLayer.ApplicationSecurity.GetUseCasePackageXUseCaseColumns("DBColumns", SessionVariables.RequestProfile);
		//}

		//private DataTable GetUseCasePackageXUseCaseData(int useCaseId)
		//{
		//	var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCase.GetByUseCase(useCaseId, AuditId);
		//	var fdt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCase.GetList(AuditId);
		//	var resultdt = fdt.Clone();
		//	foreach (DataRow row in dt.Rows)
		//	{
		//		var rows = fdt.Select("UseCasePackageId = " + row[UseCasePackageDataModel.DataColumns.UseCasePackageId]);
		//		resultdt.ImportRow(rows[0]);
		//	}
		//	return resultdt;
		//}


        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCasePackage;

			GenericControlPath = ApplicationCommon.GetControlPath("UseCasePackage", ControlType.GenericControl);
			PrimaryPlaceHolder      = plcUpdateList;
			PrimaryEntityKey        = "UseCasePackage";
			BreadCrumbObject        = Master.BreadCrumbObject;
			
            BtnUpdate               = btnUpdate;
            BtnClone                = btnClone;
            BtnCancel               = btnCancel;

        }

        #endregion

    }
}