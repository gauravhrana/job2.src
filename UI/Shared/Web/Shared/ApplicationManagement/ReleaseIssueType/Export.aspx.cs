using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web.Controls;

namespace Shared.UI.Web.ApplicationManagement.ReleaseIssueType
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
            var data = new ReleaseIssueTypeDataModel();
			var dt = Framework.Components.ReleaseLog.ReleaseIssueTypeDataManager.Search(data, AuditId);

            return dt;
        }

        private string[] GetColumns()
        {
            //return Framework.Components.ApplicationSecurity.GetApplicationEntityFieldLabelModeCategoryColumns("DBColumns", AuditId);
            return Framework.Components.ApplicationSecurity.GetIssueTypeReleaseColumns("DBColumns", AuditId);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                searchCondition = Request.QueryString["SearchCondition"];

                oList.Setup("ReleaseIssueType", "Shared/ApplicationManagement", "ReleaseIssueTypeId", false, GetData, GetColumns, false);
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