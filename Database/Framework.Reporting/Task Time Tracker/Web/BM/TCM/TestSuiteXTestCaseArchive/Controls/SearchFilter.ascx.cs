using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TestCaseManagement;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.TCM.TestSuiteXTestCaseArchive.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables
		public TestSuiteXTestCaseArchiveDataModel SearchParameters
        {
            get
            {
				var data = new TestSuiteXTestCaseArchiveDataModel();
                data.TestSuite = UIHelper.RefineAndGetSearchText(txtSearchConditionTestSuite.Text.Trim(), SettingCategory);
                data.TestCase = UIHelper.RefineAndGetSearchText(txtSearchConditionTestCase.Text.Trim(), SettingCategory);
                data.TestCaseStatus = UIHelper.RefineAndGetSearchText(txtSearchConditionTestCaseStatus.Text.Trim(), SettingCategory);
                data.TestCasePriority = UIHelper.RefineAndGetSearchText(txtSearchConditionTestCasePriority.Text.Trim(), SettingCategory);
                if (!string.IsNullOrEmpty(txtSearchConditionTestSuiteXTestCase.Text.Trim()))
                {
                    data.TestSuiteXTestCaseId=Convert.ToInt32(txtSearchConditionTestSuiteXTestCase.Text.Trim());
                }

                return data;
            }
        }

        #endregion


        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { }

        }


        #endregion

    }
}