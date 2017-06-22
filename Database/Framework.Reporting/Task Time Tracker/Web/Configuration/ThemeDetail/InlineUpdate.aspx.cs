using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;
using DataModel.Framework.Core;
using Framework.Components.Core;


namespace Shared.UI.Web.Configuration.ThemeDetail
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
				var ThemeDetaildata = new ThemeDetailDataModel();

				selectedrows = ThemeDetailDataManager.GetDetails(ThemeDetaildata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						ThemeDetaildata.ThemeDetailId = entityKey;
						var result = ThemeDetailDataManager.GetDetails(ThemeDetaildata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else
				{
					ThemeDetaildata.ThemeDetailId = SetId;
					var result = ThemeDetailDataManager.GetDetails(ThemeDetaildata, SessionVariables.RequestProfile);
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
			var data = new ThemeDetailDataModel();
			
			// copies properties from values dictionary object to data object
			PropertyMapper.CopyProperties(data, values);

			Framework.Components.Core.ThemeDetailDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ThemeDetail;
			PrimaryEntityKey = "ThemeDetail";

			InlineEditingListCore = InlineEditingList;
			BreadCrumbObject = Master.BreadCrumbObject;

			base.OnInit(e);
		}
		#endregion

	}
}