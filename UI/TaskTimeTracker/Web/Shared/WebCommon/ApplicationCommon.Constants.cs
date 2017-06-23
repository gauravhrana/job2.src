using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shared.WebCommon.UI.Web
{
	public partial class ApplicationCommon
	{
		#region Const Variables

		public const int ReturnAuditInfoFalse = 0;
		public const int ReturnAuditInfoTrue = 1;

		public const string XAxisKey = "XAxis";
		public const string YAxisKey = "YAxis";
		public const string ZAxisKey = "ZAxis";
		public const string DrillXAxisValue = "DrillXAxisValue";
		public const string AggeregateFunctionKey = "AggeregateFunction";
		public const string ShowAggeregateKey = "ShowAggeregate";

		public const string DetailsBorderClassName = "DetailControlBorder";
		public const string HistoryGridVisibilityKey = "HistoryGridVisible";
		public const string GridDefaultClickActionKey = "GridDefaultClickAction";
		public const string DefaultRowCountKey = "DefaultRowCount";
		public const string GridDetailLinkVisibleKey = "GridDetailLinkVisible";
		public const string GridDeleteLinkVisibleKey = "GridDeleteLinkVisible";
		public const string SearchFilterGridLinesKey = "SearchFilterGridLines";
		public const string HistoryAdvancedModeGroupingKey = "AuditHistoryAdvancedModeGrouping";
		public const string HistoryAdvancedModeIntervalKey = "AuditHistoryAdvancedModeInterval";
		public const string UserTimeZone = "UserTimeZone";
		public const string AutoSearchOn = "AutoSearchOn";
		public const string TabOrientation = "TabOrientation";
		public const string WildCardSearchPrefix = "WildCardSearchPrefix";
		public const string WildCardSearchPostfix = "WildCardSearchPostfix";
		public const string ParentMenuId = "ParentMenuId";
		public const string ActiveMenuId = "ActiveMenuId";
		public const string UpdateInfoStyle = "UpdateInfoStyle";
		public const string SortArrowStyle = "SortArrowStyle";
		public const string DateRangeStyle = "DateRangeStyle";
		public const string DateControlLayout = "DateControlLayout";
		public const string FieldConfigurationMode = "FieldConfigurationMode";
		public const string FieldConfigurationModeCategoryKey = "FieldConfigurationModeCategory";
		public const string UserApplicationModeId = "UserApplicationModeId";
		public const string EODEmailDateFormat = "EODEmailDateFormat";
		public const string UserDateFormat = "UserDateFormat";
		public const string UserTimeFormat = "UserTimeFormat";
		public const string GroupListBindActiveTabKey = "GroupListBindActiveTab";
		public const string UserLoginLogginInMongoDBEnabled = "UserLoginLogginInMongoDBEnabled";

		public const string UserActiveClient = "UserActiveClient";
		public const string UserActiveProject = "UserActiveProject";
		public const string UserActiveNeed = "UserActiveNeed";
		public const string UserActiveTask = "UserActiveTask";

		public const string ControlVisible = "ControlVisible";

		public const string TabHeaderBackgroundColor = "TabHeaderBackgroundColor";
		public const string SubMenuTopBackgroundColor = "SubMenuTopBackgroundColor";
		public const string SubMenuTopBorderColor = "SubMenuTopBorderColor";
		public const string SubMenuBackgroundColor = "SubMenuBackgroundColor";
		public const string SubMenuForegroundColor = "SubMenuForegroundColor";
		public const string SubMenuHoverColor = "SubMenuHoverColor";
		public const string SubMenuBorderColor = "SubMenuBorderColor";
		public const string SubMenuBorderStyle = "SubMenuBorderStyle";
		public const string SubMenuFontFamily = "SubMenuFontFamily";
		public const string SubMenuFontSize = "SubMenuFontSize";

		public const string ReleaseNotesRowStyleForeColor = "ReleaseNotesRowStyleForeColor";
		public const string ReleaseNotesRowStyleBackColor = "ReleaseNotesRowStyleBackColor";

		public const string ReleaseNotesAlternatingRowStyleBackColor = "ReleaseNotesAlternatingRowStyleBackColor";
		public const string ReleaseNotesHeaderBackColor = "ReleaseNotesHeaderBackColor";
		public const string ReleaseNotesHeaderForeColor = "ReleaseNotesHeaderForeColor";
		public const string ReleaseNotesGridLines = "ReleaseNotesGridLines";
		public const string ReleaseNotesRowStyleFontSize = "ReleaseNotesRowStyleFontSize";
		public const string ReleaseNotesAlternatingRowStyleFontSize = "ReleaseNotesAlternatingRowStyleFontSize";
		public const string ReleaseNotesRowStyleHeight = "ReleaseNotesRowStyleHeight";
		public const string ReleaseNotesAlternatingRowStyleHeight = "ReleaseNotesAlternatingRowStyleHeight";
		public const string ReleaseNotesHeaderStyleHeight = "ReleaseNotesHeaderStyleHeight";
		public const string ReleaseNotesHeaderStyleFontSize = "ReleaseNotesHeaderStyleFontSize";
		public const string ReleaseNotesStatisticUnknown = "unknown";
		public const string ScheduleStatisticUnknown = "unknown";

		public const string AceEditorNoOfLines = "AceEditorNoOfLines";
		public const string ScheduleNewGridLines = "ScheduleNewGridLines";
		public const string GridActionBarBackgroundColor = "GridActionBarBackgroundColor";
		public const string GridActionBarForegroundColor = "GridActionBarForegroundColor";
		public const string GridActionBarFontFamily = "GridActionBarFontFamily";
		public const string GridActionBarFontSize = "GridActionBarFontSize";
		public const string GridDefaultCharacterCount = "GridDefaultCharacterCount";
		public const string DynamicGridCharacter = "...";

		public const string ListHeaderForeColor = "ListHeaderForeColor";
		public const string MenuBackgroundColor = "MenuBackgroundColor";
		public const string MenuForegroundColor = "MenuForegroundColor";
		public const string MenuHoverColor = "MenuHoverColor";
		public const string MenuBorderColor = "MenuBorderColor";
		public const string MenuFontFamily = "MenuFontFamily";
		public const string MenuFontSize = "MenuFontSize";
		public const string MenuColoredCategoryColor = "MenuColoredCategoryColor";
		public const string ColoredMenuCategory = "ColoredMenuCategory";

		public const string SearchBorderColor = "SearchBorderColor";
		public const string SearchBackgroundColor = "SearchBackgroundColor";
		public const string SearchForegroundColor = "SearchForegroundColor";
		public const string SearchFontFamily = "SearchFontFamily";
		public const string SearchFontSize = "SearchFontSize";
		public const string SearchBorderStyle = "SearchBorderStyle";

		public const string AllTabExists = "AllTabExists";
		public const string AllTabSelected = "AllTabSelected";
		public const string DetailsButtonPanelVisible = "DetailsButtonPanelVisible";
		public const string DetailsPagingEnabled = "DetailPagingEnabled";
		public const string DetailsAEFLModeEnabled = "DetailsAEFLModeEnabled";

		public const string DateRangeControlPath = "~/Shared/Controls/DateRange.ascx";
		public const string ListControlPath = "~/Shared/Controls/List/List.ascx";
		public const string BucketControlPath = "~/Shared/Controls/Bucket.ascx";
		public const string BucketFor3ControlPath = "~/Shared/Controls/BucketFor3.ascx";
		public const string ReadOnlyBucketControlPath = "~/Shared/Controls/ReadOnlyBucket.ascx";
		public const string DetailTabControlPath = "~/Shared/Controls/DetailTab.ascx";
		public const string DetailTab1ControlPath = "~/Shared/Controls/DetailTab1.ascx";
		public const string VerticalTabChildControlPath = "~/Shared/Controls/VerticalTabChildControl.ascx";
		public const string DetailsWithChildrenListControl = "~/Shared/Controls/DetailsWithChildren.ascx";
		public const string ReleaseNoteStatisticControlPath = "~/Shared/ApplicationManagement/ReleaseNote/Controls/ReleaseNoteStatistics.ascx";
		public const string ScheduleStatisticControlPath = "~/BM/Scheduling/Report/ScheduleView/Controls/ScheduleStatistics.ascx";
		public const string ScheduleDetailStatisticControlPath = "~/BM/Scheduling/ScheduleDetail/Controls/ScheduleDetailStatistics.ascx";
		public const string GroupListControl = "~/Shared/Controls/GroupList.ascx";
		public const string ImagesControl = "~/Shared/Controls/Images.ascx";
		public const string DynamicMenuCharacter = " . . . ";

		public const string DetailsControlRelativePath = "Controls/Details.ascx";
		public const string GenericControlRelativePath = "Controls/Generic.ascx";

		public const string SearchFieldName = "Name";
		public const string UserMenuCategory = "UserMenuCategory";
		public const string UserActiveEntity = "UserActiveEntity";
		public const string VersionDate = "VersionDate";
		public const string Language = "Language";
		public const string DateRangeFormat = "DateRangeFormat";
		public const string FromDateRange = "FromDateRange";
		public const string ToDateRange = "ToDateRange";

		public const string MinDate = "01-01-2000";
		public const string MaxDate = "01-01-3000";
		public const string ApplicationDateFormat = "MM-dd-yyyy";

		public const string Computer = "Computer";
		public const string ConnectionKey = "ConnectionKey";
		public const string LogUser = "LogUser";
		public const string GroupBy = "GroupBy";
		public const string Action = "Action";

		public const string ParentMenuIdTestValue = "23";
		public const int FunctionalityPriority = 6;
		public const int FunctionalityStatus = 10006;
		public const string EnableGroupBy = "EnableGroupBy";

		public const string IntegerAlignment = "Right";

		public const string bgSummaryEmailFooterColor = "LightGrey";
		public const string bgSummaryEmailTotalCellColor = "LightGrey";

		public const string ProjectName = "ProjectName";
		public const string JiraNumber = "JiraNumber";
		public const string JiraItem = "JiraItem";
		public const string OrderBy = "OrderBy";
		public const string OrderByDirection = "OrderByDirection";
		public const string UnitOfDuration = "UnitOfDuration";
		public const string ColumnLayout = "ColumnLayout";


		public const string NameOfBranchControlsKey = "NameOfBranchControls";
		public const string NameOfMasterControlsKey = "NameOfMasterControls";
		public const string NumberOfBranchControlsKey = "NumberOfBranchControls";
		public const string NumberOfMasterControlsKey = "NumberOfMasterControls";

		public const string BranchControlOrder = "BranchControlOrder";
		public const string MasterControlOrder = "MasterControlOrder";

		public const string BackgroundHeaderColor = "white";
		public const string ForegroundHeaderColor = "darkblue";

		public const string AlternateColorOptionSetting = "OFF";
		public const string TotalRowLineStyle = "ON";

		public const string DebugFlag = "DebugFlag";

		public const string BaseDataModel = "BaseDataModel";

		#endregion		

	}
}