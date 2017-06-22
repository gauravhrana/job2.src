using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityHistory.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region variables




        #endregion

        #region private methods

        public void Setup(int functionalityId)
        {
            ShowData(functionalityId);
        }

        protected override void ShowData(int functionalityId)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new FunctionalityHistoryDataModel();
            data.FunctionalityHistoryId = functionalityId;

			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityHistoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblFunctionalityHistoryId.Text = item.FunctionalityHistoryId.ToString();
                lblFunctionalityId.Text = item.FunctionalityId.ToString();
                lblApplicationId.Text = SessionVariables.RequestProfile.ApplicationId.ToString();
                lblName.Text = item.Name;
                lblDescription.Text = item.Description;
                lblSortOrder.Text = item.SortOrder.ToString();
                lblFunctionalityActiveStatusId.Text = item.FunctionalityActiveStatus.ToString();
                lblFunctionalityPriorityId.Text = item.FunctionalityPriority.ToString();
                lblMemo.Text = item.Memo;
                lblAcknowledgedBy.Text = item.AcknowledgedBy;
                lblKnowledgeDate.Text = item.KnowledgeDate.ToString();

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, functionalityId, "FunctionalityHistory");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblFunctionalityHistoryId.Text = String.Empty;
            lblFunctionalityId.Text = String.Empty;
            lblName.Text = String.Empty;
            lblApplicationId.Text = string.Empty;
            lblDescription.Text = String.Empty;
            lblSortOrder.Text = String.Empty;
            lblFunctionalityActiveStatusId.Text = string.Empty;
            lblFunctionalityPriorityId.Text = String.Empty;
            lblMemo.Text = string.Empty;
            lblAcknowledgedBy.Text = string.Empty;
            lblKnowledgeDate.Text = string.Empty;
        }

        protected override void PopulateLabelsText()
        {
            var validColumns = new Dictionary<string, string>();
            var labelslist = new List<Label>(new Label[] { lblFunctionalityIdText
													  , lblNameText, lblApplicationIdText, lblDescriptionText, lblSortOrderText});
            labelslist = GetLabels();
            if (Cache[CacheConstants.FunctionalityLabelDictionary] == null)
            {
                validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.FunctionalityHistory, SessionVariables.RequestProfile.AuditId);
                Cache.Add(CacheConstants.SystemEntityTypeLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

            }

            else
            {
                validColumns = (Dictionary<string, string>)Cache[CacheConstants.FunctionalityHistoryLabelDictionary];
            }
            UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.Functionality, SessionVariables.RequestProfile.AuditId, labelslist);


        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblFunctionalityHistoryIdText, lblFunctionalityIdText, lblNameText, lblApplicationIdText, lblDescriptionText, lblSortOrderText, lblFunctionalityActiveStatusIdText, lblFunctionalityPriorityIdText, lblMemoText, lblAcknowledgeByText, lblKnowledgeDateText });
            }

            return LabelListCore;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.FunctionalityHistoryLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityHistory;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynFunctionalityHistoryId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblFunctionalityHistoryIdText.Visible = isTesting;
                lblFunctionalityHistoryId.Visible = isTesting;
            }
            PopulateLabelsText();
        }




        #endregion

    }
}