using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Layer
{
    public partial class Export : Shared.UI.WebFramework.BasePage
    {

        #region variables

        #endregion

        #region private methods

        private System.Data.DataTable GetData()
        {
			var data = new LayerDataModel();
            var dt = LayerDataManager.Search(data, SessionVariables.RequestProfile);

            return dt;
        }

        private string[] GetColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.Layer, "DBColumns", SessionVariables.RequestProfile);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            oList.Setup("Layer", " ", "LayerId", false, GetData, GetColumns, false);
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