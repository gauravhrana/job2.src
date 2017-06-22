using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Framework.Components.Core;
using Framework.Components.DataAccess;


namespace Shared.UI.Web.SystemIntegrity.QuickPaginationRun
{
	public partial class InlineUpdate : PageInlineUpdate
	{

		#region Methods

		protected override DataTable GetData()
		{
			try
			{
				SuperKey = ApplicationCommon.GetSuperKey();
				SetId = ApplicationCommon.GetSetId();

				var selectedrows = new DataTable();
				var quickPaginationRundata = new QuickPaginationRunDataModel();

				selectedrows = Framework.Components.Core.QuickPaginationRunDatatManager.GetDetails(quickPaginationRundata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						quickPaginationRundata.QuickPaginationRunId = entityKey;
						var result = QuickPaginationRunDatatManager.GetDetails(quickPaginationRundata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else
				{
					quickPaginationRundata.QuickPaginationRunId = SetId;
					var result = QuickPaginationRunDatatManager.GetDetails(quickPaginationRundata, SessionVariables.RequestProfile);
					selectedrows.ImportRow(result.Rows[0]);

				}
				return selectedrows;
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}

			return null;
		}

		protected override void Update(Dictionary<string, string> values)
		{
			var data = new QuickPaginationRunDataModel();

			// copies properties from values dictionary object to data object
			PropertyMapper.CopyProperties(data, values);

			QuickPaginationRunDatatManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			PrimaryEntity = SystemEntity.QuickPaginationRun;
			PrimaryEntityKey = "QuickPaginationRun";

			InlineEditingListCore = InlineEditingList;
			BreadCrumbObject = Master.BreadCrumbObject;

			base.OnInit(e);
		}

		#endregion
	}
}