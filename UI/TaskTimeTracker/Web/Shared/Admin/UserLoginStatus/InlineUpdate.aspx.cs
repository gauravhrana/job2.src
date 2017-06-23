using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.Framework.LogAndTrace;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;
using Dapper;

namespace Shared.UI.Web.Admin.UserLoginStatus
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
				var selectedrows = new List<UserLoginStatusDataModel>();
				var userLoginStatusdata = new UserLoginStatusDataModel();

				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						userLoginStatusdata.UserLoginStatusId = entityKey;
						var result = Framework.Components.LogAndTrace.UserLoginStatusDataManager.GetDetails(userLoginStatusdata, SessionVariables.RequestProfile);
						selectedrows.Add(result);
					}
				}
				else if (SetId != 0)
				{
					userLoginStatusdata.UserLoginStatusId = SetId;
					var result = Framework.Components.LogAndTrace.UserLoginStatusDataManager.GetDetails(userLoginStatusdata, SessionVariables.RequestProfile);
                    selectedrows.Add(result);

				}
				return selectedrows.ToDataTable();
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
			return null;
		}

		protected override string[] GetColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UserLoginStatus, "DBColumns", SessionVariables.RequestProfile);
		}

		protected override void Update(Dictionary<string, string> values)
		{
			var data = new UserLoginStatusDataModel();
			data.UserLoginStatusId = int.Parse(values[UserLoginStatusDataModel.DataColumns.UserLoginStatusId].ToString());
			data.Name = values[StandardDataModel.StandardDataColumns.Name].ToString();
			data.Description = values[StandardDataModel.StandardDataColumns.Description].ToString();
			data.SortOrder = int.Parse(values[StandardDataModel.StandardDataColumns.SortOrder].ToString());
			Framework.Components.LogAndTrace.UserLoginStatusDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			InlineEditingListCore = InlineEditingList;
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserLoginStatus;
			PrimaryEntityKey = "UserLoginStatus";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
		
	}
}