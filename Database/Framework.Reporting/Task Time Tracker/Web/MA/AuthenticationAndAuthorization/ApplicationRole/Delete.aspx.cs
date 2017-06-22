using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Framework.Components.ApplicationUser;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationRole
{
    public partial class Delete : PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.ApplicationRole;
            PrimaryEntityKey = "ApplicationRole";
            BreadCrumbObject = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("ApplicationRole", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var notDeletableIds = new List<int>();
                var deleteIndexList = DeleteIds.Split(',');

                foreach (var index in deleteIndexList)
                {
                    var data = new ApplicationRoleDataModel();
                    data.ApplicationRoleId = int.Parse(index);

                    if (!ApplicationRoleDataManager.IsDeletable(data, SessionVariables.RequestProfile))
                    {
                        notDeletableIds.Add((int)(data.ApplicationRoleId));
                    }
                }

                if (notDeletableIds.Count == 0)
                {
                    foreach (var index in deleteIndexList)
                    {
                        var data = new ApplicationRoleDataModel();
                        data.ApplicationRoleId = int.Parse(index);

                        ApplicationRoleDataManager.Delete(data, SessionVariables.RequestProfile);
                    }
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
                        msg += "ApplicationRoleId: " + id + " has detail records";
                    }

                    foreach (string index in deleteIndexList)
                    {
                        var data = new ApplicationRoleDataModel();
                        data.ApplicationRoleId = int.Parse(index);

                        ApplicationRoleDataManager.DeleteChildren(data, SessionVariables.RequestProfile);
                        ApplicationRoleDataManager.Delete(data, SessionVariables.RequestProfile);
                    }
                }

                DeleteAndRedirect();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void DeleteAndRedirect()
        {
            AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.ApplicationRole, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("ApplicationRoleEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        #endregion

    }
}