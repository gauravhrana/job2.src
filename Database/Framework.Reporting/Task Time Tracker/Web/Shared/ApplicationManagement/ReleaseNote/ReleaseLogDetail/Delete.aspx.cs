using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationManagement.ReleaseLogDetail
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

       #region Events       

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new ReleaseLogDetailDataModel();
                    data.ReleaseLogDetailId = int.Parse(index);
					Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.Delete(data, SessionVariables.RequestProfile);
                }

				DeleteAndRedirect();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
	
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleaseLogDetail;
			PrimaryEntityKey = "ReleaseLogDetail";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("ReleaseLogDetail", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		} 

       
        #endregion

		#region methods

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ReleaseLogDetail, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("ReleaseLogEntityRoute", new { Action = "Default", SetId = true }), false);
		}



		#endregion

    }
}