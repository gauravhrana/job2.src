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

namespace ApplicationContainer.UI.Web.RiskAndReward.Risk
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
				var newId = Framework.Components.Core.SystemEntityTypeDataManager.GetNextSequence("Risk", (int)Framework.Components.DataAccess.SystemEntity.Risk, SessionVariables.RequestProfile);
				var Riskdata = new RiskDataModel();
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
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.Risk;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (System.Data.DataRow dr in dt.Rows)
						{
							var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
							Riskdata.RiskId = key;

                            var Riskdt = RiskDataManager.GetDetails(Riskdata, SessionVariables.RequestProfile);

							if (Riskdt.Rows.Count == 1)
							{
								var row = Riskdt.Rows[0];

								if (Request.QueryString["Mode"].ToString().Equals("Test"))
								{
									Riskdata.RiskId = GetNextValidId(rangefrom);
								}
								Riskdata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
								Riskdata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
								Riskdata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

                                RiskDataManager.Create(Riskdata, SessionVariables.RequestProfile);


							}
						}
					}
				}
				else if (Request.QueryString["SetId"] != null)
				{
					var key = int.Parse(Request.QueryString["SetId"]);
					Riskdata.RiskId = key;

                    var Riskdt = RiskDataManager.GetDetails(Riskdata, SessionVariables.RequestProfile);

					if (Riskdt.Rows.Count == 1)
					{
						var row = Riskdt.Rows[0];

						var newRiskdata = new RiskDataModel();
						if (Request.QueryString["Mode"].ToString().Equals("Test"))
							newRiskdata.RiskId = newId = (int)GetNextValidId(rangefrom);
						newRiskdata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
						newRiskdata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
						newRiskdata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

						UpdatedFKDepenedencies(key, newId);
                        RiskDataManager.Delete(Riskdata, SessionVariables.RequestProfile);
                        RiskDataManager.Create(newRiskdata, SessionVariables.RequestProfile);
					}


				}
				else if (Request.QueryString["Mode"].ToString().Equals("Renumber"))
				{
					var seed = int.Parse(Request.QueryString["Seed"].ToString());
					//ApplicationVariables.Seed;
					var increment = int.Parse(Request.QueryString["Increment"].ToString());
					//ApplicationVariables.Increment;
                    RiskDataManager.Renumber(seed, increment, SessionVariables.RequestProfile);
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
            var dt = RiskDataManager.GetList(SessionVariables.RequestProfile);

			foreach (DataRow dr in dt.Rows)
			{
				if (dr[RiskDataModel.DataColumns.RiskId].ToString().Equals(tempId.ToString()))
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