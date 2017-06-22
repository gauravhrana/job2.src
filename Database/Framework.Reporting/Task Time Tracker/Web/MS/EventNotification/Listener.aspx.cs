using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.EventMonitoring;

namespace ApplicationContainer.UI.Web.EventNotification
{
	public partial class Listener : Shared.UI.WebFramework.BasePage
	{
		public static decimal maxcreatedDateid;

		static Listener()
		{
			var data = new NotificationRegistrarDataModel();
			var dt = Framework.Components.EventMonitoring.NotificationRegistrarDataManager.GetPublishDetails(maxcreatedDateid);

			maxcreatedDateid = Convert.ToDecimal(dt.Rows[0]["MaxPublishDate"]);  
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				SettingCategory = "ListenerDefaultView";
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
			//bcControl.Setup("Listener");
			//bcControl.GenerateMenu();

		}

		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			DateTime publishTime;
			publishTime = System.DateTime.Now;

			var dt = Framework.Components.EventMonitoring.NotificationRegistrarDataManager.GetPublishDetails(publishTime, maxcreatedDateid);
			var count	= dt.Rows[0]["Count"].ToString();
			var lastdate = dt.Rows[0]["lastDate"].ToString();
			lblStatus.Text = count +" records to be Published";
			maxcreatedDateid = Convert.ToDecimal(lastdate);
			
		}
	}
}