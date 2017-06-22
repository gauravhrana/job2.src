using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.TimeTracking;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.WBS.TaskAlgorithmAlgorithm
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
				var newId = Framework.Components.Core.SystemEntityTypeDataManager.GetNextSequence("TaskAlgorithm", (int)Framework.Components.DataAccess.SystemEntity.TaskAlgorithm, SessionVariables.RequestProfile);
				var TaskAlgorithmdata = new TaskAlgorithmDataModel();
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
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.TaskAlgorithm;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (System.Data.DataRow dr in dt.Rows)
						{
							var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
							TaskAlgorithmdata.TaskAlgorithmId = key;

							var TaskAlgorithmdt = TaskTimeTracker.Components.Module.TimeTracking.TaskAlgorithmDataManager.GetDetails(TaskAlgorithmdata, SessionVariables.RequestProfile);

							if (TaskAlgorithmdt.Rows.Count == 1)
							{
								var row = TaskAlgorithmdt.Rows[0];

								if (Request.QueryString["Mode"].ToString().Equals("Test"))
								{
									TaskAlgorithmdata.TaskAlgorithmId = GetNextValidId(rangefrom);
								}
								TaskAlgorithmdata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
								TaskAlgorithmdata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
								TaskAlgorithmdata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

								TaskTimeTracker.Components.Module.TimeTracking.TaskAlgorithmDataManager.Create(TaskAlgorithmdata, SessionVariables.RequestProfile);


							}
						}
					}
				}
				else if (Request.QueryString["SetId"] != null)
				{
					var key = int.Parse(Request.QueryString["SetId"]);
					TaskAlgorithmdata.TaskAlgorithmId = key;

					var TaskAlgorithmdt = TaskTimeTracker.Components.Module.TimeTracking.TaskAlgorithmDataManager.GetDetails(TaskAlgorithmdata, SessionVariables.RequestProfile);

					if (TaskAlgorithmdt.Rows.Count == 1)
					{
						var row = TaskAlgorithmdt.Rows[0];

						var newTaskAlgorithmdata = new TaskAlgorithmDataModel();
						if (Request.QueryString["Mode"].ToString().Equals("Test"))
							newTaskAlgorithmdata.TaskAlgorithmId = newId = (int)GetNextValidId(rangefrom);
						newTaskAlgorithmdata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
						newTaskAlgorithmdata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
						newTaskAlgorithmdata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

						UpdatedFKDepenedencies(key, newId);
						TaskTimeTracker.Components.Module.TimeTracking.TaskAlgorithmDataManager.Delete(TaskAlgorithmdata, SessionVariables.RequestProfile);
						TaskTimeTracker.Components.Module.TimeTracking.TaskAlgorithmDataManager.Create(newTaskAlgorithmdata, SessionVariables.RequestProfile);
					}


				}
				else if (Request.QueryString["Mode"].ToString().Equals("Renumber"))
				{
					var seed = int.Parse(Request.QueryString["Seed"].ToString());
					//ApplicationVariables.Seed;
					var increment = int.Parse(Request.QueryString["Increment"].ToString());
					//ApplicationVariables.Increment;
					TaskTimeTracker.Components.Module.TimeTracking.TaskAlgorithmDataManager.Renumber(seed, increment, SessionVariables.RequestProfile);
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
			var dt = TaskTimeTracker.Components.Module.TimeTracking.TaskAlgorithmDataManager.GetList(SessionVariables.RequestProfile);

			foreach (DataRow dr in dt.Rows)
			{
				if (dr[TaskAlgorithmDataModel.DataColumns.TaskAlgorithmId].ToString().Equals(tempId.ToString()))
				{
					tempId -= 1;
					return GetNextValidId(tempId);
				}
			}

			return tempId;
		}

		private void UpdatedFKDepenedencies(int oldId, int newId)
		{
			try
			{

			}
			catch (Exception ex)
			{

				Response.Write(ex.Message);
			}
		}
	}
}