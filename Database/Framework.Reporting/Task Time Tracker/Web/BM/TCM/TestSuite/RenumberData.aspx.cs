using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TestCaseManagement;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.TCM.TestSuite
{
    public partial class RenumberData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            try
            {
                var superKey = "";
                var newId = Framework.Components.Core.SystemEntityTypeDataManager.GetNextSequence("TestSuite", (int)Framework.Components.DataAccess.SystemEntity.TestSuite, SessionVariables.RequestProfile);
				var TestSuitedata = new TestSuiteDataModel();
                var systemdevdata = new SystemDevNumbersDataModel();
                systemdevdata.ApplicationUserId = SessionVariables.RequestProfile.AuditId;
				var dtnumbers = Framework.Components.Core.SystemDevNumbersDataManager.Search(systemdevdata, SessionVariables.RequestProfile);
                var rangefrom =
                    Convert.ToInt32(dtnumbers.Rows[0][SystemDevNumbersDataModel.DataColumns.RangeFrom].ToString());
                var rangeto =
                    Convert.ToInt32(dtnumbers.Rows[0][SystemDevNumbersDataModel.DataColumns.RangeTo].ToString());
                if (Request.QueryString["SuperKey"] != null)
                {
                    superKey = Request.QueryString["SuperKey"].ToString();
                    var data = new SuperKeyDetailDataModel();
                    data.SuperKeyId = Convert.ToInt32(superKey);
                    data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.TestSuite;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
                            TestSuitedata.TestSuiteId = key;

                            var TestSuitedt = TestCaseManagement.Components.DataAccess.TestSuiteDataManager.GetDetails(TestSuitedata, SessionVariables.RequestProfile);

                            if (TestSuitedt.Rows.Count == 1)
                            {
                                var row = TestSuitedt.Rows[0];

                                if (Request.QueryString["Mode"].ToString().Equals("Test"))
                                {
                                    TestSuitedata.TestSuiteId = GetNextValidId(rangefrom);
                                }
                                TestSuitedata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
                                TestSuitedata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
                                TestSuitedata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

                                TestCaseManagement.Components.DataAccess.TestSuiteDataManager.Create(TestSuitedata, SessionVariables.RequestProfile);
                            }
                        }
                    }
                }
                else if (Request.QueryString["SetId"] != null)
                {
                    var key = int.Parse(Request.QueryString["SetId"]);
                    TestSuitedata.TestSuiteId = key;

					var TestSuitedt = TestCaseManagement.Components.DataAccess.TestSuiteDataManager.GetDetails(TestSuitedata, SessionVariables.RequestProfile);

                    if (TestSuitedt.Rows.Count == 1)
                    {
                        var row = TestSuitedt.Rows[0];

						var newTestSuitedata = new TestSuiteDataModel();
                        if (Request.QueryString["Mode"].ToString().Equals("Test"))
                            newTestSuitedata.TestSuiteId = newId = (int)GetNextValidId(rangefrom);
                        newTestSuitedata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
                        newTestSuitedata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
                        newTestSuitedata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

                        // UpdatedFKDepenedencies(key, newId);
                        TestCaseManagement.Components.DataAccess.TestSuiteDataManager.Delete(TestSuitedata, SessionVariables.RequestProfile);
                        TestCaseManagement.Components.DataAccess.TestSuiteDataManager.Create(newTestSuitedata, SessionVariables.RequestProfile);
                    }

                }
                else if (Request.QueryString["Mode"].ToString().Equals("Renumber"))
                {
                    var seed = int.Parse(Request.QueryString["Seed"].ToString());
                    //ApplicationVariables.Seed;
                    var increment = int.Parse(Request.QueryString["Increment"].ToString());
                    //ApplicationVariables.Increment;
                    TestCaseManagement.Components.DataAccess.TestSuiteDataManager.Renumber(seed, increment, SessionVariables.RequestProfile);
                }
                base.OnInit(e);

                Response.Redirect("Default.aspx?Added=true", false);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private int GetNextValidId(int tempId)
        {
			var dt = TestCaseManagement.Components.DataAccess.TestSuiteDataManager.GetList(SessionVariables.RequestProfile);

            foreach (DataRow dr in dt.Rows)
            {
                if (dr[TestSuiteDataModel.DataColumns.TestSuiteId].ToString().Equals(tempId.ToString()))
                {
                    tempId -= 1;
                    return GetNextValidId(tempId);
                }
            }

            return tempId;
        }

        //private void UpdatedFKDepenedencies(int oldId, int newId)
        //{
        //    try
        //    {
        //        var TestSuitexprojectdt = TaskTimeTracker.Components.BusinessLayer.TestSuiteXProject.GetByTestSuite(oldId, SessionVariables.RequestProfile.AuditId);
        //        var projectIds = new int[TestSuitexprojectdt.Rows.Count];
        //        for (var i = 0; i < TestSuitexprojectdt.Rows.Count; i++)
        //        {
        //            projectIds[i] = int.Parse(TestSuitexprojectdt.Rows[i][TestSuiteXProject.DataColumns.ProjectId].ToString());
        //        }
        //        if (projectIds.Length > 0)
        //        {
        //            Components.BusinessLayer.TestSuiteXProject.DeleteByTestSuite(oldId, SessionVariables.RequestProfile.AuditId);
        //            Components.BusinessLayer.TestSuiteXProject.CreateByTestSuite(newId, projectIds, SessionVariables.RequestProfile.AuditId);

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Response.Write(ex.Message);
        //    }
        //}
    }
}