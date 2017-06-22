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
					var upcdt = Framework.Components.UserPreference.UserPreferenceCategoryDataManager.Search(data, SessionVariables.RequestProfile);
                    if (upcdt.Rows.Count == 1)
                    {
                        updata.UserPreferenceCategoryId = Convert.ToInt32(
                            upcdt.Rows[0][UserPreferenceCategoryDataModel.DataColumns.UserPreferenceCategoryId].ToString());
                        updata.ApplicationUserId = SessionVariables.RequestProfile.AuditId;
                    }
					var updt = UserPreferenceDataManager.Search(updata, SessionVariables.RequestProfile);
                    SearchSettingsRepeater.DataSource = updt.DefaultView;
                    SearchSettingsRepeater.DataBind();

                    hprEntity.Text = entityName;
                    hprEntity.NavigateUrl = Page.GetRouteUrl(entityName + "EntityRoute", null); 

                }

            }
        }

        private void SetUpStandardSettings(string settingCategory)
        {
            var wcpostfix = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.WildCardSearchPostfix, settingCategory);
            var wcprefix = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.WildCardSearchPrefix, settingCategory);
            var autosearchon = PerferenceUtility.GetUserPreferenceByKey(ApplicationCommon.WildCardSearchPrefix, settingCategory);

        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            var entityName = Request.QueryString["EN"].ToString();
            var returnUrl = Page.GetRouteUrl(entityName + "EntityRoute", null); 
            if (string.IsNullOrEmpty(entityName))
            {
                var data = new MenuDataModel();
                data.Name = Request.QueryString["EN"].ToString();
				var dt = Framework.Components.Core.MenuDataManager.Search(data, SessionVariables.RequestProfile);
                if (dt.Rows.Count > 0)
                {
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        if (!dt.Rows[i][MenuDataModel.DataColumns.NavigateURL].ToString().Equals("#"))
                        {
                            returnUrl = dt.Rows[i][MenuDataModel.DataColumns.NavigateURL].ToString();
                        }
                    }
                }
                else
                {
                    if (!dt.Rows[0][MenuDataModel.DataColumns.NavigateURL].ToString().Equals("#"))
                    {
                        returnUrl = dt.Rows[0][MenuDataModel.DataColumns.NavigateURL].ToString();
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
                        PerferenceUtility.UpdateUserPreference
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