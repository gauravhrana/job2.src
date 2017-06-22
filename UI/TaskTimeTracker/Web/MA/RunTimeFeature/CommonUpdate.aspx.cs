using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using Dapper;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.Feature;
using TaskTimeTracker.Components.BusinessLayer.Feature;

namespace ApplicationContainer.UI.Web.RunTimeFeature
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new RunTimeFeatureDataModel();

			var UpdatedData = new List<RunTimeFeatureDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.RunTimeFeatureId = Convert.ToInt32(SelectedData.Rows[i][RunTimeFeatureDataModel.DataColumns.RunTimeFeatureId].ToString());
				data.Name = SelectedData.Rows[i][RunTimeFeatureDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(RunTimeFeatureDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(RunTimeFeatureDataModel.DataColumns.Description) : SelectedData.Rows[i][RunTimeFeatureDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(RunTimeFeatureDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(RunTimeFeatureDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][RunTimeFeatureDataModel.DataColumns.SortOrder].ToString());

				RunTimeFeatureDataManager.Update(data, SessionVariables.RequestProfile);

				data = new RunTimeFeatureDataModel();

				data.RunTimeFeatureId = Convert.ToInt32(SelectedData.Rows[i][RunTimeFeatureDataModel.DataColumns.RunTimeFeatureId].ToString());

				var dt = RunTimeFeatureDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new RunTimeFeatureDataModel();
			data.RunTimeFeatureId = entityKey;
			var results = RunTimeFeatureDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.RunTimeFeature;		
				PrimaryEntityKey	= "RunTimeFeature";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
