using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Priority;
using DataModel.TaskTimeTracker.Task;
using Framework.Components.ApplicationUser;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer.Task;
using TaskTimeTracker.Components.Module.Priority;
using TaskTimeTracker.Components.Module.RiskReward;

namespace ApplicationContainer.UI.Web.WBS.TaskPriorityXApplicationUser.Controls
{
    public partial class SearchFilter : ControlSearchFilter
    {
        
				
		public TaskPriorityXApplicationUserDataManager.Data SearchParameters
        {
            get
            {
				var data = new TaskPriorityXApplicationUserDataManager.Data();

				if (CheckIfValueIsValidAsInt(CheckAndGetFieldValue(TaskPriorityXApplicationUserDataManager.DataColumns.ApplicationUserId).ToString()) != null)
					data.ApplicationUserId = Convert.ToInt32(CheckAndGetFieldValue(
					   TaskPriorityXApplicationUserDataManager.DataColumns.ApplicationUserId));

				if (CheckIfValueIsValidAsInt(CheckAndGetFieldValue(TaskPriorityXApplicationUserDataManager.DataColumns.TaskPriorityTypeId).ToString()) != null)
					data.TaskPriorityTypeId = Convert.ToInt32(CheckAndGetFieldValue(
					   TaskPriorityXApplicationUserDataManager.DataColumns.TaskPriorityTypeId));

				if (CheckIfValueIsValidAsInt(CheckAndGetFieldValue(TaskPriorityXApplicationUserDataManager.DataColumns.TaskId).ToString()) != null)
					data.TaskId = Convert.ToInt32(CheckAndGetFieldValue(
					   TaskPriorityXApplicationUserDataManager.DataColumns.TaskId));
				
                return data;
            }
        }
        

		#region private methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("ApplicationUserId"))
			{
				var applicationUserdata = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(applicationUserdata, dropDownListControl,
					ApplicationUserDataModel.DataColumns.FirstName,
					ApplicationUserDataModel.DataColumns.ApplicationUserId);

			}
			if (fieldName.Equals("TaskPriorityTypeId"))
			{
                var taskPriorityTypedata = TaskPriorityTypeDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(taskPriorityTypedata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					TaskPriorityTypeDataModel.DataColumns.TaskPriorityTypeId);

			}
			if (fieldName.Equals("TaskId"))
			{
                var taskdata = TaskDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(taskdata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					TaskDataModel.DataColumns.TaskId);

			}		
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.TaskPriorityXApplicationUser;
			PrimaryEntityKey = "TaskPriorityXApplicationUser";
			FolderLocationFromRoot = "WBS/TaskPriorityXApplicationUser";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion      
        
    }
}