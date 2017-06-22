using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using Dapper;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using TaskTimeTracker.Components.Module.ApplicationDevelopment;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityPriority
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new FunctionalityPriorityDataModel();

			var UpdatedData = new List<FunctionalityPriorityDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.FunctionalityPriorityId = Convert.ToInt32(SelectedData.Rows[i][FunctionalityPriorityDataModel.DataColumns.FunctionalityPriorityId].ToString());
				data.Name = SelectedData.Rows[i][FunctionalityPriorityDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityPriorityDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(FunctionalityPriorityDataModel.DataColumns.Description) : SelectedData.Rows[i][FunctionalityPriorityDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityPriorityDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(FunctionalityPriorityDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][FunctionalityPriorityDataModel.DataColumns.SortOrder].ToString());

				FunctionalityPriorityDataManager.Update(data, SessionVariables.RequestProfile);

				data = new FunctionalityPriorityDataModel();

				data.FunctionalityPriorityId = Convert.ToInt32(SelectedData.Rows[i][FunctionalityPriorityDataModel.DataColumns.FunctionalityPriorityId].ToString());

				var dt = FunctionalityPriorityDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new FunctionalityPriorityDataModel();
			data.FunctionalityPriorityId = entityKey;
			var results = FunctionalityPriorityDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.FunctionalityPriority;		
				PrimaryEntityKey	= "FunctionalityPriority";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
