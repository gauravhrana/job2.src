using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationManagement.HelpPageContext.Controls
{
	public partial class SearchFilter : ControlSearchFilter
	{
		#region variables
		
		public HelpPageContextDataModel SearchParameters
		{
			get
			{
                var data = new HelpPageContextDataModel();


				if (SearchParametersRepeater.Items.Count != 0 && PreferenceUtility.GetUserPreferenceByKeyAsBoolean(
				   StandardDataModel.StandardDataColumns.Name + "Visibility", SettingCategory)
				   && CheckAndGetFieldValue(StandardDataModel.StandardDataColumns.Name) != "")
				{
					data.Name = CheckAndGetFieldValue(StandardDataModel.StandardDataColumns.Name).ToString();
				}

				return data;
			}

			//get
			//{
			//    var data = new HelpPageContext();

			//    data.Name = UIHelper.RefineAndGetSearchText(txtSearchConditionName.Text, SettingCategory);

			//    return data;
			//}
		}
		
		#endregion

		#region Events
		
		protected void Page_Load(object sender, EventArgs e)
		{
			oSearchActionBar.Setup("HelpPageContext", SaveSearchKey);
		}        

		#endregion
	}
}