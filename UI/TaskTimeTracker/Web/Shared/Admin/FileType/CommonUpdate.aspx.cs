using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using Dapper;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.Import;
using Framework.Components.Import;

namespace Shared.UI.Web.FileType
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new FileTypeDataModel();

			var UpdatedData = new List<FileTypeDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.FileTypeId = Convert.ToInt32(SelectedData.Rows[i][FileTypeDataModel.DataColumns.FileTypeId].ToString());
				data.Name = SelectedData.Rows[i][FileTypeDataModel.DataColumns.Name].ToString();
				data.Description = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FileTypeDataModel.DataColumns.Description)) ? CheckAndGetRepeaterTextBoxValue(FileTypeDataModel.DataColumns.Description) : SelectedData.Rows[i][FileTypeDataModel.DataColumns.Description].ToString();
				data.SortOrder = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(FileTypeDataModel.DataColumns.SortOrder)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(FileTypeDataModel.DataColumns.SortOrder)) : int.Parse(SelectedData.Rows[i][FileTypeDataModel.DataColumns.SortOrder].ToString());

				FileTypeDataManager.Update(data, SessionVariables.RequestProfile);

				data = new FileTypeDataModel();

				data.FileTypeId = Convert.ToInt32(SelectedData.Rows[i][FileTypeDataModel.DataColumns.FileTypeId].ToString());

				var dt = FileTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new FileTypeDataModel();
			data.FileTypeId = entityKey;
			var results = FileTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.FileType;		
				PrimaryEntityKey	= "FileType";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
