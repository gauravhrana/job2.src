using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.ReleaseLog;

namespace Shared.UI.Web.ApplicationManagement.ReleaseLog
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region methods

        private void DeleteAndRedirect()
        {
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ReleaseLog, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("ReleaseLogEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        #endregion

        #region Events      

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleaseLog;
            PrimaryEntityKey = "ReleaseLog";
            BreadCrumbObject = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("ReleaseLog", ControlType.DetailsControl);
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
                    var data = new ReleaseLogDataModel();
                    data.ReleaseLogId = int.Parse(index);
					if (!Framework.Components.ReleaseLog.ReleaseLogDataManager.IsDeletable(data, SessionVariables.RequestProfile))
                    {
                        notDeletableIds.Add(Convert.ToInt32(data.ReleaseLogId));
                    }
                }
                if (notDeletableIds.Count == 0)
                {
                    foreach (string index in deleteIndexList)
                    {
                        var data = new ReleaseLogDataModel();
                        data.ReleaseLogId = int.Parse(index);
						Framework.Components.ReleaseLog.ReleaseLogDataManager.Delete(data, SessionVariables.RequestProfile);
                    }
					Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ReleaseLog, SessionVariables.RequestProfile);
                    Response.Redirect(Page.GetRouteUrl("ReleaseLogEntityRoute", new { Action = "Default", SetId = true }), false);
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
                        msg += "ReleaseLogId: " + id + " has detail records";
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