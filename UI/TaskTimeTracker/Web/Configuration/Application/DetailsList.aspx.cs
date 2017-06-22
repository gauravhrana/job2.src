using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.Configuration.Application
{
    public partial class DetailsList : System.Web.UI.Page
    {

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string indexes = Request.QueryString["DeleteIds"].ToString();

                string[] deleteindexlist = indexes.Split(',');
                string path = "~/Application/Controls/Details.ascx";
                int i = 0;
                foreach (var index in deleteindexlist)
                {

					var data = new ApplicationDataModel();
                    data.ApplicationId = int.Parse(index);
                    var auditId = SessionVariables.RequestProfile.AuditId;
					var itemApplication = Framework.Components.ApplicationUser.ApplicationDataManager.GetDetails(data, SessionVariables.RequestProfile);

					var Applicationcontrol = new Shared.UI.Web.Configuration.Application.Controls.Details();

                    plcDetailsList.Controls.Add(Page.LoadControl(path));

                    Label lblAppId = (Label)plcDetailsList.Controls[i].FindControl("lblApplicationId");
                    Label lblName = (Label)plcDetailsList.Controls[i].FindControl("lblName");
                    Label lblDescription = (Label)plcDetailsList.Controls[i].FindControl("lblDescription");
                    Label lblSortOrder = (Label)plcDetailsList.Controls[i].FindControl("lblSortOrder");
                    if (lblAppId != null)
                    {
                        lblAppId.Text = Convert.ToString(itemApplication.ApplicationId);
                    }
                    if (lblName != null)
                    {
                        lblName.Text = itemApplication.Name;
                    }
                    if (lblDescription != null)
                    {
                        lblDescription.Text = itemApplication.Description;
                    }
                    if (lblSortOrder != null)
                    {
                        lblSortOrder.Text = Convert.ToString(itemApplication.SortOrder);
                    }
                    i++;
                    i++;
                    LiteralControl delimiter = new LiteralControl();
                    delimiter.Text = "=================================================";
                    plcDetailsList.Controls.Add(delimiter);

                }


            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string indexes = Request.QueryString["DeleteIds"].ToString();

                string[] deleteindexlist = indexes.Split(',');
                foreach (string index in deleteindexlist)
                {
					var data = new ApplicationDataModel();
                    data.ApplicationId = int.Parse(index);
                    var auditId = SessionVariables.RequestProfile.AuditId;

					Framework.Components.ApplicationUser.ApplicationDataManager.Delete(data, SessionVariables.RequestProfile);
                }
				Response.Redirect(Page.GetRouteUrl("ApplicationEntityRoute", new { Action = "Default", SetId = true }), false);
                //Response.Redirect("Default.aspx?Deleted=" + true, false);
                //ShowData();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        #endregion

    }
}