using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using log4net;
using Shared.UI.Web.Controls.TabControl;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;
using System.Web.UI.HtmlControls;
using System.Data;
using System.IO;

namespace Shared.UI.Web.Controls
{
	public partial class DetailTabControl : BaseControl
	{
		#region Variables

		private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		//private int _setId;

		//public int SetId
		//{
		//	set
		//	{
		//		_setId = value;
		//	}
		//	get
		//	{
		//		return _setId;
		//	}
		//}

		public Control GenericControl;

		public TabOrientation TabOrientation
		{

			get
			{
				if (ViewState["TabOrientation"] != null)
				{
					return (TabOrientation)ViewState["TabOrientation"];
				}
				return TabOrientation.Horizontal;
			}
			set
			{
				ViewState["TabOrientation"] = value;
			}
		}

		private bool IsAllTab
		{
			get
			{
				if (ViewState["IsAllTab"] != null)
				{
					return Convert.ToBoolean(ViewState["IsAllTab"]);
				}
				return false;
			}
			set
			{
				ViewState["IsAllTab"] = value;
			}
		}

		private bool IsAllTabSelected
		{
			get
			{
				if (ViewState["IsAllTabSelected"] != null)
				{
					return Convert.ToBoolean(ViewState["IsAllTabSelected"]);
				}
				return false;
			}
			set
			{
				ViewState["IsAllTabSelected"] = value;
			}
		}

		private bool IsBindAllTabs
		{
			get
			{
				if (ViewState["IsBindAllTabs"] != null)
				{
					return Convert.ToBoolean(ViewState["IsBindAllTabs"]);
				}
				return true; // default value should be true
			}
			set
			{
				ViewState["IsBindAllTabs"] = value;
			}
		}

		private string LastSelectedTab
		{
			get
			{
				if (ViewState["LastSelectedTab"] != null)
				{
					return Convert.ToString(ViewState["LastSelectedTab"]);
				}
				return String.Empty;
			}
			set
			{
				ViewState["LastSelectedTab"] = value;
			}
		}

		private string TabHeaderBackgroundColor
		{
			get
			{
				if (ViewState["TabHeaderBackgroundColor"] != null)
				{
					return Convert.ToString(ViewState["TabHeaderBackgroundColor"]);
				}
				return String.Empty;
			}
			set
			{
				ViewState["TabHeaderBackgroundColor"] = value;
			}
		}

		private List<string> LastOpenVerticalTabs
		{
			get
			{
				if (ViewState["LastOpenVerticalTabs"] != null)
				{
					return (List<string>)ViewState["LastOpenVerticalTabs"];
				}
				return new List<string>();
			}
			set
			{
				ViewState["LastOpenVerticalTabs"] = value;
			}
		}

		private List<MyTabTrackingData> myListTabTracking = new List<MyTabTrackingData>();

		#endregion

		private class MyTabTrackingData
		{
			public string Key { get; set; }
			public HtmlGenericControl Link { get; set; }
			public HtmlGenericControl Target { get; set; }
		}

		#region Private Methods

		//private HtmlGenericControl GetBlankDiv()
		//{
		//	var xDiv = new HtmlGenericControl("div");

		//	xDiv.Attributes.CssStyle.Add("height", "0px");

		//	return xDiv;
		//}

