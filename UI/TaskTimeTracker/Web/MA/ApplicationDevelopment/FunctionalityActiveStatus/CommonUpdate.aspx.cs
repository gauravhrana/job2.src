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

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityActiveStatus
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new FunctionalityActiveStatusDataModel();

			var UpdatedData = new List<FunctionalityActiveStatusDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.FunctionalityActiveStatusId = Convert.ToInt32(SelectedData.Rows[i][FunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId].ToString());
				data.Name = SelectedData.Rows[i][FunctionalityActiveStatusDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityActiveStatusDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(FunctionalityActiveStatusDataModel.DataColumns.Description) : SelectedData.Rows[i][FunctionalityActiveStatusDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FunctionalityActiveStatusDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(FunctionalityActiveStatusDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][FunctionalityActiveStatusDataModel.DataColumns.SortOrder].ToString());

				FunctionalityActiveStatusDataManager.Update(data, SessionVariables.RequestProfile);

				data = new FunctionalityActiveStatusDataModel();

				data.FunctionalityActiveStatusId = Convert.ToInt32(SelectedData.Rows[i][FunctionalityActiveStatusDataModel.DataColumns.FunctionalityActiveStatusId].ToString());

				var dt = FunctionalityActiveStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new FunctionalityActiveStatusDataModel();
			data.FunctionalityActiveStatusId = entityKey;
			var results = FunctionalityActiveStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.FunctionalityActiveStatus;		
				PrimaryEntityKey	= "FunctionalityActiveStatus";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
