using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.ActivityXDeliverableArtifact.Controls
{
    public partial class Generic : ControlGeneric
    {
        #region properties

        public int? ActivityXDeliverableArtifactId
        {
            get
            {
                if (txtActivityXDeliverableArtifactId.Enabled)
                {
                    return DefaultDataRules.CheckAndGetEntityId(txtActivityXDeliverableArtifactId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtActivityXDeliverableArtifactId.Text);
                }
            }
        }

        public int? DeliverableArtifactId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtDeliverableArtifactsId.Text.Trim());
                else
                    return int.Parse(drpDeliverableArtifactsList.SelectedItem.Value);
            }

        }

        public int? ActivityId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtActivityId.Text.Trim());
                else
                    return int.Parse(drpActivityList.SelectedItem.Value);
            }

        }

        public int? DeliverableArtifactStatusId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtDeliverableArtifactsStatusId.Text.Trim());
                else
                    return int.Parse(drpDeliverableArtifactsStatusList.SelectedItem.Value);
            }

        }       

        #endregion properties

        #region private methods

		public override int? Save(string action)
		{
			var data = new ActivityXDeliverableArtifactDataModel();

			data.ActivityXDeliverableArtifactId = ActivityXDeliverableArtifactId;
			data.ActivityId = ActivityId;
			data.DeliverableArtifactId = DeliverableArtifactId;
			data.DeliverableArtifactStatusId = DeliverableArtifactStatusId;
			
			if (action == "Insert")
			{
                var dtActivityXDeliverableArtifact = ActivityXDeliverableArtifactDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtActivityXDeliverableArtifact.Rows.Count == 0)
				{
                    ActivityXDeliverableArtifactDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                ActivityXDeliverableArtifactDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ActivityXDeliverableArtifactID ?
			return data.ActivityXDeliverableArtifactId;
		}

        public override void SetId(int setId, bool chkActivityXDeliverableArtifactId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkActivityXDeliverableArtifactId);
            txtActivityXDeliverableArtifactId.Enabled = chkActivityXDeliverableArtifactId;
            //txtActivityId.Enabled = !chkActivityXDeliverableArtifactId;
            //txtDeliverableArtifactsStatusId.Enabled = !chkActivityXDeliverableArtifactId;
            //txtDeliverableArtifactsId.Enabled = !chkActivityXDeliverableArtifactId;

            //drpDeliverableArtifactsStatusList.Enabled = !chkActivityXDeliverableArtifactId;
            //drpActivityList.Enabled = !chkActivityXDeliverableArtifactId;
            //drpDeliverableArtifactsList.Enabled = !chkActivityXDeliverableArtifactId;
        }

        public void LoadData(int activityXDeliverableArtifactId, bool showId)
        {
			// clear UI
			Clear();

			// set up parameters
			var data = new ActivityXDeliverableArtifactDataModel();
			data.ActivityXDeliverableArtifactId = activityXDeliverableArtifactId;

			// get data
            var items = ActivityXDeliverableArtifactDataManager.GetEntityList(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count != 1) return;

			var item = items[0];

			txtActivityXDeliverableArtifactId.Text = item.ActivityXDeliverableArtifactId.ToString();
			txtActivityId.Text = item.ActivityId.ToString();
			txtDeliverableArtifactsId.Text = item.DeliverableArtifactId.ToString();
			txtDeliverableArtifactsStatusId.Text = item.DeliverableArtifactStatusId.ToString();

			if (!showId)
			{
				txtActivityXDeliverableArtifactId.Text = item.ActivityXDeliverableArtifactId.ToString();
				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(PrimaryEntity, activityXDeliverableArtifactId, PrimaryEntityKey);
			}
			else
			{
				txtActivityXDeliverableArtifactId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);         			
        }
       
        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

            var deliverableArtifactData = DeliverableArtifactDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(deliverableArtifactData, drpDeliverableArtifactsList, StandardDataModel.StandardDataColumns.Name,
                DeliverableArtifactDataModel.DataColumns.DeliverableArtifactId);

			var activityData = ActivityDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(activityData, drpActivityList, StandardDataModel.StandardDataColumns.Name,
                ActivityDataModel.DataColumns.ActivityId);

            var DeliverableArtifactsStatusData = DeliverableArtifactStatusDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(DeliverableArtifactsStatusData, drpDeliverableArtifactsStatusList, StandardDataModel.StandardDataColumns.Name, DeliverableArtifactStatusDataModel.DataColumns.DeliverableArtifactStatusId);

            if (isTesting)
            {
                drpActivityList.AutoPostBack = true;
                drpDeliverableArtifactsList.AutoPostBack = true;
                drpDeliverableArtifactsStatusList.AutoPostBack = true;
                if (drpDeliverableArtifactsList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtDeliverableArtifactsId.Text.Trim()))
                    {
                        drpDeliverableArtifactsList.SelectedValue = txtDeliverableArtifactsId.Text;
                    }
                    else
                    {
                        txtDeliverableArtifactsId.Text = drpDeliverableArtifactsList.SelectedItem.Value;
                    }
                }
                if (drpActivityList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtActivityId.Text.Trim()))
                    {
                        drpActivityList.SelectedValue = txtActivityId.Text;
                    }
                    else
                    {
                        txtActivityId.Text = drpActivityList.SelectedItem.Value;
                    }
                }
                if (drpDeliverableArtifactsStatusList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtDeliverableArtifactsStatusId.Text.Trim()))
                    {
                        drpDeliverableArtifactsStatusList.SelectedValue = txtDeliverableArtifactsStatusId.Text;
                    }
                    else
                    {
                        txtDeliverableArtifactsStatusId.Text = drpDeliverableArtifactsStatusList.SelectedItem.Value;
                    }
                }
                txtDeliverableArtifactsId.Visible = true;
                txtActivityId.Visible = true;
                txtDeliverableArtifactsStatusId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtDeliverableArtifactsId.Text.Trim()))
                {
                    drpDeliverableArtifactsList.SelectedValue = txtDeliverableArtifactsId.Text;
                }
                if (!string.IsNullOrEmpty(txtActivityId.Text.Trim()))
                {
                    drpActivityList.SelectedValue = txtActivityId.Text;
                }
                if (!string.IsNullOrEmpty(txtDeliverableArtifactsStatusId.Text.Trim()))
                {
                    drpDeliverableArtifactsStatusList.SelectedValue = txtDeliverableArtifactsStatusId.Text;
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
                txtActivityXDeliverableArtifactId.Visible = isTesting;
                lblActivityXDeliverableArtifactId.Visible = isTesting;
                SetupDropdown();
            }
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.ActivityXDeliverableArtifact;
			PrimaryEntityKey = "ActivityXDeliverableArtifact";
			FolderLocationFromRoot = "ActivityXDeliverableArtifact";

			// set object variable reference            
			PlaceHolderCore = dynActivityXDeliverableArtifactId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

        protected void drpDeliverableArtifactsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDeliverableArtifactsId.Text = drpDeliverableArtifactsList.SelectedItem.Value;
        }

        protected void drpActivityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtActivityId.Text = drpActivityList.SelectedItem.Value;
        }

        protected void drpDeliverableArtifactsStatusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDeliverableArtifactsStatusId.Text = drpDeliverableArtifactsStatusList.SelectedItem.Value;
        }

        #endregion
    }
}