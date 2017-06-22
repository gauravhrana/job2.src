using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.Import;

namespace Shared.UI.Web.BatchFileSet.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

		#region private methods

		protected override void ShowData(int batchFileSetId)
		{
			base.ShowData(batchFileSetId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new BatchFileSetDataModel();
			data.BatchFileSetId = batchFileSetId;

			var items = Framework.Components.Import.BatchFileSetDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);			

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblBatchFileSetId.Text = item.BatchFileSetId.ToString();
				lblName.Text = item.Name;
				lblDescription.Text = item.Description;
				
				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, batchFileSetId, "BatchFileSet");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblBatchFileSetIdText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.BatchFileSetLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.BatchFileSet;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynBatchFileSetId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblBatchFileSetIdText.Visible = isTesting;
				lblBatchFileSetId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion
        
	}

}