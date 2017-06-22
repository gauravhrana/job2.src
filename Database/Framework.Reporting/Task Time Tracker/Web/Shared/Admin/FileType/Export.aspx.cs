using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Import;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;

namespace Shared.UI.Web.FileType
{
    public partial class Export : Shared.UI.WebFramework.BasePage
    {

        #region private methods

        private System.Data.DataTable GetData()
        {
            var data = new FileTypeDataModel();

			var dt = Framework.Components.Import.FileTypeDataManager.Search(data, SessionVariables.RequestProfile);

            return dt;
        }

        private string[] GetColumns()
        {
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.FileType, "DBColumns", SessionVariables.RequestProfile);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            oList.Setup("FileType", " ", "FileTypeId", false, GetData, GetColumns, false);
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