﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.SuperKeyDetail
{
    public partial class Delete : Framework.UI.Web.BaseClasses.PageDelete
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            try
            {
				base.OnInit(e);

				var detailsControlPath = "~/SystemIntegrity/SuperKeyDetail/Controls/Details.ascx";

                SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();

                if (!string.IsNullOrEmpty(SuperKey))
                {

                    var data = new SuperKeyDetailDataModel();
                    data.SuperKeyId = Convert.ToInt32(SuperKey);

                    // Change System Entity Type
                    data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.SuperKeyDetail;
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

                            chkVisible.Checked = detailsControl.IsHistoryVisible;
                        }
                    }
                }
                else if (SetId != 0)
                {
                    DeleteIds = SetId.ToString();

                    var detailsControl = (Controls.Details)Page.LoadControl(detailsControlPath);
                    detailsControl.SetId = SetId;
                    plcDetailsList.Controls.Add(detailsControl);
                    chkVisible.Checked = detailsControl.IsHistoryVisible;
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
                

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "SuperKeyDetailDefaultView";
			
		}       

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new SuperKeyDetailDataModel();
                    data.SuperKeyDetailId = int.Parse(index);
					Framework.Components.Core.SuperKeyDetailDataManager.Delete(data, SessionVariables.RequestProfile);
                }

				Framework.Components.Audit.AuditHistoryDataManager.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.SuperKeyDetail, SessionVariables.RequestProfile);
                Response.Redirect(Page.GetRouteUrl("SuperKeyDetailEntityRoute", new { Action = "Default", SetId = true }), false);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.GetRouteUrl("SuperKeyDetailEntityRoute", new { Action = "Default" }), false);
        }

        #endregion

    }
}