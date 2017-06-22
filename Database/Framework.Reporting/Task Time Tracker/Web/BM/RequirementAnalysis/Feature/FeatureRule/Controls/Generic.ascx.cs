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
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.FeatureRule.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
	{
		#region properties

		public int? FeatureRuleCategoryId
		{
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtFeatureRuleCategoryId.Text.Trim());
				else
					return int.Parse(drpFeatureRuleCategoryList.SelectedItem.Value);
			}
			set
			{
				txtFeatureRuleCategoryId.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		#endregion properties

		#region methods

		public override int? Save(string action)
        {
            var data = new FeatureRuleDataModel();

            data.FeatureRuleId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;
			data.FeatureRuleCategoryId = FeatureRuleCategoryId;

            if (action == "Insert")
            {
                var dtFeatureRule = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtFeatureRule.Rows.Count == 0)
                {
                    TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.FeatureRuleId;
        }

        public override void SetId(int setId, bool chkFeatureRuleId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkFeatureRuleId);
            CoreSystemKey.Enabled = chkFeatureRuleId;            
        }

        public void LoadData(int featureRuleId, bool showId)
        {
            Clear();

            var data = new FeatureRuleDataModel();
			data.FeatureRuleId = featureRuleId;

            var items = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.FeatureRuleId;
				FeatureRuleCategoryId = item.FeatureRuleCategoryId;

                oHistoryList.Setup(PrimaryEntity, featureRuleId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new FeatureRuleDataModel();

            SetData(data);
        }

        public void SetData(FeatureRuleDataModel data)
        {
            SystemKeyId = data.FeatureRuleId;

            base.SetData(data);
        }

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
            var featureRuleCategoryData = TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleCategoryDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(featureRuleCategoryData, drpFeatureRuleCategoryList, StandardDataModel.StandardDataColumns.Name, FeatureRuleCategoryDataModel.DataColumns.FeatureRuleCategoryId);

			if (isTesting)
			{
				drpFeatureRuleCategoryList.AutoPostBack = true;
				if (drpFeatureRuleCategoryList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtFeatureRuleCategoryId.Text.Trim()))
					{
						drpFeatureRuleCategoryList.SelectedValue = txtFeatureRuleCategoryId.Text;
					}
					else
					{
						txtFeatureRuleCategoryId.Text = drpFeatureRuleCategoryList.SelectedItem.Value;
					}
				}
				txtFeatureRuleCategoryId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtFeatureRuleCategoryId.Text.Trim()))
				{
					drpFeatureRuleCategoryList.SelectedValue = txtFeatureRuleCategoryId.Text;
				}
			}
		}

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
			if (!IsPostBack)
			{
				SetupDropdown();
			}
            var isTesting            = SessionVariables.IsTesting;
            CoreSystemKey.Visible    = isTesting;
            lblFeatureRuleId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity           = Framework.Components.DataAccess.SystemEntity.FeatureRule;
            PrimaryEntityKey        = "FeatureRule";
            FolderLocationFromRoot  = "FeatureRule";

            PlaceHolderCore         = dynFeatureRuleId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv               = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey          = txtFeatureRuleId;
            CoreControlName        = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder   = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}