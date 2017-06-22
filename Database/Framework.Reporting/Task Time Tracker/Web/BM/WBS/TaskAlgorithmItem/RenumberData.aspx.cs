using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TaskTimeTracker.TimeTracking;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.WBS.TaskAlgorithmItemItem
{
	public partial class RenumberData : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override void OnInit(EventArgs e)
		{
			try
			{
				var superKey = "";
				var newId = SystemEntityTypeDataManager.GetNextSequence("TaskAlgorithmItem", (int)SystemEntity.TaskAlgorithmItem, SessionVariables.RequestProfile);
				var TaskAlgorithmItemdata = new TaskAlgorithmItemDataModel();
				var systemdevdata = new SystemDevNumbersDataModel();
				systemdevdata.ApplicationUserId = SessionVariables.RequestProfile.AuditId;
				var dtnumbers = SystemDevNumbersDataManager.Search(systemdevdata, SessionVariables.RequestProfile);
				var rangefrom =
					Convert.ToInt32(dtnumbers.Rows[0][SystemDevNumbersDataModel.DataColumns.RangeFrom].ToString());
				var rangeto =
					Convert.ToInt32(dtnumbers.Rows[0][SystemDevNumbersDataModel.DataColumns.RangeTo].ToString());
				if (Request.QueryString["SuperKey"] != null)
				{
					superKey = Request.QueryString["SuperKey"].ToString();
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(superKey);
					data.SystemEntityTypeId = (int)SystemEntity.TaskAlgorithmItem;
					var dt = SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (DataRow dr in dt.Rows)
						{
							var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
							TaskAlgorithmItemdata.TaskAlgorithmItemId = key;

                            var TaskAlgorithmItemdt = TaskAlgorithmItemDataManager.GetDetails(TaskAlgorithmItemdata, SessionVariables.RequestProfile);

							if (TaskAlgorithmItemdt.Rows.Count == 1)
							{
								var row = TaskAlgorithmItemdt.Rows[0];

								if (Request.QueryString["Mode"].ToString().Equals("Test"))
								{
									TaskAlgorithmItemdata.TaskAlgorithmItemId = GetNextValidId(rangefrom);
								}
								TaskAlgorithmItemdata.ActivityId = Convert.ToInt32(row[TaskAlgorithmItemDataModel.DataColumns.ActivityId]);
								TaskAlgorithmItemdata.Description = Convert.ToString(row[TaskAlgorithmItemDataModel.DataColumns.Description]);
								TaskAlgorithmItemdata.SortOrder = Convert.ToInt32(row[TaskAlgorithmItemDataModel.DataColumns.SortOrder]);

								TaskAlgorithmItemDataManager.Create(TaskAlgorithmItemdata, SessionVariables.RequestProfile);


							}
						}
					}
				}
				else if (Request.QueryString["SetId"] != null)
				{
					var key = int.Parse(Request.QueryString["SetId"]);
					TaskAlgorithmItemdata.TaskAlgorithmItemId = key;

					var TaskAlgorithmItemdt = TaskAlgorithmItemDataManager.GetDetails(TaskAlgorithmItemdata, SessionVariables.RequestProfile);

					if (TaskAlgorithmItemdt.Rows.Count == 1)
					{
						var row = TaskAlgorithmItemdt.Rows[0];

						var newTaskAlgorithmItemdata = new TaskAlgorithmItemDataModel();
						if (Request.QueryString["Mode"].ToString().Equals("Test"))
							newTaskAlgorithmItemdata.TaskAlgorithmItemId = newId = (int)GetNextValidId(rangefrom);
						newTaskAlgorithmItemdata.ActivityId = Convert.ToInt32(row[TaskAlgorithmItemDataModel.DataColumns.ActivityId]);
						newTaskAlgorithmItemdata.Description = Convert.ToString(row[TaskAlgorithmItemDataModel.DataColumns.Description]);
						newTaskAlgorithmItemdata.SortOrder = Convert.ToInt32(row[TaskAlgorithmItemDataModel.DataColumns.SortOrder]);

						UpdatedFKDepenedencies(key, newId);
						TaskAlgorithmItemDataManager.Delete(TaskAlgorithmItemdata, SessionVariables.RequestProfile);
						TaskAlgorithmItemDataManager.Create(newTaskAlgorithmItemdata, SessionVariables.RequestProfile);
					}


				}
				else if (Request.QueryString["Mode"].ToString().Equals("Renumber"))
				{
					var seed = int.Parse(Request.QueryString["Seed"].ToString());
					//ApplicationVariables.Seed;
					var increment = int.Parse(Request.QueryString["Increment"].ToString());
					//ApplicationVariables.Increment;
					TaskAlgorithmItemDataManager.Renumber(seed, increment, SessionVariables.RequestProfile);
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
            var dt = TaskAlgorithmItemDataManager.GetList(SessionVariables.RequestProfile);

			foreach (DataRow dr in dt.Rows)
			{
				if (dr[TaskAlgorithmItemDataModel.DataColumns.TaskAlgorithmItemId].ToString().Equals(tempId.ToString()))
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