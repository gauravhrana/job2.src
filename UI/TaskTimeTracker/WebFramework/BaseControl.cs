using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;

namespace Shared.UI.WebFramework
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:BaseControl runat=server></{0}:BaseControl>")]
	public class BaseControl : System.Web.UI.UserControl
	{
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				var s = (String)ViewState["Text"];
				return ((s == null) ? String.Empty : s);
			}

			set
			{
				ViewState["Text"] = value;
			}
		}

		//protected override void RenderContents(HtmlTextWriter output)
		//{
		//    output.Write(Text);
        //}

        #region Properties

		protected string PrimaryEntityKey { get; set; }
		protected string FolderLocationFromRoot { get; set; }

		public string SettingCategory
		{
			get
			{
				return Convert.ToString(ViewState["SettingCategory"]);
			}
			set
			{
				ViewState["SettingCategory"] = value;
			}
		}

        public string Title
        {
            get
            {
                return Convert.ToString(ViewState["ControlTitle"]);
            }
            set
            {
                ViewState["ControlTitle"] = value;
            }
        }

        #endregion

        #region Methods

        protected void BeginLogMethod(log4net.ILog logger)
		{
			log4net.MDC.Set("StackTrace", Environment.StackTrace);

			logger.Info(DevelopementHelper.GetMyMethodName(2) + " Start");
		}

		protected void EndLogMethod(log4net.ILog logger)
		{
			log4net.MDC.Set("StackTrace", String.Empty);
			logger.Info(DevelopementHelper.GetMyMethodName(2) + " End");
		}

		protected virtual void SaveSettings()
		{
			//
		}

		protected virtual void GetSettings()
		{
			//
		}

		protected virtual void GetDefaultSettings()
		{
			//
        }

        #endregion

    }
}
