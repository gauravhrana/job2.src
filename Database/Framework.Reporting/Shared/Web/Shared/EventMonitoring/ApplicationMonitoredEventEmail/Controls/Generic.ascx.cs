using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.ApplicationMonitoredEventEmail.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? ApplicationMonitoredEventEmailId
        {
            get
            {
                if (txtApplicationMonitoredEventEmailId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtApplicationMonitoredEventEmailId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtApplicationMonitoredEventEmailId.Text);
                }
            }
			set
			{
				txtApplicationMonitoredEventEmailId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? ApplicationMonitoredEventSourceId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtApplicationMonitoredEventSourceId.Text.Trim());
                else
                    return int.Parse(drpApplicationMonitoredEventSourceList.SelectedItem.Value);
            }
			set
			{
				txtApplicationMonitoredEventSourceId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? UserId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtUserId.Text.Trim());
                else
                    return int.Parse(drpUserList.SelectedItem.Value);

            }
			set
			{
				txtUserId.Text = (value == null) ? String.Empty : value.ToString();
			}

        }

        public string CorrespondenceLevel
        {
            get
            {
                return txtCorrespondenceLevel.Text;
            }
			set
			{
				txtCorrespondenceLevel.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public bool? Active
        {
            get
            {
                return Convert.ToBoolean(drpActive.SelectedValue);
            }
			set
			{
				drpActive.SelectedValue = (value == null) ? String.Empty : value.ToString();
			}
        }
        

        #endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new ApplicationMonitoredEventEmailDataModel();

			data.ApplicationMonitoredEventEmailId = ApplicationMonitoredEventEmailId;
			data.ApplicationMonitoredEventSourceId = ApplicationMonitoredEventSourceId;
			data.Active = Active;
			data.CorrespondenceLevel = CorrespondenceLevel;
			data.UserId = UserId;
			
			if (action == "Insert")
			{
				Framework.Components.EventMonitoring.ApplicationMonitoredEventEmailDataManager.Create(data, SessionVariables.RequestProfile);
			}
			else
			{
				Framework.Components.EventMonitoring.ApplicationMonitoredEventEmailDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ReleaseLogID ?
			return ApplicationMonitoredEventEmailId;
		}

		public void LoadData(int applicationMonitoredEventEmailId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new ApplicationMonitoredEventEmailDataModel();
			data.ApplicationMonitoredEventEmailId = applicationMonitoredEventEmailId;

			// get data
			var items = Framework.Components.EventMonitoring.ApplicationMonitoredEventEmailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];
			
			ApplicationMonitoredEventEmailId = item.ApplicationMonitoredEventEmailId;
			ApplicationMonitoredEventSourceId = item.ApplicationMonitoredEventSourceId;
			Active = item.Active;
			CorrespondenceLevel = item.CorrespondenceLevel;
			UserId = item.UserId;

			if (!showId)
			{
				txtApplicationMonitoredEventEmailId.Text = item.ApplicationMonitoredEventEmailId.ToString();

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(base.PrimaryEntity,applicationMonitoredEventEmailId, PrimaryEntityKey);

			}
			else
			{
				txtApplicationMonitoredEventEmailId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ApplicationMonitoredEventEmailDataModel();
			
			ApplicationMonitoredEventEmailId = data.ApplicationMonitoredEventEmailId;
			ApplicationMonitoredEventSourceId = data.ApplicationMonitoredEventSourceId;
			Active = data.Active;
			CorrespondenceLevel = data.CorrespondenceLevel;
			UserId = data.UserId;
		}

		#endregion	

        #region private methods

        public override void SetId(int setId, bool chkApplicationMonitoredEventEmailId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkApplicationMonitoredEventEmailId);
            txtApplicationMonitoredEventEmailId.Enabled = chkApplicationMonitoredEventEmailId;
            //txtApplicationMonitoredEventSourceId.Enabled = !chkApplicationMonitoredEventEmailId;
            //txtUserId.Enabled = !chkApplicationMonitoredEventEmailId;
            //txtCorrespondenceLevel.Enabled = !chkApplicationMonitoredEventEmailId;
            //drpActive.Enabled = !chkApplicationMonitoredEventEmailId;
            //drpApplicationMonitoredEventSourceList.Enabled = !chkApplicationMonitoredEventEmailId;
            //drpUserList.Enabled = !chkApplicationMonitoredEventEmailId;
        }
        

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var ApplicationMonitoredEventSourceData = Framework.Components.EventMonitoring.ApplicationMonitoredEventSourceDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(ApplicationMonitoredEventSourceData, drpApplicationMonitoredEventSourceList, ApplicationMonitoredEventSourceDataModel.DataColumns.Code,
                ApplicationMonitoredEventSourceDataModel.DataColumns.ApplicationMonitoredEventSourceId);

			var userData = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(userData, drpUserList, ApplicationUserDataModel.DataColumns.FullName, ApplicationUserDataModel.DataColumns.ApplicationUserId);

            if (isTesting)
            {
                drpApplicationMonitoredEventSourceList.AutoPostBack = true;
                drpUserList.AutoPostBack = true;
                if (drpApplicationMonitoredEventSourceList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtApplicationMonitoredEventSourceId.Text.Trim()))
                    {
                        drpApplicationMonitoredEventSourceList.SelectedValue = txtApplicationMonitoredEventSourceId.Text;
                    }
                    else
                    {
                        txtApplicationMonitoredEventSourceId.Text = drpApplicationMonitoredEventSourceList.SelectedItem.Value;
                    }
                }
                if (drpUserList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtUserId.Text.Trim()))
                    {
                        drpUserList.SelectedValue = txtUserId.Text;
                    }
                    else
                    {
                        txtUserId.Text = drpUserList.SelectedItem.Value;
                    }
                }
                txtApplicationMonitoredEventSourceId.Visible = true;
                txtUserId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtApplicationMonitoredEventSourceId.Text.Trim()))
                {
                    drpApplicationMonitoredEventSourceList.SelectedValue = txtApplicationMonitoredEventSourceId.Text;
                }
                if (!string.IsNullOrEmpty(txtUserId.Text.Trim()))
                {
                    drpUserList.SelectedValue = txtUserId.Text;
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
                txtApplicationMonitoredEventEmailId.Visible = isTesting;
                lblApplicationMonitoredEventEmailId.Visible = isTesting;
                SetupDropdown();
            }
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			base.PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMonitoredEventEmail;
			PrimaryEntityKey = "ApplicationMonitoredEventEmail";
			FolderLocationFromRoot = "Shared/EventMonitoring/ApplicationMonitoredEventEmail";

			// set object variable reference            
			PlaceHolderCore = dynApplicationMonitoredEventEmailId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		protected void drpApplicationMonitoredEventSourceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplicationMonitoredEventSourceId.Text = drpApplicationMonitoredEventSourceList.SelectedItem.Value;
        }

        protected void drpUserList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUserId.Text = drpUserList.SelectedItem.Value;
        }

        #endregion

    }
}