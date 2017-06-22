using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Client
{
	public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
	{
		//delegate void DelUpdateData(ArrayList values);

		protected void Page_Load(object sender, EventArgs e)
		{
			//DelUpdateData delUpdate = new DelUpdateData(UpdateData);
			////this.eList1.DelUpdateRef = delUpdate;
			//var data = new ClienDataModel();
			//var dtClient = TaskTimeTracker.Components.BusinessLayer.Client.Search(data, AuditId);
			//eList1.SetUp(ApplicationSecurity.GetClientColumns("DBColumns", SessionVariables.RequestProfile), "Client", dtClient);

			eSettingsList.SetUp((int)SystemEntity.Client, "Client");
		}

		private void UpdateData(ArrayList values)
		{
			var data = new ClientDataModel();

			data.ClientId = int.Parse(values[0].ToString());
			data.Name = values[1].ToString();
			data.Description = values[2].ToString();
			data.SortOrder = int.Parse(values[3].ToString());
			ClientDataManager.Update(data, SessionVariables.RequestProfile);
			ReBindEditableGrid();
		}

		private void ReBindEditableGrid()
		{
			var data = new ClientDataModel();
            var dtClient = ClientDataManager.Search(data, SessionVariables.RequestProfile);
			//eList1.BindData(dtClient);
		}
	}
}