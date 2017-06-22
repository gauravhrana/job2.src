using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Client
{
    public partial class Export : BasePage
    {
        #region private methods

        private DataTable GetData()
        {
            // TODO: on all export pages 
            var data = new ClientDataModel();

            var dt = ClientDataManager.Search(data, SessionVariables.RequestProfile);

            return dt;
        }

		private string[] GetColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.Client, "DBColumns", SessionVariables.RequestProfile);
		}

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
			oList.Setup("Client", " ", "ClientId", false, GetData, GetColumns, false);
			oList.ExportMenu.Visible = false;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            oList.ShowData(true, true);
        }

        #endregion
    }
}