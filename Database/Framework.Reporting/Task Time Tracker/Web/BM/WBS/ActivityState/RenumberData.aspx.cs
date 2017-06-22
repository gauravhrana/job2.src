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
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.WBS.ActivityState
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
				var newId = SystemEntityTypeDataManager.GetNextSequence("Layer", (int)SystemEntity.Layer, SessionVariables.RequestProfile);
				var Layerdata = new LayerDataModel();
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
					data.SystemEntityTypeId = (int)SystemEntity.Layer;
					var dt = SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (DataRow dr in dt.Rows)
						{
							var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
							Layerdata.LayerId = key;

                            var Layerdt = LayerDataManager.GetDetails(Layerdata, SessionVariables.RequestProfile);

							if (Layerdt.Rows.Count == 1)
							{
								var row = Layerdt.Rows[0];

								if (Request.QueryString["Mode"].ToString().Equals("Test"))
								{
									Layerdata.LayerId = GetNextValidId(rangefrom);
								}
								Layerdata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
								Layerdata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
								Layerdata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

                                LayerDataManager.Create(Layerdata, SessionVariables.RequestProfile);


							}
						}
					}
				}
				else if (Request.QueryString["SetId"] != null)
				{
					var key = int.Parse(Request.QueryString["SetId"]);
					Layerdata.LayerId = key;

                    var Layerdt = LayerDataManager.GetDetails(Layerdata, SessionVariables.RequestProfile);

					if (Layerdt.Rows.Count == 1)
					{
						var row = Layerdt.Rows[0];

						var newLayerdata = new LayerDataModel();
						if (Request.QueryString["Mode"].ToString().Equals("Test"))
							newLayerdata.LayerId = newId = (int)GetNextValidId(rangefrom);
						newLayerdata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
						newLayerdata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
						newLayerdata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

						UpdatedFKDepenedencies(key, newId);
                        LayerDataManager.Delete(Layerdata, SessionVariables.RequestProfile);
                        LayerDataManager.Create(newLayerdata, SessionVariables.RequestProfile);
					}


				}
				else if (Request.QueryString["Mode"].ToString().Equals("Renumber"))
				{
					var seed = int.Parse(Request.QueryString["Seed"].ToString());
					//ApplicationVariables.Seed;
					var increment = int.Parse(Request.QueryString["Increment"].ToString());
					//ApplicationVariables.Increment;
                    LayerDataManager.Renumber(seed, increment, SessionVariables.RequestProfile);
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
            var dt = LayerDataManager.GetList(SessionVariables.RequestProfile);

			foreach (DataRow dr in dt.Rows)
			{
				if (dr[LayerDataModel.DataColumns.LayerId].ToString().Equals(tempId.ToString()))
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