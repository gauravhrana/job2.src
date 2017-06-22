using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TestCaseManagement;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;

namespace ApplicationContainer.UI.Web.TCM.TestSuite
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.TestSuite, "TestSuite");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new TestSuiteDataModel();

            data.TestSuiteId     = int.Parse(values[0].ToString());
            data.Name            = values[1].ToString();
            data.Description     = values[2].ToString();
            data.SortOrder       = int.Parse(values[3].ToString());

            TestCaseManagement.Components.DataAccess.TestSuiteDataManager.Update(data, SessionVariables.RequestProfile);
            ReBindEditableGrid();
        }

        private void ReBindEditableGrid()
        {
            var data = new TestSuiteDataModel();
            var dtTestSuite = TestCaseManagement.Components.DataAccess.TestSuiteDataManager.Search(data, SessionVariables.RequestProfile);

        }
    }
}