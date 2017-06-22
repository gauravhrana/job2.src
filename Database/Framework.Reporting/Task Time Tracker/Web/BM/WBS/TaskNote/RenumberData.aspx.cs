using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.TaskNote
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

				var newId = Framework.Components.Core.SystemEntityTypeDataManager.GetNextSequence("TaskNote", (int)Framework.Components.DataAccess.SystemEntity.TaskNote, SessionVariables.RequestProfile);
				var TaskNotedata = new DataModel.TaskTimeTracker.Task.TaskNoteDataModel();

				var systemDevData = new DataModel.Framework.Core.SystemDevNumbersDataModel();
                systemDevData.ApplicationUserId = SessionVariables.RequestProfile.AuditId;

				var dtnumbers = Framework.Components.Core.SystemDevNumbersDataManager.Search(systemDevData, SessionVariables.RequestProfile);
				var rangefrom = Convert.ToInt32(dtnumbers.Rows[0][DataModel.Framework.Core.SystemDevNumbersDataModel.DataColumns.RangeFrom].ToString());
				var rangeto = Convert.ToInt32(dtnumbers.Rows[0][DataModel.Framework.Core.SystemDevNumbersDataModel.DataColumns.RangeTo].ToString());
                
				if (Request.QueryString["SuperKey"] != null)
                {
                    superKey = Request.QueryString["SuperKey"].ToString();

					var data = new DataModel.Framework.Core.SuperKeyDetailDataModel();
                    data.SuperKeyId = Convert.ToInt32(superKey);
                    data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.TaskNote;

					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
							var key = Convert.ToInt32(dr[DataModel.Framework.Core.SuperKeyDetailDataModel.DataColumns.EntityKey]);
                            TaskNotedata.TaskNoteId = key;

                            var TaskNotedt = TaskTimeTracker.Components.BusinessLayer.Task.TaskNoteDataManager.GetDetails(TaskNotedata, SessionVariables.RequestProfile);

                            if (TaskNotedt.Rows.Count == 1)
                            {
                                var row = TaskNotedt.Rows[0];

                                if (Request.QueryString["Mode"].ToString().Equals("Test"))
                                {
                                    TaskNotedata.TaskNoteId = GetNextValidId(rangefrom);
                                }

								TaskNotedata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
								TaskNotedata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
								TaskNotedata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

                                TaskTimeTracker.Components.BusinessLayer.Task.TaskNoteDataManager.Create(TaskNotedata, SessionVariables.RequestProfile);


                            }
                        }
                    }
                }
                else if (Request.QueryString["SetId"] != null)
                {
                    var key = int.Parse(Request.QueryString["SetId"]);
                    TaskNotedata.TaskNoteId = key;

                    var TaskNotedt = TaskTimeTracker.Components.BusinessLayer.Task.TaskNoteDataManager.GetDetails(TaskNotedata, SessionVariables.RequestProfile);

                    if (TaskNotedt.Rows.Count == 1)
                    {
                        var row = TaskNotedt.Rows[0];

                        var newTaskNotedata = new DataModel.TaskTimeTracker.Task.TaskNoteDataModel();
                        if (Request.QueryString["Mode"].ToString().Equals("Test"))
                            newTaskNotedata.TaskNoteId = newId = (int)GetNextValidId(rangefrom);
                        newTaskNotedata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
                        newTaskNotedata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
                        newTaskNotedata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

                       // UpdatedFKDepenedencies(key, newId);
                        TaskTimeTracker.Components.BusinessLayer.Task.TaskNoteDataManager.Delete(TaskNotedata, SessionVariables.RequestProfile);
                        TaskTimeTracker.Components.BusinessLayer.Task.TaskNoteDataManager.Create(newTaskNotedata, SessionVariables.RequestProfile);
                    }


                }
                else if (Request.QueryString["Mode"].ToString().Equals("Renumber"))
                {
                    var seed = int.Parse(Request.QueryString["Seed"].ToString());
                    //ApplicationVariables.Seed;
                    var increment = int.Parse(Request.QueryString["Increment"].ToString());
                    //ApplicationVariables.Increment;
                    TaskTimeTracker.Components.BusinessLayer.Task.TaskNoteDataManager.Renumber(seed, increment, SessionVariables.RequestProfile);
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
            var dt = TaskTimeTracker.Components.BusinessLayer.Task.TaskNoteDataManager.GetList(SessionVariables.RequestProfile);

            foreach (DataRow dr in dt.Rows)
            {
				if (dr[DataModel.TaskTimeTracker.Task.TaskNoteDataModel.DataColumns.TaskNoteId].ToString().Equals(tempId.ToString()))
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
        //        var TaskNotexprojectdt = TaskTimeTracker.Components.BusinessLayer.TaskNoteXProject.GetByTaskNote(oldId, SessionVariables.RequestProfile.AuditId);
        //        var projectIds = new int[TaskNotexprojectdt.Rows.Count];
        //        for (int i = 0; i < TaskNotexprojectdt.Rows.Count; i++)
        //        {
        //            projectIds[i] = int.Parse(TaskNotexprojectdt.Rows[i][Components.BusinessLayer.TaskNoteXProject.DataColumns.ProjectId].ToString());
        //        }
        //        if (projectIds.Length > 0)
        //        {
        //            Components.BusinessLayer.TaskNoteXProject.DeleteByTaskNote(oldId, SessionVariables.RequestProfile.AuditId);
        //            Components.BusinessLayer.TaskNoteXProject.CreateByTaskNote(newId, projectIds, SessionVariables.RequestProfile.AuditId);

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Response.Write(ex.Message);
        //    }
        //}
    }
}