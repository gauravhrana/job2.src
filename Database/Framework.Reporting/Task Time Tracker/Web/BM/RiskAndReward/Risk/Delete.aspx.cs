using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RiskReward;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.RiskReward;

namespace ApplicationContainer.UI.Web.RiskAndReward.Risk
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {
        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            PrimaryEntityKey = "Risk";
            BreadCrumbObject = Master.BreadCrumbObject;

            var detailscontrolpath = ApplicationCommon.GetControlPath("Risk", ControlType.DetailsControl);
            DetailsControlPath = detailscontrolpath;
            PrimaryPlaceHolder = plcDetailsList;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Risk;
        }
        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new RiskDataModel();
                    data.RiskId = int.Parse(index);
                    RiskDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.Risk, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("RiskEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        #endregion

    }
}