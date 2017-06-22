using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Framework.Components.Core;

namespace Shared.UI.Web.Configuration.TabParentStructure
{
    public partial class Delete : PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.TabParentStructure;
            PrimaryEntityKey = "TabParentStructure";
            BreadCrumbObject = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("TabParentStructure", ControlType.DetailsControl);
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
                    var data = new TabParentStructureDataModel();
                    data.TabParentStructureId = int.Parse(index);

                    if (!TabParentStructureDataManager.IsDeletable(data, SessionVariables.RequestProfile))
                    {
                        notDeletableIds.Add((int)(data.TabParentStructureId));
                    }
                }

                if (notDeletableIds.Count == 0)
                {
                    foreach (var index in deleteIndexList)
                    {
                        var data = new TabParentStructureDataModel();
                        data.TabParentStructureId = int.Parse(index);

                        TabParentStructureDataManager.Delete(data, SessionVariables.RequestProfile);
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
                        msg += "TabParentStructureId: " + id + " has detail records";
                    }

                    foreach (string index in deleteIndexList)
                    {
                        var data = new TabParentStructureDataModel();
                        data.TabParentStructureId = int.Parse(index);

                        TabParentStructureDataManager.DeleteChildren(data, SessionVariables.RequestProfile);
                        TabParentStructureDataManager.Delete(data, SessionVariables.RequestProfile);
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
            AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.TabParentStructure, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("TabParentStructureEntityRoute", new { Action = "Default", SetId = true }), false);
        }       

        #endregion

    }
}