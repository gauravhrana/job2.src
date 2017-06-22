using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.ProjectTimeLine
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
			base.OnInit(e);
			PrimaryEntityKey = "ProjectTimeLine";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("ProjectTimeLine", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProjectTimeLine;
        }

        protected void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            ShowAuditHistory(chkVisible.Checked);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new ProjectTimeLineDataModel();
                    data.ProjectTimeLineId = int.Parse(index);
                    ProjectTimeLineDataManager.Delete(data, SessionVariables.RequestProfile.AuditId);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ProjectTimeLine, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("ProjectTimeLineEntityRoute", new { Action = "Default", SetId = true }), false);
		}

        #endregion        

    }
}