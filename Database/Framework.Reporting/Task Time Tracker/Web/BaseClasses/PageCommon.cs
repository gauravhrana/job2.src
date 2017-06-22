using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Framework.UI.Web.BaseClasses
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:WebCustomControl1 runat=server></{0}:WebCustomControl1>")]
    public class PageCommon : Shared.UI.WebFramework.BasePage
    {
        protected PlaceHolder PrimaryPlaceHolder { get; set; }
        protected string GenericControlPath { get; set; }
        protected string DetailsControlPath { get; set; }
        protected SystemEntity PrimaryEntity { get; set; }
        protected string PrimaryEntityKey { get; set; }
        protected string ViewName { get; set; }
        protected Shared.UI.Web.Controls.BreadCrumb.BreadCrumb BreadCrumbObject { get; set; }

        #region Methods

        protected void AddUpdateControl(bool isSingleRecord, int entityKey)
        {
            var updateControl = (ControlGeneric)Page.LoadControl(GenericControlPath);

            if (isSingleRecord)
            {
                PrimaryPlaceHolder.Controls.Add(GetTabControl(SetId, updateControl));
            }
            else
            {
                PrimaryPlaceHolder.Controls.Add(updateControl);
                updateControl.SetBorderClass(ApplicationCommon.DetailsBorderClassName);
                //updateControl.Controls.Add(new LiteralControl("<br />"));
            }

            updateControl.PrimaryEntity = PrimaryEntity;
            updateControl.SetId(entityKey, false);
        }

        protected void AddDetailControl(bool isSingleRecord, int entityKey)
        {
            var detailsControl = (ControlDetails)Page.LoadControl(DetailsControlPath);

            if (isSingleRecord)
            {
                PrimaryPlaceHolder.Controls.Add(GetTabControl(SetId, detailsControl));
            }
            else
            {
                PrimaryPlaceHolder.Controls.Add(detailsControl);
                detailsControl.SetBorderClass(ApplicationCommon.DetailsBorderClassName);
                PrimaryPlaceHolder.Controls.Add(new LiteralControl("<br />"));
            }

            detailsControl.PrimaryEntity = PrimaryEntity;
            detailsControl.SetId = entityKey;
        }

        protected virtual Control GetTabControl(int setId, Control detailsControl)
        {
            return detailsControl;
        }

        protected void Redirect(string action)
        {
            if (!string.IsNullOrEmpty(SuperKey))
            {
                Response.Redirect(Page.GetRouteUrl(PrimaryEntityKey + "EntityRouteSuperKey", new { Action = action, SuperKey = SuperKey }), false);
            }
            else
            {
                Response.Redirect(Page.GetRouteUrl(PrimaryEntityKey + "EntityRoute", new { Action = action, SetId = SetId }), false);
            }
        }

        protected void SetDefaultMasterPagePath()
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
                case 200:
                    this.MasterPageFile = "~/MasterPages/DayCare/Default.master";
                    break;
                default:
                    this.MasterPageFile = "~/MasterPages/Default.master";
                    break;
            }
        }

        protected void SetSiteMasterPagePath()
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
                case 200:
                    this.MasterPageFile = "~/MasterPages/DayCare/Site.master";
                    break;
                default:
                    this.MasterPageFile = "~/MasterPages/Site.master";
                    break;
            }
        }

        #endregion

        #region Events

        //protected override void OnPreInit(EventArgs e)
        //{
        //	base.OnPreInit(e);
        //
        //	//Log4Net.LogDebug("Page Pre Init");
        //}

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (string.IsNullOrEmpty(SettingCategory))
            {
                SettingCategory = PrimaryEntityKey + "DefaultView";
            }
            BreadCrumbObject.SettingCategory = SettingCategory + "BreadCrumbControl";
            BreadCrumbObject.Setup(ViewName);
            BreadCrumbObject.GenerateMenu();
        }

        protected virtual void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Page.GetRouteUrl(PrimaryEntity + "EntityRouteSearch", new { }), false);
        }

        protected virtual void chkVisible_CheckedChanged(object sender, EventArgs e) { }

        protected virtual void btnBack_Click(object sender, EventArgs e) { }

        protected virtual void btnUpdate_Click(object sender, EventArgs e) { }

        #endregion

    }
}