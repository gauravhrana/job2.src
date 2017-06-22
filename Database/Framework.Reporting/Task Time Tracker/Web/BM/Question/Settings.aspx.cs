using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Question
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.Question, "Question");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new QuestionDataModel();

            data.QuestionId          = int.Parse(values[0].ToString());
            data.QuestionPhrase      = values[1].ToString();
			data.QuestionCategoryId = int.Parse(values[2].ToString());
            data.SortOrder           = int.Parse(values[3].ToString());
            QuestionDataManager.Update(data, SessionVariables.RequestProfile);
            ReBindEditableGrid();
        }

        private void ReBindEditableGrid()
        {
            var data = new QuestionDataModel();
            var dtQuestion = QuestionDataManager.Search(data, SessionVariables.RequestProfile);

        }
    }
}