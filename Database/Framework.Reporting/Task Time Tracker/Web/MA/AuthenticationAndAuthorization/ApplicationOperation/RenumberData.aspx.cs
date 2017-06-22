using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationOperation
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
				var newId = Framework.Components.Core.SystemEntityTypeDataManager.GetNextSequence("ApplicationOperation", (int)Framework.Components.DataAccess.SystemEntity.ApplicationOperation, SessionVariables.RequestProfile);
				var ApplicationOperationdata = new ApplicationOperationDataModel();
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
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.ApplicationOperation;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (System.Data.DataRow dr in dt.Rows)
						{
							var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
							ApplicationOperationdata.ApplicationOperationId = key;

							var ApplicationOperationdt = Framework.Components.ApplicationUser.ApplicationOperationDataManager.GetDetails(ApplicationOperationdata, SessionVariables.RequestProfile);

							if (ApplicationOperationdt.Rows.Count == 1)
							{
								var row = ApplicationOperationdt.Rows[0];

								if (Request.QueryString["Mode"].ToString().Equals("Test"))
								{
									ApplicationOperationdata.ApplicationOperationId = GetNextValidId(rangefrom);
								}
								ApplicationOperationdata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
								ApplicationOperationdata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
								ApplicationOperationdata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

								Framework.Components.ApplicationUser.ApplicationOperationDataManager.Create(ApplicationOperationdata, SessionVariables.RequestProfile);


							}
						}
					}
				}
				else if (Request.QueryString["SetId"] != null)
				{
					var key = int.Parse(Request.QueryString["SetId"]);
					ApplicationOperationdata.ApplicationOperationId = key;

					var ApplicationOperationdt = Framework.Components.ApplicationUser.ApplicationOperationDataManager.GetDetails(ApplicationOperationdata, SessionVariables.RequestProfile);

					if (ApplicationOperationdt.Rows.Count == 1)
					{
						var row = ApplicationOperationdt.Rows[0];

						var newApplicationOperationdata = new ApplicationOperationDataModel();
						if (Request.QueryString["Mode"].ToString().Equals("Test"))
							newApplicationOperationdata.ApplicationOperationId = newId = (int)GetNextValidId(rangefrom);
						newApplicationOperationdata.Name = Convert.ToString(row[StandardDataModel.StandardDataColumns.Name]);
						newApplicationOperationdata.Description = Convert.ToString(row[StandardDataModel.StandardDataColumns.Description]);
						newApplicationOperationdata.SortOrder = Convert.ToInt32(row[StandardDataModel.StandardDataColumns.SortOrder]);

						UpdatedFKDepenedencies(key, newId);
						Framework.Components.ApplicationUser.ApplicationOperationDataManager.Delete(ApplicationOperationdata, SessionVariables.RequestProfile);
						Framework.Components.ApplicationUser.ApplicationOperationDataManager.Create(newApplicationOperationdata, SessionVariables.RequestProfile);
					}


				}
				else if (Request.QueryString["Mode"].ToString().Equals("Renumber"))
				{
					var seed = int.Parse(Request.QueryString["Seed"].ToString());
					//ApplicationVariables.Seed;
					var increment = int.Parse(Request.QueryString["Increment"].ToString());
					//ApplicationVariables.Increment;
					Framework.Components.ApplicationUser.ApplicationOperationDataManager.Renumber(seed, increment, SessionVariables.RequestProfile);
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
			var dt = Framework.Components.ApplicationUser.ApplicationOperationDataManager.GetList(SessionVariables.RequestProfile);

			foreach (DataRow dr in dt.Rows)
			{
				if (dr[ApplicationOperationDataModel.DataColumns.ApplicationOperationId].ToString().Equals(tempId.ToString()))
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