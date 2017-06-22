using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Text;
using System.Collections.Specialized;
using System.Collections;

namespace Shared.UI.Web.Controls
{
	public partial class ListControl
	{
		private void RunCommon(string tableName, string tableFolder, string primaryKey, bool pageLoad, string userPreferenceCategory)
		{
			RunCommonA(tableName, tableFolder, primaryKey, pageLoad, userPreferenceCategory);

			//?
			dynBarContainer.Visible = false;

			MainGridView.PageSize = DefaultRowCount;

			dtGlobal = GetGroupedRecords();

			SetPaging(tableName, true);

			ResetSessionAttributes();

			if (!IsPostBack)
			{
				SetUpDropDownFieldConfigurationMode();
			}
		}

		private void RunCommonA(string tableName, string tableFolder, string primaryKey, bool pageLoad, string userPreferenceCategory)
		{
			ViewState["TableFolder"] = tableFolder;

			if (SessionVariables.ActiveTableName == null)
				SessionVariables.ActiveTableName = tableName;

			if (!string.IsNullOrEmpty(SessionVariables.ActiveTableName) && !(SessionVariables.ActiveTableName.Equals(tableName)))
			{
				SessionVariables.ActiveTableName = tableName;
			}

			if (CurrentPageIndex == null)
			{
				CurrentPageIndex = 0;
			}

			ViewState["TableName"] = tableName;
			ViewState["PrimaryKey"] = primaryKey;
			//ViewState["IsTesting"] = SessionVariables.IsTesting;
			ViewState["PageLoad"] = pageLoad;

			if (string.IsNullOrEmpty(userPreferenceCategory))
			{
				UserPreferenceCategory = tableName;
			}
			else
			{
				UserPreferenceCategory = userPreferenceCategory;
			}

			SetUserPreferenceCategory();
		}

		private void SetPaging(string tableName, bool isPaging = true)
		{
			if (!isPaging) return;
			
			MainGridView.PageSize = DefaultRowCount;
			
			//dtGlobal = GetDataSet(tableName);
			oGridPagiation = new GridPagiation();

			oGridPagiation.Setup(plcPaging, litPagingSummary, dtGlobal, MainGridView, Page, SettingCategory);
			oGridPagiation.Changed += oGridPagiation_Changed;
			if (CurrentPageIndex != null)
			{
				oGridPagiation.PageIndexInSession = CurrentPageIndex.Value;
				numberedPager.CurrentIndex = CurrentPageIndex.Value;
			}
			
			oGridPagiation.ManagePaging(dtGlobal);

			var recordsCount = 0;

			if (dtGlobal != null)
			{
				recordsCount = dtGlobal.Rows.Count;
			}

			numberedPager.Setup(recordsCount, MainGridView, DefaultRowCount);
			numberedPager.Changed += oGridPagiation_Changed;

			//if (dtGlobal == null)
			//	return;
			
		}
       
		public void Setup
		(
				string				tableName
			,   string				tableFolder
			,   string				primaryKey
			,   bool				pageLoad
			,   GetDataDelegate		getDataDelegate
			,   GetColumnDelegate	getColumnDelegate
            ,   string				userPreferenceCategory	= ""
            ,   string				searchParameters		= "" 
            ,   bool                isFieldConfigurationVisible = true
		)
		{			
			_getData = getDataDelegate;
			_getColumnDelegate = getColumnDelegate;

			RunCommonA(tableName, tableFolder, primaryKey, pageLoad, userPreferenceCategory);

			SetPaging(tableName, true);

            ExportMenu.Setup(tableName, tableFolder, getDataDelegate, getColumnDelegate, searchParameters);

            ResetSessionAttributes();

            if (isFieldConfigurationVisible)
            {
                if (!IsPostBack)
                {
                    SetUpDropDownFieldConfigurationMode();
                }
            }
            else
            {
                dynBarContainer.Visible = false;
            }
		}
		
		public void Setup
		(
			string tableName
		  , string tableFolder
		  , string primaryKey
		  , bool pageLoad
		  , GetColumnDelegate getColumnDelegate
		  , string groupBy
		  , string groupByValue
		  , string userPreferenceCategory = ""
		  , string searchParameters = ""
		)
		{
			GroupByField = groupBy;
			GroupByFieldValue = groupByValue;

			_getColumnDelegate = getColumnDelegate;
			
			RunCommon(tableName, tableFolder, primaryKey, pageLoad, userPreferenceCategory);
		}

		public DataTable Setup
		(
			 string tableName
		   , string tableFolder
		   , string primaryKey
		   , bool pageLoad
		   , GetColumnDelegate getColumnDelegate
		   , string groupBy
		   , string groupByValue
		   , string subGroupBy
		   , string subGroupByValue
		   , string userPreferenceCategory = ""
		   , string searchParameters = ""
		)
		{
			GroupByField = groupBy;
			GroupByFieldValue = groupByValue;			

			_getColumnDelegate = getColumnDelegate;

			SubGroupByField = subGroupBy;
			SubGroupByFieldValue = subGroupByValue;
			
			RunCommon(tableName, tableFolder, primaryKey, pageLoad, userPreferenceCategory);

			return GetGroupedRecords();
		} 
		
		/// <summary>
		/// 
		/// </summary>
		public void Setup
		(
				string tableName
			,   string tableFolder
			,   string primaryKey
			,   bool pageLoad
			,   GetDataDelegate getDataDelegate
			,   GetColumnDelegate getColumnDelegate
			,   bool isPaging
			,   bool isUpdateColumn = true
			,   bool isDeleteColumn = true
			,   string userPreferenceCategory = ""
            ,   string searchParameters = "" 
		)
		{
			_getData = getDataDelegate;
			_getColumnDelegate = getColumnDelegate;

			RunCommonA(tableName, tableFolder, primaryKey, pageLoad, userPreferenceCategory);

			MainGridView.AllowPaging = isPaging;
			
			SetPaging(tableName, isPaging);

            ExportMenu.Setup(tableName, tableFolder, getDataDelegate, getColumnDelegate, searchParameters);

			IsUpdateColumn = isUpdateColumn;
			IsDeleteColumn = isDeleteColumn;
		}

		//private void SetVisibilityOfListFeatures(bool Isfontpanelvisible, bool Ispagingpanelvisible, bool Issortingoptionsvisible)
		//{
		//	fontpanel.Visible = Isfontpanelvisible;
		//	pnlButtonPanel.Visible = Ispagingpanelvisible;
		//	divSortingOptions.Visible = Issortingoptionsvisible;
		//}

        private void ResetSessionAttributes()
        {
            if (Request.QueryString["Added"] != null)
            {
                SetSession(Request.QueryString["Added"]);
            }
            else if (Request.QueryString["Deleted"] != null)
            {
                SetSession(Request.QueryString["Deleted"]);
            }
            else if (Page.RouteData.Values["SetId"] != null)
            {
                SetSession(Convert.ToString(Page.RouteData.Values["SetId"]));
            }            
            else
            {
                SetSession("true");
            }
        }
	}
}