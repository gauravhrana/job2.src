using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.ApplicationMonitoredEvent.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

		#region private methods

		protected override void ShowData(int applicationMonitoredEventId)
		{
			base.ShowData(applicationMonitoredEventId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ApplicationMonitoredEventDataModel();
			data.ApplicationMonitoredEventId = applicationMonitoredEventId;

			var items = Framework.Components.EventMonitoring.ApplicationMonitoredEventDataManager.GetEntityDetails(data,SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblApplicationMonitoredEventId.Text					= item.ApplicationMonitoredEventId.ToString();
				lblApplicationMonitoredEventSource.Text				= item.ApplicationMonitoredEventSource.ToString();
				lblApplicationMonitoredEventProcessingState.Text	= item.ApplicationMonitoredEventProcessingState.ToString();
				lblReferenceId.Text									= item.ReferenceId.ToString();
				lblReferenceCode.Text								= item.ReferenceCode.ToString();
				lblCategory.Text									= item.Category.ToString();
				lblMessage.Text										= item.Message.ToString();
				lblIsDuplicate.Text									= item.IsDuplicate.ToString();
				lblLastModifiedOn.Text								= item.LastModifiedOn.ToString();
				lblLastModifiedBy.Text								= item.LastModifiedBy.ToString();				

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, applicationMonitoredEventId, "ApplicationMonitoredEvent");
			}

		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblApplicationMonitoredEventIdText, lblApplicationMonitoredEventSourceText, lblApplicationMonitoredEventProcessingStateText, lblReferenceIdText, lblReferenceCodeText, lblCategoryText, lblMessageText, lblIsDuplicateText, lblLastModifiedOnText, lblLastModifiedByText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ApplicationMonitoredEventLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMonitoredEvent;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynApplicationMonitoredEventId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblApplicationMonitoredEventIdText.Visible = isTesting;
				lblApplicationMonitoredEventId.Visible = isTesting;
			}
			PopulateLabelsText();
		}
		#endregion     
        
	}

}