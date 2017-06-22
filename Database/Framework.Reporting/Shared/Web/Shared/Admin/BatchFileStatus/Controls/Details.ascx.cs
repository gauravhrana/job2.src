using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.Import;

namespace Shared.UI.Web.BatchFileStatus.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

		#region private methods

		protected override void ShowData(int batchFileStatusId)
		{
			base.ShowData(batchFileStatusId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new BatchFileStatusDataModel();
			data.BatchFileStatusId = batchFileStatusId;

			var items = Framework.Components.Import.BatchFileStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblBatchFileStatusId.Text = item.BatchFileStatusId.ToString();
				lblName.Text = item.Name;
				lblDescription.Text = item.Description;
				lblSortOrder.Text = item.SortOrder.ToString();

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, batchFileStatusId, "BatchFileStatus");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblBatchFileStatusIdText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.BatchFileStatusLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.BatchFileStatus;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynBatchFileStatusId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblBatchFileStatusIdText.Visible = isTesting;
				lblBatchFileStatusId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion
        
	}

}