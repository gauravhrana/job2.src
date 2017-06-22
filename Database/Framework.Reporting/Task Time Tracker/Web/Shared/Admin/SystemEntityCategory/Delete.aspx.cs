using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.SystemEntityCategory
{
	public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
	{
        #region Events

        protected override void OnInit(EventArgs e)
        {
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SystemEntityCategory;
			PrimaryEntityKey = "SystemEntityCategory";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("SystemEntityCategory", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new SystemEntityCategoryDataModel();
                    data.SystemEntityCategoryId = int.Parse(index);
                    Framework.Components.Core.SystemEntityCategoryDataManager.Delete(data, SessionVariables.RequestProfile);
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
            Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.SystemEntityCategory, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("SystemEntityCategoryEntityRoute", new { Action = "Default", SetId = true }), false);
		}

        #endregion

	}
}