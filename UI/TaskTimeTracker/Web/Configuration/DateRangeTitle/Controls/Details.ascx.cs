using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace Shared.UI.Web.Configuration.DateRangeTitle.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
	{
		#region private methods

		protected override void ShowData(int dateRangeTitleId)
		{
			base.ShowData(dateRangeTitleId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new DateRangeTitleDataModel();
			
			data.DateRangeTitleId = dateRangeTitleId;

			var items = Framework.Components.UserPreference.DateRangeTitleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				SetData(item);

				oHistoryList.Setup(PrimaryEntity, dateRangeTitleId, "DateRangeTitle");
			}            					
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblDateRangeTitleId, lblNameText, lblDescriptionText
													  , lblSortOrderText});
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new DateRangeTitleDataModel();

			SetData(data);
		}

		public void SetData(DateRangeTitleDataModel item)
		{
			SystemKeyId = item.DateRangeTitleId;

			base.SetData(item);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblDateRangeTitleIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.DateRangeTitleLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.DateRangeTitle;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynDateRangeTitleId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

			CoreSystemKey = lblDateRangeTitleId;
			CoreControlName = lblName;
			CoreControlDescription = lblDescription;
			CoreControlSortOrder = lblSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}
		

		#endregion
	}
}