using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.Scheduling.VacationPlan
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();

			var data = new VacationPlanDataModel();
            UpdatedData = VacationPlanDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.VacationPlanId =
		            Convert.ToInt32(SelectedData.Rows[i][VacationPlanDataModel.DataColumns.VacationPlanId].ToString());
		        data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
		        data.Description =
		            !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
		            ? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
		            : SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

		        data.SortOrder =
		            !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
		            ? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
		            : int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

		        data.ApplicationId =
		            !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.ApplicationId))
		            ? int.Parse(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.ApplicationId).ToString())
		            : int.Parse(SelectedData.Rows[i][BaseDataModel.BaseDataColumns.ApplicationId].ToString());

		        data.ApplicationUserId =
		            !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(VacationPlanDataModel.DataColumns.ApplicationUserId))
		            ? int.Parse(CheckAndGetRepeaterTextBoxValue(VacationPlanDataModel.DataColumns.ApplicationUserId).ToString())
		            : int.Parse(SelectedData.Rows[i][VacationPlanDataModel.DataColumns.ApplicationUserId].ToString());

		        data.StartDate =
		            !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(VacationPlanDataModel.DataColumns.StartDate))
		            ? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(VacationPlanDataModel.DataColumns.StartDate).ToString())
		            : DateTime.Parse(SelectedData.Rows[i][VacationPlanDataModel.DataColumns.StartDate].ToString());

		        data.EndDate =
		            !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(VacationPlanDataModel.DataColumns.EndDate))
		            ? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(VacationPlanDataModel.DataColumns.EndDate).ToString())
		            : DateTime.Parse(SelectedData.Rows[i][VacationPlanDataModel.DataColumns.EndDate].ToString());

				data.CreatedByAuditId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.CreatedByAuditId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.CreatedByAuditId).ToString())
					: int.Parse(SelectedData.Rows[i][BaseDataModel.BaseDataColumns.CreatedByAuditId].ToString());

				data.ModifiedByAuditId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.ModifiedByAuditId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.ModifiedByAuditId).ToString())
					: int.Parse(SelectedData.Rows[i][BaseDataModel.BaseDataColumns.ModifiedByAuditId].ToString());

				data.CreatedDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.CreatedDate))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.CreatedDate).ToString())
					: DateTime.Parse(SelectedData.Rows[i][BaseDataModel.BaseDataColumns.CreatedDate].ToString());

				data.ModifiedDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.ModifiedDate))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(BaseDataModel.BaseDataColumns.ModifiedDate).ToString())
					: DateTime.Parse(SelectedData.Rows[i][BaseDataModel.BaseDataColumns.ModifiedDate].ToString());

                VacationPlanDataManager.Update(data, SessionVariables.RequestProfile);
				data = new VacationPlanDataModel();
				data.VacationPlanId = Convert.ToInt32(SelectedData.Rows[i][VacationPlanDataModel.DataColumns.VacationPlanId].ToString());
                var dt = VacationPlanDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}
			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var vacationPlandata = new VacationPlanDataModel();
			vacationPlandata.VacationPlanId = entityKey;
            var results = VacationPlanDataManager.Search(vacationPlandata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = SystemEntity.VacationPlan;
			PrimaryEntityKey = "VacationPlan";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion

	}
}