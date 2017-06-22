using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationMonitoredEventProcessingState.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {
		#region private methods

		protected override void ShowData(int applicationMonitoredEventProcessingStateId)
		{
			base.ShowData(applicationMonitoredEventProcessingStateId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ApplicationMonitoredEventProcessingStateDataModel();
			data.ApplicationMonitoredEventProcessingStateId = applicationMonitoredEventProcessingStateId;

			var items = Framework.Components.EventMonitoring.ApplicationMonitoredEventProcessingStateDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblApplicationMonitoredEventProcessingStateId.Text = item.ApplicationMonitoredEventProcessingStateId.ToString();
				lblCode.Text = item.Code.ToString();
				lblDescription.Text = item.Description.ToString();				

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, applicationMonitoredEventProcessingStateId, "ApplicationMonitoredEventProcessingState");
			}

		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblApplicationMonitoredEventProcessingStateIdText, lblCodeText, lblDescriptionText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ApplicationMonitoredEventProcessingStateLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMonitoredEventProcessingState;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynApplicationMonitoredEventProcessingStateId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblApplicationMonitoredEventProcessingStateId.Visible = isTesting;
				lblApplicationMonitoredEventProcessingStateIdText.Visible = isTesting;
			}
			PopulateLabelsText();
		}
		#endregion    
       
    }
}