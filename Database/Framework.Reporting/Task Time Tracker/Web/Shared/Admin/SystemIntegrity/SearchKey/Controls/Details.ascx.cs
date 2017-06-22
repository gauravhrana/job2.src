using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace Shared.UI.Web.SystemIntegrity.SearchKey.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region private methods

		protected override void ShowData(int searchKeyid)
		{
			base.ShowData(searchKeyid);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new SearchKeyDataModel();
			data.SearchKeyId = searchKeyid;

			var items = Framework.Components.Core.SearchKeyDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblSearchKeyId.Text = item.SearchKeyId.ToString();
				lblName.Text = item.Name;
				lblDescription.Text = item.Description;
				lblSortOrder.Text = item.SortOrder.ToString();
				lblView.Text = item.View.ToString();

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, searchKeyid, "SearchKey");
			}            

		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblSearchKeyIdText, lblNameText, lblDescriptionText, lblSortOrderText,lblViewText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblSearchKeyId.Visible = false;
				lblSearchKeyIdText.Visible = false;				
			}
		}

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.SearchKeyLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SearchKey;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynSearchKeyId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}
		

		#endregion
	}
}