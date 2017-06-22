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

namespace Shared.UI.Web.Configuration.MenuCategory
{
    public partial class Delete : PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.MenuCategory;
            PrimaryEntityKey = "MenuCategory";
            BreadCrumbObject = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("MenuCategory", ControlType.DetailsControl);
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
                    var data = new MenuCategoryDataModel();
                    data.MenuCategoryId = int.Parse(index);

                    if (!MenuCategoryDataManager.IsDeletable(data, SessionVariables.RequestProfile))
                    {
                        notDeletableIds.Add((int)(data.MenuCategoryId));
                    }
                }

                if (notDeletableIds.Count == 0)
                {
                    foreach (var index in deleteIndexList)
                    {
                        var data = new MenuCategoryDataModel();
                        data.MenuCategoryId = int.Parse(index);

                        MenuCategoryDataManager.Delete(data, SessionVariables.RequestProfile);
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
                        msg += "MenuCategoryId: " + id + " has detail records";
                    }

                    foreach (string index in deleteIndexList)
                    {
                        var data = new MenuCategoryDataModel();
                        data.MenuCategoryId = int.Parse(index);

                        MenuCategoryDataManager.DeleteChildren(data, SessionVariables.RequestProfile);
                        MenuCategoryDataManager.Delete(data, SessionVariables.RequestProfile);
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
            AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.MenuCategory, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("MenuCategoryEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        #endregion

    }
}