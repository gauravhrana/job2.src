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
                var superKey = ApplicationCommon.GetSuperKey();
                var setId = ApplicationCommon.GetSetId();
                var mode = ApplicationCommon.GetMode();
               
				var newId = Framework.Components.Core.SystemEntityTypeDataManager.GetNextSequence("ApplicationUser", (int)Framework.Components.DataAccess.SystemEntity.ApplicationUser, SessionVariables.RequestProfile);
				var ApplicationUserdata = new ApplicationUserDataModel();
				var systemdevdata = new SystemDevNumbersDataModel();
				systemdevdata.ApplicationUserId = SessionVariables.RequestProfile.AuditId;

				var devNumber = Framework.Components.Core.SystemDevNumbersDataManager.GetDetails(systemdevdata, SessionVariables.RequestProfile);
				var rangefrom = devNumber.RangeFrom.Value;
				var rangeto = devNumber.RangeTo.Value;

                if (!string.IsNullOrEmpty(superKey))
				{
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(superKey);
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.ApplicationUser;
					var listSuperKeyDetails = Framework.Components.Core.SuperKeyDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

                    if (listSuperKeyDetails != null && listSuperKeyDetails.Count > 0)
                    {
                        foreach (var itemSuperKeyDetail in listSuperKeyDetails)
                        {
                            var key = itemSuperKeyDetail.EntityKey;
							ApplicationUserdata.ApplicationUserId = key;

							var oAppUser = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetDetails(ApplicationUserdata, SessionVariables.RequestProfile);

                            if (oAppUser != null)
							{

                                if (mode == "Test")
								{
									ApplicationUserdata.ApplicationUserId = GetNextValidId(rangefrom);
								}
								ApplicationUserdata.FirstName              = oAppUser.FirstName;
								ApplicationUserdata.LastName               = oAppUser.LastName;
								ApplicationUserdata.MiddleName             = oAppUser.MiddleName;
								ApplicationUserdata.ApplicationUserTitleId = Convert.ToInt32(oAppUser.ApplicationUserTitleId);

								Framework.Components.ApplicationUser.ApplicationUserDataManager.Create(ApplicationUserdata, SessionVariables.RequestProfile);

							}
						}
					}
				}
                else if (setId != 0)
				{
                    var key = setId;
					ApplicationUserdata.ApplicationUserId = key;

					var oAppUser = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetDetails(ApplicationUserdata, SessionVariables.RequestProfile);

                    if (oAppUser != null)
					{

						var newApplicationUserdata = new ApplicationUserDataModel();

                        if (mode == "Test")

						newApplicationUserdata.ApplicationUserId      = newId = (int)GetNextValidId(rangefrom);
						newApplicationUserdata.FirstName              = oAppUser.FirstName;
						newApplicationUserdata.LastName               = oAppUser.LastName;
						newApplicationUserdata.MiddleName             = oAppUser.MiddleName;
						newApplicationUserdata.ApplicationUserTitleId = oAppUser.ApplicationUserTitleId;

						UpdatedFKDepenedencies(key, newId);
						Framework.Components.ApplicationUser.ApplicationUserDataManager.Delete(ApplicationUserdata, SessionVariables.RequestProfile);
						Framework.Components.ApplicationUser.ApplicationUserDataManager.Create(newApplicationUserdata, SessionVariables.RequestProfile);
					}

				}
                else if (mode == "Renumber")
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
            foreach (var dr in dt)
			{
				if (dr.ApplicationUserId == tempId)
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