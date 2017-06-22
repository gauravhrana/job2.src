using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.ConnectionString
{
	public partial class CommonUpdate : Framework.UI.Web.BaseClasses.PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new ConnectionStringDataModel();
			UpdatedData = Framework.Components.Core.ConnectionStringDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{				
				data.ConnectionStringId =
					Convert.ToInt32(SelectedData.Rows[i][ConnectionStringDataModel.DataColumns.ConnectionStringId].ToString());

				data.Name = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ConnectionStringDataModel.DataColumns.Name))
					? CheckAndGetRepeaterTextBoxValue(ConnectionStringDataModel.DataColumns.Name).ToString()
					: SelectedData.Rows[i][ConnectionStringDataModel.DataColumns.Name].ToString();

				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ConnectionStringDataModel.DataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(ConnectionStringDataModel.DataColumns.Description).ToString()
					: SelectedData.Rows[i][ConnectionStringDataModel.DataColumns.Description].ToString();

				data.DataSource = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ConnectionStringDataModel.DataColumns.DataSource))
					 ? CheckAndGetRepeaterTextBoxValue(ConnectionStringDataModel.DataColumns.DataSource).ToString()
					 : SelectedData.Rows[i][ConnectionStringDataModel.DataColumns.DataSource].ToString();

				data.InitialCatalog = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ConnectionStringDataModel.DataColumns.InitialCatalog))
					 ? CheckAndGetRepeaterTextBoxValue(ConnectionStringDataModel.DataColumns.InitialCatalog).ToString()
					 : SelectedData.Rows[i][ConnectionStringDataModel.DataColumns.InitialCatalog].ToString();

				data.UserName = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ConnectionStringDataModel.DataColumns.UserName))
					 ? CheckAndGetRepeaterTextBoxValue(ConnectionStringDataModel.DataColumns.UserName).ToString()
					 : SelectedData.Rows[i][ConnectionStringDataModel.DataColumns.UserName].ToString();

				data.Password = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ConnectionStringDataModel.DataColumns.Password))
					 ? CheckAndGetRepeaterTextBoxValue(ConnectionStringDataModel.DataColumns.Password).ToString()
					 : SelectedData.Rows[i][ConnectionStringDataModel.DataColumns.Password].ToString();

				data.ProviderName = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ConnectionStringDataModel.DataColumns.ProviderName))
					 ? CheckAndGetRepeaterTextBoxValue(ConnectionStringDataModel.DataColumns.ProviderName).ToString()
					 : SelectedData.Rows[i][ConnectionStringDataModel.DataColumns.ProviderName].ToString();

				Framework.Components.Core.ConnectionStringDataManager.Update(data, SessionVariables.RequestProfile);
				data = new ConnectionStringDataModel();
				data.ConnectionStringId = Convert.ToInt32(SelectedData.Rows[i][ConnectionStringDataModel.DataColumns.ConnectionStringId].ToString());
				var dt = Framework.Components.Core.ConnectionStringDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}
			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var connectionStringdata = new ConnectionStringDataModel();
			connectionStringdata.ConnectionStringId = entityKey;
			var results = Framework.Components.Core.ConnectionStringDataManager.Search(connectionStringdata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DynamicUpdatePanelCore	= DynamicUpdatePanel;
			PrimaryEntity			= Framework.Components.DataAccess.SystemEntity.ConnectionString;
			PrimaryEntityKey		= "ConnectionString";
			BreadCrumbObject		= Master.BreadCrumbObject;
		}

		#endregion		
		
	}
}