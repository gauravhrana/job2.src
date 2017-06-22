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

namespace ApplicationContainer.UI.Web.TCM.TestRun
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
                var newId = Framework.Components.Core.SystemEntityTypeDataManager.GetNextSequence("TestRun", (int)Framework.Components.DataAccess.SystemEntity.TestRun, SessionVariables.RequestProfile);
				var TestRundata = new TestRunDataModel();
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
                    data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.TestRun;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
                            TestRundata.TestRunId = key;

                            var TestRundt = TestCaseManagement.Components.DataAccess.TestRunDataManager.GetDetails(TestRundata, SessionVariables.RequestProfile);

                            if (TestRundt.Rows.Count == 1)
                            {
                                var row = TestRundt.Rows[0];

                                if (Request.QueryString["Mode"].ToString().Equals("Test"))
                                {
                                    TestRundata.TestRunId = GetNextValidId(rangefrom);
                                }
                                TestRundata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
                                TestRundata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
                                TestRundata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

                                TestCaseManagement.Components.DataAccess.TestRunDataManager.Create(TestRundata, SessionVariables.RequestProfile);
                            }
                        }
                    }
                }
                else if (Request.QueryString["SetId"] != null)
                {
                    var key = int.Parse(Request.QueryString["SetId"]);
                    TestRundata.TestRunId = key;

					var TestRundt = TestCaseManagement.Components.DataAccess.TestRunDataManager.GetDetails(TestRundata, SessionVariables.RequestProfile);

                    if (TestRundt.Rows.Count == 1)
                    {
                        var row = TestRundt.Rows[0];

						var newTestRundata = new TestRunDataModel();
                        if (Request.QueryString["Mode"].ToString().Equals("Test"))
                            newTestRundata.TestRunId = newId = (int)GetNextValidId(rangefrom);
                        newTestRundata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
                        newTestRundata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
                        newTestRundata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

                        // UpdatedFKDepenedencies(key, newId);
                        TestCaseManagement.Components.DataAccess.TestRunDataManager.Delete(TestRundata, SessionVariables.RequestProfile);
                        TestCaseManagement.Components.DataAccess.TestRunDataManager.Create(newTestRundata, SessionVariables.RequestProfile);
                    }
                }
                else if (Request.QueryString["Mode"].ToString().Equals("Renumber"))
                {
                    var seed = int.Parse(Request.QueryString["Seed"].ToString());
                    //ApplicationVariables.Seed;
                    var increment = int.Parse(Request.QueryString["Increment"].ToString());
                    //ApplicationVariables.Increment;
					TestCaseManagement.Components.DataAccess.TestRunDataManager.Renumber(seed, increment, SessionVariables.RequestProfile);
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
            var dt = TestCaseManagement.Components.DataAccess.TestRunDataManager.GetList(SessionVariables.RequestProfile);

            foreach (DataRow dr in dt.Rows)
            {
                if (dr[TestRunDataModel.DataColumns.TestRunId].ToString().Equals(tempId.ToString()))
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
        //        var TestRunxprojectdt = TaskTimeTracker.Components.BusinessLayer.TestRunXProject.GetByTestRun(oldId, SessionVariables.RequestProfile.AuditId);
        //        var projectIds = new int[TestRunxprojectdt.Rows.Count];
        //        for (var i = 0; i < TestRunxprojectdt.Rows.Count; i++)
        //        {
        //            projectIds[i] = int.Parse(TestRunxprojectdt.Rows[i][TestRunXProject.DataColumns.ProjectId].ToString());
        //        }
        //        if (projectIds.Length > 0)
        //        {
        //            Components.BusinessLayer.TestRunXProject.DeleteByTestRun(oldId, SessionVariables.RequestProfile.AuditId);
        //            Components.BusinessLayer.TestRunXProject.CreateByTestRun(newId, projectIds, SessionVariables.RequestProfile.AuditId);

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Response.Write(ex.Message);
        //    }
        //}
    }
}