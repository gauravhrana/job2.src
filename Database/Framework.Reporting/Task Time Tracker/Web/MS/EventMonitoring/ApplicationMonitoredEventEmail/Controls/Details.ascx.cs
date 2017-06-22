using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.ApplicationMonitoredEventEmail.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

		#region private methods

		protected override void ShowData(int applicationMonitoredEventEmailId)
		{
			base.ShowData(applicationMonitoredEventEmailId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ApplicationMonitoredEventEmailDataModel();
			data.ApplicationMonitoredEventEmailId = applicationMonitoredEventEmailId;

			var items = Framework.Components.EventMonitoring.ApplicationMonitoredEventEmailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblApplicationMonitoredEventEmailId.Text = item.ApplicationMonitoredEventEmailId.ToString();
				lblApplicationMonitoredEventSource.Text = item.ApplicationMonitoredEventSource.ToString();
				lblUser.Text							= item.User.ToString();
				lblCorrespondenceLevel.Text				= item.CorrespondenceLevel.ToString();
				lblActive.Text							= item.Active.ToString();
				
				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, applicationMonitoredEventEmailId, "ApplicationMonitoredEventEmail");
			}

		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblApplicationMonitoredEventEmailIdText, lblApplicationMonitoredEventSourceText, lblCorrespondenceLevelText, lblUserText, lblActiveText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ApplicationMonitoredEventEmailLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMonitoredEventEmail;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynApplicationMonitoredEventEmailId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblApplicationMonitoredEventEmailId.Visible = isTesting;
				lblApplicationMonitoredEventEmailIdText.Visible = isTesting;
			}
			PopulateLabelsText();
		}
		#endregion     

	}

}