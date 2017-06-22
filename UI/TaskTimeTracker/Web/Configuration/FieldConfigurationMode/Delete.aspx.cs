using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.FieldConfigurationMode
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {      
        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfigurationMode;
			PrimaryEntityKey = "FieldConfigurationMode";
			BreadCrumbObject = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("FieldConfigurationMode", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new FieldConfigurationModeDataModel();
                    data.FieldConfigurationModeId = int.Parse(index);
					FieldConfigurationModeDataManager.Delete(data, SessionVariables.RequestProfile);
                }

				Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.FieldConfigurationMode, SessionVariables.RequestProfile);
                Response.Redirect(Page.GetRouteUrl("FieldConfigurationModeEntityRoute", new { Action = "Default", SetId = true }), false);
            }
            catch (Exception ex)
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                var msg = String.Empty;
                foreach (var id in deleteIndexList)
                {
                    if (!string.IsNullOrEmpty(msg))
                    {
                        msg += ", <br/>";
                    }
                    msg += "FieldConfigurationModeId: " + id + " has dependent FieldConfiguration records";
                }
                Response.Write(msg);
            }
        }

		private void DeleteAndRedirect()
		{
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.FieldConfigurationMode, SessionVariables.RequestProfile);
			Response.Redirect(Page.GetRouteUrl("FieldConfigurationModeEntityRoute", new { Action = "Default", SetId = true }), false);
		}

        #endregion
    }
}