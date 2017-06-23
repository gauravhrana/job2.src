using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.ApplicationManagement.ReleaseLog
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {

		

        #region Methods

        private DataTable GetReleaseLogDetailData(int ReleaseLogId)
        {
            var data = new ReleaseLogDetailDataModel();
            data.ReleaseLogId = ReleaseLogId;
			var dt = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.Search(data, SessionVariables.RequestProfile);
            return dt;
        }

        private string[] GetReleaseLogDetailColumns()
        {
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ReleaseLogDetail, "DBColumns", SessionVariables.RequestProfile);
        }

		protected override Control GetTabControl(int setId, Control detailsControl)
        {
            var tabControl = ApplicationCommon.GetNewDetailTabControl();
			tabControl.AddTab("Release Log", detailsControl, String.Empty, true);

            var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);
			tabControl.AddTab("Release Log Detail", listControl);

			listControl.Setup("ReleaseLogDetail", "Shared/ApplicationManagement", "ReleaseLogDetailId", setId, true, GetData, GetReleaseLogDetailColumns, "ReleaseLogDetail");
            listControl.SetSession("true");

			tabControl.Setup("ReleaseLogUpdateView");   
            
            return tabControl;
        }

		private DataTable GetData(string key)
		{
			return GetReleaseLogDetailData(int.Parse(key));
		}

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity      = Framework.Components.DataAccess.SystemEntity.ReleaseLog;

            GenericControlPath = ApplicationCommon.GetControlPath("ReleaseLog", ControlType.GenericControl);
            PrimaryPlaceHolder = plcUpdateList;
            PrimaryEntityKey   = "ReleaseLog";
            BreadCrumbObject   = Master.BreadCrumbObject;

            BtnUpdate          = btnUpdate;
            BtnClone           = btnClone;
            BtnCancel          = btnCancel;
        }

        #endregion

    }
}
