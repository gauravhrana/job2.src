using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.Audit.AuditHistory
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            try
            {
                var superKey = "";
                var detailsControlPath = "~/Shared/Admin/Audit/AuditHistory/Controls/Details.ascx";

                if (Request.QueryString["SuperKey"] != null)
                {
                    superKey = Request.QueryString["SuperKey"];

                    var data = new SuperKeyDetailDataModel();
                    data.SuperKeyId = Convert.ToInt32(superKey);

                    // Change System Entity Type
                    data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.AuditHistory;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
                            if (string.IsNullOrEmpty(DeleteIds))
                            {
                                DeleteIds = key.ToString();
                            }
                            else
                            {
                                DeleteIds += "," + key.ToString();
                            }
                            var detailsControl = (Controls.Details)Page.LoadControl(detailsControlPath);
                            detailsControl.SetId = key;
                            detailsControl.BorderClass = ApplicationCommon.DetailsBorderClassName;

                            plcDetailsList.Controls.Add(detailsControl);
                            plcDetailsList.Controls.Add(new LiteralControl("<br />"));

                            //chkVisible.Checked = detailsControl.IsHistoryVisible;
                        }
                    }
                }
                else
                {
                    var key = Convert.ToInt32(Request.QueryString["SetId"]);
                    DeleteIds = Request.QueryString["SetId"];

                    var detailsControl = (Controls.Details)Page.LoadControl(detailsControlPath);
                    detailsControl.SetId = key;
                    plcDetailsList.Controls.Add(detailsControl);
                    //chkVisible.Checked = detailsControl.IsHistoryVisible;
                }


                base.OnInit(e);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }        

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new DataModel.Framework.Audit.AuditHistory();
                    data.AuditHistoryId = int.Parse(index);
					Framework.Components.Audit.AuditHistoryDataManager.Delete(data, SessionVariables.RequestProfile);
                }

                //Framework.Components.Audit.AuditHistory.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.AuditHistory, AuditId);
                Response.Redirect(Page.GetRouteUrl("AuditHistoryEntityRoute", new { Action = "Default", SetId = true }), false);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        #endregion

    }
}