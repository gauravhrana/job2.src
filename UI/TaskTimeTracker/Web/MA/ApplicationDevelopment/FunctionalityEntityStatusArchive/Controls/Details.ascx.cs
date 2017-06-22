using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatusArchive.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {	
	
        #region variables


        #endregion

        #region private methods

        protected override void ShowData(int functionalityEntityStatusArchiveid)
        {
            //oDetailButtonPanel.SetId = SetId;
            var data = new FunctionalityEntityStatusArchiveDataModel();
            data.FunctionalityEntityStatusArchiveId = functionalityEntityStatusArchiveid;
			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusArchiveDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblFunctionalityEntityStatusArchiveId.Text        = Convert.ToString(item.FunctionalityEntityStatusArchiveId);
                lblRecordDate.Text                                = Convert.ToString(item.RecordDate);
                lblSystemEntityType.Text                          = Convert.ToString(item.SystemEntityType);
                lblFunctionality.Text                             = Convert.ToString(item.Functionality);
                lblFunctionalityStatus.Text                       = Convert.ToString(item.FunctionalityStatus);
                lblFunctionalityPriority.Text                     = Convert.ToString(item.FunctionalityPriority);
                lblFunctionalityEntityStatusId.Text               = Convert.ToString(item.FunctionalityEntityStatusId);
                lblAssignedTo.Text                                = Convert.ToString(item.AssignedTo);
                lblMemo.Text                                      = Convert.ToString(item.Memo);
                lblKnowledgeDate.Text                             = Convert.ToString(item.KnowledgeDate);
				lblTargetDate.Text								  = Convert.ToString(item.TargetDate);
				lblStartDate.Text								  = Convert.ToString(item.StartDate);
				lblAcknowledgedById.Text                          = Convert.ToString(item.AcknowledgedById);
                lblAcknowledgedBy.Text                            = Convert.ToString(item.AcknowledgedBy);

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, functionalityEntityStatusArchiveid, "FunctionalityEntityStatusArchive");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
           lblFunctionalityEntityStatusArchiveId.Text        = String.Empty;
           lblRecordDate.Text                                = String.Empty;
           lblSystemEntityType.Text                          = String.Empty;
           lblFunctionality.Text                             = String.Empty;
           lblFunctionalityStatus.Text                       = String.Empty;
           lblFunctionalityPriority.Text                     = String.Empty;
           lblFunctionalityEntityStatusId.Text               = String.Empty;
           lblAssignedTo.Text                                = String.Empty;
           lblMemo.Text                                      = String.Empty;
           lblKnowledgeDate.Text                             = String.Empty;
           lblAcknowledgedById.Text                          = String.Empty;
           lblAcknowledgedBy.Text                            = String.Empty;
        }

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
            var labelslist = new List<Label>(new Label[] { lblFunctionalityEntityStatusArchiveIdText,  lblRecordDateText,
                                                        lblSystemEntityTypeText, lblFunctionalityText, lblFunctionalityStatusText, lblFunctionalityPriorityText, lblFunctionalityEntityStatusIdText,
													    lblAssignedToText, lblMemoText, 
                                                        lblKnowledgeDateText, lblAcknowledgedByIdText, lblAcknowledgedByText
            });
			if (Cache[CacheConstants.FunctionalityEntityStatusArchiveLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.FunctionalityEntityStatusArchive, SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.FunctionalityEntityStatusArchiveLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.FunctionalityEntityStatusArchiveLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.FunctionalityEntityStatusArchive, SessionVariables.RequestProfile.AuditId, labelslist);

		}

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblAcknowledgedByIdText, lblAcknowledgedByText, lblAssignedToText, 
                    lblFunctionalityEntityStatusArchiveIdText, lblFunctionalityEntityStatusIdText, 
                    lblFunctionalityPriorityText, lblFunctionalityText, lblFunctionalityStatusText, lblKnowledgeDateText, 
                    lblMemoText, lblRecordDateText, lblStartDateText, lblSystemEntityTypeText, lblTargetDateText });
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
                lblFunctionalityEntityStatusArchiveIdText.Visible = isTesting;
                lblFunctionalityEntityStatusArchiveId.Visible = isTesting;
            }
			PopulateLabelsText();
        }

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.FunctionalityEntityStatusArchiveLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityEntityStatusArchive;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynFunctionalityEntityStatusArchiveId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

        #endregion

    }

}