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
			var gridRowBackColor = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleBackColor);
			return gridRowBackColor;
		}

		public string GetGridRowHeight()
		{
			var gridRowHeight = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleHeight);
			return gridRowHeight;
		}

		public string GetGridRowForeColor()
		{
			var gridRowForeColor = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleForeColor);
			return gridRowForeColor;
		}

		public string GetGridRowFontSize()
		{
			var gridRowFontSize = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesRowStyleFontSize);
			return gridRowFontSize;
		}

		public string GetGridHeaderBackColor()
		{
			var gridHeaderBackColor = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderBackColor);
			return gridHeaderBackColor;
		}

		public string GetGridHeaderHeight()
		{
			var gridHeaderHeight = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderStyleHeight);
			return gridHeaderHeight;
		}

		public string GetGridHeaderForeColor()
		{
			var gridHeaderForeColor = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderForeColor);
			return gridHeaderForeColor;
		}

		public string GetGridHeaderFontSize()
		{
			var gridHeaderFontSize = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesHeaderStyleFontSize);
			return gridHeaderFontSize;
		}
		public string GetGridAlternatingRowFontSize()
		{
			var gridAlternatingRowFontSize = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleFontSize);
			return gridAlternatingRowFontSize;
		}
		public string GetGridAlternatingRowHeight()
		{
			var gridAlternatingRowHeight = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleHeight);
			return gridAlternatingRowHeight;
		}

		public string GetGridAlternatingRowBackColor()
		{
			var gridAlternatingRowBackColor = PerferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.ReleaseNotesAlternatingRowStyleBackColor);
			return gridAlternatingRowBackColor;
		}		
	}
}