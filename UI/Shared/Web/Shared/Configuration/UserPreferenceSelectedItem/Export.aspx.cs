using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.UserPreferenceSelectedItem
{
    public partial class Export : Shared.UI.WebFramework.BasePage
    {
        string SearchCondition = String.Empty;

        private System.Data.DataTable GetData()
        {
            var data = new Framework.Components.UserPreference.UserPreferenceSelectedItemDataModel();
			var dt = Framework.Components.UserPreference.UserPreferenceSelectedItemDataManager.Search(data, SessionVariables.RequestProfile);

            return dt;
        }

		private string[] GetColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UserPreferenceSelectedItem, "DBColumns", SessionVariables.RequestProfile);
		}

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // see what parameter was passed 
                SearchCondition = Request.QueryString["SearchCondition"];

                oList.Setup("UserPreferenceSelectedItem", " ", "UserPreferenceSelectedItemId", false, GetData, GetColumns, false);
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
        /// <param PersonId="e"></param>
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            oList.ShowData(true, true);
        }

    }
}