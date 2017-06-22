using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Configuration.SystemForeignRelationshipDatabase.Controls
{
	public partial class Details : ControlDetailsStandard
	{
		#region private methods

		protected override void ShowData(int SystemForeignRelationshipDatabaseId)
		{

			base.ShowData(SystemForeignRelationshipDatabaseId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new SystemForeignRelationshipDatabaseDataModel();

			data.SystemForeignRelationshipDatabaseId = SystemForeignRelationshipDatabaseId;

			var items = Framework.Components.Core.SystemForeignRelationshipDatabaseDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				SetData(item);

				oHistoryList.Setup(PrimaryEntity, SystemForeignRelationshipDatabaseId, "SystemForeignRelationshipDatabase");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblSystemForeignRelationshipDatabaseIdText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new SystemForeignRelationshipDatabaseDataModel();

			SetData(data);
		}

		public void SetData(SystemForeignRelationshipDatabaseDataModel item)
		{
			SystemKeyId = item.SystemForeignRelationshipDatabaseId;

			base.SetData(item);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblSystemForeignRelationshipDatabaseIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.SystemForeignRelationshipDatabaseLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemForeignRelationshipDatabase;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynSystemForeignRelationshipDatabaseId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

			CoreSystemKey = lblSystemForeignRelationshipDatabaseId;
			CoreControlName = lblName;
			CoreControlDescription = lblDescription;
			CoreControlSortOrder = lblSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}


		#endregion
	}
}