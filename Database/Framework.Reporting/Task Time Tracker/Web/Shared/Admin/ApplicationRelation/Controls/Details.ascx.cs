using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Admin.ApplicationRelation.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region private methods

		protected override void ShowData(int ApplicationRelationId)
		{
			base.ShowData(ApplicationRelationId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ApplicationRelationDataModel();
			data.ApplicationRelationId = ApplicationRelationId;

			var items = Framework.Components.Core.ApplicationRelationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblApplicationRelationId.Text     = item.ApplicationRelationId.ToString();
				lblPublisherApplication.Text      = item.PublisherApplication;
				lblSubscriberApplication.Text     = item.SubscriberApplication;
				lblSystemEntityType.Text          = item.SystemEntityType;
				lblSubscriberApplicationRole.Text = item.SubscriberApplicationRole;

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, ApplicationRelationId, "ApplicationRelation");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblApplicationRelationIdText, lblPublisherApplicationText, 
					lblSubscriberApplicationText, lblSystemEntityTypeText, lblSubscriberApplicationRoleText});
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ApplicationRelationLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationRelation;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynApplicationRelationId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblApplicationRelationIdText.Visible = isTesting;
				lblApplicationRelationId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion

	}
}