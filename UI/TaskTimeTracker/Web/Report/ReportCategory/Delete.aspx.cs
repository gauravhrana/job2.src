using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ReportCategory
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            PrimaryEntityKey = "ReportCategory";
            BreadCrumbObject = Master.BreadCrumbObject;

            var detailscontrolpath = ApplicationCommon.GetControlPath("ReportCategory", ControlType.DetailsControl);
            DetailsControlPath = detailscontrolpath;
            PrimaryPlaceHolder = plcDetailsList;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReportCategory;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new ReportCategoryDataModel();
                    data.ReportCategoryId = int.Parse(index);
					Framework.Components.Core.ReportCategoryDataManager.Delete(data, SessionVariables.RequestProfile);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ReportCategory, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("ReportCategoryEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        #endregion

    }
}