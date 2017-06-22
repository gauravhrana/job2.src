using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.TasksAndWorkFlow;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.TasksAndWorkflow.TaskSchedule.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables      

       
        public TaskScheduleDataModel SearchParameters
        {
			get
			{
				var data = new TaskScheduleDataModel();


				if (SearchParametersRepeater.Items.Count != 0 && PreferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   TaskScheduleDataModel.DataColumns.TaskScheduleTypeId + "Visibility", SettingCategory)
				   && !CheckAndGetFieldValue(TaskScheduleDataModel.DataColumns.TaskScheduleTypeId).ToString().Equals("-1"))
				{
					data.TaskScheduleTypeId = Convert.ToInt32(CheckAndGetFieldValue(TaskScheduleDataModel.DataColumns.TaskScheduleTypeId));
				}

				if (SearchParametersRepeater.Items.Count != 0 && PreferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   TaskScheduleDataModel.DataColumns.TaskEntityId + "Visibility", SettingCategory)
				   && !CheckAndGetFieldValue(TaskScheduleDataModel.DataColumns.TaskEntityId).ToString().Equals("-1"))
				{
					data.TaskEntityId = Convert.ToInt32(CheckAndGetFieldValue(
					   TaskScheduleDataModel.DataColumns.TaskEntityId));
				}								

				return data;
			}						
        }	    

	    #endregion

		#region Methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("TaskScheduleTypeId"))
			{
				var taskScheduleTypeData = Framework.Components.TasksAndWorkflow.TaskScheduleTypeDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(taskScheduleTypeData, dropDownListControl, StandardDataModel.StandardDataColumns.Name,
					TaskScheduleTypeDataModel.DataColumns.TaskScheduleTypeId);
			}
			if (fieldName.Equals("TaskEntityId"))
			{
				var taskEntityData = Framework.Components.TasksAndWorkflow.TaskEntityDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(taskEntityData, dropDownListControl, StandardDataModel.StandardDataColumns.Name,
					TaskEntityDataModel.DataColumns.TaskEntityId);
			}
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			base.OnLoad(e);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "TaskSchedule";
			FolderLocationFromRoot = "TasksAndWorkFlow/TaskSchedule";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskSchedule;

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion
        
    }
}