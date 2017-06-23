using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Text;

namespace Shared.UI.Web.Admin.UserLoginHistory
{
	public partial class Default : Framework.UI.Web.BaseClasses.PageDefault
	{

		#region private methods

		protected override DataTable GetData()
		{
			var dt = Framework.Components.LogAndTrace.UserLoginHistoryDataManager.Search(oSearchFilter.SearchParameters, SessionVariables.RequestProfile);
			return dt;
		}		

		#endregion
		
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserLoginHistory;
			PrimaryEntityKey = "UserLoginHistory";
			PrimaryEntityIdColumn = "UserLoginHistoryId";

			MasterPageCore = Master;
			SubMenuCore = Master.SubMenuObject;
			BreadCrumbObject = Master.BreadCrumbObject;

			SearchFilterCore = oSearchFilter;
			GroupListCore = oGroupList;

			IsDynamicSearchControl = true;

			VisibilityManagerCore = oVC;
		}

		#endregion

		//#region Events

		//protected void Page_Load(object sender, EventArgs e)
		//{
		//	if (!IsPostBack)
		//	{
		//		SettingCategory = "UserLoginHistoryDefaultView";
		//		oSearchFilter.SettingCategory = SettingCategory + "SearchControl";
		//		oGroupList.SettingCategory = SettingCategory + "ListControl";
		//	}
			
		//	oGroupList.Setup(oSearchFilter.GroupBy,"UserLoginHistory", string.Empty, "UserLoginHistoryId", true, GetData, GetColumns, "UserLoginHistory",
		//		 oSearchFilter.SearchParameters.ToURLQuery());

		//	oSearchFilter.OnSearch += oSearchFilter_OnSearch;

		//}

		//protected override void OnPreRender(EventArgs e)
		//{
		//	base.OnPreRender(e);

		//	var sbm = this.Master.SubMenuObject;
		//	var bcControl = this.Master.BreadCrumbObject;

		//	sbm.SettingCategory = SettingCategory + "SubMenuControl";
		//	sbm.Setup();
		//	sbm.GenerateMenu();

		//	bcControl.SettingCategory = SettingCategory + "BreadCrumbControl";
		//	bcControl.Setup("");
		//	bcControl.GenerateMenu();
		//}

		//void oSearchFilter_OnSearch(object sender, EventArgs e)
		//{
		//	oGroupList.ShowData(false, true);
		//}

		

	}
}