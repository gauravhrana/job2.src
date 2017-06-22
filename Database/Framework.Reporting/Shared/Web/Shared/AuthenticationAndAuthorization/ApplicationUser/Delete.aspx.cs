using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey		= "ApplicationUser";
			BreadCrumbObject		= Master.BreadCrumbObject;

			var detailscontrolpath	= ApplicationCommon.GetControlPath("ApplicationUser", ControlType.DetailsControl);
			DetailsControlPath		= detailscontrolpath;
			PrimaryPlaceHolder		= plcDetailsList;
			PrimaryEntity			= Framework.Components.DataAccess.SystemEntity.ApplicationUser;
		}
       
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var notDeletableIds = new List<int>();
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new ApplicationUserDataModel();
                    data.ApplicationUserId = int.Parse(index);
					if (!Framework.Components.ApplicationUser.ApplicationUserDataManager.IsDeletable(data, SessionVariables.RequestProfile))
                    {
                        notDeletableIds.Add(Convert.ToInt32(data.ApplicationUserId));
                    }
                }
                if (notDeletableIds.Count == 0)
                {
                    foreach (string index in deleteIndexList)
                    {
                        var data = new ApplicationUserDataModel();
                        data.ApplicationUserId = int.Parse(index);
						Framework.Components.ApplicationUser.ApplicationUserDataManager.Delete(data, SessionVariables.RequestProfile);
                    }
					DeleteAndRedirect();
                }
                else
                {
                    var msg = String.Empty;
                    foreach (var id in notDeletableIds)
                    {
                        if (!string.IsNullOrEmpty(msg))
                        {
                            msg += ", <br/>";
                        }
                        msg += "ApplicationUserId: " + id + " has detail records";
                    }
                    Response.Write(msg); 
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ApplicationUser, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("ApplicationUserEntityRoute", new { Action = "Default", SetId = true }), false);
		}	

        #endregion

	}
}