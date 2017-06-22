using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Activity
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
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

		protected override Control GetTabControl(int setId, Control updateControl)
		{
			
			var tabControl = ApplicationCommon.GetNewDetailTabControl();

			tabControl.Setup("ActivityUpdateView");

			var selected = false;
			if (string.IsNullOrEmpty(Request.QueryString["tab"]) || Request.QueryString["tab"] == "1")
			{
				selected = true;
			}

			var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			listControl.Setup("ActivityXDeliverableArtifact", "WBS", "ActivityXDeliverableArtifactId", setId, true, GetData, GetActivityXDeliverableArtifactColumns, "ActivityXDeliverableArtifact");
			listControl.SetSession("true");


			tabControl.AddTab("Activity", updateControl, String.Empty, selected);
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

            GenericControlPath = ApplicationCommon.GetControlPath("Activity", ControlType.GenericControl);
            PrimaryPlaceHolder = plcUpdateList;
            PrimaryEntityKey = "Activity";
            BreadCrumbObject = Master.BreadCrumbObject;

            BtnUpdate = btnUpdate;
            BtnClone = btnClone;
            BtnCancel = btnCancel;
		}

		#endregion
	}
}