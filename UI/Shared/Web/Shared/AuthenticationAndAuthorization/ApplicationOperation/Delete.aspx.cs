using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationOperation
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {
        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity      = Framework.Components.DataAccess.SystemEntity.ApplicationOperation;
            PrimaryEntityKey   = "ApplicationOperation";
            BreadCrumbObject   = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("ApplicationOperation", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;
        }     
           
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var notDeletableIds = new List<int>();
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new ApplicationOperationDataModel();
                    data.ApplicationOperationId = int.Parse(index);
					if (!Framework.Components.ApplicationUser.ApplicationOperationDataManager.IsDeletable(data, SessionVariables.RequestProfile))
                    {
                        notDeletableIds.Add(Convert.ToInt32(data.ApplicationOperationId));
                    }
                }
                if (notDeletableIds.Count == 0)
                {
                    foreach (string index in deleteIndexList)
                    {
                        var data = new ApplicationOperationDataModel();
                        data.ApplicationOperationId = int.Parse(index);
						Framework.Components.ApplicationUser.ApplicationOperationDataManager.Delete(data, SessionVariables.RequestProfile);
                    }
					Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ApplicationOperation, SessionVariables.RequestProfile);

                    Response.Redirect(Page.GetRouteUrl("ApplicationOperationEntityRoute", new { Action = "Default", SetId = true }), false);
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
                        msg += "ApplicationOperationId: " + id + " has detail records";
                    }
                    Response.Write(msg);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
             

        #endregion

    }
}