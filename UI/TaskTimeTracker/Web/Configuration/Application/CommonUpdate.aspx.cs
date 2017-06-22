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

namespace Shared.UI.Web.Configuration.Application
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new ApplicationDataModel();

			var UpdatedData = new List<ApplicationDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 

				ApplicationDataManager.Update(data, SessionVariables.RequestProfile);

				data = new ApplicationDataModel();

				data.ApplicationId = Convert.ToInt32(SelectedData.Rows[i][ApplicationDataModel.DataColumns.ApplicationId].ToString());

				var dt = ApplicationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new ApplicationDataModel();
			data.ApplicationId = entityKey;
			var results = ApplicationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.Application;		
				PrimaryEntityKey	= "Application";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
