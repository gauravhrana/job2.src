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

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityStatus
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new FunctionalityStatusDataModel();

			var UpdatedData = new List<FunctionalityStatusDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.FunctionalityStatusId = Convert.ToInt32(SelectedData.Rows[i][FunctionalityStatusDataModel.DataColumns.FunctionalityStatusId].ToString());
				data.Application = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityStatusDataModel.DataColumns.Application)) ? CheckAndGetRepeaterTextBoxValue(FunctionalityStatusDataModel.DataColumns.Application) : SelectedData.Rows[i][FunctionalityStatusDataModel.DataColumns.Application].ToString();
				data.Name = SelectedData.Rows[i][FunctionalityStatusDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityStatusDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(FunctionalityStatusDataModel.DataColumns.Description) : SelectedData.Rows[i][FunctionalityStatusDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityStatusDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(FunctionalityStatusDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][FunctionalityStatusDataModel.DataColumns.SortOrder].ToString());

				FunctionalityStatusDataManager.Update(data, SessionVariables.RequestProfile);

				data = new FunctionalityStatusDataModel();

				data.FunctionalityStatusId = Convert.ToInt32(SelectedData.Rows[i][FunctionalityStatusDataModel.DataColumns.FunctionalityStatusId].ToString());

				var dt = FunctionalityStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new FunctionalityStatusDataModel();
			data.FunctionalityStatusId = entityKey;
			var results = FunctionalityStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.FunctionalityStatus;		
				PrimaryEntityKey	= "FunctionalityStatus";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
