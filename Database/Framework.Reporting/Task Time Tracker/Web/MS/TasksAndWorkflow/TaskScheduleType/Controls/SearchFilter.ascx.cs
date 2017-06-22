using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.TasksAndWorkFlow;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;

namespace Shared.UI.Web.TasksAndWorkflow.TaskScheduleType.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables

        public TaskScheduleTypeDataModel SearchParameters
        {
            get
            {
                var data = new TaskScheduleTypeDataModel();

                GetParameterValue(data, StandardDataModel.StandardDataColumns.Name);

                GroupBy = GetParameterValue("GroupBy");

                SubGroupBy = GetParameterValue("SubGroupBy");

                return data;
            }
			
        }		

        #endregion

		#region Events

        public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
        {
            base.LoadDropDownListSources(fieldName, dropDownListControl);            

            if (fieldName.Equals("GroupBy") || fieldName.Equals("SubGroupBy"))
            {
            }
        }


		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskScheduleType;
			PrimaryEntityKey = "TaskScheduleType";
			FolderLocationFromRoot = "TasksAndWorkFlow/TaskScheduleType";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion

    }
}