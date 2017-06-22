using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TestCaseManagement;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.TCM.TestSuiteXTestCase
{
    public partial class InlineUpdate : PageInlineUpdate
    {
        #region Methods

        protected override DataTable GetData()
        {
            try
            {
                SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();

                var selectedrows = new DataTable();
                var testSuiteXTestCasedata = new TestSuiteXTestCaseDataModel();

				selectedrows = TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseDataManager.GetDetails(testSuiteXTestCasedata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
                {
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        testSuiteXTestCasedata.TestSuiteXTestCaseId = entityKey;
						var result = TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseDataManager.GetDetails(testSuiteXTestCasedata, SessionVariables.RequestProfile);
                        selectedrows.ImportRow(result.Rows[0]);
                    }
                }
                else
                {
                    testSuiteXTestCasedata.TestSuiteXTestCaseId = SetId;
					var result = TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseDataManager.GetDetails(testSuiteXTestCasedata, SessionVariables.RequestProfile);
                    selectedrows.ImportRow(result.Rows[0]);

                }
                return selectedrows;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

            return null;
        }

        protected override void Update(Dictionary<string, string> values)
        {
            var data = new TestSuiteXTestCaseDataModel();

			PropertyMapper.CopyProperties(data, values);

			TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseDataManager.Update(data, SessionVariables.RequestProfile);
            base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestSuiteXTestCase;
            PrimaryEntityKey = "TestSuiteXTestCase";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}