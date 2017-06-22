using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.SuperKey
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            try
            {
				base.OnInit(e);

				var detailsControlPath = "~/SystemIntegrity/SuperKey/Controls/Details.ascx";

                SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();

                if (!string.IsNullOrEmpty(SuperKey))
                {
                    btnClone.Visible = false;

                    var data = new SuperKeyDetailDataModel();
                    data.SuperKeyId = Convert.ToInt32(SuperKey);

                    // Change System Entity Type
                    data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.SuperKey;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);

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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "SuperKeyDefaultView";
			
		}

        

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(SuperKey))
                {
                    Response.Redirect(Page.GetRouteUrl("SuperKeyEntityRouteSuperKey", new { Action = "Delete", SuperKey = SuperKey }), false);
                }
                else
                {
                    Response.Redirect(Page.GetRouteUrl("SuperKeyEntityRoute", new { Action = "Delete", SetId = SetId }), false);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        override protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.GetRouteUrl("SuperKeyEntityRoute", new { Action = "Default" }), false);
        }

        override protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SuperKey))
            {
                Response.Redirect(Page.GetRouteUrl("SuperKeyEntityRouteSuperKey", new { Action = "Update", SuperKey = SuperKey }), false);
            }
            else
            {
                Response.Redirect(Page.GetRouteUrl("SuperKeyEntityRoute", new { Action = "Update", SetId = SetId }), false);
            }
        }

        override protected void btnClone_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.GetRouteUrl("SuperKeyEntityRoute", new { Action = "Clone", SetId = SetId }), false);
        }

        #endregion

    }
}