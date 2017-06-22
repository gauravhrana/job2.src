using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;

namespace ApplicationContainer.UI.Web.EventNotification.NotificationEventType
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.NotificationEventType, "NotificationEventType");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new NotificationEventTypeDataModel();

            data.NotificationEventTypeId    = int.Parse(values[0].ToString());
            data.Name                       = values[1].ToString();
            data.Description                = values[2].ToString();
            data.SortOrder                  = int.Parse(values[3].ToString());
			Framework.Components.EventMonitoring.NotificationEventTypeDataManager.Update(data, SessionVariables.RequestProfile);
        }
    }
}