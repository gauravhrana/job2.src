using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace Shared.Configuration.ApplicationRoute
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()       
   		{
			var UpdatedData = new List<ApplicationRouteDataModel>();
			var data = new ApplicationRouteDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ApplicationRouteId =
					Convert.ToInt32(SelectedData.Rows[i][ApplicationRouteDataModel.DataColumns.ApplicationRouteId].ToString());
				data.RouteName =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRouteDataModel.DataColumns.RouteName))
					? CheckAndGetRepeaterTextBoxValue(ApplicationRouteDataModel.DataColumns.RouteName)
					: SelectedData.Rows[i][ApplicationRouteDataModel.DataColumns.RouteName].ToString();
				data.EntityName =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRouteDataModel.DataColumns.EntityName))
					? CheckAndGetRepeaterTextBoxValue(ApplicationRouteDataModel.DataColumns.EntityName)
					: SelectedData.Rows[i][ApplicationRouteDataModel.DataColumns.EntityName].ToString();
				data.ProposedRoute =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRouteDataModel.DataColumns.ProposedRoute))
					? CheckAndGetRepeaterTextBoxValue(ApplicationRouteDataModel.DataColumns.ProposedRoute)
					: SelectedData.Rows[i][ApplicationRouteDataModel.DataColumns.ProposedRoute].ToString();
				data.RelativeRoute =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRouteDataModel.DataColumns.RelativeRoute))
					? CheckAndGetRepeaterTextBoxValue(ApplicationRouteDataModel.DataColumns.RelativeRoute)
					: SelectedData.Rows[i][ApplicationRouteDataModel.DataColumns.RelativeRoute].ToString();

				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRouteDataModel.DataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(ApplicationRouteDataModel.DataColumns.Description)
					: SelectedData.Rows[i][ApplicationRouteDataModel.DataColumns.Description].ToString();

				Framework.Components.Core.ApplicationRouteDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ApplicationRouteDataModel();
				data.ApplicationRouteId = Convert.ToInt32(SelectedData.Rows[i][ApplicationRouteDataModel.DataColumns.ApplicationRouteId].ToString());
				var dt = Framework.Components.Core.ApplicationRouteDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
                }
            }
            return UpdatedData.ToDataTable();
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var applicationRoutedata = new ApplicationRouteDataModel();
            applicationRoutedata.ApplicationRouteId = entityKey;
			var results = Framework.Components.Core.ApplicationRouteDataManager.GetEntityDetails(applicationRoutedata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationRoute;
            PrimaryEntityKey = "ApplicationRoute";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}