using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.Entity.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region variables




		#endregion

		#region private methods

		public void Setup(int EntityId)
		{
			ShowData(EntityId);
		}

		protected override void ShowData(int entityId)
		{
			oDetailButtonPanel.SetId = SetId;
			var data = new EntityDataModel();
			data.EntityId = entityId;

			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.EntityDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

				lblEntityId.Text = item.EntityId.ToString();
				lblApplicationId.Text = SessionVariables.RequestProfile.ApplicationId.ToString();
				lblName.Text = item.Name;
				lblDescription.Text = item.Description;
				lblSortOrder.Text = item.SortOrder.ToString();
			

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, entityId, "Entity");
			}
			else
			{
				Clear();
			}
		}

		protected override void Clear()
		{
			lblEntityId.Text = String.Empty;
			lblName.Text = String.Empty;
			lblApplicationId.Text = string.Empty;
			lblDescription.Text = String.Empty;
			lblSortOrder.Text = String.Empty;
			
		}

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblEntityIdText
													  , lblNameText, lblApplicationIdText, lblDescriptionText, lblSortOrderText});
			if (Cache[CacheConstants.EntityLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.Entity, SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.SystemEntityTypeLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.EntityLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.Entity, SessionVariables.RequestProfile.AuditId, labelslist);


		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblEntityIdText, lblNameText, lblApplicationIdText, lblDescriptionText, lblSortOrderText});
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.EntityLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Entity;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynEntityId;
			PlaceHolderAuditHistory = dynAuditHistory;

			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblEntityIdText.Visible = isTesting;
				lblEntityId.Visible = isTesting;
			}
			PopulateLabelsText();
		}




		#endregion

	}
}