		private HtmlGenericControl CreateTabItemHeader(string tabId, string tabHeaderValue, bool isTabSelected, MyTabTrackingData oMyData, string contentClientId = "")
		{
			var liTabElement = new HtmlGenericControl("li");
			liTabElement.ID = "li" + tabId;

			// highlight the section background
			if (string.IsNullOrEmpty(LastSelectedTab) && isTabSelected && !IsAllTabSelected)
			{
				liTabElement.Attributes.CssStyle.Add("background-color", "Yellow");
			}
			else if (LastSelectedTab == tabId)
			{
				liTabElement.Attributes.CssStyle.Add("background-color", "Yellow");
			}

			var aLink = new HtmlGenericControl("a");
			aLink.Attributes.Add("href", "#" + contentClientId);
			aLink.InnerHtml = "<span>" + tabHeaderValue + "</span>";

			// null in case of All Tab Li Item
			if (oMyData != null)
			{
				oMyData.Link = aLink;
			}

			// case for bind all, Client side Hypelinks won't be visible.
			if (!IsBindAllTabs)
			{
				aLink.Style.Add("display", "none");
			}

			liTabElement.Controls.Add(aLink);

			// case for bind all, only server side hyperlinks will be created/visible
			if (!IsBindAllTabs)
			{
				// add button to enable clicking on tab
				var btnTabNavButton = new LinkButton();
				btnTabNavButton.Text = tabHeaderValue;

				btnTabNavButton.CommandArgument = tabId;
				btnTabNavButton.Click += TabNavButton_Click;
				liTabElement.Controls.Add(btnTabNavButton);
			}

			return liTabElement;
		}

		private void AddHorizontalTab(string tabHeaderId, Control detailControl, string tabHeaderValue, bool isTabSelected)
		{
			if (string.IsNullOrEmpty(tabHeaderValue))
			{
				tabHeaderValue = tabHeaderId;
			}

			var oMyData = new MyTabTrackingData();
			oMyData.Key = tabHeaderId;

			// * * * * * *
			// tab page
			// * * * * * *
			var divTabPageHeader = new HtmlGenericControl("div");
			divTabPageHeader.ID = "divTabPageHeader" + tabHeaderId;
			divTabPageHeader.InnerHtml = tabHeaderValue;
			divTabPageHeader.Attributes.Add("class", "tab-header");
			divTabPageHeader.Style["display"] = "none";

			var divTabPage = new HtmlGenericControl("div");
			//divTabPage.Attributes.Add("class", ApplicationCommon.DetailsBorderClassName + " row col-sm-12");

			// Add Tab Header
			divTabPage.Controls.Add(divTabPageHeader);

			//"#TabContent-" + tabId.Replace(" ", string.Empty)
			if (detailControl != null)
			{
				if (GenericControl == null)
				{
					GenericControl = detailControl;
				}

				divTabPage.Controls.Add(detailControl);
			}

			oMyData.Target = divTabPage;

			// tab header
			var liTabElement = CreateTabItemHeader(tabHeaderId, tabHeaderValue, isTabSelected, oMyData, divTabPage.ClientID);

			// add 'ALL' Tab Last ... though it will be ordered in the manner its called
			if (IsAllTab)
			{
				divTabHeaderList.Controls.AddAt(divTabHeaderList.Controls.Count - 1, liTabElement);

				divTabContentContainer.Controls.AddAt(divTabContentContainer.Controls.Count - 1, divTabPage);

				myListTabTracking.Insert(myListTabTracking.Count - 1, oMyData);
			}
			else
			{
				divTabHeaderList.Controls.Add(liTabElement);

				divTabContentContainer.Controls.Add(divTabPage);

				myListTabTracking.Add(oMyData);
			}

			divTabPage.Visible = true;

			// TO-REVIEW: not in favor of IsTesting embeded here
			if (!SessionVariables.IsTesting || IsAllTab)
			{
				var xInnerDiv = new HtmlGenericControl("div");
				xInnerDiv.InnerHtml = "<table class='sectionTabName' width='100%'><tr><td align='left'>" + tabHeaderValue + "</td></tr></table>";
				xInnerDiv.Visible = true;

				divTabPage.Visible = true;
			}
			else if (isTabSelected)
			{
				divTabPage.Visible = true;
			}
		}

