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

namespace ApplicationContainer.UI.Web.TCM.TestCase
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();

            var data = new TestCaseDataModel();
            UpdatedData = TestCaseManagement.Components.DataAccess.TestCaseDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.TestCaseId =
                    Convert.ToInt32(SelectedData.Rows[i][TestCaseDataModel.DataColumns.TestCaseId].ToString());
                data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

                TestCaseManagement.Components.DataAccess.TestCaseDataManager.Update(data, SessionVariables.RequestProfile);
                data = new TestCaseDataModel();
                data.TestCaseId = Convert.ToInt32(SelectedData.Rows[i][TestCaseDataModel.DataColumns.TestCaseId].ToString());
                var dt = TestCaseManagement.Components.DataAccess.TestCaseDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }
       
        protected override DataTable GetEntityData(int? entityKey)
        {
            var testCasedata = new TestCaseDataModel();
            testCasedata.TestCaseId = entityKey;
            var results = TestCaseManagement.Components.DataAccess.TestCaseDataManager.Search(testCasedata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestCase;
            PrimaryEntityKey = "TestCase";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}