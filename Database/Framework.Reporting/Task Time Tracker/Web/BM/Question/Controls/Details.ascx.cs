using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Question.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region private methods

        protected override void ShowData(int questionId)
        {
            base.ShowData(questionId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new QuestionDataModel();
            data.QuestionId = questionId;

            var items = QuestionDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblQuestionId.Text = item.QuestionId.ToString();
                lblQuestion.Text = item.QuestionPhrase;
				lblCategory.Text = item.QuestionCategory;
                lblSortOrder.Text = item.SortOrder.ToString();

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, questionId, "Question");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblQuestionIdText, lblQuestionText, lblCategoryText, lblSortOrderText });
            }

            return LabelListCore;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            DictionaryLabel = CacheConstants.QuestionLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Question;

            base.OnInit(e);

            PlaceHolderCore = dynQuestionId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblQuestionIdText.Visible = isTesting;
                lblQuestionId.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }

}