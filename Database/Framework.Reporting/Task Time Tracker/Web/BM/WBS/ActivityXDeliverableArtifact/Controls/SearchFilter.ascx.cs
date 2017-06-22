using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.ActivityXDeliverableArtifact.Controls
{
    public partial class SearchFilter : ControlSearchFilter
    {
        #region variables       

        public ActivityXDeliverableArtifactDataModel SearchParameters
        {
            get
            {
                var data = new ActivityXDeliverableArtifactDataModel();

				data.ActivityId = GetParameterValueAsInt(ActivityXDeliverableArtifactDataModel.DataColumns.ActivityId);

				data.DeliverableArtifactId = GetParameterValueAsInt(ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactId);

				data.DeliverableArtifactStatusId = GetParameterValueAsInt(ActivityXDeliverableArtifactDataModel.DataColumns.DeliverableArtifactStatusId);								
				
                return data;
            }
        }

        #endregion

        #region methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);			

			if (fieldName.Equals("ActivityId"))
			{
				var activitydata = ActivityDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(activitydata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					ActivityDataModel.DataColumns.ActivityId);

			}
			if (fieldName.Equals("DeliverableArtifactId"))
			{
                var dasdata = DeliverableArtifactDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(dasdata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					DeliverableArtifactDataModel.DataColumns.DeliverableArtifactId);

			}
			if (fieldName.Equals("DeliverableArtifactStatusId"))
			{
                var dasdata = DeliverableArtifactStatusDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(dasdata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					DeliverableArtifactStatusDataModel.DataColumns.DeliverableArtifactStatusId);

			}
		}

        #endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "ActivityXDeliverableArtifact";
			FolderLocationFromRoot = "ActivityXDeliverableArtifact";
			PrimaryEntity = SystemEntity.ActivityXDeliverableArtifact;

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}
        
        #endregion
    }
}