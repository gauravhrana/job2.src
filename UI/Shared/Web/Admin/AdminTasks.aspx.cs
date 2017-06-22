using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin
{
    public partial class AdminTasks : System.Web.UI.Page
    {

        #region Methods

        private void LoadExpiredSuperKeys()
        {
            var selectedDate = DateTime.Now.AddDays((Convert.ToInt32(drpCalendar.SelectedValue) + 1) * -1);
            var data = new Framework.Components.Core.SuperKey.Data();
            data.ExpirationDate = Convert.ToInt32(selectedDate.ToString("yyyyMMdd"));
            var dt = Framework.Components.Core.SuperKey.Search(data, SessionVariables.AuditId);
            dgvSuperKey.DataSource = dt;
            dgvSuperKey.DataBind();
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                LoadExpiredSuperKeys();
            }
        }

        protected void btnDeleteSuperKey_Click(object sender, EventArgs e)
        {
            var data = new Framework.Components.Core.SuperKey.Data();
            var selectedDate = DateTime.Now.AddDays((Convert.ToInt32(drpCalendar.SelectedValue) + 1) * -1);
            data.ExpirationDate = Convert.ToInt32(selectedDate.ToString("yyyyMMdd"));
            Framework.Components.Core.SuperKey.DeleteExpired(data, SessionVariables.AuditId);
            lblMessage.Text = "Expired SuperKey Records deleted.";

            LoadExpiredSuperKeys();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadExpiredSuperKeys();
        }

        #endregion

    }
}