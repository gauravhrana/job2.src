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


namespace ApplicationContainer.UI.Web.TCM.TestCase
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
                var newId = Framework.Components.Core.SystemEntityTypeDataManager.GetNextSequence("TestCase", (int)Framework.Components.DataAccess.SystemEntity.TestCase, SessionVariables.RequestProfile);
				var TestCasedata = new TestCaseDataModel();
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
                    data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.TestCase;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
                            TestCasedata.TestCaseId = key;

                            var TestCasedt = TestCaseManagement.Components.DataAccess.TestCaseDataManager.GetDetails(TestCasedata, SessionVariables.RequestProfile);

                            if (TestCasedt.Rows.Count == 1)
                            {
                                var row = TestCasedt.Rows[0];

                                if (Request.QueryString["Mode"].ToString().Equals("Test"))
                                {
                                    TestCasedata.TestCaseId = GetNextValidId(rangefrom);
                                }
                                TestCasedata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
                                TestCasedata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
                                TestCasedata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

                                TestCaseManagement.Components.DataAccess.TestCaseDataManager.Create(TestCasedata, SessionVariables.RequestProfile);
                            }
                        }
                    }
                }
                else if (Request.QueryString["SetId"] != null)
                {
                    var key = int.Parse(Request.QueryString["SetId"]);
                    TestCasedata.TestCaseId = key;

                    var TestCasedt = TestCaseManagement.Components.DataAccess.TestCaseDataManager.GetDetails(TestCasedata, SessionVariables.RequestProfile);

                    if (TestCasedt.Rows.Count == 1)
                    {
                        var row = TestCasedt.Rows[0];

						var newTestCasedata = new TestCaseDataModel();
                        if (Request.QueryString["Mode"].ToString().Equals("Test"))
                            newTestCasedata.TestCaseId = newId = (int)GetNextValidId(rangefrom);
                        newTestCasedata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
                        newTestCasedata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
                        newTestCasedata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

                        // UpdatedFKDepenedencies(key, newId);
                        TestCaseManagement.Components.DataAccess.TestCaseDataManager.Delete(TestCasedata, SessionVariables.RequestProfile);
                        TestCaseManagement.Components.DataAccess.TestCaseDataManager.Create(newTestCasedata, SessionVariables.RequestProfile);
                    }
                }
                else if (Request.QueryString["Mode"].ToString().Equals("Renumber"))
                {
                    var seed = int.Parse(Request.QueryString["Seed"].ToString());
                    //ApplicationVariables.Seed;
                    var increment = int.Parse(Request.QueryString["Increment"].ToString());
                    //ApplicationVariables.Increment;
                    TestCaseManagement.Components.DataAccess.TestCaseDataManager.Renumber(seed, increment, SessionVariables.RequestProfile);
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
            var dt = TestCaseManagement.Components.DataAccess.TestCaseDataManager.GetList(SessionVariables.RequestProfile);

            foreach (DataRow dr in dt.Rows)
            {
                if (dr[TestCaseDataModel.DataColumns.TestCaseId].ToString().Equals(tempId.ToString()))
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
        //        var TestCasexprojectdt = TaskTimeTracker.Components.BusinessLayer.TestCaseXProject.GetByTestCase(oldId, SessionVariables.RequestProfile.AuditId);
        //        var projectIds = new int[TestCasexprojectdt.Rows.Count];
        //        for (var i = 0; i < TestCasexprojectdt.Rows.Count; i++)
        //        {
        //            projectIds[i] = int.Parse(TestCasexprojectdt.Rows[i][TestCaseXProject.DataColumns.ProjectId].ToString());
        //        }
        //        if (projectIds.Length > 0)
        //        {
        //            Components.BusinessLayer.TestCaseXProject.DeleteByTestCase(oldId, SessionVariables.RequestProfile.AuditId);
        //            Components.BusinessLayer.TestCaseXProject.CreateByTestCase(newId, projectIds, SessionVariables.RequestProfile.AuditId);

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Response.Write(ex.Message);
        //    }
        //}
    }
}