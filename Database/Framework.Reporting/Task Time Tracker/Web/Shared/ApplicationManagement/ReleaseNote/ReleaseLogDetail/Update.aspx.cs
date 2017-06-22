using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.ReleaseLog;



namespace Shared.UI.Web.ApplicationManagement.ReleaseLogDetail
{

    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {
		#region Methods

		protected override Control GetTabControl(int setId, Control updateControl)
		{
			var tabControl = ApplicationCommon.GetNewDetailTabControl();	
			
			tabControl.AddTab("ReleaseLogDetail", updateControl, String.Empty, true);

			var releaseLogControlPath = ApplicationCommon.GetControlPath("ReleaseLog", ControlType.DetailsControl);
			var releaseLogDetail = GetReleaseLogData(setId);

			var releaseLogControl = (Shared.UI.Web.ApplicationManagement.ReleaseLog.Controls.Details)Page.LoadControl(releaseLogControlPath);
			releaseLogControl.LoadTabData((int)releaseLogDetail.Rows[0].ItemArray[0]);
			tabControl.AddTab("Release Log", releaseLogControl, String.Empty);

			tabControl.Setup("ReleaseLogDetailUpdateView");
			return tabControl;			
		}		

		private DataTable GetReleaseLogData(int releaseLogDetailId)
		{
			var dt = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetByReleaseLogDetail(releaseLogDetailId, SessionVariables.RequestProfile);

            var data = new ReleaseLogDataModel();
            data.ReleaseLogId = Convert.ToInt32(dt.Rows[0]["ReleaseLogId"]);

			var releaseLogdt = Framework.Components.ReleaseLog.ReleaseLogDataManager.Search(data, SessionVariables.RequestProfile);
            return releaseLogdt;
		}

		private string[] GetReleaseLogColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ReleaseLog, "DBColumns", SessionVariables.RequestProfile);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleaseLogDetail;

			GenericControlPath = ApplicationCommon.GetControlPath("ReleaseLogDetail", ControlType.GenericControl);
			PrimaryPlaceHolder = plcUpdateList;
			PrimaryEntityKey = "ReleaseLogDetail";
			BreadCrumbObject = Master.BreadCrumbObject;

			BtnUpdate = btnUpdate;
			BtnClone = btnClone;
			BtnCancel = btnCancel;
		}

		#endregion

    }
}