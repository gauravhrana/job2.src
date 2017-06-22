using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Import;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;

namespace Shared.UI.Web.BatchFileHistory.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

		#region variables


		#endregion


        #region private methods

        

        protected override void ShowData(int batchFileHistoryId)
        {
            var data = new BatchFileHistoryDataModel();
            data.BatchFileHistoryId = batchFileHistoryId;

			var oDetail = Framework.Components.Import.BatchFileHistoryDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oDetail != null)
            {

                lblBatchFileHistoryId.Text = oDetail.BatchFileHistoryId.ToString();
				lblBatchFileId.Text        = oDetail.BatchFileId.ToString();
				lblBatchFileSet.Text       = oDetail.BatchFileSetId.ToString();
				lblBatchFileStatus.Text    = oDetail.BatchFileStatusId.ToString();

			}
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblBatchFileHistoryId.Text = String.Empty;
            lblBatchFileId.Text = String.Empty;
            lblBatchFileSet.Text = String.Empty;
            lblBatchFileStatus.Text = String.Empty;           

		}

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblBatchFileHistoryIdText, lblBatchFileIdText, lblBatchFileSetText
													  , lblBatchFileStatusText});
			
			if (Cache[CacheConstants.BatchFileHistoryLabelDictionary] == null)
			{
				UIHelper.PopulateLabelsText(null, (int)Framework.Components.DataAccess.SystemEntity.BatchFileHistory, SessionVariables.RequestProfile.AuditId, labelslist);
				Cache.Add(CacheConstants.BatchFileHistoryLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);
			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.BatchFileHistoryLabelDictionary];
				UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.BatchFileHistory, SessionVariables.RequestProfile.AuditId, labelslist);
			}

		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblBatchFileHistoryIdText.Visible = isTesting;
				lblBatchFileHistoryId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DictionaryLabel			= CacheConstants.BatchFileHistoryLabelDictionary;
			PrimaryEntity			= SystemEntity.BatchFileHistory;

			PlaceHolderCore			= dynBatchFileHistoryId;
			PlaceHolderAuditHistory = dynAuditHistory;

			BorderDiv				= borderdiv;
			
		}
		

		#endregion

	}

}