using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityPriority
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityPriority;
            PrimaryEntityKey = "FunctionalityPriority";
            BreadCrumbObject = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("FunctionalityPriority", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;
        }

        private void DeleteAndRedirect()
        {
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.FunctionalityPriority, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("FunctionalityPriorityEntityRoute", new { Action = "Default", SetId = true }), false);
        }

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			
			SettingCategory = "FunctionalityPriorityDefaultView";			
		}

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var notDeletableIds = new List<int>();
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new FunctionalityPriorityDataModel();
                    data.FunctionalityPriorityId = int.Parse(index);
                    if (!TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityPriorityDataManager.IsDeletable(data, SessionVariables.RequestProfile))
                    {
                        notDeletableIds.Add(Convert.ToInt32(data.FunctionalityPriorityId));
                    }
                }
                if (notDeletableIds.Count == 0)
                {
                    foreach (string index in deleteIndexList)
                    {
                        var data = new FunctionalityPriorityDataModel();
                        data.FunctionalityPriorityId = int.Parse(index);
                        TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityPriorityDataManager.Delete(data, SessionVariables.RequestProfile);
                    }
					Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.FunctionalityPriority, SessionVariables.RequestProfile);
                    Response.Redirect(Page.GetRouteUrl("FunctionalityPriorityEntityRoute", new { Action = "Default", SetId = true }), false);
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
                        msg += "FunctionalityPriorityId: " + id + " has detail records";
                    }
                    Response.Write(msg);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        override protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.GetRouteUrl("FunctionalityPriorityEntityRoute", new { Action = "Default" }), false);
        }

        #endregion

	}
}