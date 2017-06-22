using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TestCaseManagement;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.TCM.TestSuite
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();
			var data = new TestSuiteDataModel();
            UpdatedData = TestCaseManagement.Components.DataAccess.TestSuiteDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.TestSuiteId =
                    Convert.ToInt32(SelectedData.Rows[i][TestSuiteDataModel.DataColumns.TestSuiteId].ToString());
                data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

                TestCaseManagement.Components.DataAccess.TestSuiteDataManager.Update(data, SessionVariables.RequestProfile);
				data = new TestSuiteDataModel();
                data.TestSuiteId = Convert.ToInt32(SelectedData.Rows[i][TestSuiteDataModel.DataColumns.TestSuiteId].ToString());
                var dt = TestCaseManagement.Components.DataAccess.TestSuiteDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }
       
        protected override DataTable GetEntityData(int? entityKey)
        {
            var testSuitedata = new TestSuiteDataModel();
            testSuitedata.TestSuiteId = entityKey;
            var results = TestCaseManagement.Components.DataAccess.TestSuiteDataManager.Search(testSuitedata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestSuite;
            PrimaryEntityKey = "TestSuite";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}