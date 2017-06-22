using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.ApplicationMonitoredEvent.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? ApplicationMonitoredEventId
        {
            get
            {
                if (txtApplicationMonitoredEventId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtApplicationMonitoredEventId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtApplicationMonitoredEventId.Text);
                }
            }
			set
			{
				txtApplicationMonitoredEventId.Text = (value == null) ? String.Empty : value.ToString();
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

        public int? ApplicationMonitoredEventProcessingStateId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtApplicationMonitoredEventProcessingStateId.Text.Trim());
                else
                    return int.Parse(drpApplicationMonitoredEventProcessingStateList.SelectedItem.Value);
            }
			set
			{
				txtApplicationMonitoredEventProcessingStateId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? ReferenceId
        {
            get
            {
                if (txtReferenceId.Text == "")
                {
                    return null;
                }
                else
                {

                    return int.Parse(txtReferenceId.Text);
                }

            }

			set
			{
				txtReferenceId.Text = (value == null) ? String.Empty : value.ToString();
			}

        }

        public string ReferenceCode
        {
            get
            {
                return txtReferenceCode.Text;
            }

			set
			{
				txtReferenceCode.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public string Category
        {
            get
            {
                return txtCategory.Text;
            }

			set
			{
				txtCategory.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public string Message
        {
            get
            {
                return txtMessage.Text;
            }

			set
			{
				txtMessage.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public bool? IsDuplicate
        {
            get
            {
                return Convert.ToBoolean(drpIsDuplicate.SelectedValue);
            }

			set
			{
				drpIsDuplicate.SelectedValue = (value == null) ? String.Empty : value.ToString();
			}
        }

        public string LastModifiedBy
        {
            get
            {
                return txtLastModifiedBy.Text;
            }

			set
			{
				txtLastModifiedBy.Text = (value == null) ? String.Empty : value.ToString();
			}
        }
        

        #endregion properties


		#region private methods

		public override int? Save(string action)
		{
			var data = new ApplicationMonitoredEventDataModel();

			data.ApplicationMonitoredEventSourceId = ApplicationMonitoredEventId;
			data.ApplicationMonitoredEventProcessingStateId = ApplicationMonitoredEventProcessingStateId;
			data.ReferenceId			= ReferenceId;
			data.ReferenceCode			= ReferenceCode;
			data.Category				= Category;
			data.Message				= Message;
			data.IsDuplicate			= IsDuplicate;
			data.LastModifiedBy			= LastModifiedBy;
			
			if (action == "Insert")
			{
				Framework.Components.EventMonitoring.ApplicationMonitoredEventDataManager.Create(data, SessionVariables.RequestProfile);
			}
			else
			{
				Framework.Components.EventMonitoring.ApplicationMonitoredEventDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ReleaseLogID ?
			return ApplicationMonitoredEventSourceId;
		}



		public void LoadData(int applicationMonitoredEventId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new ApplicationMonitoredEventDataModel();
			data.ApplicationMonitoredEventId = applicationMonitoredEventId;

			// get data
			var items = Framework.Components.EventMonitoring.ApplicationMonitoredEventDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			ApplicationMonitoredEventSourceId = item.ApplicationMonitoredEventId;
			ApplicationMonitoredEventProcessingStateId = item.ApplicationMonitoredEventProcessingStateId;
			ReferenceId = item.ReferenceId;
			ReferenceCode = item.ReferenceCode;
			Category = item.Category;
			Message = item.Message;
			IsDuplicate = item.IsDuplicate;
			LastModifiedBy =item. LastModifiedBy;			

			if (!showId)
			{
				txtApplicationMonitoredEventId.Text = item.ApplicationMonitoredEventSourceId.ToString();				

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(base.PrimaryEntity, applicationMonitoredEventId, PrimaryEntityKey);

			}
			else
			{
				txtApplicationMonitoredEventId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ApplicationMonitoredEventDataModel();
			
			ApplicationMonitoredEventSourceId			= data.ApplicationMonitoredEventId;
			ApplicationMonitoredEventProcessingStateId	= data.ApplicationMonitoredEventProcessingStateId;
			ReferenceId									= data.ReferenceId;
			ReferenceCode								= data.ReferenceCode;
			Category									= data.Category;
			Message										= data.Message;
			IsDuplicate									= data.IsDuplicate;
			LastModifiedBy								= data.LastModifiedBy;
		}

		#endregion	


        #region private methods

        public override void SetId(int setId, bool chkApplicationMonitoredEventId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkApplicationMonitoredEventId);
            txtApplicationMonitoredEventId.Enabled = chkApplicationMonitoredEventId;
            //txtApplicationMonitoredEventSourceId.Enabled = !chkApplicationMonitoredEventId;
            //txtApplicationMonitoredEventProcessingStateId.Enabled = !chkApplicationMonitoredEventId;
            //txtReferenceId.Enabled = !chkApplicationMonitoredEventId;
            //txtReferenceCode.Enabled = !chkApplicationMonitoredEventId;
            //txtCategory.Enabled = !chkApplicationMonitoredEventId;
            //txtMessage.Enabled = !chkApplicationMonitoredEventId;
            //drpIsDuplicate.Enabled = !chkApplicationMonitoredEventId;
            //drpApplicationMonitoredEventProcessingStateList.Enabled = !chkApplicationMonitoredEventId;
            //drpApplicationMonitoredEventSourceList.Enabled = !chkApplicationMonitoredEventId;
            //txtLastModifiedBy.Enabled = !chkApplicationMonitoredEventId;
		}        

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;
			var applicationMonitoredEventSourceData = Framework.Components.EventMonitoring.ApplicationMonitoredEventSourceDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(applicationMonitoredEventSourceData, drpApplicationMonitoredEventSourceList, ApplicationMonitoredEventSourceDataModel.DataColumns.Code, 
                ApplicationMonitoredEventSourceDataModel.DataColumns.ApplicationMonitoredEventSourceId);

			var applicationMonitoredEventProcessingStateData = Framework.Components.EventMonitoring.ApplicationMonitoredEventProcessingStateDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(applicationMonitoredEventProcessingStateData, drpApplicationMonitoredEventProcessingStateList, ApplicationMonitoredEventProcessingStateDataModel.DataColumns.Code, 
                ApplicationMonitoredEventProcessingStateDataModel.DataColumns.ApplicationMonitoredEventProcessingStateId);

            if (isTesting)
            {
                drpApplicationMonitoredEventSourceList.AutoPostBack = true;
                drpApplicationMonitoredEventProcessingStateList.AutoPostBack = true;
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
                if (drpApplicationMonitoredEventProcessingStateList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtApplicationMonitoredEventProcessingStateId.Text.Trim()))
                    {
                        drpApplicationMonitoredEventProcessingStateList.SelectedValue = txtApplicationMonitoredEventProcessingStateId.Text;
                    }
                    else
                    {
                        txtApplicationMonitoredEventProcessingStateId.Text = drpApplicationMonitoredEventProcessingStateList.SelectedItem.Value;
                    }
                }
                txtApplicationMonitoredEventSourceId.Visible = true;
                txtApplicationMonitoredEventProcessingStateId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtApplicationMonitoredEventSourceId.Text.Trim()))
                {
                    drpApplicationMonitoredEventSourceList.SelectedValue = txtApplicationMonitoredEventSourceId.Text;
                }

                if (!string.IsNullOrEmpty(txtApplicationMonitoredEventProcessingStateId.Text.Trim()))
                {
                    drpApplicationMonitoredEventProcessingStateList.SelectedValue = txtApplicationMonitoredEventProcessingStateId.Text;
                }
            }
        }

        #endregion

        #region Events		

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			base.PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMonitoredEvent;
			PrimaryEntityKey = "ApplicationMonitoredEvent";
			FolderLocationFromRoot = "Shared/EventMonitoring/ApplicationMonitoredEvent";

			// set object variable reference            
			PlaceHolderCore = dynApplicationMonitoredEventId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                txtApplicationMonitoredEventId.Visible = isTesting;
                lblApplicationMonitoredEventId.Visible = isTesting;
                SetupDropdown();
            }
        }
        

        protected void drpApplicationMonitoredEventSourceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplicationMonitoredEventSourceId.Text = drpApplicationMonitoredEventSourceList.SelectedItem.Value;
        }

        protected void drpApplicationMonitoredEventProcessingStateList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApplicationMonitoredEventProcessingStateId.Text = drpApplicationMonitoredEventProcessingStateList.SelectedItem.Value;
        }

        #endregion

    }
}