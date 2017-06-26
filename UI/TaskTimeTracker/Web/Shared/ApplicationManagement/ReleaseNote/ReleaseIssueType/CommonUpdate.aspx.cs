﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace Shared.UI.Web.ApplicationManagement.ReleaseIssueType
{
    public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new List<ReleaseIssueTypeDataModel>();
			var data = new ReleaseIssueTypeDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.ReleaseIssueTypeId =
					Convert.ToInt32(SelectedData.Rows[i][ReleaseIssueTypeDataModel.DataColumns.ReleaseIssueTypeId].ToString());
				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				Framework.Components.ReleaseLog.ReleaseIssueTypeDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ReleaseIssueTypeDataModel();
				data.ReleaseIssueTypeId = Convert.ToInt32(SelectedData.Rows[i][ReleaseIssueTypeDataModel.DataColumns.ReleaseIssueTypeId].ToString());
				var dt = Framework.Components.ReleaseLog.ReleaseIssueTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
				
			}
			return UpdatedData.ToDataTable();
		}
		
		protected override DataTable GetEntityData(int? entityKey)
		{
			var releaseIssueTypedata = new ReleaseIssueTypeDataModel();
			releaseIssueTypedata.ReleaseIssueTypeId = entityKey;
			var results = Framework.Components.ReleaseLog.ReleaseIssueTypeDataManager.GetEntityDetails(releaseIssueTypedata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
			return results.ToDataTable();
		}

		#endregion	

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleaseIssueType;
			PrimaryEntityKey = "ReleaseIssueType";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
    }
}