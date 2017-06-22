using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.ActivityStream.Controls
{
    public partial class AuditActionDisplay : BaseControl
    {

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region Methods

        public void LoadAlerts(UserActivityItem item)
        {
            if (!item.ShowImage)
            {
                imgAvatar.Visible = false;
                recordSeperator.Visible = false;
            }
            if (item.ShowTimeGroup)
            {
                lblTimeGroup.Text = item.TimeGrouping;
                tblTimeGroup.Visible = true;
                recordSeperator.Visible = false;
            }
            if (!recordSeperator.Visible && !tblTimeGroup.Visible)
            {
                smallRecordSeperator.Visible = true;
            }
            lblMessage.Text = item.ActivityText;
            if (item.gropedItems != null && item.gropedItems.Count > 0)
            {
                childRecords.Visible = true;
                repChildItems.DataSource = item.gropedItems;
                repChildItems.DataBind();
                seperateTimeStamp.Visible = true;
                lblTimeStamp.Text = item.TimeStampText;
            }
        }

        #endregion

    }
}