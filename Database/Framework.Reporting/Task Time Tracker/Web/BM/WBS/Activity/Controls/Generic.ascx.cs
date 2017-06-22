using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.TimeTracking;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using System.Data;
using DataModel.Framework.DataAccess;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Activity.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
	{
		#region properties		

		public int? LayerId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtLayerId.Text.Trim());
				else
					return int.Parse(drpLayerList.SelectedItem.Value);
			}
			set
			{
				txtLayerId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		#endregion properties
		
		#region methods

		public override int? Save(string action)
        {
            var data = new ActivityDataModel();

            data.ActivityId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;
			data.LayerId = LayerId;

            if (action == "Insert")
            {
				var dtActivity = ActivityDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtActivity.Rows.Count == 0)
                {
					ActivityDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				ActivityDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.ActivityId;
        }

        public override void SetId(int setId, bool chkActivityId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkActivityId);
            CoreSystemKey.Enabled = chkActivityId;
            //txtDescription.Enabled = !chkActivityId;
            //txtName.Enabled = !chkActivityId;
            //txtSortOrder.Enabled = !chkActivityId;
        }

        public void LoadData(int activityId, bool showId)
        {
            Clear();

            var data = new ActivityDataModel();
			data.ActivityId = activityId;

			var items = ActivityDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

			LayerId = item.LayerId;
			

            if (!showId)
            {
                SystemKeyId = item.ActivityId;				
                oHistoryList.Setup(PrimaryEntity, activityId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new ActivityDataModel();

            SetData(data);
        }

        public void SetData(ActivityDataModel data)
        {
            SystemKeyId = data.ActivityId;

            base.SetData(data);
        }

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
            var layerData = LayerDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(layerData, drpLayerList, StandardDataModel.StandardDataColumns.Name, LayerDataModel.DataColumns.LayerId);

			if (isTesting)
			{
				drpLayerList.AutoPostBack = true;
				if (drpLayerList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtLayerId.Text.Trim()))
					{
						drpLayerList.SelectedValue = txtLayerId.Text;
					}
					else
					{
						txtLayerId.Text = drpLayerList.SelectedItem.Value;
					}
				}
				txtLayerId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtLayerId.Text.Trim()))
				{
					drpLayerList.SelectedValue = txtLayerId.Text;
				}
			}
		}

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				CoreSystemKey.Visible = isTesting;
				lblActivityId.Visible = isTesting;
				SetupDropdown();
			}
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Activity;
            PrimaryEntityKey = "Activity";
            FolderLocationFromRoot = "Activity";

            PlaceHolderCore = dynActivityId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtActivityId;
			CoreControlName = txtName;
			CoreControlDescription = txtDescription;
			CoreControlSortOrder = txtSortOrder;
            //CoreSystemKey = txtLayerId;

            CoreUpdateInfo = oUpdateInfo;
        }

		protected void drpLayerList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtLayerId.Text = drpLayerList.SelectedItem.Value;
		}

        #endregion

    }
}