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

namespace Shared.UI.Web.Configuration.SystemForeignRelationshipType.Controls
{
	public partial class Details : ControlDetailsStandard
	{
		#region private methods

		protected override void ShowData(int systemForeignRelationshipTypeId)
		{

			base.ShowData(systemForeignRelationshipTypeId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new SystemForeignRelationshipTypeDataModel();

			data.SystemForeignRelationshipTypeId = systemForeignRelationshipTypeId;

			var items = Framework.Components.Core.SystemForeignRelationshipTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				SetData(item);

				oHistoryList.Setup(PrimaryEntity, systemForeignRelationshipTypeId, "SystemForeignRelationshipType");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblSystemForeignRelationshipTypeIdText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new SystemForeignRelationshipTypeDataModel();

			SetData(data);
		}

		public void SetData(SystemForeignRelationshipTypeDataModel item)
		{
			SystemKeyId = item.SystemForeignRelationshipTypeId;

			base.SetData(item);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblSystemForeignRelationshipTypeIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.SystemForeignRelationshipTypeLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemForeignRelationshipType;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynSystemForeignRelationshipTypeId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

			CoreSystemKey = lblSystemForeignRelationshipTypeId;
			CoreControlName = lblName;
			CoreControlDescription = lblDescription;
			CoreControlSortOrder = lblSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}


		#endregion
	}
}