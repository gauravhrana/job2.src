using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using Dapper;
using DataModel.Framework.Audit;

namespace Shared.UI.Web.Admin.Trace
{
    public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
            var UpdatedData = new List<TraceDataModel>();
			var data = new DataModel.Framework.Audit.TraceDataModel();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.TraceId =
					Convert.ToInt32(SelectedData.Rows[i][DataModel.Framework.Audit.TraceDataModel.DataColumns.TraceId].ToString());
				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				Framework.Components.Audit.TraceDataManager.Update(data, SessionVariables.RequestProfile);
				data = new DataModel.Framework.Audit.TraceDataModel();
				data.TraceId = Convert.ToInt32(SelectedData.Rows[i][DataModel.Framework.Audit.TraceDataModel.DataColumns.TraceId].ToString());
				var dt = Framework.Components.Audit.TraceDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
				
			}
			return UpdatedData.ToDataTable();
		}
	
		protected override DataTable GetEntityData(int? entityKey)
		{
			var tracedata = new DataModel.Framework.Audit.TraceDataModel();
			tracedata.TraceId = entityKey;
			var results = Framework.Components.Audit.TraceDataManager.GetEntityDetails(tracedata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
			return results.ToDataTable();
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Trace;
			PrimaryEntityKey = "Trace";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
       
    }
}