using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;

namespace Shared.UI.Web.Configuration.FieldConfiguration
{
    public partial class Export : Shared.UI.WebFramework.BasePage
    {
        string SearchCondition = String.Empty;

        private System.Data.DataTable GetData()
        {
            var data = new FieldConfigurationDataModel();
			var dt = FieldConfigurationDataManager.Search(data, SessionVariables.RequestProfile);

            return dt;
        }

        private string[] GetColumns()
        {
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.FieldConfiguration, "DBColumns", SessionVariables.RequestProfile);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // see what parameter was passed 
                SearchCondition = Request.QueryString["SearchCondition"];
                oList.Setup("FieldConfiguration", " ", "FieldConfigurationId", false, GetData, GetColumns, false);
                oList.ExportMenu.Visible = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// After all the control have loaded, we can set values
        /// try to follow stratedy in general avoid doing alot of work in OnLoad
        /// OnLoad is quick operation ...
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            oList.ShowData(true, true);
        }
    }
}