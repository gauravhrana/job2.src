using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.ApplicationMonitoredEventProcessingState.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {

        #region properties

        public int? ApplicationMonitoredEventProcessingStateId
        {
            get
            {
                if (txtApplicationMonitoredEventProcessingStateId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtApplicationMonitoredEventProcessingStateId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtApplicationMonitoredEventProcessingStateId.Text);
                }
            }
			set
			{
				txtApplicationMonitoredEventProcessingStateId.Text = (value == null) ? String.Empty : value.ToString();
			}

        }

        public string Code
        {
            get
            {
                return txtCode.Text;
            }
			set
			{
				txtCode.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public string Description
        {
            get
            {
                return txtDescription.Text;
            }
			set
			{
				txtDescription.Text = (value == null) ? String.Empty : value.ToString();
			}
        }       

        #endregion properties

		#region private methods

		public override int? Save(string action)
		{
			var data = new ApplicationMonitoredEventProcessingStateDataModel();

			data.ApplicationMonitoredEventProcessingStateId = ApplicationMonitoredEventProcessingStateId;
			data.Code			= Code;
			data.Description	= Description;
			
			if (action == "Insert")
			{
				Framework.Components.EventMonitoring.ApplicationMonitoredEventProcessingStateDataManager.Create(data, SessionVariables.RequestProfile);
			}
			else
			{
				Framework.Components.EventMonitoring.ApplicationMonitoredEventProcessingStateDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ReleaseLogID ?
			return ApplicationMonitoredEventProcessingStateId;
		}

		public void LoadData(int applicationMonitoredEventProcessingStateId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new ApplicationMonitoredEventProcessingStateDataModel();
			data.ApplicationMonitoredEventProcessingStateId = applicationMonitoredEventProcessingStateId;

			// get data
			var items = Framework.Components.EventMonitoring.ApplicationMonitoredEventProcessingStateDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			ApplicationMonitoredEventProcessingStateId = item.ApplicationMonitoredEventProcessingStateId;
			Code		= item.Code;
			Description = item.Description;

			if (!showId)
			{
				txtApplicationMonitoredEventProcessingStateId.Text = item.ApplicationMonitoredEventProcessingStateId.ToString();

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(base.PrimaryEntity, applicationMonitoredEventProcessingStateId, PrimaryEntityKey);

			}
			else
			{
				txtApplicationMonitoredEventProcessingStateId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ApplicationMonitoredEventProcessingStateDataModel();

			ApplicationMonitoredEventProcessingStateId = data.ApplicationMonitoredEventProcessingStateId;
			Code		= data.Code;
			Description = data.Description;
		}

		#endregion	

        #region private methods

        public override void SetId(int setId, bool chkApplicationMonitoredEventProcessingStateId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkApplicationMonitoredEventProcessingStateId);
            txtApplicationMonitoredEventProcessingStateId.Enabled = chkApplicationMonitoredEventProcessingStateId;
            //txtDescription.Enabled = !chkApplicationMonitoredEventProcessingStateId;
            //txtCode.Enabled = !chkApplicationMonitoredEventProcessingStateId;
        }       

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            txtApplicationMonitoredEventProcessingStateId.Visible = isTesting;
            lblApplicationMonitoredEventProcessingStateId.Visible = isTesting;
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			base.PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMonitoredEventProcessingState;
			PrimaryEntityKey = "ApplicationMonitoredEventProcessingState";
			FolderLocationFromRoot = "Shared/EventMonitoring/ApplicationMonitoredEventProcessingState";

			// set object variable reference            
			PlaceHolderCore = dynApplicationMonitoredEventProcessingStateId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

        #endregion

    }
}