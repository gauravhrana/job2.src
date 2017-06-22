using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using Dapper;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;

namespace Shared.UI.Web.Configuration.ApplicationMode
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new ApplicationModeDataModel();

			var UpdatedData = new List<ApplicationModeDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.ApplicationModeId = Convert.ToInt32(SelectedData.Rows[i][ApplicationModeDataModel.DataColumns.ApplicationModeId].ToString());
				data.Name = SelectedData.Rows[i][ApplicationModeDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationModeDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(ApplicationModeDataModel.DataColumns.Description) : SelectedData.Rows[i][ApplicationModeDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationModeDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(ApplicationModeDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][ApplicationModeDataModel.DataColumns.SortOrder].ToString());

				ApplicationModeDataManager.Update(data, SessionVariables.RequestProfile);

				data = new ApplicationModeDataModel();

				data.ApplicationModeId = Convert.ToInt32(SelectedData.Rows[i][ApplicationModeDataModel.DataColumns.ApplicationModeId].ToString());

				var dt = ApplicationModeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new ApplicationModeDataModel();
			data.ApplicationModeId = entityKey;
			var results = ApplicationModeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.ApplicationMode;		
				PrimaryEntityKey	= "ApplicationMode";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
