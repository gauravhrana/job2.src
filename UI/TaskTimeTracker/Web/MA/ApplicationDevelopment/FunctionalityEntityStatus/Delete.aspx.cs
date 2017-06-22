using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityEntityStatus
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region variables

        

        #endregion

        #region Events

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            
            SettingCategory = "FunctionalityEntityStatusDefaultView";           

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityEntityStatus;
            PrimaryEntityKey = "FunctionalityEntityStatus";
            BreadCrumbObject = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("FunctionalityEntityStatus", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;
        }       
          
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var notDeletableIds = new List<int>();
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new FunctionalityEntityStatusDataModel();
                    data.FunctionalityEntityStatusId = int.Parse(index);
					if (!TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.IsDeletable(data, SessionVariables.RequestProfile))
                    {
                        notDeletableIds.Add(Convert.ToInt32(data.FunctionalityEntityStatusId));
                    }
                }
                if (notDeletableIds.Count == 0)
                {
                    foreach (string index in deleteIndexList)
                    {
                        var data = new FunctionalityEntityStatusDataModel();
                        data.FunctionalityEntityStatusId = int.Parse(index);
						TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.Delete(data, SessionVariables.RequestProfile);
                    }
					Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.FunctionalityEntityStatus, SessionVariables.RequestProfile);

                    Response.Redirect(Page.GetRouteUrl("FunctionalityEntityStatusEntityRoute", new { Action = "Default", SetId = true }), false);
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
                        msg += "FunctionalityEntityStatusId: " + id + " has detail records";
                        var data = new FunctionalityEntityStatusArchiveDataModel();
                        data.FunctionalityEntityStatusId = id;
						var dt = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusArchiveDataManager.Search(data, SessionVariables.RequestProfile);
                        foreach (DataRow dr in dt.Rows)
                        {
                            var fesarchid = int.Parse(dr[FunctionalityEntityStatusArchiveDataModel.DataColumns.FunctionalityEntityStatusArchiveId].ToString());
                            data.FunctionalityEntityStatusArchiveId = fesarchid;
							TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusArchiveDataManager.Delete(data, SessionVariables.RequestProfile);
                        }
                        var fesdata = new FunctionalityEntityStatusDataModel();
                        fesdata.FunctionalityEntityStatusId = id;
						TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityEntityStatusDataManager.Delete(fesdata, SessionVariables.RequestProfile);

						Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(id.ToString(), (int)Framework.Components.DataAccess.SystemEntity.FunctionalityEntityStatus, SessionVariables.RequestProfile);

                        Response.Redirect(Page.GetRouteUrl("FunctionalityEntityStatusEntityRoute", new { Action = "Default", SetId = true }), false);
                
                    }
                    Response.Write(msg);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void DeleteAndRedirect()
        {
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.FunctionalityEntityStatus, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("FunctionalityEntityStatusEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        #endregion

    }
}