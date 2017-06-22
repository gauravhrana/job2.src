using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Controls
{
    public partial class DetailButtonPanel : System.Web.UI.UserControl
    {

        #region Variables

        private int _setId;

        public int SetId
        {
            set
            {
                _setId = value;
                SetButtonVisibility();
            }
            get
            {
                return _setId;
            }
        }

        #endregion

        #region private methods

        private void SetButtonVisibility()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["setid"]))
            {
                if (Request.Path.ToLower().Contains("delete.aspx"))
                    ButtonDelete.Visible = false;
                else if (Request.Path.ToLower().Contains("details.aspx"))
                    ButtonDetails.Visible = false;
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["deleteids"]))
            {
                ButtonDelete.Visible = false;
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            var userMode = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["user"]))
            {
                userMode = "&user=" + Request.QueryString["user"];
            }
            Response.Redirect("Delete.aspx?SetId=" + SetId + userMode);
        }

        protected void ButtonDetails_Click(object sender, EventArgs e)
        {
            var userMode = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["user"]))
            {
                userMode = "&user=" + Request.QueryString["user"];
            }
            Response.Redirect("Details.aspx?SetId=" + SetId + userMode);
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            var userMode = string.Empty;
            if (!string.IsNullOrEmpty(Request.QueryString["user"]))
            {
                userMode = "&user=" + Request.QueryString["user"];
            }
            Response.Redirect("Update.aspx?SetId=" + SetId + userMode);
        }

        #endregion

    }
}