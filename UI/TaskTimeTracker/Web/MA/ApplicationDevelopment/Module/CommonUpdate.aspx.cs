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

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.Module
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new ModuleDataModel();

			var UpdatedData = new List<ModuleDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.ModuleId = Convert.ToInt32(SelectedData.Rows[i][ModuleDataModel.DataColumns.ModuleId].ToString());
				data.Name = SelectedData.Rows[i][ModuleDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ModuleDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(ModuleDataModel.DataColumns.Description) : SelectedData.Rows[i][ModuleDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ModuleDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(ModuleDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][ModuleDataModel.DataColumns.SortOrder].ToString());

				ModuleDataManager.Update(data, SessionVariables.RequestProfile);

				data = new ModuleDataModel();

				data.ModuleId = Convert.ToInt32(SelectedData.Rows[i][ModuleDataModel.DataColumns.ModuleId].ToString());

				var dt = ModuleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new ModuleDataModel();
			data.ModuleId = entityKey;
			var results = ModuleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.Module;		
				PrimaryEntityKey	= "Module";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
