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

namespace Shared.UI.Web.Admin.ApplicationRelation
{

	public partial class CommonUpdate : PageCommonUpdate
	{ 
		#region Methods

		protected override DataTable UpdateData()
		{ 
			var data = new ApplicationRelationDataModel();

			var UpdatedData = new List<ApplicationRelationDataModel>();

			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{ 
				data.ApplicationRelationId = Convert.ToInt32(SelectedData.Rows[i][ApplicationRelationDataModel.DataColumns.ApplicationRelationId].ToString());
				data.PublisherApplicationId = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.PublisherApplicationId)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.PublisherApplicationId)) : int.Parse(SelectedData.Rows[i][ApplicationRelationDataModel.DataColumns.PublisherApplicationId].ToString());
				data.SubscriberApplicationId = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SubscriberApplicationId)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SubscriberApplicationId)) : int.Parse(SelectedData.Rows[i][ApplicationRelationDataModel.DataColumns.SubscriberApplicationId].ToString());
				data.SystemEntityTypeId = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SystemEntityTypeId)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SystemEntityTypeId)) : int.Parse(SelectedData.Rows[i][ApplicationRelationDataModel.DataColumns.SystemEntityTypeId].ToString());
				data.SubscriberApplicationRoleId = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SubscriberApplicationRoleId)) ? int.Parse(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SubscriberApplicationRoleId)) : int.Parse(SelectedData.Rows[i][ApplicationRelationDataModel.DataColumns.SubscriberApplicationRoleId].ToString());
				data.PublisherApplication = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.PublisherApplication)) ? CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.PublisherApplication) : SelectedData.Rows[i][ApplicationRelationDataModel.DataColumns.PublisherApplication].ToString();
				data.SubscriberApplication = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SubscriberApplication)) ? CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SubscriberApplication) : SelectedData.Rows[i][ApplicationRelationDataModel.DataColumns.SubscriberApplication].ToString();
				data.SystemEntityType = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SystemEntityType)) ? CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SystemEntityType) : SelectedData.Rows[i][ApplicationRelationDataModel.DataColumns.SystemEntityType].ToString();
				data.SubscriberApplicationRole = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SubscriberApplicationRole)) ? CheckAndGetRepeaterTextBoxValue(ApplicationRelationDataModel.DataColumns.SubscriberApplicationRole) : SelectedData.Rows[i][ApplicationRelationDataModel.DataColumns.SubscriberApplicationRole].ToString();

				ApplicationRelationDataManager.Update(data, SessionVariables.RequestProfile);

				data = new ApplicationRelationDataModel();

				data.ApplicationRelationId = Convert.ToInt32(SelectedData.Rows[i][ApplicationRelationDataModel.DataColumns.ApplicationRelationId].ToString());

				var dt = ApplicationRelationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
				}
			}
			return UpdatedData.ToDataTable();
		} 

		protected override DataTable GetEntityData(int? entityKey)
		{
			var data = new ApplicationRelationDataModel();
			data.ApplicationRelationId = entityKey;
			var results = ApplicationRelationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
			return results.ToDataTable();
		}

		#endregion

			#region Events

			protected override void OnInit(EventArgs e)
			{
				base.OnInit(e);

				DynamicUpdatePanelCore	= DynamicUpdatePanel;
				PrimaryEntity		= SystemEntity.ApplicationRelation;		
				PrimaryEntityKey	= "ApplicationRelation";
				BreadCrumbObject	= Master.BreadCrumbObject;
			}

			#endregion

		}
	}
