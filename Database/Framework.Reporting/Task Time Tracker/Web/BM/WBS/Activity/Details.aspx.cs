using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Activity
{
    public partial class Details : PageDetails
	{
		#region Get Methods

		protected string[] GetActivityXDeliverableArtifactColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ActivityXDeliverableArtifact, "DBColumns", SessionVariables.RequestProfile);
		}

		private DataTable GetActivityXDeliverableArtifactData(int activityId)
		{
			var data = new ActivityXDeliverableArtifactDataModel();
			data.ActivityId = activityId;
            var dt = ActivityXDeliverableArtifactDataManager.Search(data, SessionVariables.RequestProfile);
			return dt;
		}

		protected override Control GetTabControl(int setId, Control detailsControl)
		{
			
			var tabControl = ApplicationCommon.GetNewDetailTabControl();

			var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			listControl.Setup("ActivityXDeliverableArtifact", "WBS", "ActivityXDeliverableArtifactId", setId, true, GetData, GetActivityXDeliverableArtifactColumns, "ActivityXDeliverableArtifact");
			listControl.SetSession("true");

			tabControl.Setup("ActivityDetailsView");
			tabControl.AddTab("Activity", detailsControl, String.Empty, true);
			tabControl.AddTab("Deliverable Artifact", listControl);

			return tabControl;
		}

		private DataTable GetData(string key)
		{
			return GetActivityXDeliverableArtifactData(int.Parse(key));
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Activity;
            PrimaryEntityKey = "Activity";
            DetailsControlPath = ApplicationCommon.GetControlPath("Activity", ControlType.DetailsControl);
			PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
			BreadCrumbObject = Master.BreadCrumbObject;
        }       

        #endregion

    }
}