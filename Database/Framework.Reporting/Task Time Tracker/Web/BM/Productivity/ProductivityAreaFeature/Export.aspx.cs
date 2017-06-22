using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;


namespace ApplicationContainer.UI.Web.Productivity.ProductivityAreaFeature
{
    public partial class Export : Shared.UI.WebFramework.BasePage
    {
        string searchCondition = String.Empty;

        private System.Data.DataTable GetData()
        {
            // TODO: on all export pages 
			var data = new ProductivityAreaFeatureDataModel();

            var dt = TaskTimeTracker.Components.BusinessLayer.ProductivityAreaFeatureDataManager.Search(data, SessionVariables.RequestProfile);

            return dt;
        }

        private string[] GetColumns()
        {
            return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ProductivityAreaFeature, "DBColumns", SessionVariables.RequestProfile);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // searchCondition = Request.QueryString["SearchCondition"];

                oList.Setup("ProductivityAreaFeature", " ", "ProjectId", false, GetData, GetColumns, false);
                oList.ExportMenu.Visible = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (!IsPostBack)
            {
                oList.ShowData(true, true);
            }
        }
    }
}