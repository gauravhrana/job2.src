using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using Dapper;
using Framework.Components.Audit;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.ApplicationUser;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationRole
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new ApplicationRoleDataModel();

			var UpdatedData = new List<ApplicationRoleDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 

				ApplicationRoleDataManager.Update(data, SessionVariables.RequestProfile);

				data = new ApplicationRoleDataModel();

				data.ApplicationRoleId = Convert.ToInt32(SelectedData.Rows[i][ApplicationRoleDataModel.DataColumns.ApplicationRoleId].ToString());

				var dt = ApplicationRoleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new ApplicationRoleDataModel();
			data.ApplicationRoleId = entityKey;
			var results = ApplicationRoleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.ApplicationRole;		
				PrimaryEntityKey	= "ApplicationRole";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
