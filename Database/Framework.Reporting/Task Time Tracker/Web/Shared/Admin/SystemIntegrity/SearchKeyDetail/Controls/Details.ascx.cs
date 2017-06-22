using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace Shared.UI.Web.SystemIntegrity.SearchKeyDetail.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{
		#region private methods

		protected override void ShowData(int searchKeyDetailid)
		{
			base.ShowData(searchKeyDetailid);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new SearchKeyDetailDataModel();
			data.SearchKeyDetailId = searchKeyDetailid;

			var items = Framework.Components.Core.SearchKeyDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblSearchKeyDetailId.Text = item.SearchKeyDetailId.ToString();
				lblSearchKeyName.Text = item.SearchKey;
				lblSearchParameter.Text = item.SearchParameter.ToString();
				lblSortOrder.Text = item.SortOrder.ToString();

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, searchKeyDetailid, "SearchKeyDetail");
			}
			
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblSearchKeyDetailIdText, lblSearchKeyText, lblSearchParameterText, lblSortOrderText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.SearchKeyDetailLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SearchKeyDetail;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynSearchKeyDetailId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblSearchKeyDetailId.Visible = false;
				lblSearchKeyDetailIdText.Visible = false;
			}
		}

		

		#endregion
	}
}