using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TestCaseManagement;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.TCM.TestSuiteXTestCase
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {
		
        #region Methods

        private DataTable GetTestSuiteXTestCaseArchiveData(int TestSuiteXTestCaseId)
        {
            var data = new TestSuiteXTestCaseArchiveDataModel();
            data.TestSuiteXTestCaseId = TestSuiteXTestCaseId;
			var dt = TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseArchiveDataManager.SearchHistory(data, SessionVariables.RequestProfile);
            return dt;
        }

        private string[] GetTestSuiteXTestCaseArchiveColumns()
        {
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.TestSuiteXTestCaseArchive, "TestSuiteXTestCaseArchive_PC", SessionVariables.RequestProfile);
        }

        //private Shared.UI.Web.Controls.DetailTabControl GetTabControl(int setId, Control detailsControl)
        //{
            
        //    var tabControl = ApplicationCommon.GetNewDetailTabControl();
           
        //    var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
        //    listControl.Setup("TestSuiteXTestCaseArchive", "", "TestSuiteXTestCaseArchiveId", setId, true, GetData, GetTestSuiteXTestCaseArchiveColumns, "TestSuiteXTestCaseArchive");
        //    listControl.SetSession("true");

        //    tabControl.Setup("TestSuiteXTestCaseDetailsView");
        //    tabControl.AddTab("TestSuiteXTestCase", detailsControl, String.Empty, true);
        //    tabControl.AddTab("TestSuiteXTestCaseArchive", listControl, "History");
        //    return tabControl;
        //}

		private DataTable GetData(string key)
		{
			return GetTestSuiteXTestCaseArchiveData(int.Parse(key));
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "TestSuiteXTestCase";
			var detailsPath = ApplicationCommon.GetControlPath("TestSuiteXTestCase", ControlType.DetailsControl);
			DetailsControlPath = detailsPath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestSuiteXTestCase;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

       

    }
}