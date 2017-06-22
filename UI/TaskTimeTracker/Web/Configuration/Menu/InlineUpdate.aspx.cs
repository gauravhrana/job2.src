using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Dapper;

namespace Shared.UI.Web.Configuration.Menu
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

				var selectedrows = new List<MenuDataModel>();
				var menudata = new MenuDataModel();

				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						menudata.MenuId = entityKey;
						var result = MenuDataManager.GetDetails(menudata, SessionVariables.RequestProfile);
                        selectedrows.Add(result);
					}
				}
				else
				{
					menudata.MenuId = SetId;
					var result = MenuDataManager.GetDetails(menudata, SessionVariables.RequestProfile);
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
			return FieldConfigurationUtility.GetEntityColumns(InlineUpdateColumnsModeId.ToString(), Framework.Components.DataAccess.SystemEntity.Menu, SessionVariables.RequestProfile);
		}

		protected override void Update(Dictionary<string, string> values)
		{
			// set data
			var data = new MenuDataModel();

			// copies properties from values dictionary object to Menu data object
			PropertyMapper.CopyProperties(data, values);

			// save datga
			MenuDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			InlineEditingListCore = InlineEditingList;
			base.OnInit(e);

			PrimaryEntity         = Framework.Components.DataAccess.SystemEntity.Menu;
			PrimaryEntityKey      = "Menu";
			BreadCrumbObject      = Master.BreadCrumbObject;
		}

		#endregion

	}
}