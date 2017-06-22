using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.QuestionCategory.Controls
{
    public partial class Generic : ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new QuestionCategoryDataModel();

            data.QuestionCategoryId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtQuestionCategory = QuestionCategoryDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtQuestionCategory.Rows.Count == 0)
                {
					QuestionCategoryDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				QuestionCategoryDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.QuestionCategoryId;
        }

        public override void SetId(int setId, bool chkQuestionCategoryId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkQuestionCategoryId);
            CoreSystemKey.Enabled = chkQuestionCategoryId;
            //txtDescription.Enabled = !chkQuestionCategoryId;
            //txtName.Enabled = !chkQuestionCategoryId;
            //txtSortOrder.Enabled = !chkQuestionCategoryId;
        }

        public void LoadData(int questionCategoryId, bool showId)
        {
            Clear();

            var data = new QuestionCategoryDataModel();
			data.QuestionCategoryId = questionCategoryId;

			var items = QuestionCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.QuestionCategoryId;
                oHistoryList.Setup(PrimaryEntity, questionCategoryId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new QuestionCategoryDataModel();

            SetData(data);
        }

        public void SetData(QuestionCategoryDataModel data)
        {
            SystemKeyId = data.QuestionCategoryId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblQuestionCategoryId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.QuestionCategory;
            PrimaryEntityKey = "QuestionCategory";
            FolderLocationFromRoot = "QuestionCategory";

            PlaceHolderCore = dynQuestionCategoryId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtQuestionCategoryId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}