using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataModel.Framework.Configuration;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.Components.UserPreference;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.Controls
{
	public partial class GroupList : BaseControl
	{
		private class DetailRecord
		{
			public string GroupByColumn;
			public int RecordCount;
		}

		#region variables

		protected Framework.UI.Web.BaseClasses.ControlSearchFilter SearchFilterCore { get; set; }
		
		public bool OnlyBindActiveTab
		{
			get
			{
				return PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.GroupListBindActiveTabKey, SettingCategory);
			}
		}

		public ListControl ListControl { get; set; }

		public string LastOpenGroupedTab
		{
			get
			{
				return Convert.ToString(ViewState["LastOpenGroupedTab"]);
			}
			set
			{
				ViewState["LastOpenGroupedTab"] = value;
			}
		}

		private string GroupByField
		{
			get
			{
				return Convert.ToString(ViewState["GroupByField"]);
			}
			set
			{
				ViewState["GroupByField"] = value;
			}
		}

		private string SubGroupByField
		{
			get
			{
				return Convert.ToString(ViewState["SubGroupByField"]);
			}
			set
			{
				ViewState["SubGroupByField"] = value;
			}
		}

		private string TableName
		{
			get
			{
				return Convert.ToString(ViewState["TableName"]);
			}
			set
			{
				ViewState["TableName"] = value;
			}
		}

		public string FieldConfigurationMode
		{
			get
			{
				if (!String.IsNullOrEmpty(GroupByField))
				{
					return ddlFieldConfigurationMode.SelectedValue;
				}
				else
				{
					foreach (Control control in pnlGroupListContainer.Controls)
					{
						if (control is ListControl)
						{
							return ((ListControl)control).FieldConfigurationMode;
						}
					}
				}
				return "DBColumns";
			}
		}

		public string PrimaryKey
		{ 
			get; set; 
		}

		public string UserPreferenceCategory
		{
			get;
			set;
		}

		private ListControl.GetColumnDelegate GetColumnDelegate;

		#endregion

		#region private methods

		private static List<string> OrderListByCount(List<string> lstSource, string groupByField, DataTable dtRecords, string groupByDirection)
		{			
			//temp collection of value and count of respective records
			var lstFieldWithCount = new List<DetailRecord>();

			foreach (var strSource in lstSource)
			{
				var obj = new DetailRecord();
				obj.GroupByColumn = strSource;
				obj.RecordCount = dtRecords.AsEnumerable().Where(t => t[groupByField].ToString() == strSource).ToList().Count;

				lstFieldWithCount.Add(obj);
			}

			// perform ordering based on temp collection by count by default ASC
			lstFieldWithCount = lstFieldWithCount.OrderBy(t => t.RecordCount).ToList();

			// check if it is desc, then reverse
			if (groupByDirection.ToLower().Contains("desc"))
			{
				lstFieldWithCount.Reverse();
			}

			// return only the string collection
			return lstFieldWithCount.Select(t => t.GroupByColumn).ToList();
		}

		#region MyRegion
		//private DataTable GetApplicableModesList(int systemEntityTypeId)
		//{
		//	var sortedvalidmodes = new DataTable();

		//	if (Session["ValidModes_" + systemEntityTypeId] != null)
		//	{
		//		sortedvalidmodes = (DataTable)Session["ValidModes_" + systemEntityTypeId];
		//	}
		//	else
		//	{
		//		var data = new FieldConfigurationDataModel();
		//		data.SystemEntityTypeId = systemEntityTypeId;

		//		var columns = FieldConfigurationDataManager.Search(data, SessionVariables.RequestProfile.AuditId, SessionVariables.ApplicationMode);
		//		var modes = FieldConfigurationModeDataManager.GetList(SessionVariables.RequestProfile.AuditId);

		//		var validmodes1 = modes.Clone();
		//		var validmodes2 = modes.Clone();				
		//		var validmodes = modes.Clone();

		//		var modesBySystemEntity = new DataTable();

		//		modesBySystemEntity = modes.Clone();

		//		var catdata = new FieldConfigurationModeCategoryDataModel();
		//		var modeId = 0;
		//		catdata.Name = "List";

		//		var cat = FieldConfigurationModeCategoryDataManager.Search(catdata, SessionVariables.RequestProfile.AuditId);

		//		if (cat.Rows.Count == 1)
		//		{
		//			hdnFieldConfigurationModeCategory.Value = cat.Rows[0][FieldConfigurationModeCategoryDataModel.DataColumns.FieldConfigurationModeCategoryId].ToString();
		//		}

		//		var modeData = new ApplicationModeDataModel();

		//		if (SessionVariables.IsTesting)
		//			modeData.Name = "Testing";
		//		else
		//			modeData.Name = "Live";

		//		var modedt = ApplicationModeDataManager.Search(modeData, SessionVariables.RequestProfile.AuditId, SessionVariables.ApplicationMode);

		//		if (modedt.Rows.Count == 1)
		//		{
		//			modeId = Convert.ToInt32(modedt.Rows[0][ApplicationModeDataModel.DataColumns.ApplicationModeId].ToString());
		//		}

		//		//Added SystemEntityTypeId = 3000 only for testing FCModeCategory
		//		if (!string.IsNullOrEmpty(hdnFieldConfigurationModeCategory.Value) && modeId != 0)
		//		{
		//			var appModeData = new ApplicationModeXFieldConfigurationModeDataModel();
		//			appModeData.ApplicationModeId = modeId;

		//			var appModeDt = ApplicationModeXFieldConfigurationModeDataManager.GetByApplicationMode(modeId, SessionVariables.RequestProfile.AuditId);

		//			var distinctFieldValues = (from row in appModeDt.AsEnumerable()
		//									   orderby row[ApplicationModeXFieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId]
		//									   select row[ApplicationModeXFieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId]).Distinct().ToList();


		//			var modeIds = String.Empty;

		//			foreach (var valModeId in distinctFieldValues)
		//			{
		//				if (!string.IsNullOrEmpty(modeIds))
		//				{
		//					modeIds += ", " + valModeId;
		//				}
		//				else
		//				{
		//					modeIds += valModeId;
		//				}
		//			}

		//			var rows = modes.Select(FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId + " in (" + modeIds + ")");
		//			if (rows.Length > 0)
		//			{
		//				foreach (var dr in rows)
		//				{
		//					validmodes1.ImportRow(dr);
		//				}
		//			}

		//			var aeflModeCatModesData = new FieldConfigurationModeCategoryXFCModeDataModel();
		//			aeflModeCatModesData.FieldConfigurationModeCategoryId = int.Parse(hdnFieldConfigurationModeCategory.Value);
		//			var aeflModeCatModesDt = FieldConfigurationModeCategoryXFCModeDataManager.GetByFieldConfigurationModeCategory(int.Parse(hdnFieldConfigurationModeCategory.Value), SessionVariables.RequestProfile.AuditId);

		//			distinctFieldValues = (from row in aeflModeCatModesDt.AsEnumerable()
		//								   orderby row[ApplicationModeXFieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId]
		//								   select row[ApplicationModeXFieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId]).Distinct().ToList();

		//			modeIds = String.Empty;
		//			foreach (var valModeId in distinctFieldValues)
		//			{
		//				if (!string.IsNullOrEmpty(modeIds))
		//				{
		//					modeIds += ", " + valModeId;
		//				}
		//				else
		//				{
		//					modeIds += valModeId;
		//				}
		//			}

		//			rows = modes.Select(FieldConfigurationModeDataModel.DataColumns.FieldConfigurationModeId + " in (" + modeIds + ")");
		//			if (rows.Length > 0)
		//			{
		//				foreach (var dr in rows)
		//				{
		//					validmodes2.ImportRow(dr);
		//				}
		//			}

		//			validmodes = validmodes1.AsEnumerable()
		//					 .Intersect(validmodes2.AsEnumerable(), DataRowComparer.Default).CopyToDataTable();

		//			modesBySystemEntity = FieldConfigurationModeCategoryDataManager.GetApplicableModesListByEntity(columns, modes);

		//			validmodes.Merge(modesBySystemEntity, false);

		//		}
		//		else
		//		{
		//			#region hide
		//			//for (var j = 0; j < modes.Rows.Count; j++)
		//			//{
		//			//    for (var i = 0; i < columns.Rows.Count; i++)
		//			//    {

		//			//        if (
		//			//            int.Parse(
		//			//                columns.Rows[i][
		//			//                    FieldConfiguration.DataColumns.FieldConfigurationModeId].
		//			//                    ToString()) ==
		//			//            int.Parse(
		//			//                modes.Rows[j][
		//			//                    FieldConfigurationMode.DataColumns.
		//			//                        FieldConfigurationModeId].ToString())
		//			//            )
		//			//        {
		//			//            var zKey = FieldConfigurationMode.DataColumns.FieldConfigurationModeId;

		//			//            var temp = validmodes.Select(zKey + " = " + int.Parse(
		//			//                modes.Rows[j][
		//			//                    FieldConfigurationMode.DataColumns.
		//			//                        FieldConfigurationModeId].ToString()));
		//			//            if (temp.Length == 0)
		//			//                validmodes.ImportRow(modes.Rows[j]);


		//			//        }
		//			//    }                    

		//			//}
		//			#endregion hide

		//			validmodes = FieldConfigurationModeCategoryDataManager.GetApplicableModesListByEntity(columns, modes);
		//		}

		//		var dv = validmodes.DefaultView;
		//		dv.Sort = "SortOrder ASC";
		//		sortedvalidmodes = dv.ToTable(true, "FieldConfigurationModeId", "Name", "Description", "SortOrder");
		//		Session["ValidModes_" + systemEntityTypeId] = sortedvalidmodes;
		//	}

		//	return sortedvalidmodes;

		//} 
		#endregion

		// TODO: Correct this
		// should not be session mode as you can have multiple group list.
		// this is view sate of Group List
		private int GetSessionInstanceFieldConfigurationMode()
		{
			if (Session[TableName + "SelectedMode"] != null)
				return Convert.ToInt32(Session[TableName + "SelectedMode"].ToString());
			else
				return -1;
		}

		private int SetUpDropDownFieldConfigurationMode()
		{
			//var systemEntityType = Helper.GetSystemEntity(TableName);
			//var systemEntityType = (int)Enum.Parse(typeof(SystemEntity), ViewState["TableName"].ToString());
			var systemEntityType = (SystemEntity)Enum.Parse(typeof(SystemEntity), ViewState["TableName"].ToString());
			//SettingCategory = ViewState["TableName"] + "DefaultViewListControl";
			//var settingCategory = SettingCategory;

			//var dt = GetApplicableModesList(systemEntityType);
			var dt = FieldConfigurationUtility.GetApplicableModesList(systemEntityType);

			var modeSelected = GetSessionInstanceFieldConfigurationMode();
			
			var settingCategory = TableName + "DefaultViewListControl";

			var upcId = PerferenceUtility.CreateUserPreferenceCategoryIfNotExists(settingCategory, settingCategory);
			
			var fcModeSelected = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.FieldConfigurationMode, settingCategory);
			
			if (dt.Rows.Count > 0)
			{
				try
				{
					ddlFieldConfigurationMode.ClearSelection();
					ddlFieldConfigurationMode.SelectedIndex = -1;
					ddlFieldConfigurationMode.DataSource = dt;
					ddlFieldConfigurationMode.DataTextField = "Name";
					ddlFieldConfigurationMode.DataValueField = "FieldConfigurationModeId";
					ddlFieldConfigurationMode.DataBind();

					if (Convert.ToInt32(fcModeSelected) > 0)
						ddlFieldConfigurationMode.SelectedValue = fcModeSelected;
					else
						ddlFieldConfigurationMode.SelectedValue = modeSelected.ToString();
				}
				catch (Exception ex)
				{
					// if no match on list 
				}

				var modeselected = int.Parse(ddlFieldConfigurationMode.SelectedValue);
				//FieldConfigurationMode = ddlFieldConfigurationMode.SelectedValue;
			}
			else
			{
				ddlFieldConfigurationMode.Visible = false;
			}

			return modeSelected;
		}		

		private ListControl CreateListControl(string tableName, string tableFolder, string primaryKey,
									   ListControl.GetColumnDelegate getColumnDelegate, string userPreferenceCategory,
									   string searchParameters, string tabName)
		{
			var oList = ApplicationCommon.GetNewListControl();

			oList.SettingCategory = SettingCategory;

			oList.Setup(tableName, tableFolder, primaryKey, true, getColumnDelegate, GroupByField, tabName, userPreferenceCategory, searchParameters);

			// becuase we are in group list, it will be delegated to parent
			// logic should extracted out
			oList.FormattingPanel.Visible = false;
			//oList.FieldConfigurationMode = ddlFieldConfigurationMode.SelectedValue;
			//oList.FieldConfigurationModeText = ddlFieldConfigurationMode.SelectedItem.Text;

			return oList;
		}

		private void AddListControlForFirstTab(
				string tableName
			,   string tableFolder
			,   string primaryKey
			,   ListControl.GetColumnDelegate getColumnDelegate
			,   string userPreferenceCategory
			,   string searchParameters
			,   string tabName
			,   int totalRecordsInTab
			,   DetailTabControl parentTabControl)
		{

			if (parentTabControl.TabOrientation == TabOrientation.Horizontal && OnlyBindActiveTab && (string.IsNullOrEmpty(LastOpenGroupedTab) || LastOpenGroupedTab == tabName))
			{
				var oList = CreateListControl(tableName, tableFolder, primaryKey, getColumnDelegate, userPreferenceCategory, searchParameters, tabName);
				parentTabControl.AddHorizontalTabDetailItem(0, oList);
				//if (string.IsNullOrEmpty(tabName))
				//{
				//	tabName = "blank";
				//}
				parentTabControl.ApplyTabSelection(tabName);
			}
		}

		private void DoNoSubGroup
		(
				string tableName
			,   string tableFolder
			,   string primaryKey
			,   ListControl.GetColumnDelegate getColumnDelegate
			,   string userPreferenceCategory
			,   string searchParameters
			,   string tabName
			,   int totalRecordsInTab
			,   int iTabIndex
			,   DetailTabControl parentTabControl
			,	string groupHeader = ""
		)
		{
			// SetupList
			var strHeader = groupHeader + " (" + totalRecordsInTab.ToString(CultureInfo.InvariantCulture) + ")";

			// if vertical tab orientation then bind/render all the lists
			// else if horizontal tab orientation then bind/render only required list control to reduce load time while switching betwen tabs
			if (parentTabControl.TabOrientation == TabOrientation.Horizontal && OnlyBindActiveTab)
			{
				if ((iTabIndex == 0 && string.IsNullOrEmpty(LastOpenGroupedTab)) || LastOpenGroupedTab == tabName || LastOpenGroupedTab.ToLower() == "all")
				{
					var oList = CreateListControl(tableName, tableFolder, primaryKey, getColumnDelegate, userPreferenceCategory, searchParameters, tabName);

					// attach list control but don't show tab as selected
					parentTabControl.AddTab(tabName, oList, strHeader);
				}
				else
				{
					// add tab header, don't attach list control 
					parentTabControl.AddTab(tabName, null, strHeader);
				}
			}
			else
			{
				var oList = CreateListControl(tableName, tableFolder, primaryKey, getColumnDelegate, userPreferenceCategory, searchParameters, tabName);
				parentTabControl.AddTab(tabName, oList, strHeader);
			}
		}

		private void DoSubGrouping
		(
				string tableName
			,   string tableFolder
			,   string primaryKey
			,   ListControl.GetColumnDelegate getColumnDelegate
			,   string userPreferenceCategory
			,   string searchParameters
			,   DataTable dt
			,   string parentTabName
			,   int totalRecordsInTab
			,   int iTabIndex
			,   DetailTab1Control vtabcontrol
			,   string subGroupByDirection
			,	string groupheader
		)
		{
			var strParentHeader = groupheader + " (" + totalRecordsInTab + ")";

			// Filter datatable
			var dv = dt.DefaultView;
			dv.RowFilter = GroupByField + " = '" + parentTabName + "'";

			var groupedView = dv.ToTable();

			var tabControl2 = ApplicationCommon.GetNewDetailTabControl();

			tabControl2.Setup(SettingCategory);

			var SubGroupHeaderFormatting = string.Empty;
			var SubGroupHeaderControlType = string.Empty;
			var systemEntityType = Enum.Parse(typeof(SystemEntity), tableName);
			if (systemEntityType != null)
			{
				var systemEntityTypeId = (int)systemEntityType;
				var fcRecords = FieldConfigurationUtility.GetFieldConfigurations(systemEntityTypeId, SessionVariables.SearchControlColumnsModeId, SubGroupByField);
				if (fcRecords.Rows.Count > 0)
				{
					SubGroupHeaderControlType = Convert.ToString(fcRecords.Rows[0][FieldConfigurationDataModel.DataColumns.ControlType]);
					SubGroupHeaderFormatting = Convert.ToString(fcRecords.Rows[0][FieldConfigurationDataModel.DataColumns.Formatting]);
				}
			}

			// get list sub group --> sub tabs
			// by default asc ordering
			var subTabs = (from row in groupedView.AsEnumerable()
						   orderby row[SubGroupByField].ToString().Trim()
						   select row[SubGroupByField].ToString().Trim())
									   .Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();

			// if desc ordering, reverse the list
			if (subGroupByDirection.ToLower() == "desc")
			{
				subTabs.Reverse();
			}
			else if (subGroupByDirection.ToLower().Contains("count"))
			{
				subTabs = OrderListByCount(subTabs, SubGroupByField, groupedView, subGroupByDirection);
			}

			var count = 0;

			foreach (var subTab in subTabs)
			{
				var subGroupHeader = subTab;
				if (!string.IsNullOrEmpty(SubGroupHeaderFormatting))
				{
					if (SubGroupHeaderControlType == "DatePanel")
					{
						subGroupHeader = (from row in dt.AsEnumerable()
										  where row[SubGroupByField].ToString().Equals(subTab) && row[GroupByField].ToString().Equals(parentTabName)
									   select row.Field<DateTime>(SubGroupByField))
									   .First().ToString(SubGroupHeaderFormatting);
					}
				}

				// SetupList
				var oList = ApplicationCommon.GetNewListControl();
				oList.SettingCategory = SettingCategory;
				var dt2 = oList.Setup(tableName, tableFolder, primaryKey, true, getColumnDelegate, GroupByField, parentTabName, SubGroupByField, subTab, userPreferenceCategory, searchParameters);

				oList.FormattingPanel.Visible = false;

				var recordCount = dt2.AsEnumerable().Where(a => a[SubGroupByField].ToString() == subTab).ToList().Count;

				var header = subGroupHeader + " (" + recordCount + ")";

				if (count == 0)
				{
					// add tab and bind list and selected
					tabControl2.AddTab(subTab, oList, header, true);
				}
				else
				{
					// add tab and bind list
					tabControl2.AddTab(subTab, oList, header);
				}

				count++;
			}

			// TODO: this going to do same thing ... same call
			if (iTabIndex == 0)
			{
				vtabcontrol.AddTab(parentTabName, tabControl2, strParentHeader, true);
			}
			else
			{
				vtabcontrol.AddTab(parentTabName, tabControl2, strParentHeader);
			}
		}

		private void ShowBasicList
		(
				string tableName
			,   string tableFolder
			,   string primaryKey
			,   ListControl.GetDataDelegate getDataDelegate
			,   ListControl.GetColumnDelegate getColumnDelegate
			,   string userPreferenceCategory
			,   string searchParameters)
		{

			var oList = ApplicationCommon.GetNewListControl();

			// set up list control
			oList.SettingCategory = SettingCategory;
			oList.Setup(tableName, tableFolder, primaryKey, true, getDataDelegate, getColumnDelegate, userPreferenceCategory, searchParameters, false);

			oList.HideFormattingPanel();
			// do these need to be set .. are they going to be valid?
			oList.FieldConfigurationMode = ddlFieldConfigurationMode.SelectedValue;
			oList.FieldConfigurationModeText = ddlFieldConfigurationMode.SelectedItem.Text;

			// this is going to happen every time
			// this is false everytime ? that is why it is in this section
			if (string.IsNullOrEmpty(GroupByField))
			{
				pnlGroupListContainer.Controls.Clear();
			}

			ListControl = oList;

			pnlGroupListContainer.Controls.Add(oList);
			

			// this is true everytime ? that is why it is in this section
			// GroupByField = string.Empty;
		}	    

		#endregion

		#region Methods

		public void AddListControlForChangedTab(string tabName, Control xDiv)
		{
			var oList = ApplicationCommon.GetNewListControl();
			oList.SettingCategory = SettingCategory;
			oList.Setup(TableName, string.Empty, PrimaryKey, true, GetColumnDelegate, GroupByField, tabName, UserPreferenceCategory, string.Empty);

			oList.FormattingPanel.Visible = false;

			oList.ShowData(false, true);

			ListControl = oList;

			xDiv.Controls.Add(oList);
		}

		public void Setup
		(
				string groupByField
			,   string groupByFieldDirection
			,   string subgroupByField
			,   string subgroupByFieldDirection
			,   string primaryEntityKey
			,   string tableFolder
			,   string primaryKey
			,   bool pageLoad
			,   ListControl.GetDataDelegate getDataDelegate
			,   ListControl.GetColumnDelegate getColumnDelegate
			,   string userPreferenceCategory = ""
			,   string searchParameters = ""
			,   Framework.UI.Web.BaseClasses.ControlSearchFilter searchFilterCore = null
			,   bool reloadData = false
		)
		{
			TableName				= primaryEntityKey;
			GroupByField			= groupByField;
			SubGroupByField			= subgroupByField;
			PrimaryKey				= primaryKey;
			UserPreferenceCategory	= userPreferenceCategory;
			GetColumnDelegate		= getColumnDelegate;
			SearchFilterCore		= searchFilterCore;

			// will it be loaded from view state ... otherwise ?
			if (!IsPostBack)
			{
				SetUpDropDownFieldConfigurationMode();
			}

			myExportMenu.Setup(primaryEntityKey, tableFolder, getDataDelegate, getColumnDelegate, searchParameters);
					
			//SearchFilterCore.Visible = ApplicationCommon.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.ControlVisible, SearchFilterCore.SettingCategory);

			// No Grouping  or Grouping
			if (string.IsNullOrEmpty(groupByField) || GroupByField.ToLower() == "none" || GroupByField == "-1")
			{
				ShowBasicList(primaryEntityKey, tableFolder, primaryKey, getDataDelegate, getColumnDelegate, userPreferenceCategory, searchParameters);
			}			
			else
			{
				// Get Data - should only be after search criteria has changed
				//Session[primaryEntityKey] = null;

				if (reloadData)
				{
					Session[primaryEntityKey] = getDataDelegate();
				}
							
				// get cached data
				var dt = (DataTable)Session[primaryEntityKey];

				// TODO: why/what happens if this true
				if (dt == null || dt.Rows.Count == 0 || !dt.Columns.Contains(groupByField))
				{
					// some sort of error
					return;
				}

				var headerFormatting = string.Empty;
				var headerControlType = string.Empty;
				var systemEntityType = Enum.Parse(typeof(SystemEntity), primaryEntityKey);
				if(systemEntityType != null)
				{
					var systemEntityTypeId = (int)systemEntityType;
					var fcRecords = FieldConfigurationUtility.GetFieldConfigurations(systemEntityTypeId, SessionVariables.SearchControlColumnsModeId, GroupByField);
					if (fcRecords.Rows.Count > 0)
					{
						headerControlType = Convert.ToString(fcRecords.Rows[0][FieldConfigurationDataModel.DataColumns.ControlType]);
						headerFormatting = Convert.ToString(fcRecords.Rows[0][FieldConfigurationDataModel.DataColumns.Formatting]);
					}
				}
				
				// by default asc ordering
				var distinctTabNames = (from row in dt.AsEnumerable()                                        
											orderby row[GroupByField].ToString().Trim()
											select row[GroupByField].ToString().Trim())
											.Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
				
				// if desc ordering, reverse the list
				if (groupByFieldDirection.ToLower() == "desc")
				{
					distinctTabNames.Reverse();
				}
				else if (groupByFieldDirection.ToLower().Contains("count"))
				{
					distinctTabNames = OrderListByCount(distinctTabNames, GroupByField, dt, groupByFieldDirection);
				}

				// PARENT TAB                     
				var tabControl = ApplicationCommon.GetNewDetailTabControl();
				var vTabControl = ApplicationCommon.GetNewDetail1TabControl();

				// OnlyBindActiveTab flag is passed in tab control setup for tab control to render links client/server side
				tabControl.Setup(SettingCategory, !OnlyBindActiveTab);

				// remove duplicate cases ignoring case comparision?
				var iTabIndex = 0;

				foreach (var tabName in distinctTabNames)
				{

					var list = dt.AsEnumerable().Where(t => t[GroupByField].ToString().Trim() == tabName).ToList();

					var totalRecordsInTab = list.Count();

					var groupHeader = tabName;
					if (!string.IsNullOrEmpty(headerFormatting))
					{
						if (headerControlType == "DatePanel")
						{
							groupHeader = (from row in dt.AsEnumerable()
										   where row[GroupByField].ToString().Equals(tabName)
										   select row.Field<DateTime>(GroupByField))
										   .First().ToString(headerFormatting);
						}
					}

					// see if it has sub grouping
					if (string.IsNullOrEmpty(SubGroupByField) || SubGroupByField.ToLower().Equals("none") || SubGroupByField.Equals("-1"))
					{	

						DoNoSubGroup(primaryEntityKey, tableFolder, primaryKey, getColumnDelegate,
										userPreferenceCategory, searchParameters, tabName, totalRecordsInTab, iTabIndex,
										tabControl, groupHeader);
					}
					else
					{
						DoSubGrouping(primaryEntityKey, tableFolder, primaryKey, getColumnDelegate,
										userPreferenceCategory, searchParameters, dt, tabName, totalRecordsInTab, iTabIndex,
										vTabControl, subgroupByFieldDirection, groupHeader);
					}

					iTabIndex++;
				}

				// Sub Grouping has first layer of tab as vertical, So horizontal tab item logic doesn't apply to it.
				if (string.IsNullOrEmpty(SubGroupByField) || SubGroupByField.ToLower().Equals("none") || SubGroupByField.Equals("-1"))
				{
					var totalRecordsInFirstTab = dt.AsEnumerable().Where(t => t[GroupByField].ToString() == distinctTabNames[0]).ToList().Count();

					// Add First Tab List if applicable. Method itself will decide whether to add or not.
					AddListControlForFirstTab(primaryEntityKey, tableFolder, primaryKey, getColumnDelegate,
											userPreferenceCategory, searchParameters, distinctTabNames[0], totalRecordsInFirstTab,
											tabControl);
				}

				// clear the container
				// what happens to sub controls and event handling such as next page.
				pnlGroupListContainer.Controls.Clear();

				// add tab control
				if (!string.IsNullOrEmpty(SubGroupByField) && !SubGroupByField.ToLower().Equals("none") && !SubGroupByField.Equals("-1"))
				{
					pnlGroupListContainer.Controls.Add(vTabControl);
				}	            
				else
				{
					pnlGroupListContainer.Controls.Add(tabControl);
				}
					
				//Framework.Components.DataAccess.Log4Net.LogInfo("Step5 Create Group Tabs and List Controls in GroupList END", "Create Group TABS", SetupConfiguration.ApplicationId);       
			}
		}

		public void ShowData(bool dataHide, bool search)
		{
			foreach (Control control in pnlGroupListContainer.Controls)
			{
				if (control is ListControl)
				{
					((ListControl)control).ShowData(dataHide, search);
				}
				else if (control is DetailTabControl)
				{
					((DetailTabControl)control).Reload();
				}
				else if (control is DetailTab1Control)
				{
					((DetailTab1Control)control).Reload();
				}
			}
		}

		#endregion

		#region Events

		protected void lnkfontsmall_Click(object sender, EventArgs e)
		{
			//SetFontForGrid("12px", "smallfontgrid");
		}

		protected void lnkfontmedium_Click(object sender, EventArgs e)
		{
			//SetFontForGrid("14px", "mediumfontgrid");
		}

		protected void lnkfontlarger_Click(object sender, EventArgs e)
		{
			//SetFontForGrid("16px", "largerfontgrid");
		}

		//private void SetFontForGrid(string fontsize, string cssclass)
		//{
		//	pnlGroupListContainer.Style.Add("font-size", fontsize);
		//}

		protected void ddlFieldConfigurationMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			var fcMode = ddlFieldConfigurationMode.SelectedValue;
				var fcModeText = ddlFieldConfigurationMode.SelectedItem.Text;

				// find all list controls and set the mode 
				foreach (Control control in pnlGroupListContainer.Controls)
				{
					if (control is ListControl)
					{
						((ListControl)control).ApplyFieldConfigurationMode(fcMode, fcModeText);
					}
					else if (control is DetailTabControl)
					{
						((DetailTabControl)control).ApplyFieldConfigurationMode(fcMode, fcModeText);
					}
				}			
		}

		protected void lnkBtnToggleSearchControl_Click(object sender, EventArgs e)
		{
			if (SearchFilterCore != null)
			{
				var isVisible = !SearchFilterCore.Visible;
				PerferenceUtility.UpdateUserPreference(SearchFilterCore.SettingCategory, ApplicationCommon.ControlVisible, isVisible.ToString());
				SearchFilterCore.Visible = isVisible;
			}
		}

		#endregion

	}
}