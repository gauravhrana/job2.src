using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Shared.UI.Web.Controls.TabControl
{
	public class TabHelper
	{
		public static HtmlGenericControl SetupHelper(string tabId, string LastSelectedTab, bool isTabSelected, bool IsAllTabSelected, string contentClientId = "", string parentPath = "")
		{
			var item = new HtmlGenericControl("li");
			item.ID = "li" + tabId;

			// highlight the section background
			if (string.IsNullOrEmpty(LastSelectedTab) && isTabSelected && !IsAllTabSelected)
			{
				item.Attributes.CssStyle.Add("background-color", "Yellow");
			}
			else if (LastSelectedTab == tabId)
			{
				item.Attributes.CssStyle.Add("background-color", "Yellow");
			}

			var aLink = new HtmlAnchor();
			//aLink.HRef = ResolveUrl(".") + "#" + contentClientId;
			//aLink.Attributes.Add("href", parentPath + "#" + contentClientId);
			aLink.HRef = "#" + contentClientId;
			aLink.InnerHtml = "<span>CL-" + tabId + "</span>";

			item.Controls.Add(aLink);

			return item;
		}
	}
}