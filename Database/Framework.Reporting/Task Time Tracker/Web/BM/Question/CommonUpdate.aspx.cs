using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Question
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();

            var data = new QuestionDataModel();
            UpdatedData = QuestionDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.QuestionId =
                    Convert.ToInt32(SelectedData.Rows[i][QuestionDataModel.DataColumns.QuestionId].ToString());
                data.QuestionPhrase = SelectedData.Rows[i][QuestionDataModel.DataColumns.QuestionPhrase].ToString();
				data.QuestionCategoryId = Convert.ToInt32(SelectedData.Rows[i][QuestionDataModel.DataColumns.QuestionCategoryId].ToString());
                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(QuestionDataModel.DataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(QuestionDataModel.DataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][QuestionDataModel.DataColumns.SortOrder].ToString());

                QuestionDataManager.Update(data, SessionVariables.RequestProfile);
                data = new QuestionDataModel();
                data.QuestionId = Convert.ToInt32(SelectedData.Rows[i][QuestionDataModel.DataColumns.QuestionId].ToString());
                var dt = QuestionDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }
       
        protected override DataTable GetEntityData(int? entityKey)
        {
            var questiondata = new QuestionDataModel();
            questiondata.QuestionId = entityKey;
            var results = QuestionDataManager.Search(questiondata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Question;
            PrimaryEntityKey = "Question";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}