using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.ApplicationEntityFieldLabelMode
{
	public partial class Update : Shared.UI.WebFramework.BasePage
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
		private bool showMultipleUpdateView = false;

		protected override void OnInit(EventArgs e)
		{

			try
			{
				base.OnInit(e);

				SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();
				var path = "~/Shared/Configuration/ApplicationEntityFieldLabelMode/Controls/Details.ascx";
				var genericcontrolpath = "~/Shared/Configuration/ApplicationEntityFieldLabelMode/Controls/Generic.ascx";

                if (!string.IsNullOrEmpty(SuperKey))
                {
					btnCancel.Visible = true;
					btnUpdate.Visible = true;
					btnClone.Visible = false;
					showMultipleUpdateView = true;
				}
                else if (SetId != 0)
                {
					btnUpdate.Visible = true;
					btnCancel.Visible = true;
					btnClone.Visible = false;
				}
				if (showMultipleUpdateView)
				{
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

							var ApplicationEntityFieldLabelupdatecontrol = (Controls.Generic)Page.LoadControl(genericcontrolpath);
							ApplicationEntityFieldLabelupdatecontrol.SetId(key, false);
							plcUpdateList.Controls.Add(ApplicationEntityFieldLabelupdatecontrol);
							if (dt.Rows.Count > 1)
							{
								ApplicationEntityFieldLabelupdatecontrol.BorderClass = ApplicationCommon.DetailsBorderClassName;
								ApplicationEntityFieldLabelupdatecontrol.Controls.Add(new LiteralControl("<br />"));
							}
							chkVisible.Checked = ApplicationEntityFieldLabelupdatecontrol.IsHistoryVisible;
						}
					}
				}
				else
				{
                    var key = SetId;

					var ApplicationEntityFieldLabelupdatecontrol = (Controls.Generic)Page.LoadControl(genericcontrolpath);
					ApplicationEntityFieldLabelupdatecontrol.SetId(key, false);
					plcUpdateList.Controls.Add(ApplicationEntityFieldLabelupdatecontrol);
					chkVisible.Checked = ApplicationEntityFieldLabelupdatecontrol.IsHistoryVisible;
				}
			}
			catch (Exception ex)
			{

				System.Diagnostics.Debug.WriteLine(ex.Message);
				//throw
			}
		}

		protected void chkVisible_CheckedChanged(object sender, EventArgs e)
		{
			var isVisible = chkVisible.Checked;

			foreach (var control in plcUpdateList.Controls)
			{
				try
				{
					((Controls.Generic)control).IsHistoryVisible = isVisible;
				}
				catch { }
			}
		}

		protected void btnUpdate_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < plcUpdateList.Controls.Count; i++)
			{
				var myGenericControl = (Controls.Generic)plcUpdateList.Controls[i];
				var data = new Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.Data();
				data.ApplicationEntityFieldLabelModeId = (int?)myGenericControl.ApplicationEntityFieldLabelModeId;
				data.Name = myGenericControl.Name;
				data.Description = myGenericControl.Description;
				data.SortOrder = myGenericControl.SortOrder;
				

				Framework.Components.UserPreference.ApplicationEntityFieldLabelMode.Update(data, AuditId);

			}

			// To refresh values in the default page on an update.
            Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabelMode", Action = "Default", SetId = true }), false);
		}

		protected void btnClone_Click(object sender, EventArgs e)
		{
            Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabelMode", Action = "Clone", SetId = SetId }), false);
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
            Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabelMode", Action = "Default" }), false);
		}


	}
}