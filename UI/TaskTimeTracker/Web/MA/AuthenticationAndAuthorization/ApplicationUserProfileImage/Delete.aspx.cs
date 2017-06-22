using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserProfileImage
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "ApplicationUserProfileImage";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("ApplicationUserProfileImage", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationUserProfileImage;

		}

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ApplicationUserProfileImage, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("ApplicationUserProfileImageEntityRoute", new { Action = "Default", SetId = true }), false);
		}

        #region Events
        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');

                foreach (string index in deleteIndexList)
                {
                    var data = new ApplicationUserProfileImageDataModel();
                    data.ApplicationUserProfileImageId = int.Parse(index);
					Framework.Components.ApplicationUser.ApplicationUserProfileImageDataManager.Delete(data, SessionVariables.RequestProfile);
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