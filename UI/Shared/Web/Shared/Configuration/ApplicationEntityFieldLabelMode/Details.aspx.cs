using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.ApplicationEntityFieldLabelMode
{
	public partial class Details : Shared.UI.WebFramework.BasePage
	{

        private string SuperKey
        {
            get
            {
                return Convert.ToString(ViewState["SuperKey"]);
            }
            set
            {
                ViewState["SuperKey"] = value;
            }
        }

        private int SetId
        {
            get
            {
                return Convert.ToInt32(ViewState["SetId"]);
            }
            set
            {
                ViewState["SetId"] = value;
            }
        }

		#region Events

		protected override void OnInit(EventArgs e)
		{
			try
			{
				base.OnInit(e);

				SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();
				var detailsControlPath = "~/Shared/Configuration/ApplicationEntityFieldLabelMode/Controls/Details.ascx";

                if (!string.IsNullOrEmpty(SuperKey))
                {
					btnClone.Visible = false;

					var data = new Framework.Components.Core.SuperKeyDetail.Data();
                    data.SuperKeyId = Convert.ToInt32(SuperKey);

					// Change System Entity Type
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.ApplicationEntityFieldLabelMode;
					var dt = Framework.Components.Core.SuperKeyDetail.Search(data, SessionVariables.AuditId);
					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (System.Data.DataRow dr in dt.Rows)
						{
							var key = Convert.ToInt32(dr[Framework.Components.Core.SuperKeyDetail.DataColumns.EntityKey]);

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

		protected void chkVisible_CheckedChanged(object sender, EventArgs e)
		{
			var isVisible = chkVisible.Checked;
			foreach (var control in plcDetailsList.Controls)
			{
				try
				{
					((Controls.Details)control).IsHistoryVisible = isVisible;
				}
				catch { }
			}
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{

            if (!string.IsNullOrEmpty(SuperKey))
            {
                Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutesSuperKey", new { EntityName = "ApplicationEntityFieldLabelMode", Action = "Delete", SuperKey = SuperKey }), false);
            }
            else
            {
                Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabelMode", Action = "Delete", SetId = SetId }), false);
            }
		}

		protected void btnBack_Click(object sender, EventArgs e)
		{
            Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabelMode", Action = "Default" }), false);
		}

		protected void btnUpdate_Click(object sender, EventArgs e)
		{
            if (!string.IsNullOrEmpty(SuperKey))
            {
                Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutesSuperKey", new { EntityName = "ApplicationEntityFieldLabelMode", Action = "Update", SuperKey = SuperKey }), false);
            }
            else
            {
                Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabelMode", Action = "Update", SetId = SetId }), false);
            }	
		}

		protected void btnClone_Click(object sender, EventArgs e)
		{
            Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabelMode", Action = "Clone", SetId = SetId }), false);
		}

		#endregion

	}
}