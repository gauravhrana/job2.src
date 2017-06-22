using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;


namespace ApplicationContainer.UI.Web.EventNotification.NotificationRegistrar.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

		#region private methods

		protected override void ShowData(int notificationRegistrarId)
		{
			base.ShowData(notificationRegistrarId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new NotificationRegistrarDataModel();
			data.NotificationRegistrarId = notificationRegistrarId;

			var items = Framework.Components.EventMonitoring.NotificationRegistrarDataManager.GetEntityList(data, SessionVariables.RequestProfile);
			

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblNotificationRegistrarId.Text = item.NotificationRegistrarId.ToString();
				lblNotificationEventTypeId.Text = item.NotificationEventTypeId.ToString();
				lblNotificationPublisherId.Text = item.NotificationPublisherId.ToString();
				lblMessage.Text = item.Message.ToString();

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, notificationRegistrarId, "NotificationRegistrar");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblNotificationRegistrarIdText, lblNotificationEventTypeText, lblNotificationPublisherText, lblMessageText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DictionaryLabel = CacheConstants.NotificationRegistrarLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.NotificationRegistrar;

			PlaceHolderCore = dynNotificationRegistrarId;
			PlaceHolderAuditHistory = dynAuditHistory;

			BorderDiv = borderdiv;			
		}
		
		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblNotificationRegistrarIdText.Visible = isTesting;
				lblNotificationRegistrarId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion        

    }
}