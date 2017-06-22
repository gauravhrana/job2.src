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

namespace ApplicationContainer.UI.Web.TCM.TestCase
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.TestCase, "TestCase");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new TestCaseDataModel();

            data.TestCaseId          = int.Parse(values[0].ToString());
            data.Name                = values[1].ToString();
            data.Description         = values[2].ToString();
            data.SortOrder           = int.Parse(values[3].ToString());

            TestCaseManagement.Components.DataAccess.TestCaseDataManager.Update(data, SessionVariables.RequestProfile);
            ReBindEditableGrid();
        }

        private void ReBindEditableGrid()
        {
            var data = new TestCaseDataModel();
            var dtTestCase = TestCaseManagement.Components.DataAccess.TestCaseDataManager.Search(data, SessionVariables.RequestProfile);

        }
    }
}