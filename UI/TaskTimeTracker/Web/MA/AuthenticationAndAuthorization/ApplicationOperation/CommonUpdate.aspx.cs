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

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationOperation
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new ApplicationOperationDataModel();

			var UpdatedData = new List<ApplicationOperationDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 

				ApplicationOperationDataManager.Update(data, SessionVariables.RequestProfile);

				data = new ApplicationOperationDataModel();

				data.ApplicationOperationId = Convert.ToInt32(SelectedData.Rows[i][ApplicationOperationDataModel.DataColumns.ApplicationOperationId].ToString());

				var dt = ApplicationOperationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new ApplicationOperationDataModel();
			data.ApplicationOperationId = entityKey;
			var results = ApplicationOperationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.ApplicationOperation;		
				PrimaryEntityKey	= "ApplicationOperation";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
