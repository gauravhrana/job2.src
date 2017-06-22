using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.ApplicationMonitoredEventSource.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
	{

		#region properties

		public int? ApplicationMonitoredEventSourceId
		{
			get
			{
				if (txtApplicationMonitoredEventSourceId.Enabled)
				{
					return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtApplicationMonitoredEventSourceId.Text, SessionVariables.IsTesting);
				}
				else
				{
					return int.Parse(txtApplicationMonitoredEventSourceId.Text);
				}
			}
			set
			{
				txtApplicationMonitoredEventSourceId.Text = (value == null) ? String.Empty : value.ToString();
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
			var data = new ApplicationMonitoredEventSourceDataModel();

			data.ApplicationMonitoredEventSourceId = ApplicationMonitoredEventSourceId;
			data.Code = Code;
			data.Description = Description;

			if (action == "Insert")
			{
				Framework.Components.EventMonitoring.ApplicationMonitoredEventSourceDataManager.Create(data, SessionVariables.RequestProfile);
			}
			else
			{
				Framework.Components.EventMonitoring.ApplicationMonitoredEventSourceDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ReleaseLogID ?
			return ApplicationMonitoredEventSourceId;
		}

		public void LoadData(int applicationMonitoredEventSourceId, bool showId)
		{
			// clear UI				
			Clear();

			// set up parameters
			var data = new ApplicationMonitoredEventSourceDataModel();
			data.ApplicationMonitoredEventSourceId = applicationMonitoredEventSourceId;

			// get data
			var items = Framework.Components.EventMonitoring.ApplicationMonitoredEventSourceDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
			if (items.Count != 1) return;

			var item = items[0];

			ApplicationMonitoredEventSourceId = item.ApplicationMonitoredEventSourceId;
			Code = item.Code;
			Description = item.Description;

			if (!showId)
			{
				txtApplicationMonitoredEventSourceId.Text = item.ApplicationMonitoredEventSourceId.ToString();

				//PlaceHolderAuditHistory.Visible = true;
				// only show Audit History in case of Update page, not for Clone.
				oHistoryList.Setup(base.PrimaryEntity, applicationMonitoredEventSourceId, PrimaryEntityKey);

			}
			else
			{
				txtApplicationMonitoredEventSourceId.Text = String.Empty;
			}

			oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ApplicationMonitoredEventSourceDataModel();

			ApplicationMonitoredEventSourceId = data.ApplicationMonitoredEventSourceId;
			Code = data.Code;
			Description = data.Description;
		}

		#endregion

		#region private methods

		public override void SetId(int setId, bool chkApplicationMonitoredEventSourceId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkApplicationMonitoredEventSourceId);
			txtApplicationMonitoredEventSourceId.Enabled = chkApplicationMonitoredEventSourceId;
			//txtDescription.Enabled = !chkApplicationMonitoredEventSourceId;
			//txtCode.Enabled = !chkApplicationMonitoredEventSourceId;
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			txtApplicationMonitoredEventSourceId.Visible = isTesting;
			lblApplicationMonitoredEventSourceId.Visible = isTesting;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			base.PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMonitoredEventSource;
			PrimaryEntityKey = "ApplicationMonitoredEventSource";
			FolderLocationFromRoot = "Shared/EventMonitoring/ApplicationMonitoredEventSource";

			// set object variable reference            
			PlaceHolderCore = dynApplicationMonitoredEventSourceId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
		}

		#endregion

	}
}