using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.Framework.ReleaseLog;



namespace Shared.UI.Web.ApplicationManagement.ReleaseLogDetail
{

    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {
		#region Methods

		protected override Control GetTabControl(int setId, Control detailsControl)
		{
			var tabControl = ApplicationCommon.GetNewDetailTabControl();

			tabControl.AddTab("Release Log Detail", detailsControl, String.Empty, true);

			var releaseLogControlPath = ApplicationCommon.GetControlPath("ReleaseLog", ControlType.DetailsControl);
			var releaseLogDetail = GetReleaseLogData(setId);
			
			var releaseLogControl = (Shared.UI.Web.ApplicationManagement.ReleaseLog.Controls.Details)Page.LoadControl(releaseLogControlPath);
			releaseLogControl.LoadTabData((int)releaseLogDetail.Rows[0].ItemArray[0]);
			tabControl.AddTab("Release Log", releaseLogControl, String.Empty);			

			tabControl.Setup("ReleaseLogDetailDetailsView");

			return tabControl;
		}

		private DataTable GetReleaseLogData(int releaseLogDetailId)
		{
			var dt = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetByReleaseLogDetail(releaseLogDetailId, SessionVariables.RequestProfile);
			var releaseLogdt = Framework.Components.ReleaseLog.ReleaseLogDataManager.Search(
                ReleaseLogDataModel.Empty, SessionVariables.RequestProfile);
			var resultdt = releaseLogdt.Clone();

			foreach (DataRow row in dt.Rows)
			{
				var rows = releaseLogdt.Select("ReleaseLogId = " + row[ReleaseLogDataModel.DataColumns.ReleaseLogId]);
				resultdt.ImportRow(rows[0]);
			}

			return resultdt;
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
			PrimaryEntityKey = "ReleaseLogDetail";
			DetailsControlPath = ApplicationCommon.GetControlPath("ReleaseLogDetail", ControlType.DetailsControl);
			PrimaryPlaceHolder = plcDetailsList;
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

    }
}