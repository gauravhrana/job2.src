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
    public partial class Update : PageUpdate
    {

        #region Methods

        private DataTable GetQuestionCategoryList()
        {
            var dt = QuestionCategoryDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        private DataTable GetAssociatedQuestionCategorys(int questionId)
        {
            QuestionDataModel data = new QuestionDataModel();
            data.QuestionId = questionId;
            var dt = QuestionDataManager.GetDetails(data, SessionVariables.RequestProfile);
            return dt;
        }

        private string[] GetQuestionCategoryColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(SystemEntity.QuestionCategory, "DBColumns", SessionVariables.RequestProfile);
        }

        private DataTable GetQuestionCategoryData(int questionId)
        {
            //var dt = QuestionCategoryDataManager.GetByQuestion(QuestionId, SessionVariables.RequestProfile);
            QuestionDataModel data = new QuestionDataModel();
            data.QuestionId = questionId;
            var dt = QuestionDataManager.GetDetails(data, SessionVariables.RequestProfile);
            var fdt = QuestionCategoryDataManager.GetList(SessionVariables.RequestProfile);
            var resultdt = fdt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                var rows = fdt.Select("QuestionCategoryId = " + row[QuestionCategoryDataModel.DataColumns.QuestionCategoryId]);
                resultdt.ImportRow(rows[0]);
            }
            return resultdt;
        }


        protected override Control GetTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();

            tabControl.Setup("QuestionUpdateView");

            tabControl.AddTab("QuestionCategory", detailsControl, "Question Category", true);

            var listControl = (DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

            tabControl.AddTab("Question", listControl);

			listControl.Setup("Question", "Question", "QuestionId", setId, true, GetData, GetQuestionColumns, "Question");
            listControl.SetSession("true");  


            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetQuestionData(int.Parse(key));
		}

        private DataTable GetQuestionData(int questionCategoryId)
        {
            var data = new QuestionDataModel();
            data.QuestionCategoryId = questionCategoryId;
            var dt = QuestionDataManager.Search(data, SessionVariables.RequestProfile);
            var fdt = QuestionDataManager.GetList(SessionVariables.RequestProfile);
            var resultdt = fdt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                var rows = fdt.Select(QuestionDataModel.DataColumns.QuestionId + " = " + row[QuestionDataModel.DataColumns.QuestionId]);
                resultdt.ImportRow(rows[0]);
            }
            return resultdt;
        }

        private string[] GetQuestionColumns()
        {

            return FieldConfigurationUtility.GetEntityColumns(SystemEntity.Question, "DBColumns", SessionVariables.RequestProfile);
        }

        #endregion

        #region

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.QuestionCategory;

            GenericControlPath = ApplicationCommon.GetControlPath("QuestionCategory", ControlType.GenericControl);
            PrimaryPlaceHolder = plcUpdateList;
            PrimaryEntityKey = "QuestionCategory";
            BreadCrumbObject = Master.BreadCrumbObject;

            BtnUpdate = btnUpdate;
            BtnClone = btnClone;
            BtnCancel = btnCancel;

          #endregion

        }
    }
}