using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;

namespace Shared.UI.Web.Configuration.ApplicationMode
{
    public partial class Export : Shared.UI.WebFramework.BasePage
    {

        #region variables

        string searchCondition = String.Empty;

        #endregion

        #region private methods

        private System.Data.DataTable GetData()
        {
            // TODO: on all export pages 
            var data = new ApplicationModeDataModel();
			var dt = Framework.Components.UserPreference.ApplicationModeDataManager.Search(data, SessionVariables.RequestProfile);

            return dt;
        }

        private string[] GetColumns()
        {
            //return Framework.Components.ApplicationSecurity.GetApplicationModeColumns("DBColumns", SessionVariables.RequestProfile);
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ApplicationMode, "DBColumns", SessionVariables.RequestProfile);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                searchCondition = Request.QueryString["SearchCondition"];

                oList.Setup("ApplicationMode", "Shared/Configuration", "ApplicationModeId", false, GetData, GetColumns, false);
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
            oList.ShowData(true, true);
        }

        #endregion
    }
}