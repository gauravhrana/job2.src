using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking;
using TaskTimeTracker.Components.Module.TimeTracking.DomainModel;

namespace ApplicationContainer.UI.Web.Scheduling.ScheduleItem
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {        

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey		= "ScheduleItem";
			BreadCrumbObject		= Master.BreadCrumbObject;

			var detailscontrolpath	= ApplicationCommon.GetControlPath("ScheduleItem", ControlType.DetailsControl);
			DetailsControlPath		= detailscontrolpath;
			PrimaryPlaceHolder		= plcDetailsList;
			PrimaryEntity			= Framework.Components.DataAccess.SystemEntity.ScheduleItem;
		}

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new ScheduleItemDataModel();
                    data.ScheduleItemId = int.Parse(index);
                    ScheduleItemDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ScheduleItem, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("ScheduleItemEntityRoute", new { Action = "Default", SetId = true }), false);
		}

		#endregion

    }
}