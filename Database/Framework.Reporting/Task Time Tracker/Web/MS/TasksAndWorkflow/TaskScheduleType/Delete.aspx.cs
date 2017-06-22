using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.TasksAndWorkFlow;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.TasksAndWorkflow.TaskScheduleType
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            PrimaryEntityKey = "TaskScheduleType";
            BreadCrumbObject = Master.BreadCrumbObject;

            var detailscontrolpath = ApplicationCommon.GetControlPath("TaskScheduleType", ControlType.DetailsControl);
            DetailsControlPath = detailscontrolpath;
            PrimaryPlaceHolder = plcDetailsList;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TaskScheduleType;
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new TaskScheduleTypeDataModel();
                    data.TaskScheduleTypeId = int.Parse(index);
					Framework.Components.TasksAndWorkflow.TaskScheduleTypeDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.TaskScheduleType, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("TaskScheduleTypeEntityRoute", new { Action = "Default", SetId = true }), false);
        }

    
        #endregion

    }
}