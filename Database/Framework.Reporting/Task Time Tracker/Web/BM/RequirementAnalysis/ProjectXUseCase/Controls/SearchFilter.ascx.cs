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
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectXUseCase.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
	{
		public ProjectXUseCaseDataModel SearchParameters
		{
			get
			{
				var data = new ProjectXUseCaseDataModel();


                data.UseCaseId = GetParameterValueAsInt(ProjectXUseCaseDataModel.DataColumns.UseCaseId);

                data.ProjectId = GetParameterValueAsInt(ProjectXUseCaseDataModel.DataColumns.ProjectId);

                data.ProjectUseCaseStatusId = GetParameterValueAsInt(ProjectXUseCaseDataModel.DataColumns.ProjectUseCaseStatusId);

                GroupBy = GetParameterValue("GroupBy");

                SubGroupBy = GetParameterValue("SubGroupBy");


				return data;
			}
		}        

		#region methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("ProjectId"))
			{
                var projectdata = ProjectDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(projectdata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					ProjectDataModel.DataColumns.ProjectId);
			}

			if (fieldName.Equals("UseCaseId"))
			{
                var useCasedata = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(useCasedata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					UseCaseDataModel.DataColumns.UseCaseId);
			}

			if (fieldName.Equals("ProjectUseCaseStatusId"))
			{
                var useCaseProjectStatusdata = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.ProjectUseCaseStatusDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(useCaseProjectStatusdata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					ProjectUseCaseStatusDataModel.DataColumns.ProjectUseCaseStatusId);
			}

            if (fieldName.Equals("GroupBy") || fieldName.Equals("SubGroupBy"))
            {              
            }
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProjectXUseCase;
			PrimaryEntityKey = "ProjectXUseCase";
			FolderLocationFromRoot = "/RequirementAnalysis";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion
	}
}