
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Project
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
				var newId = SystemEntityTypeDataManager.GetNextSequence("Project", (int)SystemEntity.Project, SessionVariables.RequestProfile);
				var Projectdata = new ProjectDataModel();
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
					data.SystemEntityTypeId = (int)SystemEntity.Project;
					var dt = SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (DataRow dr in dt.Rows)
						{
							var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
							Projectdata.ProjectId = key;

                            var Projectdt = ProjectDataManager.GetDetails(Projectdata, SessionVariables.RequestProfile);

							if (Projectdt.Rows.Count == 1)
							{
								var row = Projectdt.Rows[0];

								if (Request.QueryString["Mode"].ToString().Equals("Test"))
								{
									Projectdata.ProjectId = GetNextValidId(rangefrom);
								}
								Projectdata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
								Projectdata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
								Projectdata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

								ProjectDataManager.Create(Projectdata, SessionVariables.RequestProfile);


							}
						}
					}
				}
				else if (Request.QueryString["SetId"] != null)
				{
					var key = int.Parse(Request.QueryString["SetId"]);
					Projectdata.ProjectId = key;

                    var Projectdt = ProjectDataManager.GetDetails(Projectdata, SessionVariables.RequestProfile);

					if (Projectdt.Rows.Count == 1)
					{
						var row = Projectdt.Rows[0];

						var newProjectdata = new ProjectDataModel();
						if (Request.QueryString["Mode"].ToString().Equals("Test"))
							newProjectdata.ProjectId = newId = (int)GetNextValidId(rangefrom);
						newProjectdata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
						newProjectdata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
						newProjectdata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

						UpdatedFKDepenedencies(key, newId);
                        ProjectDataManager.Delete(Projectdata, SessionVariables.RequestProfile);
						ProjectDataManager.Create(newProjectdata, SessionVariables.RequestProfile);
					}


				}
				else if (Request.QueryString["Mode"].ToString().Equals("Renumber"))
				{
					var seed = int.Parse(Request.QueryString["Seed"].ToString());
					//ApplicationVariables.Seed;
					var increment = int.Parse(Request.QueryString["Increment"].ToString());
					//ApplicationVariables.Increment;
                    ProjectDataManager.Renumber(seed, increment, SessionVariables.RequestProfile);
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
            var dt = ProjectDataManager.GetList(SessionVariables.RequestProfile);

			foreach (DataRow dr in dt.Rows)
			{
				if (dr[ProjectDataModel.DataColumns.ProjectId].ToString().Equals(tempId.ToString()))
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
				var ClientXProjectdt = ClientXProjectDataManager.GetByProject(oldId, SessionVariables.RequestProfile.AuditId);
				var ProjectIds = new int[ClientXProjectdt.Rows.Count];
				for (var i = 0; i < ClientXProjectdt.Rows.Count; i++)
				{
					ProjectIds[i] = int.Parse(ClientXProjectdt.Rows[i][ClientXProjectDataModel.DataColumns.ProjectId].ToString());
				}
				if (ProjectIds.Length > 0)
				{
					ClientXProjectDataManager.DeleteByProject(oldId, SessionVariables.RequestProfile.AuditId);
                    ClientXProjectDataManager.CreateByProject(newId, ProjectIds, SessionVariables.RequestProfile);

				}

				var projectxneeddt = ClientXProjectDataManager.GetByProject(oldId, SessionVariables.RequestProfile.AuditId);
				var needIds = new int[ClientXProjectdt.Rows.Count];
				for (var i = 0; i < projectxneeddt.Rows.Count; i++)
				{
					needIds[i] = int.Parse(projectxneeddt.Rows[i][ProjectXNeedDataModel.DataColumns.NeedId].ToString());
				}
				if (needIds.Length > 0)
				{
                    ProjectXNeedDataManager.DeleteByProject(oldId, SessionVariables.RequestProfile);
                    ProjectXNeedDataManager.CreateByProject(newId, needIds, SessionVariables.RequestProfile);

				}

			}
			catch (Exception ex)
			{

				Response.Write(ex.Message);
			}
		}
	}
}