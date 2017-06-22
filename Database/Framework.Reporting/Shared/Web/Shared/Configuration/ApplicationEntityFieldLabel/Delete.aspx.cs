using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.ApplicationEntityFieldLabel
{
	public partial class Delete : Shared.UI.WebFramework.BasePage
	{

        #region variables

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

        private string DeleteIds
        {
            get
            {
                return Convert.ToString(ViewState["DeleteIds"]);
            }
            set
            {
                ViewState["DeleteIds"] = value;
            }
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            try
            {
				base.OnInit(e);

				SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();
                var detailsControlPath = "~/Shared/Configuration/ApplicationEntityFieldLabel/Controls/Details.ascx";

                if (!string.IsNullOrEmpty(SuperKey))
                {

                    var data = new Framework.Components.Core.SuperKeyDetail.Data();
                    data.SuperKeyId = Convert.ToInt32(SuperKey);

                    // Change System Entity Type
                    data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.ApplicationEntityFieldLabel;
                    var dt = Framework.Components.Core.SuperKeyDetail.Search(data, SessionVariables.AuditId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            var key = Convert.ToInt32(dr[Framework.Components.Core.SuperKeyDetail.DataColumns.EntityKey]);
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
            try
            {
                string[] deleteIndexList = DeleteIds.Split(',');
                foreach (string index in deleteIndexList)
                {
                    var data = new Framework.Components.UserPreference.ApplicationEntityFieldLabel.Data();
                    data.ApplicationEntityFieldLabelId = int.Parse(index);
                    Framework.Components.UserPreference.ApplicationEntityFieldLabel.Delete(data, SessionVariables.AuditId);
                }

                Framework.Components.Audit.AuditHistory.DeleteDataBySystemEntity(DeleteIds, (int)Framework.Components.DataAccess.SystemEntity.ApplicationEntityFieldLabel, AuditId);
                Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabel", Action = "Default", SetId = true }), false);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.GetRouteUrl("ConfiguratonSubRoutes", new { EntityName = "ApplicationEntityFieldLabel", Action = "Default" }), false);
        }

        #endregion

	}
}