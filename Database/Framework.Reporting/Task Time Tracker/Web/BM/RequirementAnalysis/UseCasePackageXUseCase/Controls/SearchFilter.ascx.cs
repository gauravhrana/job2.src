using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.TaskTimeTracker.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.UseCasePackageXUseCase.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
	{

		#region variables

		public UseCasePackageXUseCaseDataModel SearchParameters
		{
			get
			{
				var data = new UseCasePackageXUseCaseDataModel();


				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   UseCasePackageXUseCaseDataModel.DataColumns.UseCaseId + "Visibility", SettingCategory)
				   && !CheckAndGetFieldValue(UseCasePackageXUseCaseDataModel.DataColumns.UseCaseId).ToString().Equals("-1"))
				{
					data.UseCaseId = Convert.ToInt32(CheckAndGetFieldValue(
					   UseCasePackageXUseCaseDataModel.DataColumns.UseCaseId));
				}

				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageId + "Visibility", SettingCategory)
				   && !CheckAndGetFieldValue(
				   UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageId).ToString().Equals("-1"))
				{
					data.UseCasePackageId = Convert.ToInt32(CheckAndGetFieldValue(
					   UseCasePackageXUseCaseDataModel.DataColumns.UseCasePackageId));
				}				

				return data;
			}
		}

		#endregion

		#region methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("UseCasePackageId"))
			{
                var useCasePackagedata = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(useCasePackagedata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					UseCasePackageDataModel.DataColumns.UseCasePackageId);
			}

			if (fieldName.Equals("UseCaseId"))
			{
                var useCasedata = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(useCasedata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					UseCaseDataModel.DataColumns.UseCaseId);
			}
			
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCasePackageXUseCase;
			PrimaryEntityKey = "UseCasePackageXUseCase";
			FolderLocationFromRoot = "/RequirementAnalysis";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion
	}
}