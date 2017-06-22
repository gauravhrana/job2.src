using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.UI.Web.BaseClasses;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.Components.DataAccess;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.MilestoneXFeature
{
    public partial class Update : PageUpdate
    {
        #region private methods

        private DataTable GetMilestoneXFeatureArchiveData(int MilestoneXFeatureid)
        {
            var data = new MilestoneXFeatureArchiveDataModel();
            data.MilestoneXFeatureId = MilestoneXFeatureid;
            var dt = MilestoneXFeatureArchiveDataManager.SearchHistory(data, SessionVariables.RequestProfile);
            return dt;          
        }

        private string[] GetMilestoneXFeatureArchiveColumns()
        {
			return FieldConfigurationUtility.GetEntityColumns(SystemEntity.MilestoneXFeatureArchive, "DBColumns", SessionVariables.RequestProfile);

        }

		protected override Control GetTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();
            
            var listControl = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			listControl.Setup("MilestoneXFeatureArchive", "", "MilestoneXFeatureArchiveId", setId, true, GetData, GetMilestoneXFeatureArchiveColumns, "MilestoneXFeatureArchive");            
            listControl.SetSession("true");

            tabControl.Setup("MilestoneXFeatureUpdateView");
            tabControl.AddTab("MilestoneXFeature", detailsControl, String.Empty, true);
            tabControl.AddTab("MilestoneXFeatureArchive", listControl, "History");
            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetMilestoneXFeatureArchiveData(int.Parse(key));
		}

        #endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.MilestoneXFeature;

			GenericControlPath = ApplicationCommon.GetControlPath("MilestoneXFeature", ControlType.GenericControl);
			PrimaryPlaceHolder = plcUpdateList;
			PrimaryEntityKey = "MilestoneXFeature";
			BreadCrumbObject = Master.BreadCrumbObject;

			BtnUpdate = btnUpdate;
			BtnClone = btnClone;
			BtnCancel = btnCancel;
		}

        #endregion
    }
}