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
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {
		#region Methods

		protected override Control GetTabControl(int setId, Control detailsControl)
		{
			var tabControl = ApplicationCommon.GetNewDetailTabControl();

			tabControl.AddTab("Release Log", detailsControl, String.Empty, true);

			var listControl = (Shared.UI.Web.Controls.DetailsWithChildrenControl)Page.LoadControl(ApplicationCommon.DetailsWithChildrenListControl);

			tabControl.AddTab("Release Log Detail", listControl);

			listControl.Setup("ReleaseLogDetail", String.Empty, "ReleaseLogDetailId", setId, true, GetData, GetReleaseLogDetailColumns, "ReleaseLogDetail");
			listControl.SetSession("true");

			tabControl.Setup("ReleaseLogDetailsView");

			return tabControl;
		}

		private DataTable GetData(string key)
		{
			return GetReleaseLogDetailData(int.Parse(key));
		}

		private DataTable GetReleaseLogDetailData(int releaseLogId)
		{
			var dt = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetByReleaseLog(releaseLogId, SessionVariables.RequestProfile);
			var releaseLogdt = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.Search(
               ReleaseLogDetailDataModel.Empty, SessionVariables.RequestProfile);
			var resultdt = releaseLogdt.Clone();

			foreach (DataRow row in dt.Rows)
			{
				var rows = releaseLogdt.Select("ReleaseLogDetailId = " + row[ReleaseLogDetailDataModel.DataColumns.ReleaseLogDetailId]);
				resultdt.ImportRow(rows[0]);
			}

			return resultdt;
		}

		private string[] GetReleaseLogDetailColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ReleaseLogDetail, "DBColumns", SessionVariables.RequestProfile);
		}		

		#endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity      = Framework.Components.DataAccess.SystemEntity.ReleaseLog;
            PrimaryEntityKey   = "ReleaseLog";
            DetailsControlPath = ApplicationCommon.GetControlPath("ReleaseLog", ControlType.DetailsControl);
            PrimaryPlaceHolder    = oDetailsControl.PlaceHolderDetails;
            BreadCrumbObject   = Master.BreadCrumbObject;
        }

        #endregion

    }
}