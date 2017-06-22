using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.EventNotification
{
	public partial class Publisher : Shared.UI.WebFramework.BasePage
	{
			
		protected void btnPublish_Click(object sender, EventArgs e)
		{
			var data = new NotificationRegistrarDataModel();

			data.NotificationEventTypeId = 2;
			data.Message = "Menu Event";
			data.NotificationPublisherId = -101;

			Framework.Components.EventMonitoring.NotificationRegistrarDataManager.Create(data, SessionVariables.RequestProfile);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				SettingCategory = "PublisherDefaultView";
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			var sbm = this.Master.SubMenuObject;
			

			sbm.SettingCategory = SettingCategory + "SubMenuControl";
			sbm.Setup();
			sbm.GenerateMenu();

			//bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
			//bcControl.Setup("Publisher");
			//bcControl.GenerateMenu();

		}

	}
}

