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

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserTitle
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
                var seed = ApplicationCommon.GetSeed();
                var increment = ApplicationCommon.GetIncrement();

				var newId = Framework.Components.Core.SystemEntityTypeDataManager.GetNextSequence("ApplicationUserTitle", (int)Framework.Components.DataAccess.SystemEntity.ApplicationUserTitle, SessionVariables.RequestProfile);
				var ApplicationUserTitledata = new ApplicationUserTitleDataModel();
				var systemdevdata = new SystemDevNumbersDataModel();
				systemdevdata.ApplicationUserId = SessionVariables.RequestProfile.AuditId;

                var devNumber = Framework.Components.Core.SystemDevNumbersDataManager.GetDetails(systemdevdata, SessionVariables.RequestProfile);
                var rangefrom = devNumber.RangeFrom.Value;
                var rangeto = devNumber.RangeTo.Value;

                if (!string.IsNullOrEmpty(superKey))
				{					
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(superKey);
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.ApplicationUserTitle;
					var listSuperKeyDetails = Framework.Components.Core.SuperKeyDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

                    if (listSuperKeyDetails != null && listSuperKeyDetails.Count > 0)
                    {
                        foreach (var itemSuperKeyDetail in listSuperKeyDetails)
                        {
                            var key = itemSuperKeyDetail.EntityKey;
							ApplicationUserTitledata.ApplicationUserTitleId = key;

							var oAppUserTitle = Framework.Components.ApplicationUser.ApplicationUserTitleDataManager.GetDetails(ApplicationUserTitledata, SessionVariables.RequestProfile);

                            if (oAppUserTitle != null)
							{
                                if (mode == "Test")
								{
									ApplicationUserTitledata.ApplicationUserTitleId = GetNextValidId(rangefrom);
								}
                                ApplicationUserTitledata.Name        = oAppUserTitle.Name;
								ApplicationUserTitledata.Description = oAppUserTitle.Description;
								ApplicationUserTitledata.SortOrder   = Convert.ToInt32(oAppUserTitle.SortOrder);

								Framework.Components.ApplicationUser.ApplicationUserTitleDataManager.Create(ApplicationUserTitledata, SessionVariables.RequestProfile);

							}
						}
					}
				}
                else if (setId != 0)
				{
                    var key = setId;
					ApplicationUserTitledata.ApplicationUserTitleId = key;

					var oAppUserTitle = Framework.Components.ApplicationUser.ApplicationUserTitleDataManager.GetDetails(ApplicationUserTitledata, SessionVariables.RequestProfile);

                    if (oAppUserTitle != null)
					{

						var newApplicationUserTitledata = new ApplicationUserTitleDataModel();

                        if (mode == "Test")

						newApplicationUserTitledata.ApplicationUserTitleId = newId = (int)GetNextValidId(rangefrom);
						newApplicationUserTitledata.Name = Convert.ToString(oAppUserTitle.Name);
						newApplicationUserTitledata.Description = Convert.ToString(oAppUserTitle.Description);
						newApplicationUserTitledata.SortOrder = Convert.ToInt32(oAppUserTitle.SortOrder);

						UpdatedFKDepenedencies(key, newId);
						Framework.Components.ApplicationUser.ApplicationUserTitleDataManager.Delete(ApplicationUserTitledata, SessionVariables.RequestProfile);
						Framework.Components.ApplicationUser.ApplicationUserTitleDataManager.Create(newApplicationUserTitledata, SessionVariables.RequestProfile);
					}

				}
                else if (mode == "Renumber")
				{
                    Framework.Components.ApplicationUser.ApplicationUserTitleDataManager.Renumber(Convert.ToInt32(seed), Convert.ToInt32(increment), SessionVariables.RequestProfile);
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
			var dt = Framework.Components.ApplicationUser.ApplicationUserTitleDataManager.GetList(SessionVariables.RequestProfile);

            foreach (var dr in dt)
            {
                if (dr.ApplicationUserTitleId.ToString().Equals(tempId.ToString()))
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