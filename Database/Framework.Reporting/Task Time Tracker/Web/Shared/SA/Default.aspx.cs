using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;
using System.Web.Services;
using TaskTimeTracker.Components.Module.TimeTracking;

namespace SA.UI.Web
{
    public partial class Default : Framework.UI.Web.BaseClasses.PageBasePage
	{

		#region Page Methods

		/// <summary>
		/// This method is being called from  SliederMenu.ascx control for updating User Preference value for "SearchFilterGridLines" key.
		/// </summary>
		/// <param name="value"></param>
		[WebMethod]
		public static void UpdateUserPreferenceForGridLines(string value)
		{
			PerferenceUtility.UpdateUserPreference("General", ApplicationCommon.SearchFilterGridLinesKey, value);
		}

		[WebMethod]
		public static void UpdateSearchControlParameterVisibility(string name, string category)
		{
			PerferenceUtility.UpdateUserPreference(category, name + "Visibility", "false");
		}

		#endregion

		#region Events

		protected override void OnPreInit(EventArgs e) 
		{
            ApplicationCommon.ResetApplicationCache("SA");

			base.OnPreInit(e); 
		} 

		protected void Page_Load(object sender, EventArgs e)
        {            
			SessionVariables.SiteMenuData = null;
			SessionVariables.UserPreferedMenuData = MenuHelper.GetUserPreferedMenu();
        }

		#endregion

    }
}
