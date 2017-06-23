using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using DataModel.Framework.Core;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin
{
	public partial class SearchSettings : Framework.UI.Web.BaseClasses.PageBasePage
    {
        string SettingCategory = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["EN"] != null)
                {

                    var entityName = Request.QueryString["EN"].ToString();
                    var upSearchCategory = entityName + "DefaultViewSearchControl";
					lblEntity.Text = entityName;	
                    SettingCategory = upSearchCategory;
                    SetUpStandardSettings(SettingCategory);
                    var data = new UserPreferenceCategoryDataModel();
                    var updata = new UserPreferenceDataModel();
                    data.Name = upSearchCategory;
					var obj = Framework.Components.UserPreference.UserPreferenceCategoryDataManager.GetDetails(data, SessionVariables.RequestProfile);
                    if (obj != null)
                    {
                        updata.UserPreferenceCategoryId = obj.UserPreferenceCategoryId.Value;
                        updata.ApplicationUserId = SessionVariables.RequestProfile.AuditId;
                    }
					var updt = UserPreferenceDataManager.GetEntityDetails(updata, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);
                    SearchSettingsRepeater.DataSource = updt;
                    SearchSettingsRepeater.DataBind();

                    hprEntity.Text = entityName;
                    hprEntity.NavigateUrl = Page.GetRouteUrl(entityName + "EntityRoute", null); 

                }

            }
        }

        private void SetUpStandardSettings(string settingCategory)
        {
            var wcpostfix = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.WildCardSearchPostfix, settingCategory);
            var wcprefix = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.WildCardSearchPrefix, settingCategory);
            var autosearchon = PreferenceUtility.GetUserPreferenceByKey(ApplicationCommon.WildCardSearchPrefix, settingCategory);

        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            var entityName = Request.QueryString["EN"].ToString();
            var returnUrl = Page.GetRouteUrl(entityName + "EntityRoute", null); 
            if (string.IsNullOrEmpty(entityName))
            {
                var data = new MenuDataModel();
                data.Name = Request.QueryString["EN"].ToString();
				var dt = Framework.Components.Core.MenuDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
                if (dt.Count > 0)
                {
                    for (var i = 0; i < dt.Count; i++)
                    {
                        if (!dt[i].NavigateURL.Equals("#"))
                        {
                            returnUrl = dt[i].NavigateURL;
                        }
                    }
                }
                else
                {
                    if (!dt[0].NavigateURL.Equals("#"))
                    {
                        returnUrl = dt[0].NavigateURL;
                    }
                }
            }
            Response.Redirect(returnUrl);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SettingCategory = Request.QueryString["EN"].ToString() + "DefaultViewSearchControl";
            for (var i = 0; i < SearchSettingsRepeater.Items.Count; i++)
            {
                if (SearchSettingsRepeater.Items[i].ItemType == ListItemType.Item ||
                    SearchSettingsRepeater.Items[i].ItemType == ListItemType.AlternatingItem)
                {
                    var label = (Label)SearchSettingsRepeater.Items[i].FindControl("lblKey");
                    var txtbox = (TextBox)SearchSettingsRepeater.Items[i].FindControl("txtValue");

                    if (label != null && txtbox != null)
                    {
                        PreferenceUtility.UpdateUserPreference
                        (
                            SettingCategory
                            , label.Text
                            , txtbox.Text
                        );
                    }
                }

            }

        }
    }
}