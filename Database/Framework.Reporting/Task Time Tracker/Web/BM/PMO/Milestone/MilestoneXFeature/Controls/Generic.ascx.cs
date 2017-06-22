using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.Feature;
using Framework.Components;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.BusinessLayer.Feature;

namespace ApplicationContainer.UI.Web.MilestoneXFeature.Controls
{
    public partial class Generic : ControlGeneric
    {
        #region properties

        public int? MilestoneXFeatureId
        {
            get
            {
                if (txtMilestoneXFeatureId.Enabled)
                {
                    return DefaultDataRules.CheckAndGetEntityId(txtMilestoneXFeatureId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtMilestoneXFeatureId.Text);
                }
            }
        }

        public int? FeatureId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtFeatureId.Text.Trim());
                else
                    return int.Parse(drpFeatureList.SelectedItem.Value);
            }

        }

        public int? MilestoneId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtMilestoneId.Text.Trim());
                else
                    return int.Parse(drpMilestoneList.SelectedItem.Value);
            }

        }

        public int? MilestoneFeatureStateId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtMilestoneFeatureStateId.Text.Trim());
                else
                    return int.Parse(drpMilestoneFeatureStateList.SelectedItem.Value);
            }

        }

        public string Memo
        {
            get
            {
                return txtMemo.Text;
            }
        }       

        #endregion properties

        #region private methods

		public override int? Save(string action)
		{
			var data = new MilestoneXFeatureDataModel();

			data.MilestoneXFeatureId = MilestoneXFeatureId;
			data.MilestoneId = MilestoneId;
			data.FeatureId = FeatureId;
			data.MilestoneFeatureStateId = MilestoneFeatureStateId;
			data.Memo = Memo;
			
			if (action == "Insert")
			{
                var dtMilestoneXFeature = MilestoneXFeatureDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtMilestoneXFeature.Rows.Count == 0)
				{
                    MilestoneXFeatureDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                MilestoneXFeatureDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of MilestoneID ?
			return MilestoneXFeatureId;
		}

        public override void SetId(int setId, bool chkMilestoneXFeatureId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkMilestoneXFeatureId);
            txtMilestoneXFeatureId.Enabled = chkMilestoneXFeatureId;
            //txtMilestoneId.Enabled = !chkMilestoneXFeatureId;
            //txtMilestoneFeatureStateId.Enabled = !chkMilestoneXFeatureId;
            //txtFeatureId.Enabled = !chkMilestoneXFeatureId;

            //drpMilestoneFeatureStateList.Enabled = !chkMilestoneXFeatureId;
            drpMilestoneList.Enabled = chkMilestoneXFeatureId;
            drpFeatureList.Enabled = chkMilestoneXFeatureId;
        }

        public void LoadData(int MilestoneXFeatureId, bool showId)
        {
            var data = new MilestoneXFeatureDataModel();
            data.MilestoneXFeatureId = MilestoneXFeatureId;
            var oMilestoneXFeatureTable = MilestoneXFeatureDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oMilestoneXFeatureTable.Rows.Count == 1)
            {
                var row = oMilestoneXFeatureTable.Rows[0];

                if (!showId)
                {
                    txtMilestoneXFeatureId.Text = Convert.ToString(row[MilestoneXFeatureDataModel.DataColumns.MilestoneXFeatureId]);

                    dynAuditHistory.Visible = true;

                    // only show Audit History in case of Update page, not for Clone.
                    oHistoryList.Setup((int)SystemEntity.MilestoneXFeature, MilestoneXFeatureId, "MilestoneXFeature");
                    dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "MilestoneXFeature");

                   
                }
                else
                {
                    txtMilestoneXFeatureId.Text = String.Empty;
                }
                txtFeatureId.Text = Convert.ToString(row[MilestoneXFeatureDataModel.DataColumns.FeatureId]);
                txtMilestoneId.Text = Convert.ToString(row[MilestoneXFeatureDataModel.DataColumns.MilestoneId]);
                txtMilestoneFeatureStateId.Text = Convert.ToString(row[MilestoneXFeatureDataModel.DataColumns.MilestoneFeatureStateId]);
                txtMemo.Text = Convert.ToString(row[MilestoneXFeatureDataModel.DataColumns.Memo]);

                drpMilestoneFeatureStateList.SelectedValue = Convert.ToString(row[MilestoneXFeatureDataModel.DataColumns.MilestoneFeatureStateId]);
                drpMilestoneList.SelectedValue = Convert.ToString(row[MilestoneXFeatureDataModel.DataColumns.MilestoneId]);
                drpFeatureList.SelectedValue = Convert.ToString(row[MilestoneXFeatureDataModel.DataColumns.FeatureId]);

                oUpdateInfo.LoadText(oMilestoneXFeatureTable.Rows[0]);
            }
            else
            {
                txtMilestoneXFeatureId.Text = String.Empty;
                txtMilestoneId.Text = String.Empty;
                txtMemo.Text = String.Empty;
                txtMilestoneFeatureStateId.Text = String.Empty;
                txtFeatureId.Text = String.Empty;

            }
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

            var taskPriorityTypeData = FeatureDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(taskPriorityTypeData, drpFeatureList, StandardDataModel.StandardDataColumns.Name,
                FeatureDataModel.DataColumns.FeatureId);

            var taskData = MilestoneDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(taskData, drpMilestoneList, StandardDataModel.StandardDataColumns.Name,
                MilestoneDataModel.DataColumns.MilestoneId);

            //var MilestoneFeatureStateData = Framework.Components.ApplicationUser.ApplicationUser.GetList(SessionVariables.RequestProfile.AuditId);
            //UIHelper.LoadDropDown(MilestoneFeatureStateData, drpMilestoneFeatureStateList, ApplicationUserDataModel.DataColumns.FirstName, ApplicationUserDataModel.DataColumns.ApplicationUserId);
            var MilestoneFeatureStateData = MilestoneFeatureStateDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(MilestoneFeatureStateData, drpMilestoneFeatureStateList, StandardDataModel.StandardDataColumns.Name, MilestoneFeatureStateDataModel.DataColumns.MilestoneFeatureStateId);

            if (isTesting)
            {
                drpMilestoneList.AutoPostBack = true;
                drpFeatureList.AutoPostBack = true;
                drpMilestoneFeatureStateList.AutoPostBack = true;
                if (drpFeatureList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtFeatureId.Text.Trim()))
                    {
                        drpFeatureList.SelectedValue = txtFeatureId.Text;
                    }
                    else
                    {
                        txtFeatureId.Text = drpFeatureList.SelectedItem.Value;
                    }
                }
                if (drpMilestoneList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtMilestoneId.Text.Trim()))
                    {
                        drpMilestoneList.SelectedValue = txtMilestoneId.Text;
                    }
                    else
                    {
                        txtMilestoneId.Text = drpMilestoneList.SelectedItem.Value;
                    }
                }
                if (drpMilestoneFeatureStateList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtMilestoneFeatureStateId.Text.Trim()))
                    {
                        drpMilestoneFeatureStateList.SelectedValue = txtMilestoneFeatureStateId.Text;
                    }
                    else
                    {
                        txtMilestoneFeatureStateId.Text = drpMilestoneFeatureStateList.SelectedItem.Value;
                    }
                }
                txtFeatureId.Visible = true;
                txtMilestoneId.Visible = true;
                txtMilestoneFeatureStateId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtFeatureId.Text.Trim()))
                {
                    drpFeatureList.SelectedValue = txtFeatureId.Text;
                }
                if (!string.IsNullOrEmpty(txtMilestoneId.Text.Trim()))
                {
                    drpMilestoneList.SelectedValue = txtMilestoneId.Text;
                }
                if (!string.IsNullOrEmpty(txtMilestoneFeatureStateId.Text.Trim()))
                {
                    drpMilestoneFeatureStateList.SelectedValue = txtMilestoneFeatureStateId.Text;
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
                txtMilestoneXFeatureId.Visible = isTesting;
                lblMilestoneXFeatureId.Visible = isTesting;
                SetupDropdown();
            }
        }       

        protected void drpFeatureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFeatureId.Text = drpFeatureList.SelectedItem.Value;
        }

        protected void drpMilestoneList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMilestoneId.Text = drpMilestoneList.SelectedItem.Value;
        }

        protected void drpMilestoneFeatureStateList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMilestoneFeatureStateId.Text = drpMilestoneFeatureStateList.SelectedItem.Value;
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "MilestoneXFeature";
			FolderLocationFromRoot = "MilestoneXFeature";
			PrimaryEntity = SystemEntity.MilestoneXFeature;

			// set object variable reference            
			PlaceHolderCore = dynMilestoneFeatureStateId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}
        #endregion
    }
}