using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.TaskTimeTracker;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Milestone
{
    public partial class Update : PageUpdate
    {
		#region Get Methods

		private string[] GetMilestoneXFeatureColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(SystemEntity.MilestoneXFeature, "DBColumns", SessionVariables.RequestProfile);
		}

		private DataTable GetMilestoneXFeatureData(int milestoneId)
		{
			var data = new MilestoneXFeatureDataModel();
			data.MilestoneId = milestoneId;

            var dt = MilestoneXFeatureDataManager.Search(data, SessionVariables.RequestProfile);
			return dt;
		}


		protected override Control GetTabControl(int setId, Control updateControl)
		{
			
			var tabControl = ApplicationCommon.GetNewDetailTabControl();

			var listControl = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			listControl.Setup("MilestoneXFeature", "Milestone", "MilestoneXFeatureId", setId, true, GetData, GetMilestoneXFeatureColumns, "MilestoneXFeature");
			listControl.SetSession("true");

			tabControl.Setup("MilestoneDetailsView");
			tabControl.AddTab("Milestone", updateControl, String.Empty, true);
			tabControl.AddTab("MilestoneXFeature", listControl);

			return tabControl;
		}

		private DataTable GetData(string key)
		{
			return GetMilestoneXFeatureData(int.Parse(key));
		}

		#endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.Milestone;

			GenericControlPath = ApplicationCommon.GetControlPath("Milestone", ControlType.GenericControl);
			PrimaryPlaceHolder = plcUpdateList;
			PrimaryEntityKey = "Milestone";
			BreadCrumbObject = Master.BreadCrumbObject;

			BtnUpdate = btnUpdate;
			BtnClone = btnClone;
			BtnCancel = btnCancel;
		}       

        #endregion
    }
}