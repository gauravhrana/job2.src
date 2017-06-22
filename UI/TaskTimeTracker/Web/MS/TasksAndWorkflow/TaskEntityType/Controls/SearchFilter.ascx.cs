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

namespace Shared.UI.Web.TasksAndWorkflow.TaskEntityType.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables
		public TaskEntityTypeDataModel SearchParameters
		{
            get
            {
                var data = new TaskEntityTypeDataModel();

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

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskEntityType;
			PrimaryEntityKey = "TaskEntityType";
			FolderLocationFromRoot = "TasksAndWorkFlow/TaskEntityType";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion
    }
}