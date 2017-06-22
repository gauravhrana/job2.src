using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.RiskReward;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.RiskReward;

namespace ApplicationContainer.UI.Web.RiskAndReward.Reward
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
				var newId = Framework.Components.Core.SystemEntityTypeDataManager.GetNextSequence("Reward", (int)Framework.Components.DataAccess.SystemEntity.Reward, SessionVariables.RequestProfile);
				var Rewarddata = new RewardDataModel();
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
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.Reward;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (System.Data.DataRow dr in dt.Rows)
						{
							var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
							Rewarddata.RewardId = key;

                            var Rewarddt = RewardDataManager.GetDetails(Rewarddata, SessionVariables.RequestProfile);

							if (Rewarddt.Rows.Count == 1)
							{
								var row = Rewarddt.Rows[0];

								if (Request.QueryString["Mode"].ToString().Equals("Test"))
								{
									Rewarddata.RewardId = GetNextValidId(rangefrom);
								}
								Rewarddata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
								Rewarddata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
								Rewarddata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

                                RewardDataManager.Create(Rewarddata, SessionVariables.RequestProfile);


							}
						}
					}
				}
				else if (Request.QueryString["SetId"] != null)
				{
					var key = int.Parse(Request.QueryString["SetId"]);
					Rewarddata.RewardId = key;

                    var Rewarddt = RewardDataManager.GetDetails(Rewarddata, SessionVariables.RequestProfile);

					if (Rewarddt.Rows.Count == 1)
					{
						var row = Rewarddt.Rows[0];

						var newRewarddata = new RewardDataModel();
						if (Request.QueryString["Mode"].ToString().Equals("Test"))
							newRewarddata.RewardId = newId = (int)GetNextValidId(rangefrom);
						newRewarddata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
						newRewarddata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
						newRewarddata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

						UpdatedFKDepenedencies(key, newId);
                        RewardDataManager.Delete(Rewarddata, SessionVariables.RequestProfile);
                        RewardDataManager.Create(newRewarddata, SessionVariables.RequestProfile);
					}


				}
				else if (Request.QueryString["Mode"].ToString().Equals("Renumber"))
				{
					var seed = int.Parse(Request.QueryString["Seed"].ToString());
					//ApplicationVariables.Seed;
					var increment = int.Parse(Request.QueryString["Increment"].ToString());
					//ApplicationVariables.Increment;
                    RewardDataManager.Renumber(seed, increment, SessionVariables.RequestProfile);
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
            var dt = RewardDataManager.GetList(SessionVariables.RequestProfile);

			foreach (DataRow dr in dt.Rows)
			{
				if (dr[RewardDataModel.DataColumns.RewardId].ToString().Equals(tempId.ToString()))
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