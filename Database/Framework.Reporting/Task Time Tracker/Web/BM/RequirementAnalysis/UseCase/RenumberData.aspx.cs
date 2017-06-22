using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.UseCase
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
				var newId = Framework.Components.Core.SystemEntityTypeDataManager.GetNextSequence("UseCase", (int)Framework.Components.DataAccess.SystemEntity.UseCase, SessionVariables.RequestProfile);
				var UseCasedata = new UseCaseDataModel();
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
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.UseCase;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (System.Data.DataRow dr in dt.Rows)
						{
							var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
							UseCasedata.UseCaseId = key;

                            var UseCasedt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetDetails(UseCasedata, SessionVariables.RequestProfile);

							if (UseCasedt.Rows.Count == 1)
							{
								var row = UseCasedt.Rows[0];

								if (Request.QueryString["Mode"].ToString().Equals("Test"))
								{
									UseCasedata.UseCaseId = GetNextValidId(rangefrom);
								}
								UseCasedata.Name		= Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
								UseCasedata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
								UseCasedata.SortOrder	= Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

                                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.Create(UseCasedata, SessionVariables.RequestProfile);


							}
						}
					}
				}
				else if (Request.QueryString["SetId"] != null)
				{
					var key = int.Parse(Request.QueryString["SetId"]);
					UseCasedata.UseCaseId = key;

                    var UseCasedt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetDetails(UseCasedata, SessionVariables.RequestProfile);

					if (UseCasedt.Rows.Count == 1)
					{
						var row = UseCasedt.Rows[0];

						var newUseCasedata = new UseCaseDataModel();
						if (Request.QueryString["Mode"].ToString().Equals("Test"))
							newUseCasedata.UseCaseId = newId = (int)GetNextValidId(rangefrom);
						newUseCasedata.Name			 = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
						newUseCasedata.Description	 = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
						newUseCasedata.SortOrder	 = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

						UpdatedFKDepenedencies(key, newId);
                        TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.Delete(UseCasedata, SessionVariables.RequestProfile);
                        TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.Create(newUseCasedata, SessionVariables.RequestProfile);
					}


				}
				else if (Request.QueryString["Mode"].ToString().Equals("Renumber"))
				{
					var seed = int.Parse(Request.QueryString["Seed"].ToString());
					//ApplicationVariables.Seed;
					var increment = int.Parse(Request.QueryString["Increment"].ToString());
					//ApplicationVariables.Increment;
                    TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.Renumber(seed, increment, SessionVariables.RequestProfile);
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
            var dt = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.GetList(SessionVariables.RequestProfile);

			foreach (DataRow dr in dt.Rows)
			{
				if (dr[UseCaseDataModel.DataColumns.UseCaseId].ToString().Equals(tempId.ToString()))
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