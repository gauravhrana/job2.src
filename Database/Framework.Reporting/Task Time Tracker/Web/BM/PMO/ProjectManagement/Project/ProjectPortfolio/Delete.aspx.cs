using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectPortfolio
{
    public partial class Delete : PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.ProjectPortfolio;
            PrimaryEntityKey = "ProjectPortfolio";
            BreadCrumbObject = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("ProjectPortfolio", ControlType.DetailsControl);
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
                        var data = new ProjectPortfolioDataModel();
                        data.ProjectPortfolioId = int.Parse(index);
                        ProjectPortfolioDataManager.Delete(data, SessionVariables.RequestProfile);
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
                        msg += "ProjectPortfolioId: " + id + " has detail records";
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
			AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)SystemEntity.ProjectPortfolio, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("ProjectPortfolioEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        #endregion        
    }
}