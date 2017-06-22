﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationRegistrar
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.NotificationRegistrar, "NotificationRegistrar");
        }

        override protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        override protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Shared/Configuration/NotificationRegistrar/Default.aspx");
        }
    }
}