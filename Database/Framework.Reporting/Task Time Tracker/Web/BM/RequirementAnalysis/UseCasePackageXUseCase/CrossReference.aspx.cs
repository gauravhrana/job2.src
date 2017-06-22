using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.RequirementAnalysis.UseCasePackageXUseCase
{
	public partial class CrossReference : Shared.UI.WebFramework.BasePage
	{
		#region Methods

		private DataTable GetUseCasePackageList()
		{
            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedUseCasePackages(int UseCaseId)
		{
			var id = Convert.ToInt32(drpUseCase.SelectedValue);
            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.GetByUseCase(id, SessionVariables.RequestProfile.AuditId);
			return dt;
		}

		private void SaveByUseCase(int UseCaseId, List<int> UseCasePackageIds)
		{
			var id = Convert.ToInt32(drpUseCase.SelectedValue);
			TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.DeleteByUseCase(id, SessionVariables.RequestProfile.AuditId);
            TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.CreateByUseCase(id, UseCasePackageIds.ToArray(), SessionVariables.RequestProfile);
		}

		private DataTable GetUseCaseList()
		{
            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetList(SessionVariables.RequestProfile);
			return dt;
		}

		private DataTable GetAssociatedUseCases(int UseCasePackageId)
		{
			var id = Convert.ToInt32(drpUseCasePackage.SelectedValue);
			var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.GetByUseCasePackage(id, SessionVariables.RequestProfile.AuditId);
			return dt;
		}

		private void SaveByUseCasePackage(int UseCasePackageId, List<int> UseCaseIds)
		{
			var id = Convert.ToInt32(drpUseCasePackage.SelectedValue);
			TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.DeleteByUseCasePackage(id, SessionVariables.RequestProfile.AuditId);
            TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageXUseCaseDataManager.CreateByUseCasePackage(id, UseCaseIds.ToArray(), SessionVariables.RequestProfile);
		}

		private void BindLists()
		{
			drpUseCase.DataSource = GetUseCaseList();
			drpUseCase.DataTextField = StandardDataModel.StandardDataColumns.Name;
			drpUseCase.DataValueField = UseCaseDataModel.DataColumns.UseCaseId;
			drpUseCase.DataBind();

			drpUseCasePackage.DataSource = GetUseCasePackageList();
			drpUseCasePackage.DataTextField =	StandardDataModel.StandardDataColumns.Name;
			drpUseCasePackage.DataValueField =	UseCasePackageDataModel.DataColumns.UseCasePackageId;
			drpUseCasePackage.DataBind();
		}

		#endregion

		#region Events

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);


			SettingCategory = "UseCasePackageXUseCaseDefaultView";

		}

		protected override void OnInit(EventArgs e)
		{

			BindLists();

			BucketOfUseCase.ConfigureBucket("UseCase", 1, GetUseCaseList, GetAssociatedUseCases, SaveByUseCasePackage);
			BucketOfUseCasePackage.ConfigureBucket("UseCasePackage", 1, GetUseCasePackageList, GetAssociatedUseCasePackages, SaveByUseCase);
		}

		protected void drpSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (drpSelection.SelectedValue == "ByUseCasePackage")
			{
				dynUseCasePackage.Visible = true;
				dynUseCase.Visible = false;
				BucketOfUseCase.ReloadBucketList();
			}
			else if (drpSelection.SelectedValue == "ByUseCase")
			{
				dynUseCasePackage.Visible = false;
				dynUseCase.Visible = true;
				BucketOfUseCasePackage.ReloadBucketList();
			}
		}

		protected void drpUseCase_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfUseCasePackage.ReloadBucketList();
		}

		protected void drpUseCasePackage_SelectedIndexChanged(object sender, EventArgs e)
		{
			BucketOfUseCase.ReloadBucketList();
		}

		#endregion
	}
}