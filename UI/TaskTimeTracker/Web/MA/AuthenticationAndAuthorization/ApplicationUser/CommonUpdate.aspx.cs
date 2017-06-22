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

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUser
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new ApplicationUserDataModel();

			var UpdatedData = new List<ApplicationUserDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 

				ApplicationUserDataManager.Update(data, SessionVariables.RequestProfile);

				data = new ApplicationUserDataModel();

				data.ApplicationUserId = Convert.ToInt32(SelectedData.Rows[i][ApplicationUserDataModel.DataColumns.ApplicationUserId].ToString());

				var dt = ApplicationUserDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new ApplicationUserDataModel();
			data.ApplicationUserId = entityKey;
			var results = ApplicationUserDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.ApplicationUser;		
				PrimaryEntityKey	= "ApplicationUser";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
