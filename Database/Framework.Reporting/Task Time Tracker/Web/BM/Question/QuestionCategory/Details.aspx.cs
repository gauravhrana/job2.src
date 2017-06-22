using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.TaskTimeTracker;
using TaskTimeTracker.Components.BusinessLayer;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;


namespace ApplicationContainer.UI.Web.QuestionCategory
{
     
    public partial class Details : PageDetails
    {

        #region Methods

        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.AddTab("QuestionCategory", detailsControl, String.Empty, true);

            var listControl = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("Question", listControl);

			listControl.Setup("Question", String.Empty, "QuestionId", setId, true, GetData, GetQuestionColumns, "QuestionCategory");
            listControl.SetSession("true");

            tabControl.Setup("QuestionCategoryDetailsView");

            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetQuestionData(int.Parse(key));
		}

        private DetailTab1Control GetVerticalTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetail1TabControl();

            tabControl.AddTab("QuestionCategory", detailsControl, "QuestionCategory", false);

            var listControl = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("Question", listControl, "Question");

            listControl.Setup("Question", String.Empty, "QuestionId", setId, true, GetData, GetQuestionColumns, "Question");
            listControl.SetSession("true");

            return tabControl;
        }

        private DataTable GetQuestionData(int? QuestionCategoryId)
        {
            QuestionCategoryDataModel data = new QuestionCategoryDataModel();
            data.QuestionCategoryId = QuestionCategoryId;
            var dt = QuestionCategoryDataManager.GetDetails(data,SessionVariables.RequestProfile);
            var Questiondt = QuestionDataManager.GetList(SessionVariables.RequestProfile);
            var resultdt = Questiondt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                var rows = Questiondt.Select("QuestionId = " + row[QuestionDataModel.DataColumns.QuestionId]);
                resultdt.ImportRow(rows[0]);
            }

            return resultdt;
        }

        private string[] GetQuestionColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(SystemEntity.Question, "DBColumns", SessionVariables.RequestProfile);
        }

        #endregion



        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.Question;
            PrimaryEntityKey = "Question";
            DetailsControlPath = ApplicationCommon.GetControlPath("Question", ControlType.DetailsControl);
            PrimaryPlaceHolder = oDetailsControl.PlaceHolderDetails;
            BreadCrumbObject = Master.BreadCrumbObject;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BreadCrumbObject = Master.BreadCrumbObject;
        }


        #endregion

    }
}
