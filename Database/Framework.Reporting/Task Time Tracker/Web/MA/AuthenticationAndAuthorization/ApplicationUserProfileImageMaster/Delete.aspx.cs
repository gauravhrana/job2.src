using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserProfileImageMaster
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {       

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "ApplicationUserProfileImageMaster";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("ApplicationUserProfileImageMaster", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationUserProfileImageMaster;

		}

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ApplicationUserProfileImageMaster, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("ApplicationUserProfileImageMasterEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				string[] deleteIndexList = DeleteIds.Split(',');

				foreach (string index in deleteIndexList)
				{
					var data = new ApplicationUserProfileImageMasterDataModel();
					data.ApplicationUserProfileImageMasterId = int.Parse(index);
					Framework.Components.ApplicationUser.ApplicationUserProfileImageMasterDataManager.Delete(data, SessionVariables.RequestProfile);
				}

				DeleteAndRedirect();
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}        

		#endregion

    }
}