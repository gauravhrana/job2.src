using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;


namespace ApplicationContainer.UI.Web.Question.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {
        #region properties

        public int? QuestionId
        {
            get
            {
                if (txtQuestionId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtQuestionId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtQuestionId.Text);
                }
            }
            set
            {
                txtQuestionId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public string Question
        {
            get
            {
                return txtQuestion.Text;
            }
            set
            {
                txtQuestion.Text = value ?? String.Empty;
            }
        }

        public int? Category
        {
			get
			{
				var isTesting = SessionVariables.IsTesting;
				if (isTesting)
					return int.Parse(txtCategoryId.Text.Trim());
				else
					return int.Parse(drpCategoryList.SelectedItem.Value);
			}
			set
			{
				txtCategoryId.Text = (value == null) ? String.Empty : value.ToString();
			}
		
        }

        public int? SortOrder
        {
            get
            {
                return Framework.Components.DefaultDataRules.CheckAndGetSortOrder(txtSortOrder.Text);
            }
            set
            {
                txtSortOrder.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        #endregion properties

        #region methods

        public override int? Save(string action)
        {
            var data = new QuestionDataModel();

            data.QuestionId = QuestionId;
            data.QuestionPhrase = Question;
			data.QuestionCategoryId = Category;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtQuestion = QuestionDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtQuestion.Rows.Count == 0)
                {
                    QuestionDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                QuestionDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return QuestionId;
        }

        public override void SetId(int setId, bool chkQuestionId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkQuestionId);
            txtQuestionId.Enabled = chkQuestionId;
            //txtCategory.Enabled = !chkQuestionId;
            //txtQuestion.Enabled = !chkQuestionId;
            //txtSortOrder.Enabled = !chkQuestionId;
        }

        public void LoadData(int questionId, bool showId)
        {

            Clear();

            var data = new QuestionDataModel();
            data.QuestionId = questionId;
            var items = QuestionDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);


            if (items.Count != 1) return;

            var item = items[0];

            QuestionId = item.QuestionId;
            Question = item.QuestionPhrase;
			Category = item.QuestionCategoryId;
            SortOrder = item.SortOrder;

            if (!showId)
            {
                txtQuestionId.Text = item.QuestionId.ToString();
                oHistoryList.Setup(PrimaryEntity, questionId, PrimaryEntityKey);
            }
            else
            {
                txtQuestionId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new QuestionDataModel();

            QuestionId = data.QuestionId;
            Question = data.QuestionPhrase;
			Category = data.QuestionCategoryId;
            SortOrder = data.SortOrder;
        }

		private void SetupDropdown()
		{
			var isTesting = SessionVariables.IsTesting;
			var questionCategoryData = QuestionCategoryDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(questionCategoryData, drpCategoryList, StandardDataModel.StandardDataColumns.Name, QuestionCategoryDataModel.DataColumns.QuestionCategoryId);

			if (isTesting)
			{
				drpCategoryList.AutoPostBack = true;
				if (drpCategoryList.Items.Count > 0)
				{
					if (!string.IsNullOrEmpty(txtCategoryId.Text.Trim()))
					{
						drpCategoryList.SelectedValue = txtCategoryId.Text;
					}
					else
					{
						txtCategoryId.Text = drpCategoryList.SelectedItem.Value;
					}
				}
				txtCategoryId.Visible = true;
			}
			else
			{
				if (!string.IsNullOrEmpty(txtCategoryId.Text.Trim()))
				{
					drpCategoryList.SelectedValue = txtCategoryId.Text;
				}
			}
		}

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				txtQuestionId.Visible = isTesting;
				lblQuestionId.Visible = isTesting;
				SetupDropdown();
			}
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Question;
            PrimaryEntityKey = "Question";
            FolderLocationFromRoot = "Question";

            PlaceHolderCore = dynQuestionId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

		protected void drpCategoryList_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtCategoryId.Text = drpCategoryList.SelectedItem.Value;
		}

        #endregion

    }
}