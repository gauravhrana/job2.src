using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.ApplicationMonitoredEventSource.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {
		#region private methods

		protected override void ShowData(int applicationMonitoredEventSourceId)
		{
			base.ShowData(applicationMonitoredEventSourceId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ApplicationMonitoredEventSourceDataModel();
			data.ApplicationMonitoredEventSourceId = applicationMonitoredEventSourceId;

			var items = Framework.Components.EventMonitoring.ApplicationMonitoredEventSourceDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblApplicationMonitoredEventSourceId.Text = item.ApplicationMonitoredEventSourceId.ToString();
				lblCode.Text = item.Code.ToString();
				lblDescription.Text = item.Description.ToString();

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, applicationMonitoredEventSourceId, "ApplicationMonitoredEventSource");
			}

		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblApplicationMonitoredEventSourceId, lblCodeText, lblDescriptionText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ApplicationMonitoredEventSourceLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMonitoredEventSource;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynApplicationMonitoredEventSourceId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblApplicationMonitoredEventSourceId.Visible = isTesting;
				lblApplicationMonitoredEventSourceIdText.Visible = isTesting;
			}
			PopulateLabelsText();
		}
		#endregion      

	}

}