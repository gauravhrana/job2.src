using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.EntityXWorkTicket.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region variables

		#endregion

		#region private methods

		protected override void ShowData(int entityXWorkTicketid)
		{
			oDetailButtonPanel.SetId = SetId;
			var data = new EntityXWorkTicketDataModel();
			data.EntityXWorkTicketId = entityXWorkTicketid;
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityXWorkTicketDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];
				lblEntityXWorkTicketId.Text = item.EntityXWorkTicketId.ToString();
				lblEntityId.Text = item.Entity;
				lblWorkTicketId.Text = item.WorkTicket;
				lblAcknowledgedBy.Text = item.AcknowledgedBy;
				lblMemo.Text = item.Memo;
				lblKnowledgeDate.Text = item.KnowledgeDate.ToString();


				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, entityXWorkTicketid, "EntityWorkTicket");
			}
			else
			{
				Clear();
			}
		}

		protected override void Clear()
		{
			lblEntityXWorkTicketId.Text = String.Empty;
			lblEntityId.Text = String.Empty;
			lblWorkTicketId.Text = String.Empty;
			lblAcknowledgedBy.Text = String.Empty;
			lblMemo.Text = String.Empty;
			lblKnowledgeDate.Text = String.Empty;

		}

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblEntityXWorkTicketIdText, 
                                                         lblEntityText, lblWorkTicketText, 
													    lblAcknowledgedByText, lblMemoText, lblKnowledgeDateText});
			if (Cache[CacheConstants.EntityXWorkTicketLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.EntityXWorkTicket, SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.EntityXWorkTicketLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.EntityXWorkTicketLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.EntityXWorkTicket, SessionVariables.RequestProfile.AuditId, labelslist);

		}


		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblEntityXWorkTicketIdText.Visible = isTesting;
				lblEntityXWorkTicketId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.EntityXWorkTicketLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.EntityXWorkTicket;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynEntityXWorkTicketId;
			PlaceHolderAuditHistory = dynAuditHistory;

			BorderDiv = borderdiv;
		}


		#endregion

	}

}