using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.SearchKey.Controls
{
	public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
	{

		#region variables

		public SearchKeyDataModel SearchParameters
		{
			get
			{
				var data = new SearchKeyDataModel();

				if (SearchParametersRepeater.Items.Count != 0)
				{
					var key = StandardDataModel.StandardDataColumns.Name;

					if (PerferenceUtility.GetUserPreferenceByKeyAsBoolean(key + "Visibility", SettingCategory))
					{
						if (!string.IsNullOrEmpty(CheckAndGetFieldValue(key).ToString()))
						{
							data.Name = CheckAndGetFieldValue(key).ToString();
						}
					}
				}
				
				return data;
			}
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SearchKey;
			PrimaryEntityKey = "SearchKey";
			FolderLocationFromRoot = "Shared/Admin/SystemIntegrity/SearchKey";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion		
	}
}