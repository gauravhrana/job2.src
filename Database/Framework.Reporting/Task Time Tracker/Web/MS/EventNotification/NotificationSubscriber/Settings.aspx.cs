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

namespace ApplicationContainer.UI.Web.EventNotification.NotificationSubscriber
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.NotificationSubscriber, "NotificationSubscriber");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new NotificationSubscriberDataModel();

            data.NotificationSubscriberId = int.Parse(values[0].ToString());
            data.Name = values[1].ToString();
            data.Description = values[2].ToString();
            data.SortOrder = int.Parse(values[3].ToString());
			Framework.Components.EventMonitoring.NotificationSubscriberDataManager.Update(data, SessionVariables.RequestProfile);
            ReBindEditableGrid();
        }

        private void ReBindEditableGrid()
        {
            var data = new NotificationSubscriberDataModel();
			var dtNotificationSubscriber = Framework.Components.EventMonitoring.NotificationSubscriberDataManager.Search(data, SessionVariables.RequestProfile);
        }
    }
}