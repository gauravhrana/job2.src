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

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserTitle
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new ApplicationUserTitleDataModel();

			var UpdatedData = new List<ApplicationUserTitleDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 

				ApplicationUserTitleDataManager.Update(data, SessionVariables.RequestProfile);

				data = new ApplicationUserTitleDataModel();

				data.ApplicationUserTitleId = Convert.ToInt32(SelectedData.Rows[i][ApplicationUserTitleDataModel.DataColumns.ApplicationUserTitleId].ToString());

				var dt = ApplicationUserTitleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new ApplicationUserTitleDataModel();
			data.ApplicationUserTitleId = entityKey;
			var results = ApplicationUserTitleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.ApplicationUserTitle;		
				PrimaryEntityKey	= "ApplicationUserTitle";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
