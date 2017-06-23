﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using DataModel.Framework.DataAccess;
using Dapper;

namespace ApplicationContainer.UI.Web.Report
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new List<ReportDataModel>();
            var data = new ReportDataModel();

            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.ReportId =
                    Convert.ToInt32(SelectedData.Rows[i][ReportDataModel.DataColumns.ReportId].ToString());
                data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.ApplicationId =
					Convert.ToInt32(SelectedData.Rows[i][BaseDataModel.BaseDataColumns.ApplicationId].ToString());
                data.Title = SelectedData.Rows[i][ReportDataModel.DataColumns.Title].ToString();
                data.Description =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
                    ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
                    : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

                data.SortOrder =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
                    : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				data.CreatedDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.CreatedDate))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.CreatedDate).ToString())
					: DateTime.Parse(SelectedData.Rows[i][BaseDataModel.BaseDataColumns.CreatedDate].ToString());

				Framework.Components.Core.ReportDataManager.Update(data, SessionVariables.RequestProfile);
                data = new ReportDataModel();
                data.ReportId = Convert.ToInt32(SelectedData.Rows[i][ReportDataModel.DataColumns.ReportId].ToString());
				var dt = Framework.Components.Core.ReportDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

                if (dt.Count == 1)
                {
                    UpdatedData.Add(dt[0]);
                }
            }
            return UpdatedData.ToDataTable();
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var reportdata = new ReportDataModel();
            reportdata.ReportId = entityKey;
			var results = Framework.Components.Core.ReportDataManager.GetEntityDetails(reportdata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
            return results.ToDataTable();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Report;
            PrimaryEntityKey = "Report";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}