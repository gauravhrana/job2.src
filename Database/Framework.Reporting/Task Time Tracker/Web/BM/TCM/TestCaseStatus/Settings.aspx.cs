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

namespace ApplicationContainer.UI.Web.TCM.TestCaseStatus
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.TestCaseStatus, "TestCaseStatus");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new TestCaseStatusDataModel();

            data.TestCaseStatusId    = int.Parse(values[0].ToString());
            data.Name                = values[1].ToString();
            data.Description         = values[2].ToString();
            data.SortOrder           = int.Parse(values[3].ToString());

            TestCaseManagement.Components.DataAccess.TestCaseStatusDataManager.Update(data, SessionVariables.RequestProfile);
            ReBindEditableGrid();
        }

        private void ReBindEditableGrid()
        {
            var data = new TestCaseStatusDataModel();
            var dtTestCaseStatus = TestCaseManagement.Components.DataAccess.TestCaseStatusDataManager.Search(data, SessionVariables.RequestProfile);

        }
    }
}