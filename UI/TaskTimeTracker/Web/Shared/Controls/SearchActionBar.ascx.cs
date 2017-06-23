using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.Web;
using System.Web.Routing;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.Controls
{
    public partial class SearchActionBar : BaseControl
    {
        #region Variables

        public delegate int GetSaveDelegate();
        
        private GetSaveDelegate _saveSearchKey;

        public BaseControl ParentSearchControl;

        private string _entityName;

		public bool SearchDisplay;

        public string SearchBorderColor
        {
            get
            {
                if (ViewState["SearchBorderColor"] != null)
                {
                    return Convert.ToString(ViewState["SearchBorderColor"]);
                }
                return String.Empty;
            }
            set
            {
                ViewState["SearchBorderColor"] = value;
            }
        }

        public string SearchBorderStyle
        {
            get
            {
                if (ViewState["SearchBorderStyle"] != null)
                {
                    return Convert.ToString(ViewState["SearchBorderStyle"]);
                }
                return String.Empty;
            }
            set
            {
                ViewState["SearchBorderStyle"] = value;
            }
        }

		public string SearchBackgroundColor
		{
			get
			{
				if (ViewState["SearchBackgroundColor"] != null)
				{
					return Convert.ToString(ViewState["SearchBackgroundColor"]);
				}
				return String.Empty;
			}
			set
			{
				ViewState["SearchBackgroundColor"] = value;
			}
		}

		public string SearchForegroundColor
		{
			get
			{
				if (ViewState["SearchForegroundColor"] != null)
				{
					return Convert.ToString(ViewState["SearchForegroundColor"]);
				}
				return String.Empty;
			}
			set
			{
				ViewState["SearchForegroundColor"] = value;
			}
		}

		public string SearchFontFamily
		{
			get
			{
				if (ViewState["SearchFontFamily"] != null)
				{
					return Convert.ToString(ViewState["SearchFontFamily"]);
				}
				return String.Empty;
			}
			set
			{
				ViewState["SearchFontFamily"] = value;
			}
		}

		public string SearchFontSize
		{
			get
			{
				if (ViewState["SearchFontSize"] != null)
				{
					return Convert.ToString(ViewState["SearchFontSize"]);
				}
				return String.Empty;
			}
			set
			{
				ViewState["SearchFontSize"] = value;
			}
		}

		public bool SampleDisplay
		{
			get
			{
				return SearchDisplay;
			}
			set
			{
				SearchDisplay = value;
			}
		}

        public ImageButton PanelImage
        {
            get
            {
                return Image1;
            }
        }

        #endregion

        #region Methods

        public void Setup(string entityName)
        {
            hypSettings.NavigateUrl = "~/Shared/Admin/SearchSettings.aspx?EN=" + entityName;
        }

        public void Setup(string entityName, GetSaveDelegate saveDelegate)
        {
            _entityName = entityName;
            hypSettings.NavigateUrl = "~/Shared/Admin/SearchSettings.aspx?EN=" + entityName;
            _saveSearchKey = saveDelegate;
        }

        public void Setup(string entityName, GetSaveDelegate saveDelegate, BaseControl parentControl)
        {
            _entityName = entityName;
            hypSettings.NavigateUrl = "~/Shared/Admin/SearchSettings.aspx?EN=" + entityName;
            _saveSearchKey = saveDelegate;
            ParentSearchControl = parentControl;            
        }

		public void GenerateSearchStyle()
		{
			if (!SearchDisplay)
			{
				SearchBorderColor		= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchBorderColor);
				SearchBorderStyle		= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchBorderStyle);
				SearchBackgroundColor	= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchBackgroundColor);
				SearchForegroundColor	= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchForegroundColor);
				SearchFontFamily		= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchFontFamily);
				SearchFontSize			= PreferenceUtility.GetApplicationUserPreferenceByKey(ApplicationCommon.SearchFontSize);
			}

			if (SearchBorderStyle == "Round")
			{
				Page.Header.Controls.Add(new LiteralControl(@"<style type='text/css'> 
				table.newSearchfilter td
				{
					padding: 3px;						
				} 
				
				table.newSearchfilter
				{
					font-weight: bold;
					width:100%;
					font-family:" + SearchFontFamily + ";font-size:" + SearchFontSize + ";" +
					"color:"+ SearchForegroundColor +";" +			
				"}"+
				
				".newHeaderClass"+
				"{"+
					"position: relative;"+
					"margin: 0px;"+
					"padding: 0px;"+
                    "background:" + SearchBackgroundColor + ";" +
					"width: 100%;"+		
				"}"+

				".newDivSearch"+
				"{"+
					"width: 100%;"+
                    "border:2px solid;" +
                    "border-bottom-left-radius:10px !important;" +
                    "border-bottom-right-radius:10px !important;" +
					"border-color:" + SearchBorderColor +"; " +
                    "border-collapse:collapse;" +
                "}	</style> "));				
			}

			if (SearchBorderStyle == "Box")
			{
				Page.Header.Controls.Add(new LiteralControl(@"<style type='text/css'> 
				table.newSearchfilter td
				{
						padding: 3px;						
				} 
				
				table.newSearchfilter
				{
					width:100%;					
					font-weight: bold;
					font-family:" + SearchFontFamily + ";font-size:" + SearchFontSize + ";" +
                    "border-bottom-left-radius:10px !important;" +
                    "border-bottom-right-radius:10px !important;" +
					"color:"+ SearchForegroundColor +";" +			
				"}"+

				".newHeaderClass" +
				"{" +
					"position: relative;" +
					"margin: 0px;" +
					"padding: 0px;" +
                    "background:" + SearchBackgroundColor + ";" +
					"width: 100%;" +
				"}" +
				
				".newDivSearch"+
				"{"+
					"width: 100%;"+
					"border:2px solid;"+					
					"border-color:" + SearchBorderColor + ";}	</style> "));

				
			}

			if (SearchBorderStyle == "None")
			{
				Page.Header.Controls.Add(new LiteralControl(@"<style type='text/css'> 
				table.newSearchfilter td
				{
						padding: 5px;						
				} 
				
				table.newSearchfilter
				{
					font-weight: bold;	
					font-family:" + SearchFontFamily + ";font-size:" + SearchFontSize + ";" +				
					"width:100%;color:"+ SearchForegroundColor +";" +			
				"}"+

				".newHeaderClass" +
				"{" +
					"position: relative;" +
					"margin: 0px;" +
					"padding: 0px;" +
					"background:" + SearchBackgroundColor + ";" +
					"width: 100%;" +
				"}" +

				".newDivSearch"+
				"{"+
					"width: 100%;"+
					"border:2px none;"+					
					"border-color:" + SearchBorderColor + ";}	</style> "));				
			}

		}

		public void SetUp()
		{
			//GenerateSearchStyle();

			//var tbl1 = (Table)Parent.FindControl("tblMain");
			//tbl1.CssClass = "newSearchfilter";

			//var dvSearch1 = (HtmlControl)Parent.FindControl("dvSearchFilter");
			//dvSearch1.Attributes["class"] = "newDivSearch";
			//trHeader.Attributes["class"] = "newHeaderClass";
		}

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
			//GenerateSearchStyle();

			var mstr = Parent.Page.Master as MasterPage;
			var dvSearch = mstr.FindControl("dvSearchFilter") as HtmlControl;

	        if (dvSearch != null)
	        {
				dvSearch.Attributes["class"] = "newDivSearch";
	        }

	        var tbl1 = Parent.FindControl("tblMain") as HtmlControl;

	        if (tbl1 != null)
	        {
				//tbl1.class = "newSearchfilter";
				//var dvSearch1 = (HtmlControl)Parent.FindControl("dvSearchFilter");
				//if (dvSearch1 != null)
				//	dvSearch1.Attributes["class"] = "newDivSearch";
	        }
	        
			trHeader.Attributes["class"] = "newHeaderClass";			
        }

        protected void lnkSearchKey_Click(object sender, EventArgs e)
        {
	        if (_saveSearchKey == null) return;

	        var searchKeyId = _saveSearchKey();

            var url = Page.GetRouteUrl(_entityName + "EntityRouteSearch", new { SearchKey = searchKeyId });

            Response.Redirect(url, false);
	        
            //Response.Redirect(Page.GetRouteUrl("SearchKeyLinkRoute", routeData), false);
        }

        protected void lnkClose_Click(object sender, EventArgs e)
        {
	        if (ParentSearchControl == null) return;

	        ParentSearchControl.Visible = false;
	        PreferenceUtility.UpdateUserPreference(ParentSearchControl.SettingCategory, ApplicationCommon.ControlVisible, "false");
        }
			
        #endregion

    }
}