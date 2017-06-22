using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationSubscriber
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            PrimaryEntityKey = "NotificationSubscriber";
            BreadCrumbObject = Master.BreadCrumbObject;

            var detailscontrolpath = ApplicationCommon.GetControlPath("NotificationSubscriber", ControlType.DetailsControl);
            DetailsControlPath = detailscontrolpath;
            PrimaryPlaceHolder = plcDetailsList;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.NotificationSubscriber;
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new NotificationSubscriberDataModel();
                    data.NotificationSubscriberId = int.Parse(index);
					Framework.Components.EventMonitoring.NotificationSubscriberDataManager.Delete(data, SessionVariables.RequestProfile);
                    DeleteAndRedirect();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void DeleteAndRedirect()
        {
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.NotificationSubscriber, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("NotificationSubscriberEntityRoute", new { Action = "Default", SetId = true }), false);
        }
     
        #endregion

    }
}