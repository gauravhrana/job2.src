using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.UseCaseWorkFlowCategory
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            PrimaryEntityKey = "UseCaseWorkFlowCategory";
            BreadCrumbObject = Master.BreadCrumbObject;

            var detailscontrolpath = ApplicationCommon.GetControlPath("UseCaseWorkFlowCategory", ControlType.DetailsControl);
            DetailsControlPath = detailscontrolpath;
            PrimaryPlaceHolder = plcDetailsList;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCaseWorkFlowCategory;
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new UseCaseWorkFlowCategoryDataModel();
                    data.UseCaseWorkFlowCategoryId = int.Parse(index);
                    TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseWorkFlowCategoryDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.UseCaseWorkFlowCategory, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("UseCaseWorkFlowCategoryEntityRoute", new { Action = "Default", SetId = true }), false);
        }
     
        #endregion

    }
}