		private void AddVerticalTab(string tabHeaderId, Control detailControl, string tabHeaderValue, bool isTabSelected)
		{
			if (string.IsNullOrEmpty(tabHeaderValue))
			{
				tabHeaderValue = tabHeaderId;
			}

			var oMyData = new MyTabTrackingData();
			oMyData.Key = tabHeaderId;

			// * * * * * *
			// accordion header
			// * * * * * *

			var accordinHeader = new HtmlGenericControl("h3");
			accordinHeader.InnerText = tabHeaderValue;
			accordinHeader.ID = "h3" + tabHeaderId;
			accordinHeader.Attributes["myTabIndex"] = myListTabTracking.Count.ToString();

			divTabContentContainer.Controls.Add(accordinHeader);

			// * * * * * *
			// accordion child control
			// * * * * * *

			var divAccordion = new HtmlGenericControl("div");
			divAccordion.Attributes.Add("class", ApplicationCommon.DetailsBorderClassName);

			//"#TabContent-" + tabId.Replace(" ", string.Empty);
			if (detailControl != null)
			{
				if (GenericControl == null)
				{
					GenericControl = detailControl;
				}

				divAccordion.Controls.Add(detailControl);
			}

			oMyData.Target = divAccordion;

			divTabContentContainer.Controls.Add(divAccordion);

			myListTabTracking.Add(oMyData);
		}

		#endregion

		#region Public Methods

		public void Setup(string userPreferenceCategory, bool isBindAllTabs = true)
		{
			IsBindAllTabs = isBindAllTabs;

			SettingCategory = userPreferenceCategory;

			// Get Tab Orientation based on UPCategory
			if (!string.IsNullOrEmpty(SettingCategory))
			{
				PerferenceUtility.CreateUserPreferenceCategoryIfNotExists(SettingCategory, SettingCategory);
			}

			TabOrientation = PerferenceUtility.GetUserPreferenceByKeyAsTabOrientation(ApplicationCommon.TabOrientation, SettingCategory);

			hdnTabOrientation.Value = TabOrientation.ToString();

			IsAllTab = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.AllTabExists, SettingCategory);

			if (IsAllTab)
			{
				IsAllTabSelected = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.AllTabSelected, SettingCategory);
			}

