using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.UserPreferenceDataType
{
    public partial class Export : Shared.UI.WebFramework.BasePage
    {
        string SearchCondition = String.Empty;


        private System.Data.DataTable GetData()
        {
            var data = new DataModel.Framework.Configuration.UserPreferenceDataTypeDataModel();

			var dt = Framework.Components.UserPreference.UserPreferenceDataTypeDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

		private string[] GetColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UserPreferenceDataType, "DBColumns", SessionVariables.RequestProfile);
		}

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SearchCondition = Request.QueryString["SearchCondition"];
                oList.Setup("UserPreferenceDataType", " ", "UserPreferenceDataTypeId", false, GetData, GetColumns, false);
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