using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.TimeTracking;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Activity.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables

        public ActivityDataModel SearchParameters
        {
            get
            {
                var data = new ActivityDataModel();

				GetParameterValue(data, StandardDataModel.StandardDataColumns.Name);

				data.LayerId = GetParameterValueAsInt(ActivityDataModel.DataColumns.LayerId);

                GroupBy = GetParameterValue("GroupBy");

                SubGroupBy = GetParameterValue("SubGroupBy");

                return data;
            }
        }

        #endregion

		#region private methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);			

			if (fieldName.Equals("LayerId"))
			{
				var layerData = LayerDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(layerData, dropDownListControl, StandardDataModel.StandardDataColumns.Name, LayerDataModel.DataColumns.LayerId);
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

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Activity;
            PrimaryEntityKey = "Activity";
            FolderLocationFromRoot = "Activity";

            SearchActionBarCore = oSearchActionBar;
            SearchParametersRepeaterCore = SearchParametersRepeater;
        }

        #endregion
    }
}