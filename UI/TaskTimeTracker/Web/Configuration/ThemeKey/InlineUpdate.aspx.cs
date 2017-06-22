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
using Dapper;

namespace Shared.UI.Web.Configuration.ThemeKey
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

				var selectedrows = new List<ThemeKeyDataModel>();
				var ThemeKeydata = new ThemeKeyDataModel();

				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						ThemeKeydata.ThemeKeyId = entityKey;
						var result = ThemeKeyDataManager.GetDetails(ThemeKeydata, SessionVariables.RequestProfile);
                        selectedrows.Add(result);
					}
				}
				else
				{
					ThemeKeydata.ThemeKeyId = SetId;
					var result = ThemeKeyDataManager.GetDetails(ThemeKeydata, SessionVariables.RequestProfile);
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

		protected override void Update(Dictionary<string, string> values)
		{
			var data = new ThemeKeyDataModel();

			// copies properties from values dictionary object to data object
			PropertyMapper.CopyProperties(data, values);

			Framework.Components.Core.ThemeKeyDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			InlineEditingListCore = InlineEditingList;
			
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ThemeKey;
			PrimaryEntityKey = "ThemeKey";
			BreadCrumbObject = Master.BreadCrumbObject;

			base.OnInit(e);
		}

		#endregion
	}
}