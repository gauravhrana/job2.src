﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace Shared.UI.Web.Admin.SubscriberApplicationRole
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
			var data = new SubscriberApplicationRoleDataModel();
			UpdatedData = Framework.Components.Core.SubscriberApplicationRoleDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.SubscriberApplicationRoleId =
					Convert.ToInt32(SelectedData.Rows[i][SubscriberApplicationRoleDataModel.DataColumns.SubscriberApplicationRoleId].ToString());

				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

				Framework.Components.Core.SubscriberApplicationRoleDataManager.Update(data, SessionVariables.RequestProfile);
				data = new SubscriberApplicationRoleDataModel();
				
				data.SubscriberApplicationRoleId = Convert.ToInt32(SelectedData.Rows[i][SubscriberApplicationRoleDataModel.DataColumns.SubscriberApplicationRoleId].ToString());
				var dt = Framework.Components.Core.SubscriberApplicationRoleDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}				
			}
			return UpdatedData;
		}

		protected override DataTable GetEntityData(int? entityKey)
		{
			var SubscriberApplicationRoledata = new SubscriberApplicationRoleDataModel();
			SubscriberApplicationRoledata.SubscriberApplicationRoleId = entityKey;
			var results = Framework.Components.Core.SubscriberApplicationRoleDataManager.Search(SubscriberApplicationRoledata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SubscriberApplicationRole;
			PrimaryEntityKey = "SubscriberApplicationRole";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
		
	}
}