using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;

namespace Shared.UI.Web.Admin.SystemEntityType
{
    public partial class Export : Shared.UI.WebFramework.BasePage
    {
        string SearchCondition = String.Empty;


        private System.Data.DataTable GetData()
        {
            var data = new SystemEntityTypeDataModel();
            data.EntityName = Request.QueryString["SearchCondition"];
            var dt = Framework.Components.Core.SystemEntityTypeDataManager.Search(data, SessionVariables.RequestProfile );
            return dt;
        }

		private string[] GetColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.SystemEntityType, "DBColumns", SessionVariables.RequestProfile);
		}

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SearchCondition = Request.QueryString["SearchCondition"];
				mySearchControl.Setup("SystemEntityType", "Shared/Admin", "SystemEntityTypeId", false, GetData, GetColumns, false);
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
                mySearchControl.ShowData(true,true);
            }
        }
    }
}