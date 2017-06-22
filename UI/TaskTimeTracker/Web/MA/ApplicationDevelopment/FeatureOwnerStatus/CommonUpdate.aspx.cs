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

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FeatureOwnerStatus
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new FeatureOwnerStatusDataModel();

			var UpdatedData = new List<FeatureOwnerStatusDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.FeatureOwnerStatusId = Convert.ToInt32(SelectedData.Rows[i][FeatureOwnerStatusDataModel.DataColumns.FeatureOwnerStatusId].ToString());
				data.Name = SelectedData.Rows[i][FeatureOwnerStatusDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FeatureOwnerStatusDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(FeatureOwnerStatusDataModel.DataColumns.Description) : SelectedData.Rows[i][FeatureOwnerStatusDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FeatureOwnerStatusDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(FeatureOwnerStatusDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][FeatureOwnerStatusDataModel.DataColumns.SortOrder].ToString());

				FeatureOwnerStatusDataManager.Update(data, SessionVariables.RequestProfile);

				data = new FeatureOwnerStatusDataModel();

				data.FeatureOwnerStatusId = Convert.ToInt32(SelectedData.Rows[i][FeatureOwnerStatusDataModel.DataColumns.FeatureOwnerStatusId].ToString());

				var dt = FeatureOwnerStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new FeatureOwnerStatusDataModel();
			data.FeatureOwnerStatusId = entityKey;
			var results = FeatureOwnerStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.FeatureOwnerStatus;		
				PrimaryEntityKey	= "FeatureOwnerStatus";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
