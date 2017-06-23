using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Styles
{
	public partial class StyleGrid : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		public string GetGridRowBackColor()
		{
			var gridRowBackColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleBackColor);
			return gridRowBackColor;
		}

		public string GetGridRowHeight()
		{
			var gridRowHeight = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleHeight);
			return gridRowHeight;
		}

		public string GetGridRowForeColor()
		{
			var gridRowForeColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleForeColor);
			return gridRowForeColor;
		}

		public string GetGridRowFontSize()
		{
			var gridRowFontSize = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleFontSize);
			return gridRowFontSize;
		}

		public string GetGridHeaderBackColor()
		{
			var gridHeaderBackColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderBackColor);
			return gridHeaderBackColor;
		}

		public string GetGridHeaderHeight()
		{
			var gridHeaderHeight = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderStyleHeight);
			return gridHeaderHeight;
		}

		public string GetGridHeaderForeColor()
		{
			var gridHeaderForeColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderForeColor);
			return gridHeaderForeColor;
		}

		public string GetGridHeaderFontSize()
		{
			var gridHeaderFontSize = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderStyleFontSize);
			return gridHeaderFontSize;
		}
		public string GetGridAlternatingRowFontSize()
		{
			var gridAlternatingRowFontSize = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleFontSize);
			return gridAlternatingRowFontSize;
		}
		public string GetGridAlternatingRowHeight()
		{
			var gridAlternatingRowHeight = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleHeight);
			return gridAlternatingRowHeight;
		}

		public string GetGridAlternatingRowBackColor()
		{
			var gridAlternatingRowBackColor = PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleBackColor);
			return gridAlternatingRowBackColor;
		}		
	}
}