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

            var oDetail = Framework.Components.Import.BatchFileDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oDetail != null)
            {
                lblBatchFileId.Text      = oDetail.BatchFileId.ToString();
                lblName.Text             = oDetail.Name;
				lblFolder.Text           = oDetail.Folder;
				lblBatchFile.Text        = oDetail.BatchFile;
				lblBatchFileSet.Text     = oDetail.BatchFileSetId.ToString();
				lblDescription.Text      = oDetail.Description;
                                           
				lblFileType.Text         = oDetail.FileTypeId.ToString();
                lblSystemEntityType.Text = oDetail.SystemEntityTypeId.ToString();
                lblBatchFileStatus.Text  = oDetail.BatchFileStatusId.ToString();
                                           
				lblCreatedDate.Text      = oDetail.CreatedDate.ToString();
				lblCreatedByPerson.Text  = oDetail.CreatedByPersonId.ToString();
				lblErrors.Text           = oDetail.Errors;

				oUpdateInfo.LoadText(oDetail);

                oHistoryList.Setup("AuditHistory", "Audit", "AuditHistoryId", true, true, (int)Framework.Components.DataAccess.SystemEntity.BatchFile, batchFileId, "BatchFile");
                dynAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "BatchFile");
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