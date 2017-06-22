using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.UseCase
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

			PrimaryEntity		= Framework.Components.DataAccess.SystemEntity.UseCase;
            PrimaryEntityKey	= "UseCase";
            BreadCrumbObject	= Master.BreadCrumbObject;
			
			DetailsControlPath = ApplicationCommon.GetControlPath("UseCase", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;            
        }
        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new UseCaseDataModel();
                    data.UseCaseId = int.Parse(index);
                    TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.UseCase, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("UseCaseEntityRoute", new { Action = "Default", SetId = true }), false);
        }
             
        #endregion

    }
}