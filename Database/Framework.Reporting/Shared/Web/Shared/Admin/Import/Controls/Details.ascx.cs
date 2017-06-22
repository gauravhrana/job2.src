using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.Import;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.BatchFile.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

		#region private methods		

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[]  { lblBatchFileIdText, lblNameText, lblDescriptionText, lblFolderText, lblBatchFileText,
													  lblBatchFileSetText, lblFileTypeText, lblSystemEntityTypeText, lblBatchFileStatusText, lblCreatedDateText,  lblCreatedByPersonText, lblErrors});
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.BatchFileLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.BatchFile;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynBatchFileId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblBatchFileIdText.Visible = isTesting;
				lblBatchFileId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion

        #region private methods        

        protected override void ShowData(int batchFileId)
        {
            oDetailButtonPanel.SetId = SetId;
            var data =new BatchFileDataModel();
            data.BatchFileId = batchFileId;

            var dt = Framework.Components.Import.BatchFileDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];
                lblBatchFileId.Text  = Convert.ToString(row[BatchFileDataModel.DataColumns.BatchFileId]);
                lblName.Text         = Convert.ToString(row[BatchFileDataModel.DataColumns.Name]);
				lblFolder.Text = Convert.ToString(row[BatchFileDataModel.DataColumns.Folder]);
				lblBatchFile.Text = Convert.ToString(row[BatchFileDataModel.DataColumns.BatchFile]);
				lblBatchFileSet.Text = Convert.ToString(row[BatchFileDataModel.DataColumns.BatchFileSetId]);
				lblDescription.Text = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);

				lblFileType.Text = Convert.ToString(row[BatchFileDataModel.DataColumns.FileTypeId]);
                lblSystemEntityType.Text = Convert.ToString(row[BatchFileDataModel.DataColumns.SystemEntityTypeId]);
                lblBatchFileStatus.Text  = Convert.ToString(row[BatchFileDataModel.DataColumns.BatchFileStatusId]);

				lblCreatedDate.Text = Convert.ToString(row[BatchFileDataModel.DataColumns.CreatedDate]);
				lblCreatedByPerson.Text = Convert.ToString(row[BatchFileDataModel.DataColumns.CreatedByPersonId]);
				lblErrors.Text = Convert.ToString(row[BatchFileDataModel.DataColumns.Errors]);

				oUpdateInfo.LoadText(dt.Rows[0]);

                oHistoryList.Setup("AuditHistory", "Audit", "AuditHistoryId", true, true, (int)Framework.Components.DataAccess.SystemEntity.BatchFile, batchFileId, "BatchFile");
                dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "BatchFile");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblBatchFileId.Text = String.Empty;
            lblName.Text = String.Empty;
            lblDescription.Text = String.Empty;

            lblFolder.Text = String.Empty;
            lblBatchFile.Text = String.Empty;
            lblBatchFileSet.Text = String.Empty;

            lblFileType.Text = String.Empty;
            lblSystemEntityType.Text = String.Empty;
            lblBatchFileStatus.Text = String.Empty;

            lblCreatedDate.Text = String.Empty;
            lblCreatedByPerson.Text = String.Empty;            
            lblErrors.Text = String.Empty;
		}	

		#endregion		

	}

}