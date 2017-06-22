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
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Need
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
				var newId = SystemEntityTypeDataManager.GetNextSequence("Need", (int)SystemEntity.Need, SessionVariables.RequestProfile);
				var Needdata = new NeedDataModel();
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
					data.SystemEntityTypeId = (int)SystemEntity.Need;
					var dt = SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (DataRow dr in dt.Rows)
						{
							var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
							Needdata.NeedId = key;

							var Needdt = NeedDataManager.GetDetails(Needdata, SessionVariables.RequestProfile);

							if (Needdt.Rows.Count == 1)
							{
								var row = Needdt.Rows[0];

								if (Request.QueryString["Mode"].ToString().Equals("Test"))
								{
									Needdata.NeedId = GetNextValidId(rangefrom);
								}
								Needdata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
								Needdata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
								Needdata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

								NeedDataManager.Create(Needdata, SessionVariables.RequestProfile);


							}
						}
					}
				}
				else if (Request.QueryString["SetId"] != null)
				{
					var key = int.Parse(Request.QueryString["SetId"]);
					Needdata.NeedId = key;

					var Needdt = NeedDataManager.GetDetails(Needdata, SessionVariables.RequestProfile);

					if (Needdt.Rows.Count == 1)
					{
						var row = Needdt.Rows[0];

						var newNeeddata = new NeedDataModel();
						if (Request.QueryString["Mode"].ToString().Equals("Test"))
							newNeeddata.NeedId = newId = (int)GetNextValidId(rangefrom);
						newNeeddata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
						newNeeddata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
						newNeeddata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

						UpdatedFKDepenedencies(key, newId);
                        NeedDataManager.Delete(Needdata, SessionVariables.RequestProfile);
						NeedDataManager.Create(newNeeddata, SessionVariables.RequestProfile);
					}


				}
				else if (Request.QueryString["Mode"].ToString().Equals("Renumber"))
				{
					var seed = int.Parse(Request.QueryString["Seed"].ToString());
					//ApplicationVariables.Seed;
					var increment = int.Parse(Request.QueryString["Increment"].ToString());
					//ApplicationVariables.Increment;
                    NeedDataManager.Renumber(seed, increment, SessionVariables.RequestProfile);
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
            var dt = NeedDataManager.GetList(SessionVariables.RequestProfile);

			foreach (DataRow dr in dt.Rows)
			{
				if (dr[NeedDataModel.DataColumns.NeedId].ToString().Equals(tempId.ToString()))
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