using Framework.Components.LogAndTrace;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Framework.UI.Web.BaseClasses
{

    public class PageDefaultMaster : System.Web.UI.MasterPage
    {        

		public virtual string TableName { get; set; }

		public virtual Menu MainMenu { get; set; }

		public virtual Shared.UI.Web.Controls.SubMenu.SubMenu SubMenuObject { get; set; }

        public virtual Shared.UI.Web.Controls.BreadCrumb.BreadCrumb BreadCrumbObject { get; set; }
        
        public virtual void Setup(string tableName) { }

        protected void StartupCheck()
        {
            if (!SessionVariables.StartupChecked)
            {

                SessionVariables.StartupChecked = true;

                var startupApplicationId = WebApplicationUser.GetStartupApplicationId();
                if (startupApplicationId == 0)
                {
                    startupApplicationId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["StartupApplicationId"]);
                }

                if (SessionVariables.RequestProfile.ApplicationId != startupApplicationId)
                {
                    if (SessionVariables.RequestProfile.ApplicationId != startupApplicationId)
                    {
                        if (startupApplicationId == 100)
                        {
                            Response.Redirect("~/PMT/PMT/Home");
                        }
                        else if (startupApplicationId == 100047)
                        {
                            Response.Redirect("~/TE/TE/Home");
                        }
                        else if (startupApplicationId == 100065)
                        {
                            Response.Redirect("~/PDTMGMDEVT/PDTMGMDEVT/Home");
                        }
                        else if (startupApplicationId == 100066)
                        {
                            Response.Redirect("~/SA/SA/Home");
                        }
                        else if (startupApplicationId == 100067)
                        {
                            Response.Redirect("~/Prototype/Prototype/Home");
                        }
                        else if (startupApplicationId == 200)
                        {
                            Response.Redirect("~/DayCare/Home");
                        }
                        else if (startupApplicationId == 100068)
                        {
                            Response.Redirect("~/CapitalMarkets/Home");
                        }
                        else if (startupApplicationId == 100070)
                        {
                            Response.Redirect("~/ReferenceData/Home");
                        }
                        else if (startupApplicationId == 100072)
                        {
                            Response.Redirect("~/Legal/Home");
                        }
                    }
                }

            }
        }

        protected void AddUserLoginHistoryRecord()
        {
            var data        = new UserLoginHistoryDataModel();

            data.UserId     = SessionVariables.RequestProfile.AuditId;
            data.UserName   = ApplicationCommon.GetApplicationUserName();
            data.URL        = Page.AppRelativeVirtualPath;
            data.ServerName = Environment.MachineName;

            if (ApplicationCommon.ApplicationCache.ContainsKey(SessionVariables.SystemRequestProfile.ApplicationId))
            {
                data.Application = ApplicationCommon.ApplicationCache[SessionVariables.SystemRequestProfile.ApplicationId].Name;
            }

            UserLoginHistoryDataManager.Create(data, SessionVariables.RequestProfile);
        }

        #region Events

        protected override void OnInit(EventArgs e)
        {
            var currentPage = HttpContext.Current.CurrentHandler as Page;
            var urlAppCode = Convert.ToString(currentPage.RouteData.Values["applicationCode"]);
            var urlModuleCode = Convert.ToString(currentPage.RouteData.Values["applicationModule"]);

            if (!string.IsNullOrEmpty(urlAppCode) && !string.IsNullOrEmpty(urlModuleCode))
            {
                if (urlAppCode != SessionVariables.CurrentApplicationCode)
                {
                    ApplicationCommon.ResetApplicationCache(urlAppCode);
                }
                else if (urlModuleCode != SessionVariables.CurrentApplicationModuleCode)
                {
                    ApplicationCommon.ResetApplicationModuleCache(urlModuleCode);
                }
            }
        }

        #endregion

    }

}