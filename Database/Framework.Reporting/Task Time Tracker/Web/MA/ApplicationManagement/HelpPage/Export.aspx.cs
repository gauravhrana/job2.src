using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DataModel.Framework.Core;
using Shared.UI.Web.Controls;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.HelpPage
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
            var data = new HelpPageDataModel();

			var dt = Framework.Components.Core.HelpPageDataManager.Search(data, SessionVariables.RequestProfile);

            return dt;
        }

        private string[] GetColumns()
        {
			var validColumns = FieldConfigurationUtility.GetEntityColumns("Default", Framework.Components.DataAccess.SystemEntity.HelpPage, SessionVariables.RequestProfile);
            return validColumns;
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // searchCondition = Request.QueryString["SearchCondition"];

                oList.Setup("HelpPage", "Shared/ApplicationManagement", "HelpPageId", false, GetData, GetColumns, false);
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