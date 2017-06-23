using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.ComponentModel;


namespace Shared.UI.Web.ApplicationManagement.Development.ActivityStream
{
    public partial class Default : WebFramework.BasePage
    {

        #region variables

        public string ID1
        {
            get
            {
                if (ViewState["ID1"] == null)
                {
                    ViewState["ID1"] = "";
                }

                return ViewState["ID1"].ToString();
            }
            set
            {
                ViewState["ID1"] = value;
            }
        }

        public string ID2
        {
            get
            {
                if (ViewState["ID2"] == null)
                {
                    ViewState["ID2"] = "";
                }

                return ViewState["ID2"].ToString();
            }
            set
            {
                ViewState["ID2"] = value;
            }
        }

        public int Count
        {
            get
            {
                return int.Parse(ViewState["Count"].ToString());
            }
            set
            {
                ViewState["Count"] = value;
            }
        }

        #endregion

        #region events

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                activityStream4.ActivityStreamName = activityStream4.ID + "1";
                activityStream2.ActivityStreamName = activityStream2.ID + "1";
            }
        }

        protected void btnCreateActivityStream_Click(object sender, EventArgs e)
        {
        }

        private void LoadActivityStreamControl(string ID, string method)
        {
            var activityStreamControlPath = "~/Shared/Controls/ActivityStream.ascx";
            var activityStreamControl = (Shared.UI.Web.Controls.ActivityStreamControl)Page.LoadControl(activityStreamControlPath) as Shared.UI.Web.Controls.ActivityStreamControl;
            activityStreamControl.ID = ID;
            Count++;
        }

        #endregion

    }
}