using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TestCaseManagement;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.TCM.TestSuiteXTestCase.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

     #region variables       

       public TestSuiteXTestCaseDataModel SearchParameters
        {
            get
            {
                var data = new TestSuiteXTestCaseDataModel();
                
				data.TestSuiteId = GetParameterValueAsInt(TestSuiteXTestCaseDataModel.DataColumns.TestSuiteId);
				data.TestCaseId = GetParameterValueAsInt(TestSuiteXTestCaseDataModel.DataColumns.TestCaseId);
				data.TestCaseStatusId = GetParameterValueAsInt(TestSuiteXTestCaseDataModel.DataColumns.TestCaseStatusId);
				data.TestCasePriorityId = GetParameterValueAsInt(TestSuiteXTestCaseDataModel.DataColumns.TestCasePriorityId);

				GroupBy = GetParameterValue("GroupBy");

				SubGroupBy = GetParameterValue("SubGroupBy");
                return data;
            }
        }
        #endregion


	   #region Methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);
			
			if (fieldName.Equals("TestSuiteId"))
			{
                var testSuitedata = TestCaseManagement.Components.DataAccess.TestSuiteDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(testSuitedata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					TestSuiteDataModel.DataColumns.TestSuiteId);
			}

			if (fieldName.Equals("TestCaseId"))
			{
				var testCasedata = TestCaseManagement.Components.DataAccess.TestCaseDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(testCasedata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					TestCaseDataModel.DataColumns.TestCaseId);
			}

			if (fieldName.Equals("TestCaseStatusId"))
			{
                var testCaseStatusdata = TestCaseManagement.Components.DataAccess.TestCaseStatusDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(testCaseStatusdata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					TestCaseStatusDataModel.DataColumns.TestCaseStatusId);
			}

			if (fieldName.Equals("TestCasePriorityId"))
			{
                var testCasePrioritydata = TestCaseManagement.Components.DataAccess.TestCasePriorityDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(testCasePrioritydata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					TestCasePriorityDataModel.DataColumns.TestCasePriorityId);
			}
		}

	   #endregion     

	   #region Events

	   protected void Page_Load(object sender, EventArgs e)
	   {
		   base.OnLoad(e);
	   }

	   protected override void OnInit(EventArgs e)
	   {
		   base.OnInit(e);

		   PrimaryEntityKey = "TestSuiteXTestCase";
		   FolderLocationFromRoot = "TCM/TestSuiteXTestCase";
		   PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestSuiteXTestCase;

		   SearchActionBarCore = oSearchActionBar;
		   SearchParametersRepeaterCore = SearchParametersRepeater;
	   }

	   #endregion
    }
}

        