			if (TabOrientation == TabOrientation.Vertical)
			{
				//divTabContainer.Visible = false;
				TabHeaderBackgroundColor = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.TabHeaderBackgroundColor);
			}
			else
			{
				if (IsAllTab)
				{
					var divAllTabPage = new HtmlGenericControl("div");
					divTabContentContainer.Controls.Add(divAllTabPage);

					var oMyData = new MyTabTrackingData();
					oMyData.Key = "All";
					oMyData.Target = divAllTabPage;

					var liAllTabElement = CreateTabItemHeader("All", "All", IsAllTabSelected, oMyData, divAllTabPage.ClientID);

					myListTabTracking.Add(oMyData);

					divTabHeaderList.Controls.Add(liAllTabElement);
				}
			}
		}

		public void ApplyTabSelection(string tabId)
		{
			var iCnt = 0;

			// this loop will set value for hidden  field and this value will be used on javascript for forming active tab
			foreach (var item in myListTabTracking)
			{
				if (item.Key == tabId)
				{
					break;
				}
				iCnt++;
			}

			// even if no match then set it as it will be the case for "All"
			hidtab.Value = iCnt.ToString();

			hidTabValue.Value = tabId;
		}

		public void AddHorizontalTabDetailItem(int iTabIndex, Control detailControl)
		{
			if (detailControl != null)
			{
				myListTabTracking[iTabIndex].Target.Controls.Add(detailControl);
			}
		}

		public void AddTab(string tabLabelName, Control detailControl = null, string tabHeader = "", bool isTabSelected = false)
		{
			if (TabOrientation == TabOrientation.Horizontal)
			{
				AddHorizontalTab(tabLabelName, detailControl, tabHeader, isTabSelected);
			}
			else
			{
				var isOpen = false;
				//var strIds = hdnLastOpenTab.Value;
				//if (!string.IsNullOrEmpty(strIds))
				//{
				//    var ids = strIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
				//    if (ids.Contains(tabId))
				//    {
				//        isOpen = true;
				//    }
				//}
				AddVerticalTab(tabLabelName, detailControl, tabHeader, isOpen);
			}
		}

		public void Reload()
		{
			//BeginLogMethod(logger);

			if (!SessionVariables.IsTesting || TabOrientation == TabOrientation.Horizontal)
			{
				// Show Data
				foreach (var item in myListTabTracking)
				{
					if (item.Target.Controls.Count > 0 && item.Target.Controls[0] is ListControl)
					{
						((ListControl)item.Target.Controls[0]).ShowData(false, true);
					}
				}

				//for (var i = 0; i < divTabContentContainer.Controls.Count; i++)
				//{
				//	try
				//	{
				//		if (divTabContentContainer.Controls[i].Controls[1].Controls[0] is Shared.UI.Web.Controls.ListControl)
				//		{
				//			((Shared.UI.Web.Controls.ListControl)divTabContentContainer.Controls[i].Controls[1].Controls[0]).ShowData(false, true);
				//		}
				//		else if (divTabContentContainer.Controls[i].Controls[1].Controls[0] is DetailsWithChildrenControl)
				//		{
				//			if (!string.IsNullOrEmpty(SettingCategory))
				//			{
				//				((DetailsWithChildrenControl)divTabContentContainer.Controls[i].Controls[1].Controls[0]).ShowData(false, true, false, String.Empty);
				//			}
				//			else
				//			{
				//				((DetailsWithChildrenControl)divTabContentContainer.Controls[i].Controls[1].Controls[0]).ShowData(false, true);
				//			}
				//		}
				//	}
				//	catch { }
				//}
			}
			else
			{
				for (var i = 0; i < divTabContentContainer.Controls.Count; i++)
				{
					try
					{
						if (divTabContentContainer.Controls[i].Controls.Count > 0 && divTabContentContainer.Controls[i].Controls[0] is VerticalTabChildControl)
						{
							var cntrl = (VerticalTabChildControl)divTabContentContainer.Controls[i].Controls[0];

							if (cntrl.ChildGenericControl is DetailsWithChildrenControl)
							{
								if (!string.IsNullOrEmpty(SettingCategory))
								{
									((DetailsWithChildrenControl)divTabContentContainer.Controls[i].Controls[1].Controls[0]).ShowData(false, true, false, String.Empty);
								}
								else
								{
									((DetailsWithChildrenControl)divTabContentContainer.Controls[i].Controls[1].Controls[0]).ShowData(false, true);
								}
							}
							else if (cntrl.ChildGenericControl is ListControl)
							{
								((ListControl)cntrl.ChildGenericControl).ShowData(false, true);
							}
						}
					}
					catch { }
				}
			}

			//EndLogMethod(logger);
		}

		public void ApplyFieldConfigurationMode(string fcMode, string fcModeText)
		{
			if (!SessionVariables.IsTesting || TabOrientation == TabOrientation.Horizontal)
			{

				// ApplyFieldConfigurationMode
				foreach (var item in myListTabTracking)
                {
                    if (item.Target.Controls.Count > 0)
                    {
                        if (item.Target.Controls[0] is ListControl)
                        {
                            ((ListControl)item.Target.Controls[0]).ApplyFieldConfigurationMode(fcMode, fcModeText);
                        }
                        else if (item.Target.Controls.Count > 1 && item.Target.Controls[1] is ListControl)
                        {
                            ((ListControl)item.Target.Controls[1]).ApplyFieldConfigurationMode(fcMode, fcModeText);
                        }
                    }
				}
			}
			else
			{
				for (var i = 0; i < divTabContentContainer.Controls.Count; i++)
				{
					
					if (divTabContentContainer.Controls[i].Controls.Count <= 0) continue;

					var oCtrl = divTabContentContainer.Controls[i].Controls[0] as VerticalTabChildControl;

					if (oCtrl != null)
					{

						var ctlList = oCtrl.Controls[0] as ListControl;

						if (ctlList != null)
						{
							ctlList.ApplyFieldConfigurationMode(fcMode, fcModeText);
						}
					}
					
				}
			}
		}

		#endregion

		#region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			//BeginLogMethod(logger);

			if (!SessionVariables.IsTesting || TabOrientation == TabOrientation.Horizontal)
			{
				if (!string.IsNullOrEmpty(LastSelectedTab))
				{
					ApplyTabSelection(LastSelectedTab);
				}

				if (Parent != null && Parent.Parent != null && Parent.Parent is GroupList)
				{
					if (string.IsNullOrEmpty(LastSelectedTab) && myListTabTracking.Count > 0)
					{
						LastSelectedTab = myListTabTracking[0].Key;
						if (string.IsNullOrEmpty(((GroupList)Parent.Parent).LastOpenGroupedTab))
						{
							((GroupList)Parent.Parent).LastOpenGroupedTab = LastSelectedTab;
						}
					}
				}

				foreach (var item in myListTabTracking)
				{
					if (item.Target.Visible)
					{
						try
						{
							if (item.Target.Controls[1] is DetailsWithChildrenControl)
							{
								if (!string.IsNullOrEmpty(SettingCategory))
								{
									((DetailsWithChildrenControl)item.Target.Controls[1]).ShowData(false, true, false, String.Empty);
								}
								else
								{
									((DetailsWithChildrenControl)item.Target.Controls[0]).ShowData(false, true);
								}
							}
							else if (item.Target.Controls[1] is ListControl)
							{
								((ListControl)item.Target.Controls[1]).ShowData(false, true);
							}
						}
						catch { }
					}
				}
			}
			else
			{
				// case for Vertical Tab, will be reviewed once Horizontal is working correctly
				for (var i = 0; i < divTabContentContainer.Controls.Count; i++)
				{
					try
					{
						if (divTabContentContainer.Controls[i].Controls.Count > 0 && divTabContentContainer.Controls[i].Controls[0] is VerticalTabChildControl)
						{
							var cntrl = (VerticalTabChildControl)divTabContentContainer.Controls[i].Controls[0];

							if (cntrl.ChildGenericControl is DetailsWithChildrenControl)
							{
								if (!string.IsNullOrEmpty(SettingCategory))
								{
									((DetailsWithChildrenControl)divTabContentContainer.Controls[i].Controls[1].Controls[0]).ShowData(false, true, false, String.Empty);
								}
								else
								{
									((DetailsWithChildrenControl)divTabContentContainer.Controls[i].Controls[1].Controls[0]).ShowData(false, true);
								}
							}
							else if (cntrl.ChildGenericControl is ListControl)
							{
								((ListControl)cntrl.ChildGenericControl).ShowData(false, true);
							}
						}
					}
					catch { }
				}
			}

			//EndLogMethod(logger);
		}

		private void TabNavButton_Click(object sender, EventArgs e)
		{
			//BeginLogMethod(logger);

			var tabId = ((LinkButton)sender).CommandArgument;

			ApplyTabSelection(tabId);

			LastSelectedTab = tabId;

			if (Parent != null && Parent.Parent != null && Parent.Parent is GroupList)
			{
				((GroupList)Parent.Parent).LastOpenGroupedTab = LastSelectedTab;
			}

			// go thru the collection to match the selected tab
			foreach (var item in myListTabTracking)
			{
				if (tabId != "All")
				{
					if (item.Key == tabId)
					{
						// make selected item/tab page visible
						item.Target.Visible = true;

						var isListExists = false;

						// make the tab header label visible for "single tab" visible case
						foreach (Control detailControl in item.Target.Controls)
						{
							if (detailControl.ID != null && detailControl.ID.Contains("divTabPageHeader"))
							{
								((HtmlGenericControl)detailControl).Style["display"] = "none";
							}

							if (detailControl is ListControl)
							{
								isListExists = true;
							}
						}

						// check if parent is Group List
						if (Parent != null && Parent.Parent != null && Parent.Parent is GroupList)
						{
							if (!isListExists)
							{
								// if no tab (apart from hidden tab header label) is added, add last selected tab/list
								((GroupList)Parent.Parent).AddListControlForChangedTab(LastSelectedTab, item.Target);
							}
						}

						// we need this check b'coz we add null controls too in case of IfOnlyBindActive false
						foreach (Control detailControl in item.Target.Controls)
						{
							if (detailControl is ListControl)
							{
								((ListControl)detailControl).ShowData(false, true);
							}
							else if (detailControl is DetailsWithChildrenControl)
							{
								if (!string.IsNullOrEmpty(SettingCategory))
								{
									((DetailsWithChildrenControl)detailControl).ShowData(false, true, false, String.Empty);
								}
								else
								{
									((DetailsWithChildrenControl)detailControl).ShowData(false, true);
								}
							}
						}
					}
					else
					{
						// we need this check b'coz we add null controls too in case of IfOnlyBindActive false
						if (item.Target.Controls.Count > 0)
						{
							// this case is for hiding other tabs details controls if it is not selected
							// item.Target.Controls[0].Visible = false;
						}
					}
				}
				else    // case if All is clicked
				{
					// make every item/tab page visible
					item.Target.Visible = true;
					try
					{
						var isListExists = false;

						// make the tab header label visible for "all" visible case
						foreach (Control detailControl in item.Target.Controls)
						{
							if (detailControl.ID.Contains("divTabPageHeader"))
							{
								((HtmlGenericControl)detailControl).Style["display"] = "block";
							}

							if (detailControl is ListControl)
							{
								isListExists = true;
							}
						}

						// check if parent is Group List
						if (Parent != null && Parent.Parent != null && Parent.Parent is GroupList)
						{
							if (!isListExists)
							{
								// if no tab (apart from hidden tab header label) is added, add last selected tab/list
								((GroupList)Parent.Parent).AddListControlForChangedTab(item.Key, item.Target);
							}
						}

						// make the tab header label visible for "all" visible case
						foreach (Control detailControl in item.Target.Controls)
						{
							if (detailControl is ListControl)
							{
								((ListControl)detailControl).ShowData(false, true);
							}
							else if (detailControl is DetailsWithChildrenControl)
							{
								if (!string.IsNullOrEmpty(SettingCategory))
								{
									((DetailsWithChildrenControl)detailControl).ShowData(false, true, false, String.Empty);
								}
								else
								{
									((DetailsWithChildrenControl)detailControl).ShowData(false, true);
								}
							}
						}
					}
					catch { }
				}
			}

			//EndLogMethod(logger);
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			// set the client ids
			foreach (var item in myListTabTracking)
			{
				// in case of vertical tab
				if (item.Link != null)
				{
					item.Link.Attributes["href"] = "#" + item.Target.ClientID;
				}
			}
		}

		#endregion
	}
}


//public HtmlGenericControl SetupHelper(string tabId, string LastSelectedTab, bool isTabSelected, bool IsAllTabSelected, string contentClientId = "", string parentPath = "")
//{
//	var item = new HtmlGenericControl("li");
//	item.ID = "li" + tabId;

//	// highlight the section background
//	if (string.IsNullOrEmpty(LastSelectedTab) && isTabSelected && !IsAllTabSelected)
//	{
//		item.Attributes.CssStyle.Add("background-color", "Yellow");
//	}
//	else if (LastSelectedTab == tabId)
//	{
//		item.Attributes.CssStyle.Add("background-color", "Yellow");
//	}

//	var aLink = new HtmlGenericControl("a");
//	//aLink.HRef = ResolveUrl(".") + "#" + contentClientId;
//	//aLink.Attributes.Add("href", parentPath + "#" + contentClientId);
//	aLink.Attributes.Add("href", "#" + contentClientId);
//	//aLink.HRef = "#" + contentClientId;
//	aLink.InnerHtml = "<span>CL-" + tabId + "</span>";

//	item.Controls.Add(aLink);

//	return item;
//}