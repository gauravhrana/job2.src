using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using WebCommon.UI.Web;

namespace Shared.UI.Web.ActivityStream
{
    public partial class Settings : System.Web.UI.Page
    {
        private string _categoryName;

        public string CategoryName
        {
            get
            {
                return _categoryName;
            }
            set
            {
                _categoryName = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            var genericcontrolpath = "~/ActivityStream/Controls/Generic.ascx";

            //var key = 1;
            CategoryName = Request.QueryString["CategoryName"];
            var Featureupdatecontrol = (Controls.Generic)Page.LoadControl(genericcontrolpath);
            Featureupdatecontrol.SetCategoryName(CategoryName, true);
            plcUpdateList.Controls.Add(Featureupdatecontrol);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var myGenericControl = (Controls.Generic)plcUpdateList.Controls[0];

            ApplicationCommon.UpdateUserPreference(CategoryName, ActivityStreamCommon.DateRangeKey, myGenericControl.Interval);
            ApplicationCommon.UpdateUserPreference(CategoryName, ActivityStreamCommon.BackGroungColorKey, myGenericControl.Color);
            ApplicationCommon.UpdateUserPreference(CategoryName, ActivityStreamCommon.WidthKey, myGenericControl.Width);
            ApplicationCommon.UpdateUserPreference(CategoryName, ActivityStreamCommon.DataTypeKey, myGenericControl.DataType);
            ApplicationCommon.UpdateUserPreference(CategoryName, ActivityStreamCommon.HeightKey, myGenericControl.Height);
            ApplicationCommon.UpdateUserPreference(CategoryName, ActivityStreamCommon.ActivityStreamAuditId, myGenericControl.ActivityStreamAuditId);
            ApplicationCommon.UpdateUserPreference(CategoryName, ActivityStreamCommon.ActivityStreamPageSize, myGenericControl.ActivityStreamPageSize);

            if (!string.IsNullOrEmpty(Request.QueryString["returnPage"]))
            {
                if (Request.QueryString["returnPage"] == "appUserDetail")
                {
                    Response.Redirect("~/AuthenticationAndAuthorization/ApplicationUser/Details.aspx?&SetId=" + Request.QueryString["SetId"]);
                }
                else if (Request.QueryString["returnPage"] == "appUserUpdate")
                {
                    Response.Redirect("~/AuthenticationAndAuthorization/ApplicationUser/Update.aspx?&SetId=" + Request.QueryString["SetId"]);
                }
            }
            else
            {
                Response.Redirect("~/ApplicationManagement/Development/ActivityStream/Default.aspx");
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["returnPage"]))
            {
                if (Request.QueryString["returnPage"] == "appUserDetail")
                {
                    Response.Redirect("~/AuthenticationAndAuthorization/ApplicationUser/Details.aspx?&SetId=" + Request.QueryString["SetId"]);
                }
                else if (Request.QueryString["returnPage"] == "appUserUpdate")
                {
                    Response.Redirect("~/AuthenticationAndAuthorization/ApplicationUser/Update.aspx?&SetId=" + Request.QueryString["SetId"]);
                }
            }
            else
            {
                Response.Redirect("~/ApplicationManagement/Development/ActivityStream/Default.aspx");
            }
        }

    }
}