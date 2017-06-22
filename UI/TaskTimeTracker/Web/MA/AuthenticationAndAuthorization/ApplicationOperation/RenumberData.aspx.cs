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
                var superKey = ApplicationCommon.GetSuperKey();
                var setId = ApplicationCommon.GetSetId();
                var mode = ApplicationCommon.GetMode();
                var seed = ApplicationCommon.GetSeed();
                var increment = ApplicationCommon.GetIncrement();

				var newId = Framework.Components.Core.SystemEntityTypeDataManager.GetNextSequence("ApplicationOperation", (int)Framework.Components.DataAccess.SystemEntity.ApplicationOperation, SessionVariables.RequestProfile);
				var ApplicationOperationdata = new ApplicationOperationDataModel();
				var systemdevdata = new SystemDevNumbersDataModel();
				systemdevdata.ApplicationUserId = SessionVariables.RequestProfile.AuditId;

                var devNumber = Framework.Components.Core.SystemDevNumbersDataManager.GetDetails(systemdevdata, SessionVariables.RequestProfile);
                var rangefrom = devNumber.RangeFrom.Value;
                var rangeto = devNumber.RangeTo.Value;

                if (!string.IsNullOrEmpty(superKey))
				{					
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(superKey);
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.ApplicationOperation;
					var listSuperKeyDetails = Framework.Components.Core.SuperKeyDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

                    if (listSuperKeyDetails != null && listSuperKeyDetails.Count > 0)
                    {
                        foreach (var itemSuperKeyDetail in listSuperKeyDetails)
                        {
                            var key = itemSuperKeyDetail.EntityKey;
							ApplicationOperationdata.ApplicationOperationId = key;

							var oAppOperation = Framework.Components.ApplicationUser.ApplicationOperationDataManager.GetDetails(ApplicationOperationdata, SessionVariables.RequestProfile);

                            if (oAppOperation != null)
							{
                                if (mode == "Test")
								{
									ApplicationOperationdata.ApplicationOperationId = GetNextValidId(rangefrom);
								}
                                ApplicationOperationdata.Name = oAppOperation.Name;
                                ApplicationOperationdata.Description = oAppOperation.Description;
								ApplicationOperationdata.SortOrder = Convert.ToInt32(oAppOperation.SortOrder);

								Framework.Components.ApplicationUser.ApplicationOperationDataManager.Create(ApplicationOperationdata, SessionVariables.RequestProfile);


							}
						}
					}
				}
                else if (setId != 0)
				{
                    var key = setId; 
					ApplicationOperationdata.ApplicationOperationId = key;

					var oAppOperation = Framework.Components.ApplicationUser.ApplicationOperationDataManager.GetDetails(ApplicationOperationdata, SessionVariables.RequestProfile);

                    if (oAppOperation != null)
					{
						var newApplicationOperationdata = new ApplicationOperationDataModel();

                        if (mode == "Test")

						newApplicationOperationdata.ApplicationOperationId = newId = (int)GetNextValidId(rangefrom);
						newApplicationOperationdata.Name                   = oAppOperation.Name;
						newApplicationOperationdata.Description            = oAppOperation.Description;
						newApplicationOperationdata.SortOrder              = oAppOperation.SortOrder;

						UpdatedFKDepenedencies(key, newId);
						Framework.Components.ApplicationUser.ApplicationOperationDataManager.Delete(ApplicationOperationdata, SessionVariables.RequestProfile);
						Framework.Components.ApplicationUser.ApplicationOperationDataManager.Create(newApplicationOperationdata, SessionVariables.RequestProfile);
					}
				}
                else if (mode == "Renumber")
				{
                    Framework.Components.ApplicationUser.ApplicationOperationDataManager.Renumber(Convert.ToInt32(seed), Convert.ToInt32(increment), SessionVariables.RequestProfile);
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

            foreach (var dr in dt)
			{
				if (dr.ApplicationOperationId == tempId)
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