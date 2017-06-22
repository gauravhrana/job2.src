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
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.WBS.Activity
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
				var newId = Framework.Components.Core.SystemEntityTypeDataManager.GetNextSequence("Activity", (int)Framework.Components.DataAccess.SystemEntity.Activity, SessionVariables.RequestProfile);
				var Activitydata = new ActivityDataModel();
				var systemdevdata = new SystemDevNumbersDataModel();
				systemdevdata.ApplicationUserId = SessionVariables.RequestProfile.AuditId;
				var dtnumbers = Framework.Components.Core.SystemDevNumbersDataManager.Search(systemdevdata, SessionVariables.RequestProfile);
				var rangefrom =
					Convert.ToInt32(dtnumbers.Rows[0][SystemDevNumbersDataModel.DataColumns.RangeFrom].ToString());
				var rangeto =
					Convert.ToInt32(dtnumbers.Rows[0][SystemDevNumbersDataModel.DataColumns.RangeTo].ToString());
				if (Request.QueryString["SuperKey"] != null)
				{
					superKey = Request.QueryString["SuperKey"];
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(superKey);
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.Activity;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (System.Data.DataRow dr in dt.Rows)
						{
							var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
							Activitydata.ActivityId = key;

							var Activitydt = ActivityDataManager.GetDetails(Activitydata, SessionVariables.RequestProfile);

							if (Activitydt.Rows.Count == 1)
							{
								var row = Activitydt.Rows[0];

								if (Request.QueryString["Mode"].Equals("Test"))
								{
									Activitydata.ActivityId = GetNextValidId(rangefrom);
								}
								Activitydata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
								Activitydata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
								Activitydata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

								ActivityDataManager.Create(Activitydata, SessionVariables.RequestProfile);
							}
						}
					}
				}
				else if (Request.QueryString["SetId"] != null)
				{
					var key = int.Parse(Request.QueryString["SetId"]);
					Activitydata.ActivityId = key;

					var Activitydt = ActivityDataManager.GetDetails(Activitydata, SessionVariables.RequestProfile);

					if (Activitydt.Rows.Count == 1)
					{
						var row = Activitydt.Rows[0];

						var newActivitydata = new ActivityDataModel();
						if (Request.QueryString["Mode"].Equals("Test"))
							newActivitydata.ActivityId = newId = (int)GetNextValidId(rangefrom);
						newActivitydata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
						newActivitydata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
						newActivitydata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

						UpdatedFKDepenedencies(key, newId);
						ActivityDataManager.Delete(Activitydata, SessionVariables.RequestProfile);
						ActivityDataManager.Create(newActivitydata, SessionVariables.RequestProfile);
					}


				}
				else if (Request.QueryString["Mode"].Equals("Renumber"))
				{
					var seed = int.Parse(Request.QueryString["Seed"]);
					//ApplicationVariables.Seed;
					var increment = int.Parse(Request.QueryString["Increment"]);
					//ApplicationVariables.Increment;
					ActivityDataManager.Renumber(seed, increment, SessionVariables.RequestProfile);
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
			var dt = ActivityDataManager.GetList(SessionVariables.RequestProfile);

			foreach (DataRow dr in dt.Rows)
			{
				if (dr[ActivityDataModel.DataColumns.ActivityId].ToString().Equals(tempId.ToString()))
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