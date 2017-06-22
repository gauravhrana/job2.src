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

namespace ApplicationContainer.UI.Web.TCM.TestSuiteXTestCase
{
    public partial class CommonUpdate : Framework.UI.Web.BaseClasses.PageCommonUpdate
    {
        private DataTable selectedData;
		
     

        private DataTable GetData()
        {
            return SelectedData;
        }

      

        protected void Page_Load(object sender, EventArgs e)
        {
            DynamicUpdatePanel.SetUp(GetColumns(), "TestSuiteXTestCase", GetData());
        }

        override protected void btnBack_Click(object sender, EventArgs e)
        {
			Response.Redirect(Page.GetRouteUrl("TestSuiteXTestCaseEntityRoute", new { Action = "Default" }), false);        
        }

     
        override protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var UpdatedData = new DataTable();
            var data = new TestSuiteXTestCaseDataModel();
			UpdatedData = TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.TestSuiteXTestCaseId =
                    Convert.ToInt32(SelectedData.Rows[i][TestSuiteXTestCaseDataModel.DataColumns.TestSuiteXTestCaseId].ToString());

                data.TestSuiteId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TestSuiteXTestCaseDataModel.DataColumns.TestSuiteId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(TestSuiteXTestCaseDataModel.DataColumns.TestSuiteId).ToString())
                    : int.Parse(SelectedData.Rows[i][TestSuiteXTestCaseDataModel.DataColumns.TestSuiteId].ToString());

                data.TestCaseId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TestSuiteXTestCaseDataModel.DataColumns.TestCaseId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(TestSuiteXTestCaseDataModel.DataColumns.TestCaseId).ToString())
                    : int.Parse(SelectedData.Rows[i][TestSuiteXTestCaseDataModel.DataColumns.TestCaseId].ToString());

                data.TestCaseStatusId=
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TestSuiteXTestCaseDataModel.DataColumns.TestCaseStatusId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(TestSuiteXTestCaseDataModel.DataColumns.TestCaseStatusId).ToString())
                    : int.Parse(SelectedData.Rows[i][TestSuiteXTestCaseDataModel.DataColumns.TestCaseStatusId].ToString());

                data.TestCasePriorityId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(TestSuiteXTestCaseDataModel.DataColumns.TestCasePriorityId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(TestSuiteXTestCaseDataModel.DataColumns.TestCasePriorityId).ToString())
                    : int.Parse(SelectedData.Rows[i][TestSuiteXTestCaseDataModel.DataColumns.TestCasePriorityId].ToString());

				TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseDataManager.Update(data, SessionVariables.RequestProfile);
                data = new TestSuiteXTestCaseDataModel();
                data.TestSuiteXTestCaseId = Convert.ToInt32(SelectedData.Rows[i][TestSuiteXTestCaseDataModel.DataColumns.TestSuiteXTestCaseId].ToString());
				var dt = TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
                // if everything is done and good THEN move from thsi page.
                //Response.Redirect("Default.aspx?Added=" + true, false);

            }
            DynamicUpdatePanel.SetUp(GetColumns(), "TestSuiteXTestCase", UpdatedData);
        }

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "TestSuiteXTestCaseDefaultView";
			
		}

        protected override void OnInit(EventArgs e)
        {

            try
            {
                DynamicUpdatePanel.AddColumns(GetColumns());
				SuperKey = ApplicationCommon.GetSuperKey();
				SetId = ApplicationCommon.GetSetId();
                var key = 0;
                var TestSuiteXTestCasedata = new TestSuiteXTestCaseDataModel();
				var results = TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseDataManager.Search(TestSuiteXTestCasedata, SessionVariables.RequestProfile).Clone();

				if (!string.IsNullOrEmpty(SuperKey))
				{
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(SuperKey);

                    // Change System Entity Type
                    data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.TestSuiteXTestCase;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
                            TestSuiteXTestCasedata.TestSuiteXTestCaseId = key;
							var TestSuiteXTestCasedt = TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseDataManager.Search(TestSuiteXTestCasedata, SessionVariables.RequestProfile);
                            if (TestSuiteXTestCasedt.Rows.Count == 1)
                            {
                                results.ImportRow(TestSuiteXTestCasedt.Rows[0]);
                            }
                        }
                    }
                }
                else
                {
                    key = SetId;
                    TestSuiteXTestCasedata.TestSuiteXTestCaseId = key;
					var TestSuiteXTestCasedt = TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseDataManager.Search(TestSuiteXTestCasedata, SessionVariables.RequestProfile);
                    if (TestSuiteXTestCasedt.Rows.Count == 1)
                    {
                        results.ImportRow(TestSuiteXTestCasedt.Rows[0]);
                    }
                }
                SelectedData = new DataTable();
                SelectedData = results.Copy();
                base.OnInit(e);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                //throw
            }
        }
    }
}