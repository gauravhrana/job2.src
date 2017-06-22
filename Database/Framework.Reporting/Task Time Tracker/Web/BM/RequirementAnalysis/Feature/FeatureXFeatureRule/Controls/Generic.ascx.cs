using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;

namespace ApplicationContainer.UI.Web.FeatureXFeatureRule.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {
        #region properties

        public int? FeatureXFeatureRuleId
        {
            get
            {
                if (txtFeatureXFeatureRuleId.Enabled)
                {
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtFeatureXFeatureRuleId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtFeatureXFeatureRuleId.Text);
                }
            }
        }

        public int? FeatureRuleId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtFeatureRuleId.Text.Trim());
                else
                    return int.Parse(drpFeatureRuleList.SelectedItem.Value);
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

        public int? FeatureRuleStatusId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtFeatureRuleStatusId.Text.Trim());
                else
                    return int.Parse(drpFeatureRuleStatusList.SelectedItem.Value);
            }

        }        

        #endregion properties

        #region private methods

		public override int? Save(string action)
		{
			var data = new FeatureXFeatureRuleDataModel();

			data.FeatureXFeatureRuleId = FeatureXFeatureRuleId;
			data.FeatureId = FeatureId;
			data.FeatureRuleId = FeatureRuleId;
			data.FeatureRuleStatusId = FeatureRuleStatusId;

			if (action == "Insert")
			{
                var dtFeatureXFeatureRule = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXFeatureRuleDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtFeatureXFeatureRule.Rows.Count == 0)
				{
                    TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXFeatureRuleDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXFeatureRuleDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return data.FeatureXFeatureRuleId;
		}

        public override void SetId(int setId, bool chkFeatureXFeatureRuleId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkFeatureXFeatureRuleId);
            txtFeatureXFeatureRuleId.Enabled = chkFeatureXFeatureRuleId;
            //txtFeatureId.Enabled = !chkFeatureXFeatureRuleId;
            //txtFeatureRuleStatusId.Enabled = !chkFeatureXFeatureRuleId;
            //txtFeatureRuleId.Enabled = !chkFeatureXFeatureRuleId;

            //drpFeatureRuleStatusList.Enabled = !chkFeatureXFeatureRuleId;
            //drpFeatureList.Enabled = !chkFeatureXFeatureRuleId;
            //drpFeatureRuleList.Enabled = !chkFeatureXFeatureRuleId;
        }

        public void LoadData(int FeatureXFeatureRuleId, bool showId)
        {
            var data = new FeatureXFeatureRuleDataModel();
            data.FeatureXFeatureRuleId = FeatureXFeatureRuleId;
            var oFeatureXFeatureRuleTable = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXFeatureRuleDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oFeatureXFeatureRuleTable.Rows.Count == 1)
            {
                var row = oFeatureXFeatureRuleTable.Rows[0];

                if (!showId)
                {
                    txtFeatureXFeatureRuleId.Text = Convert.ToString(row[FeatureXFeatureRuleDataModel.DataColumns.FeatureXFeatureRuleId]);

                    dynAuditHistory.Visible = true;

                    // only show Audit History in case of Update page, not for Clone.
                    oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.FeatureXFeatureRule, FeatureXFeatureRuleId, "FeatureXFeatureRule");
                    dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "FeatureXFeatureRule");
                }
                else
                {
                    txtFeatureXFeatureRuleId.Text = String.Empty;
                }
                txtFeatureRuleId.Text = Convert.ToString(row[FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleId]);
                txtFeatureId.Text = Convert.ToString(row[FeatureXFeatureRuleDataModel.DataColumns.FeatureId]);
                txtFeatureRuleStatusId.Text = Convert.ToString(row[FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatusId]);

				drpFeatureRuleStatusList.SelectedValue = Convert.ToString(row[FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleStatusId]);
				drpFeatureList.SelectedValue = Convert.ToString(row[FeatureXFeatureRuleDataModel.DataColumns.FeatureId]);
				drpFeatureRuleList.SelectedValue = Convert.ToString(row[FeatureXFeatureRuleDataModel.DataColumns.FeatureRuleId]);
            }
            else
            {
                txtFeatureXFeatureRuleId.Text = String.Empty;
                txtFeatureId.Text = String.Empty;
                txtFeatureRuleStatusId.Text = String.Empty;
                txtFeatureRuleId.Text = String.Empty;

            }
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

            var featureRuleData = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(featureRuleData, drpFeatureRuleList, StandardDataModel.StandardDataColumns.Name,
                FeatureRuleDataModel.DataColumns.FeatureRuleId);

            var featureData = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(featureData, drpFeatureList, StandardDataModel.StandardDataColumns.Name,
                FeatureDataModel.DataColumns.FeatureId);

            //var FeatureRuleStatusData = Framework.Components.ApplicationUser.ApplicationUser.GetList(SessionVariables.RequestProfile.AuditId);
            //UIHelper.LoadDropDown(FeatureRuleStatusData, drpFeatureRuleStatusList, ApplicationUserDataModel.DataColumns.FirstName, ApplicationUserDataModel.DataColumns.ApplicationUserId);
            var featureRuleStatusData = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleStatusDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(featureRuleStatusData, drpFeatureRuleStatusList, StandardDataModel.StandardDataColumns.Name, FeatureRuleStatusDataModel.DataColumns.FeatureRuleStatusId);

            if (isTesting)
            {
                drpFeatureList.AutoPostBack = true;
                drpFeatureRuleList.AutoPostBack = true;
                drpFeatureRuleStatusList.AutoPostBack = true;
                if (drpFeatureRuleList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtFeatureRuleId.Text.Trim()))
                    {
                        drpFeatureRuleList.SelectedValue = txtFeatureRuleId.Text;
                    }
                    else
                    {
                        txtFeatureRuleId.Text = drpFeatureRuleList.SelectedItem.Value;
                    }
                }
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
                if (drpFeatureRuleStatusList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtFeatureRuleStatusId.Text.Trim()))
                    {
                        drpFeatureRuleStatusList.SelectedValue = txtFeatureRuleStatusId.Text;
                    }
                    else
                    {
                        txtFeatureRuleStatusId.Text = drpFeatureRuleStatusList.SelectedItem.Value;
                    }
                }
                txtFeatureRuleId.Visible = true;
                txtFeatureId.Visible = true;
                txtFeatureRuleStatusId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtFeatureRuleId.Text.Trim()))
                {
                    drpFeatureRuleList.SelectedValue = txtFeatureRuleId.Text;
                }
                if (!string.IsNullOrEmpty(txtFeatureId.Text.Trim()))
                {
                    drpFeatureList.SelectedValue = txtFeatureId.Text;
                }
                if (!string.IsNullOrEmpty(txtFeatureRuleStatusId.Text.Trim()))
                {
                    drpFeatureRuleStatusList.SelectedValue = txtFeatureRuleStatusId.Text;
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
                txtFeatureXFeatureRuleId.Visible = isTesting;
                lblFeatureXFeatureRuleId.Visible = isTesting;
                SetupDropdown();
            }
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureXFeatureRule;
			PrimaryEntityKey = "FeatureXFeatureRule";
			FolderLocationFromRoot = "FeatureXFeatureRule";

			PlaceHolderCore = dynFeatureRuleStatusId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);			
		}

        protected void drpFeatureRuleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFeatureRuleId.Text = drpFeatureRuleList.SelectedItem.Value;
        }

        protected void drpFeatureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFeatureId.Text = drpFeatureList.SelectedItem.Value;
        }

        protected void drpFeatureRuleStatusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFeatureRuleStatusId.Text = drpFeatureRuleStatusList.SelectedItem.Value;
        }

        #endregion
    }
}