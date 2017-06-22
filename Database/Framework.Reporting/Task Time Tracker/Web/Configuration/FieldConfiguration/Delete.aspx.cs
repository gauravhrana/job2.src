using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.Configuration.FieldConfiguration
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfiguration;
            PrimaryEntityKey = "FieldConfiguration";
            BreadCrumbObject = Master.BreadCrumbObject;

            DetailsControlPath = ApplicationCommon.GetControlPath("FieldConfiguration", ControlType.DetailsControl);
            PrimaryPlaceHolder = plcDetailsList;
        }

        private void DeleteAndRedirect()
        {
			Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.FieldConfiguration, SessionVariables.RequestProfile);
            Response.Redirect(Page.GetRouteUrl("FieldConfigurationEntityRoute", new { Action = "Default", SetId = true }), false);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var notDeletableIds = new List<int>();
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new FieldConfigurationDataModel();
                    data.FieldConfigurationId = int.Parse(index);
					if (!FieldConfigurationDataManager.IsDeletable(data, SessionVariables.RequestProfile))
                    {
                        notDeletableIds.Add(Convert.ToInt32(data.FieldConfigurationId));
                    }
                }
                if (notDeletableIds.Count == 0)
                {
                    foreach (string index in deleteIndexList)
                    {
                        var data = new FieldConfigurationDataModel();
                        data.FieldConfigurationId = int.Parse(index);
						FieldConfigurationDataManager.Delete(data, SessionVariables.RequestProfile);
                    }
					Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.FieldConfiguration, SessionVariables.RequestProfile);

                    Response.Redirect(Page.GetRouteUrl("FieldConfigurationEntityRoute", new { Action = "Default", SetId = true }), false);
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
                        msg += "FieldConfigurationId: " + id + " has detail records";
                        var data = new FieldConfigurationDisplayNameDataModel();
                        data.FieldConfigurationId = id;
						var dt = FieldConfigurationDisplayNameDataManager.Search(data, SessionVariables.RequestProfile, SessionVariables.ApplicationMode);
                        foreach (DataRow dr in dt.Rows)
                        {
                            var fcdnid = int.Parse(dr[FieldConfigurationDisplayNameDataModel.DataColumns.FieldConfigurationDisplayNameId].ToString());
                            data.FieldConfigurationDisplayNameId = fcdnid;
							FieldConfigurationDisplayNameDataManager.Delete(data, SessionVariables.RequestProfile);
                        }
                        var fcdata = new FieldConfigurationDataModel();
                        fcdata.FieldConfigurationId = id;
						FieldConfigurationDataManager.Delete(fcdata, SessionVariables.RequestProfile);

						Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(id.ToString(), (int)Framework.Components.DataAccess.SystemEntity.FieldConfiguration, SessionVariables.RequestProfile);

                        Response.Redirect(Page.GetRouteUrl("FieldConfigurationEntityRoute", new { Action = "Default", SetId = true }), false);

                    }
                    Response.Write(msg);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        #endregion
    }
}