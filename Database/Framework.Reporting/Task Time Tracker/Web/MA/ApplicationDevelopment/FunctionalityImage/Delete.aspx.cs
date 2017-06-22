using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityImage
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {
		
        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			PrimaryEntityKey = "FunctionalityImage";
			BreadCrumbObject = Master.BreadCrumbObject;

			var detailscontrolpath = ApplicationCommon.GetControlPath("FunctionalityImage", ControlType.DetailsControl);
			DetailsControlPath = detailscontrolpath;
			PrimaryPlaceHolder = plcDetailsList;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityImage;
		}

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.FunctionalityImage, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("FunctionalityImageEntityRoute", new { Action = "Default", SetId = true }), false);
		}
	
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');

                foreach (string index in deleteIndexList)
                {
                    var data = new FunctionalityImageDataModel();
                    data.FunctionalityImageId = int.Parse(index);
					FunctionalityImageDataManager.Delete(data, SessionVariables.RequestProfile);
                }

				DeleteAndRedirect();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }        

        #endregion
    }
}