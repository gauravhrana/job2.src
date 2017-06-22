using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.TasksAndWorkFlow;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.TasksAndWorkflow.TaskEntity.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables        				

        public TaskEntityDataModel SearchParameters
        {
			get
			{
				var data = new TaskEntityDataModel();


				if (SearchParametersRepeater.Items.Count != 0 && PreferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   StandardDataModel.StandardDataColumns.Name + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(StandardDataModel.StandardDataColumns.Name) != "")
				{
					data.Name = CheckAndGetFieldValue(StandardDataModel.StandardDataColumns.Name).ToString();
				}

				if (SearchParametersRepeater.Items.Count != 0 && PreferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   TaskEntityDataModel.DataColumns.TaskEntityTypeId + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(TaskEntityDataModel.DataColumns.TaskEntityTypeId) != "")
				{
					data.TaskEntityTypeId = Convert.ToInt32(CheckAndGetFieldValue(
					   TaskEntityDataModel.DataColumns.TaskEntityTypeId));
				}

                GroupBy = GetParameterValue("GroupBy");

                SubGroupBy = GetParameterValue("SubGroupBy");

				return data;
			}
			
        }

		#endregion

		#region Methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("TaskEntityTypeId"))
			{
				var taskEntityTypeData = Framework.Components.TasksAndWorkflow.TaskEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
								UIHelper.LoadDropDown(taskEntityTypeData, dropDownListControl, StandardDataModel.StandardDataColumns.Name,
								   TaskEntityTypeDataModel.DataColumns.TaskEntityTypeId);
			}

            if (fieldName.Equals("GroupBy") || fieldName.Equals("SubGroupBy"))
            {
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

			PrimaryEntityKey = "TaskEntity";
			FolderLocationFromRoot = "TasksAndWorkFlow/TaskEntity";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskEntity;

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion		

    }
}