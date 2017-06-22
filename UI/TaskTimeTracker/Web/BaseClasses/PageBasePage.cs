using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Framework.UI.Web.BaseClasses
{
    public class PageBasePage : Shared.UI.WebFramework.BasePage
    {
		protected Shared.UI.Web.Controls.BreadCrumb.BreadCrumb BreadCrumbObject { get; set; }

        protected void SetSiteMasterPagePath()
        {
            if (this.MasterPageFile.IndexOf("Site.Master", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                switch (SessionVariables.RequestProfile.ApplicationId)
                {
                    case 100047:
                        this.MasterPageFile = "~/MasterPages/Schedule/Site.master";
                        break;
                    case 100065:
                        this.MasterPageFile = "~/MasterPages/PMT/Site.master";
                        break;
                    case 100066:
                        this.MasterPageFile = "~/MasterPages/SA/Site.master";
                        break;
                    case 100067:
                        this.MasterPageFile = "~/MasterPages/Prototype/Site.master";
                        break;
                    case 100070:
                        this.MasterPageFile = "~/MasterPages/ReferenceData/Site.master";
                        break;
                    case 100068:
                        this.MasterPageFile = "~/MasterPages/CM/Site.master";
                        break;
                    case 100072:
                        this.MasterPageFile = "~/MasterPages/Legal/Site.master";
                        break;
                    default:
                        this.MasterPageFile = "~/MasterPages/Site.master";
                        break;
                }
            }
            else if (this.MasterPageFile.IndexOf("Default.Master", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                switch (SessionVariables.RequestProfile.ApplicationId)
                {
                    case 100047:
                        this.MasterPageFile = "~/MasterPages/Schedule/Default.master";
                        break;
                    case 100065:
                        this.MasterPageFile = "~/MasterPages/PMT/Default.master";
                        break;
                    case 100066:
                        this.MasterPageFile = "~/MasterPages/SA/Default.master";
                        break;
                    case 100067:
                        this.MasterPageFile = "~/MasterPages/Prototype/Default.master";
                        break;
                    case 100070:
                        this.MasterPageFile = "~/MasterPages/ReferenceData/Default.master";
                        break;
                    case 100068:
                        this.MasterPageFile = "~/MasterPages/CM/Default.master";
                        break;
                    case 100072:
                        this.MasterPageFile = "~/MasterPages/Legal/Default.master";
                        break;
                    default:
                        this.MasterPageFile = "~/MasterPages/Default.master";
                        break;
                }
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            SetSiteMasterPagePath();

            base.OnPreInit(e);
        }

    }
}