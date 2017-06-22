using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Feature;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.WBS.TaskFormulation.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables     		

		public TaskFormulationDataModel SearchParameters
        {
            get
            {
				var data = new TaskFormulationDataModel();

				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   TaskFormulationDataModel.DataColumns.FeatureId + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(TaskFormulationDataModel.DataColumns.FeatureId) != "-1")
				{
					data.FeatureId = Convert.ToInt32(
						CheckAndGetFieldValue(TaskFormulationDataModel.DataColumns.FeatureId));
				}

				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   TaskFormulationDataModel.DataColumns.ProjectId + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(TaskFormulationDataModel.DataColumns.ProjectId) != "-1")
				{
					data.ProjectId = Convert.ToInt32(
						CheckAndGetFieldValue(TaskFormulationDataModel.DataColumns.ProjectId));
				}

				if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   TaskFormulationDataModel.DataColumns.TaskId + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(TaskFormulationDataModel.DataColumns.TaskId) != "-1")
				{
					data.TaskId = Convert.ToInt32(
						CheckAndGetFieldValue(TaskFormulationDataModel.DataColumns.TaskId));
				}
								
                return data;
            }
        }
	
        #endregion

		#region private methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("ProjectId"))
			{
                var projectData = ProjectDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(projectData, dropDownListControl, StandardDataModel.StandardDataColumns.Name, ProjectDataModel.DataColumns.ProjectId);
			}

			if (fieldName.Equals("TaskId"))
			{
                var taskData = TaskTimeTracker.Components.BusinessLayer.Task.TaskDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(taskData, dropDownListControl,
							StandardDataModel.StandardDataColumns.Name,
									   TaskDataModel.DataColumns.TaskId);
			}

			if (fieldName.Equals("FeatureId"))
			{
                var featureData = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(featureData, dropDownListControl,
							StandardDataModel.StandardDataColumns.Name,
							FeatureDataModel.DataColumns.FeatureId);
			}

		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskFormulation;
			PrimaryEntityKey = "TaskFormulation";
			FolderLocationFromRoot = "TaskFormulation";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion        

    }
}