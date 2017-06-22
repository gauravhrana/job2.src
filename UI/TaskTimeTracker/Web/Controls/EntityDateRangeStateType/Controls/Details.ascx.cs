using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.EntityDateRangeStateType.Controls
{
	public partial class Details : ControlDetailsStandard
	{
		#region private methods

		protected override void ShowData(int entityDateRangeStateTypeId)
		{
			base.ShowData(entityDateRangeStateTypeId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new EntityDateRangeStateTypeDataModel();
			data.EntityDateRangeStateTypeId = entityDateRangeStateTypeId;

            var items = EntityDateRangeStateTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

				SetData(item);

				oHistoryList.Setup(PrimaryEntity, entityDateRangeStateTypeId, "EntityDateRangeStateType");
			}
		}

		public void SetData(EntityDateRangeStateTypeDataModel data)
		{
			SystemKeyId = data.EntityDateRangeStateTypeId;

			base.SetData(data);
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblEntityDateRangeStateTypeIdText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new EntityDateRangeStateTypeDataModel();

			SetData(data);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DictionaryLabel = CacheConstants.EntityDateRangeStateTypeLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.EntityDateRangeStateType;

			PlaceHolderCore = dynEntityDateRangeStateTypeId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

			CoreSystemKey = lblEntityDateRangeStateTypeId;
			CoreControlName = lblName;
			CoreControlDescription = lblDescription;
			CoreControlSortOrder = lblSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblEntityDateRangeStateTypeIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion
	}
}