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

namespace Shared.UI.Web.Configuration.ApplicationRouteParameter
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()	
		{
			var UpdatedData = new List<ApplicationRouteParameterDataModel>();
			var data = new ApplicationRouteParameterDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ApplicationRouteParameterId =
					Convert.ToInt32(SelectedData.Rows[i][ApplicationRouteParameterDataModel.DataColumns.ApplicationRouteParameterId].ToString());
				data.ApplicationRouteId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRouteParameterDataModel.DataColumns.ApplicationRouteId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(ApplicationRouteParameterDataModel.DataColumns.ApplicationRouteId).ToString())
					: int.Parse(SelectedData.Rows[i][ApplicationRouteParameterDataModel.DataColumns.ApplicationRouteId].ToString());

				data.ParameterName =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRouteParameterDataModel.DataColumns.ParameterName))
					? CheckAndGetRepeaterTextBoxValue(ApplicationRouteParameterDataModel.DataColumns.ParameterName)
					: SelectedData.Rows[i][ApplicationRouteParameterDataModel.DataColumns.ParameterName].ToString();

				data.ParameterValue =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRouteParameterDataModel.DataColumns.ParameterValue))
					? CheckAndGetRepeaterTextBoxValue(ApplicationRouteParameterDataModel.DataColumns.ParameterValue)
					: SelectedData.Rows[i][ApplicationRouteParameterDataModel.DataColumns.ParameterValue].ToString();

				Framework.Components.Core.ApplicationRouteParameterDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ApplicationRouteParameterDataModel();
				data.ApplicationRouteParameterId = Convert.ToInt32(SelectedData.Rows[i][ApplicationRouteParameterDataModel.DataColumns.ApplicationRouteParameterId].ToString());
				var dt = Framework.Components.Core.ApplicationRouteParameterDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
                }
            }
            return UpdatedData.ToDataTable();
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var applicationRouteParameterdata = new ApplicationRouteParameterDataModel();
            applicationRouteParameterdata.ApplicationRouteParameterId = entityKey;
			var results = Framework.Components.Core.ApplicationRouteParameterDataManager.GetEntityDetails(applicationRouteParameterdata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationRouteParameter;
            PrimaryEntityKey = "ApplicationRouteParameter";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}