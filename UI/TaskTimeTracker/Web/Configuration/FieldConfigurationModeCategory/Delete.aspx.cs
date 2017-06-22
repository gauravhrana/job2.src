using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.FieldConfigurationModeCategory
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {
        #region Events

        protected override void OnInit(EventArgs e)
        {
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeCategory;
			PrimaryEntityKey = "FieldConfigurationModeCategory";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("FieldConfigurationModeCategory", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;	
        }
   
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string[] deleteIndexList = DeleteIds.Split(',');
            foreach (string index in deleteIndexList)
            {
                var data = new FieldConfigurationModeCategoryDataModel();
                data.FieldConfigurationModeCategoryId = int.Parse(index);
				FieldConfigurationModeCategoryDataManager.Delete(data, SessionVariables.RequestProfile);
            }

			DeleteAndRedirect();
        }

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeCategory, SessionVariables.RequestProfile);			
			Response.Redirect(Page.GetRouteUrl("FieldConfigurationModeCategoryEntityRoute", new { Action = "Default", SetId = true }), false);
		}

        #endregion
    }
}