using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using System.Data;
using Shared.UI.Web.Controls.SubMenu;
using Shared.UI.Web.Controls.BreadCrumb;
using Shared.UI.Web.Controls;
namespace Framework.UI.Web.BaseClasses
{

	[DefaultProperty("Text")]
	[ToolboxData("<{0}:PageDefault runat=server></{0}:PageDefault>")]
	public class PageDefault2 : PageCommon
	{

		#region Variables

		protected GroupList								GroupListCore           { get; set; }
		protected Shared.UI.Web.Controls.ListControl	ListCore				{ get; set; }
		protected ControlSearchFilter2					SearchFilterCore		{ get; set; }
		protected SubMenu								SubMenuCore             { get; set; }
		protected ControlVisibilityManager				VisibilityManagerCore   { get; set; }
		protected string								PrimaryEntityIdColumn   { get; set; }
		protected PageDefaultMaster						MasterPageCore          { get; set; }

		protected bool IsDynamicSearchControl = false;
		
		public virtual string GroupBy
		{
			get
			{
				return Convert.ToString(ViewState["GroupBy"]);
			}
			set
			{
				ViewState["GroupBy"] = value;
			}
		}

		public virtual string SubGroupBy
		{
			get
			{
				return Convert.ToString(ViewState["SubGroupBy"]);
			}
			set
			{
				ViewState["SubGroupBy"] = value;
			}
		}

		#endregion

		#region Methods

		protected virtual DataTable GetData()
		{
			return null;
		}

		protected string[] GetEntityColumns()
		{
			if (!GroupListCore.FieldConfigurationMode.Equals(String.Empty))
				return FieldConfigurationUtility.GetEntityColumns(GroupListCore.FieldConfigurationMode, PrimaryEntity, SessionVariables.RequestProfile);
			else
				return FieldConfigurationUtility.GetEntityColumns("DBColumns", PrimaryEntity, SessionVariables.RequestProfile);
		}

		private void ManageControlVisibility(string controlTitle)
		{
			switch (controlTitle)
			{
				case "Search Box":
					SearchFilterCore.Visible = true;
					PreferenceUtility.UpdateUserPreference(SearchFilterCore.SettingCategory, ApplicationCommon.ControlVisible, "true");
					break;

				case "Sub Menu":
					SubMenuCore.Visible = true;
					PreferenceUtility.UpdateUserPreference(SubMenuCore.SettingCategory, ApplicationCommon.ControlVisible, "true");
					break;

				case "Bread Crumb":
					BreadCrumbObject.Visible = true;
					PreferenceUtility.UpdateUserPreference(BreadCrumbObject.SettingCategory, ApplicationCommon.ControlVisible, "true");
					break;
			}
		}

		#endregion

		#region Events

		protected override void OnPreInit(EventArgs e)
		{
            base.SetDefaultMasterPagePath();

			base.OnPreInit(e);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			// done here, because its not in view state
			ViewName = "Default";			
		}

		protected override void OnLoadComplete(EventArgs e)
		{
			base.OnLoadComplete(e);

			GroupBy = SearchFilterCore.GroupBy;
			SubGroupBy = SearchFilterCore.SubGroupBy;
		}

		// add dynamic controls ...
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			// inital load of page
			if (!IsPostBack)
			{                
				SubMenuCore.Title = "Sub Menu";

				SettingCategory = PrimaryEntityKey + "DefaultView";

				SearchFilterCore.Title = "Search Box";
				SearchFilterCore.SettingCategory = SettingCategory + "SearchControl";

				if (!IsDynamicSearchControl)
				{
					SearchFilterCore.SetupSearch();
				}

				GroupListCore.SettingCategory = SettingCategory + "ListControl";

				MasterPageCore.Setup(PrimaryEntityKey);
			}

			// due to dynamic build we must re-establish child controls
			if (IsDynamicSearchControl)
			{
				SearchFilterCore.SetupSearch();
			}
			
			VisibilityManagerCore.Setup(ManageControlVisibility, SettingCategory);

			// will we loose pagiation capablitiy  ? // yes if we set it to false, we loose the pagination completely
			var blnReloadData = false;
			
			// TODO: upon intial load get data
			if (!IsPostBack)
			{
				var i = 0;

				// TODO: based on user perfernce coded ... however, for now
				blnReloadData = true;
			}

			//GroupListCore.Setup
			//(
			//		SearchFilterCore.GroupBy
			//	,   SearchFilterCore.GroupByDirection
			//	,	SearchFilterCore.SubGroupBy
			//	,   SearchFilterCore.DoesSubGroupByDirectionExist
			//	,	PrimaryEntityKey
			//	,	String.Empty
			//	,	PrimaryEntityIdColumn
			//	,	true
			//	,	GetData
			//	,	GetEntityColumns
			//	,	PrimaryEntityKey
			//	,	String.Empty
			//	,	SearchFilterCore
			//	,	blnReloadData
			//);

			// bind OnSearch
			SearchFilterCore.OnSearch += oSearchFilter_OnSearch;	
		}
		
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			// set SubMenuCore default values
			SubMenuCore.SettingCategory = SettingCategory + "SubMenuControl";
			SubMenuCore.Setup();
			SubMenuCore.GenerateMenu();

			// get visiblilty
			var isSubMenuVisible		= PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.ControlVisible, SubMenuCore.SettingCategory);
			var isSearchControlVisible	= PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.ControlVisible, SearchFilterCore.SettingCategory);
			var isBreadCrumbVisible		= PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.ControlVisible, BreadCrumbObject.SettingCategory);

			// set visibility
			SearchFilterCore.Visible	= isSearchControlVisible;
			SubMenuCore.Visible			= isSubMenuVisible;
			BreadCrumbObject.Visible	= isBreadCrumbVisible;

			// update custom visivblity control
			VisibilityManagerCore.ClearChildMenuItems();

			VisibilityManagerCore.AddChildControl(SearchFilterCore.Title, isSearchControlVisible);
			VisibilityManagerCore.AddChildControl(SubMenuCore.Title, isSubMenuVisible);
			VisibilityManagerCore.AddChildControl("Bread Crumb", isBreadCrumbVisible);

			BreadCrumbObject.IsAddedInVisibilityManager = true;
		}

		private void oSearchFilter_OnSearch(object sender, EventArgs e)
		{
			// Group By caluse enabled
			//if (!string.IsNullOrEmpty(SearchFilterCore.GroupBy))
			//{
				
			GroupBy = SearchFilterCore.GroupBy;
			SubGroupBy = SearchFilterCore.SubGroupBy;

			GroupListCore.Setup(
					SearchFilterCore.GroupBy
				,   SearchFilterCore.GroupByDirection
				,	SearchFilterCore.SubGroupBy
				,   SearchFilterCore.DoesSubGroupByDirectionExist
				,   PrimaryEntityKey
				,   String.Empty
				,   PrimaryEntityIdColumn
				,   true
				,   GetData
				,   GetEntityColumns
				,   PrimaryEntityKey
				,   String.Empty
				,   null
				,   true
			);
			//}

			GroupListCore.ShowData(false, true);
		}

		#endregion

	}

}

