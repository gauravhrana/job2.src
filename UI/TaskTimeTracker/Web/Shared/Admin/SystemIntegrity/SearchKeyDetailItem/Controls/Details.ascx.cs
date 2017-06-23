using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.SearchKeyDetailItem.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{	

		#region private methods

		protected override void ShowData(int searchKeyDetailItemid)
		{
			base.ShowData(searchKeyDetailItemid);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new SearchKeyDetailItemDataModel();
			data.SearchKeyDetailItemId = searchKeyDetailItemid;

			var items = Framework.Components.Core.SearchKeyDetailItemDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblSearchKeyDetailItemId.Text = item.SearchKeyDetailItemId.ToString();
				lblSearchKeyDetailId.Text = item.SearchKeyDetailId.ToString();
				lblValue.Text = item.Value.ToString();
				lblSortOrder.Text = item.SortOrder.ToString();

				//oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, searchKeyDetailItemid, "SearchKeyDetailItem");
			}
			
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblTextSearchKeyDetailId, lblTextSearchKeyDetailItemId,lblTextValue, lblTextSortOrder });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.SearchKeyDetailItemLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SearchKeyDetailItem;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynSearchKeyDetailItemId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblSearchKeyDetailItemId.Visible = false;
				lblTextSearchKeyDetailItemId.Visible = false;
			}
		}		

		#endregion
	}
}