using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.WBS.TaskAlgorithmItem.Controls
{
	public partial class SearchFilter : ControlSearchFilter
	{

		#region variables

		public TaskAlgorithmItemDataModel SearchParameters
		{
			get
			{
				var data = new TaskAlgorithmItemDataModel();

				if (SearchParametersRepeater.Items.Count != 0)
				{
					if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(TaskAlgorithmItemDataModel.DataColumns.ActivityId + "Visibility", SettingCategory))
					{
						if (!string.IsNullOrEmpty(CheckAndGetFieldValue(TaskAlgorithmItemDataModel.DataColumns.ActivityId).ToString()))
						{
							if (!CheckAndGetFieldValue(TaskAlgorithmItemDataModel.DataColumns.ActivityId).ToString().Equals("-1"))
							{
								data.ActivityId = Convert.ToInt32(CheckAndGetFieldValue(TaskAlgorithmItemDataModel.DataColumns.ActivityId));
							}
						}
					}

					if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmId + "Visibility", SettingCategory))
					{
						if (!string.IsNullOrEmpty(CheckAndGetFieldValue(TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmId).ToString()))
						{
							if (!CheckAndGetFieldValue(TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmId).ToString().Equals("-1"))
							{
								data.TaskAlgorithmId = Convert.ToInt32(CheckAndGetFieldValue(TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmId));
							}
						}
					}
				}

				return data;
			}
		}

		#endregion

		#region private methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("ActivityId"))
			{
				var activityData = ActivityDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(activityData, dropDownListControl, StandardDataModel.StandardDataColumns.Name, ActivityDataModel.DataColumns.ActivityId);
			}

			if (fieldName.Equals("TaskAlgorithmId"))
			{
                var taskAlgorithmItemIdData = TaskAlgorithmDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(taskAlgorithmItemIdData, dropDownListControl, StandardDataModel.StandardDataColumns.Name, TaskAlgorithmDataModel.DataColumns.TaskAlgorithmId);
			}
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.TaskAlgorithmItem;
			PrimaryEntityKey = "TaskAlgorithmItem";
			FolderLocationFromRoot = "TaskAlgorithmItem";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion
	}
}