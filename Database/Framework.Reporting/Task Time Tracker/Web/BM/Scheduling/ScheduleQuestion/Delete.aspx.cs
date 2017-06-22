using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleQuestion
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {        

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "ScheduleQuestion";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("ScheduleQuestion", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ScheduleQuestion;
		}

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new ScheduleQuestionDataModel();
                    data.ScheduleQuestionId = int.Parse(index);
                    ScheduleQuestionDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ScheduleQuestion, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("ScheduleQuestionEntityRoute", new { Action = "Default", SetId = true }), false);
		}
	
		#endregion

    }
}