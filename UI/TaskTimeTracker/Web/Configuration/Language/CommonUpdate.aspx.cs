using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using Dapper;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.Core;
using Framework.Components.Core;

namespace Shared.UI.Web.Configuration.Language
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new LanguageDataModel();

			var UpdatedData = new List<LanguageDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.LanguageId = Convert.ToInt32(SelectedData.Rows[i][LanguageDataModel.DataColumns.LanguageId].ToString());
				data.Name = SelectedData.Rows[i][LanguageDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(LanguageDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(LanguageDataModel.DataColumns.Description) : SelectedData.Rows[i][LanguageDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(LanguageDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(LanguageDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][LanguageDataModel.DataColumns.SortOrder].ToString());

				LanguageDataManager.Update(data, SessionVariables.RequestProfile);

				data = new LanguageDataModel();

				data.LanguageId = Convert.ToInt32(SelectedData.Rows[i][LanguageDataModel.DataColumns.LanguageId].ToString());

				var dt = LanguageDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new LanguageDataModel();
			data.LanguageId = entityKey;
			var results = LanguageDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.Language;		
				PrimaryEntityKey	= "Language";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
