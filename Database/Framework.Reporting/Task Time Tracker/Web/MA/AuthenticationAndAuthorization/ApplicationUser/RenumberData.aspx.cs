using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser
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
				var newId = Framework.Components.Core.SystemEntityTypeDataManager.GetNextSequence("ApplicationUser", (int)Framework.Components.DataAccess.SystemEntity.ApplicationUser, SessionVariables.RequestProfile);
				var ApplicationUserdata = new ApplicationUserDataModel();
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
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.ApplicationUser;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (System.Data.DataRow dr in dt.Rows)
						{
							var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
							ApplicationUserdata.ApplicationUserId = key;

							var ApplicationUserdt = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetDetails(ApplicationUserdata, SessionVariables.RequestProfile);

							if (ApplicationUserdt.Rows.Count == 1)
							{
								var row = ApplicationUserdt.Rows[0];

								if (Request.QueryString["Mode"].ToString().Equals("Test"))
								{
									ApplicationUserdata.ApplicationUserId = GetNextValidId(rangefrom);
								}
								ApplicationUserdata.FirstName = Convert.ToString(row[ApplicationUserDataModel.DataColumns.FirstName]);
								ApplicationUserdata.LastName = Convert.ToString(row[ApplicationUserDataModel.DataColumns.LastName]);
								ApplicationUserdata.MiddleName = Convert.ToString(row[ApplicationUserDataModel.DataColumns.MiddleName]);
								ApplicationUserdata.ApplicationUserTitleId = Convert.ToInt32(row[ApplicationUserDataModel.DataColumns.ApplicationUserTitleId]);

								Framework.Components.ApplicationUser.ApplicationUserDataManager.Create(ApplicationUserdata, SessionVariables.RequestProfile);


							}
						}
					}
				}
				else if (Request.QueryString["SetId"] != null)
				{
					var key = int.Parse(Request.QueryString["SetId"]);
					ApplicationUserdata.ApplicationUserId = key;

					var ApplicationUserdt = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetDetails(ApplicationUserdata, SessionVariables.RequestProfile);

					if (ApplicationUserdt.Rows.Count == 1)
					{
						var row = ApplicationUserdt.Rows[0];

						var newApplicationUserdata = new ApplicationUserDataModel();
						if (Request.QueryString["Mode"].ToString().Equals("Test"))
							newApplicationUserdata.ApplicationUserId = newId = (int)GetNextValidId(rangefrom);
						newApplicationUserdata.FirstName = Convert.ToString(row[ApplicationUserDataModel.DataColumns.FirstName]);
						newApplicationUserdata.LastName = Convert.ToString(row[ApplicationUserDataModel.DataColumns.LastName]);
						newApplicationUserdata.MiddleName = Convert.ToString(row[ApplicationUserDataModel.DataColumns.MiddleName]);
						newApplicationUserdata.ApplicationUserTitleId = Convert.ToInt32(row[ApplicationUserDataModel.DataColumns.ApplicationUserTitleId]);

						UpdatedFKDepenedencies(key, newId);
						Framework.Components.ApplicationUser.ApplicationUserDataManager.Delete(ApplicationUserdata, SessionVariables.RequestProfile);
						Framework.Components.ApplicationUser.ApplicationUserDataManager.Create(newApplicationUserdata, SessionVariables.RequestProfile);
					}


				}
				else if (Request.QueryString["Mode"].ToString().Equals("Renumber"))
				{
					var seed = int.Parse(Request.QueryString["Seed"].ToString());
					//ApplicationVariables.Seed;
					var increment = int.Parse(Request.QueryString["Increment"].ToString());
					//ApplicationVariables.Increment;
					Framework.Components.ApplicationUser.ApplicationUserDataManager.Renumber(seed, increment, SessionVariables.RequestProfile);
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
			var dt = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);

			foreach (DataRow dr in dt.Rows)
			{
				if (dr[ApplicationUserDataModel.DataColumns.ApplicationUserId].ToString().Equals(tempId.ToString()))
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