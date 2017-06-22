using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.TaskRole
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskRole;
            PrimaryEntityKey = "TaskRole";
            BreadCrumbObject = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("TaskRole", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var notDeletableIds = new List<int>();
                string[] deleteIndexList = DeleteIds.Split(',');

                if (notDeletableIds.Count == 0)
                {
                    foreach (string index in deleteIndexList)
                    {
                        var data = new TaskRoleDataModel();
                        data.TaskRoleId = int.Parse(index);
                        TaskTimeTracker.Components.BusinessLayer.Task.TaskRoleDataManager.Delete(data, SessionVariables.RequestProfile);
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
                        msg += "TaskRoleId: " + id + " has detail records";
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.TaskRole, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("TaskRoleEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        #endregion

    }
}