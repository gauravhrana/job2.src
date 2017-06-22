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
using DataModel.Framework.Core;
using Framework.Components.Core;

namespace Shared.UI.Web.Configuration.Theme
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
				var Themedata = new ThemeDataModel();

				selectedrows = ThemeDataManager.GetDetails(Themedata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						Themedata.ThemeId = entityKey;
						var result = ThemeDataManager.GetDetails(Themedata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else
				{
					Themedata.ThemeId = SetId;
					var result = ThemeDataManager.GetDetails(Themedata, SessionVariables.RequestProfile);
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
		
		protected override void Update(Dictionary<string, string> values)
		{
			var data = new ThemeDataModel();

			// copies properties from values dictionary object to data object
			PropertyMapper.CopyProperties(data, values);

			Framework.Components.Core.ThemeDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			InlineEditingListCore = InlineEditingList;			

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Theme;
			PrimaryEntityKey = "Theme";
			BreadCrumbObject = Master.BreadCrumbObject;

			base.OnInit(e);
		}

		#endregion
	}
}