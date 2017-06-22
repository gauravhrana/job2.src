using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.FieldConfigurationModeXApplicationRole
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
				var FieldConfigurationModeXApplicationRoledata = new FieldConfigurationModeXApplicationRoleDataModel();

				selectedrows = FieldConfigurationModeXApplicationRoleDataManager.GetDetails(FieldConfigurationModeXApplicationRoledata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						FieldConfigurationModeXApplicationRoledata.FieldConfigurationModeXApplicationRoleId = entityKey;
						var result = FieldConfigurationModeXApplicationRoleDataManager.GetDetails(FieldConfigurationModeXApplicationRoledata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else 
				{
					FieldConfigurationModeXApplicationRoledata.FieldConfigurationModeXApplicationRoleId = SetId;
					var result = FieldConfigurationModeXApplicationRoleDataManager.GetDetails(FieldConfigurationModeXApplicationRoledata, SessionVariables.RequestProfile);
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

		protected override string[] GetColumns()
		{

			return FieldConfigurationUtility.GetEntityColumns("DBColumns", Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeXApplicationRole, SessionVariables.RequestProfile);
		}

		protected override void Update(Dictionary<string, string> values)
		{
			var data = new FieldConfigurationModeXApplicationRoleDataModel();
			data.FieldConfigurationModeXApplicationRoleId = int.Parse(values[FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeXApplicationRoleId].ToString());
			data.ApplicationRoleId = int.Parse(values[FieldConfigurationModeXApplicationRoleDataModel.DataColumns.ApplicationRoleId].ToString());
			data.FieldConfigurationModeId = int.Parse(values[FieldConfigurationModeXApplicationRoleDataModel.DataColumns.FieldConfigurationModeId].ToString());
			FieldConfigurationModeXApplicationRoleDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			InlineEditingListCore = InlineEditingList;
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeXApplicationRole;
			PrimaryEntityKey = "FieldConfigurationModeXApplicationRole";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}