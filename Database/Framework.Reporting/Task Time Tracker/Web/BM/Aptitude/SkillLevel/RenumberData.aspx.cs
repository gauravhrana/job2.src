using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Competency;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.Aptitude.SkillLevel
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
				var newId = Framework.Components.Core.SystemEntityTypeDataManager.GetNextSequence("Layer", (int)Framework.Components.DataAccess.SystemEntity.SkillLevel, SessionVariables.RequestProfile);
				var SkillLeveldata = new SkillLevelDataModel();
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
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.SkillLevel;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (System.Data.DataRow dr in dt.Rows)
						{
							var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
							SkillLeveldata.SkillLevelId = key;

                            var SkillLeveldt = SkillLevelDataManager.GetDetails(SkillLeveldata, SessionVariables.RequestProfile);

							if (SkillLeveldt.Rows.Count == 1)
							{
								var row = SkillLeveldt.Rows[0];

								if (Request.QueryString["Mode"].Equals("Test"))
								{
									SkillLeveldata.SkillLevelId = GetNextValidId(rangefrom);
								}
								SkillLeveldata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
								SkillLeveldata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
								SkillLeveldata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

                                SkillLevelDataManager.Create(SkillLeveldata, SessionVariables.RequestProfile);


							}
						}
					}
				}
				else if (Request.QueryString["SetId"] != null)
				{
					var key = int.Parse(Request.QueryString["SetId"]);
					SkillLeveldata.SkillLevelId = key;

                    var SkillLeveldt = SkillLevelDataManager.GetDetails(SkillLeveldata, SessionVariables.RequestProfile);

					if (SkillLeveldt.Rows.Count == 1)
					{
						var row = SkillLeveldt.Rows[0];

						var newSkillLeveldata = new SkillLevelDataModel();
						if (Request.QueryString["Mode"].Equals("Test"))
							newSkillLeveldata.SkillLevelId = newId = (int)GetNextValidId(rangefrom);
						newSkillLeveldata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
						newSkillLeveldata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
						newSkillLeveldata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

						UpdatedFKDepenedencies(key, newId);
                        SkillLevelDataManager.Delete(SkillLeveldata, SessionVariables.RequestProfile);
                        SkillLevelDataManager.Create(newSkillLeveldata, SessionVariables.RequestProfile);
					}


				}
				else if (Request.QueryString["Mode"].Equals("Renumber"))
				{
					var seed = int.Parse(Request.QueryString["Seed"]);
					//ApplicationVariables.Seed;
					var increment = int.Parse(Request.QueryString["Increment"]);
					//ApplicationVariables.Increment;
                    SkillLevelDataManager.Renumber(seed, increment, SessionVariables.RequestProfile);
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
            var dt = SkillLevelDataManager.GetList(SessionVariables.RequestProfile);

			foreach (DataRow dr in dt.Rows)
			{
				if (dr[SkillLevelDataModel.DataColumns.SkillLevelId].ToString().Equals(tempId.ToString()))
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