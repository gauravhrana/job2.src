using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.FeatureRuleStatus
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {
        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            PrimaryEntityKey = "FeatureRuleStatus";
            BreadCrumbObject = Master.BreadCrumbObject;

            var detailscontrolpath = ApplicationCommon.GetControlPath("FeatureRuleStatus", ControlType.DetailsControl);
            DetailsControlPath = detailscontrolpath;
            PrimaryPlaceHolder = plcDetailsList;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureRuleStatus;
        }
        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new FeatureRuleStatusDataModel();
                    data.FeatureRuleStatusId = int.Parse(index);
                    TaskTimeTracker.Components.BusinessLayer.Feature.FeatureRuleStatusDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.FeatureRuleStatus, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("FeatureRuleStatusEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        #endregion

    }
}