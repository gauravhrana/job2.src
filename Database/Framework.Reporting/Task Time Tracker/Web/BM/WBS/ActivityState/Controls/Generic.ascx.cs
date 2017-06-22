using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.WBS.ActivityState.Controls
{
    public partial class Generic : ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new ActivityStateDataModel();

            data.ActivityStateId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
				var dtActivityState = ActivityStateDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtActivityState.Rows.Count == 0)
                {
					ActivityStateDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				ActivityStateDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.ActivityStateId;
        }

        public override void SetId(int setId, bool chkActivityStateId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkActivityStateId);
            CoreSystemKey.Enabled = chkActivityStateId;
            //txtDescription.Enabled = !chkActivityStateId;
            //txtName.Enabled = !chkActivityStateId;
            //txtSortOrder.Enabled = !chkActivityStateId;
        }

        public void LoadData(int activityStateId, bool showId)
        {
            Clear();

            var data = new ActivityStateDataModel();
			data.ActivityStateId = activityStateId;

			var items = ActivityStateDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.ActivityStateId;
                oHistoryList.Setup(PrimaryEntity, activityStateId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new ActivityStateDataModel();

            SetData(data);
        }

        public void SetData(ActivityStateDataModel data)
        {
            SystemKeyId = data.ActivityStateId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblActivityStateId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.ActivityState;
            PrimaryEntityKey = "ActivityState";
            FolderLocationFromRoot = "ActivityState";

            PlaceHolderCore = dynActivityStateId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtActivityStateId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}