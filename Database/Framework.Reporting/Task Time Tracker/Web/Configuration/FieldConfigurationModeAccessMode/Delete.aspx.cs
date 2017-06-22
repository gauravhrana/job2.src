using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.FieldConfigurationModeAccessMode
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {  
    
        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity      = Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeAccessMode;
			PrimaryEntityKey   = "FieldConfigurationModeAccessMode";
			BreadCrumbObject   = Master.BreadCrumbObject;

			DetailsControlPath = ApplicationCommon.GetControlPath("FieldConfigurationModeAccessMode", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
		}

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var notDeletableIds = new List<int>();
				var deleteIndexList = DeleteIds.Split(',');

				foreach (var index in deleteIndexList)
                {
                    var data = new FieldConfigurationModeAccessModeDataModel();
                    data.FieldConfigurationModeAccessModeId = int.Parse(index);

					if (!FieldConfigurationModeAccessModeDataManager.IsDeletable(data, SessionVariables.RequestProfile))
                    {
                        notDeletableIds.Add((int)(data.FieldConfigurationModeAccessModeId));
                    }
                }

                if (notDeletableIds.Count == 0)
                {
                    foreach (string index in deleteIndexList)
                    {
                        var data = new FieldConfigurationModeAccessModeDataModel();
                        data.FieldConfigurationModeAccessModeId = int.Parse(index);
						FieldConfigurationModeAccessModeDataManager.Delete(data, SessionVariables.RequestProfile);
                    }
                }
                else
                {
                    var msg = String.Empty;
                    foreach (var id in deleteIndexList)
                    {
                        if (!string.IsNullOrEmpty(msg))
                        {
                            msg += ", <br/>";
                        }
                        msg += "FieldConfigurationModeAccessModeId: " + id + " has dependent records";
                    }
                    Response.Write(msg);
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
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeAccessMode, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("FieldConfigurationModeAccessModeEntityRoute", new { Action = "Default", SetId = true }), false);
            
		}

        #endregion

    }
}