using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Feature;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.FeatureXFeatureRule
{
    public partial class Delete : PageDelete
    {
        #region Events

        protected override void OnInit(EventArgs e)
        {
			base.OnInit(e);
			PrimaryEntityKey = "FeatureXFeatureRule";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("FeatureXFeatureRule", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FeatureXFeatureRule;
        }          
           
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new FeatureXFeatureRuleDataModel();
                    data.FeatureXFeatureRuleId = int.Parse(index);
                    TaskTimeTracker.Components.BusinessLayer.Feature.FeatureXFeatureRuleDataManager.Delete(data, SessionVariables.RequestProfile);
                }

				DeleteAndRedirect();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }        

        #endregion

        #region Methods

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.FeatureXFeatureRule, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("FeatureXFeatureRuleEntityRoute", new { Action = "Default", SetId = true }), false);
		}

        #endregion

    }
}