using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Collections;

namespace Shared.UI.Web.Admin.Controls
{
	public partial class UserSettings : Shared.UI.WebFramework.BaseControl
    {

        #region Methods

        private void BindKeys()
        {
            ddlUserPreferenceKey.Items.Add(ApplicationCommon.AutoSearchOn);
            ddlUserPreferenceKey.Items.Add(ApplicationCommon.TabOrientation);
            ddlUserPreferenceKey.Items.Add(ApplicationCommon.AllTabExists);
            ddlUserPreferenceKey.Items.Add(ApplicationCommon.AllTabSelected);
            ddlUserPreferenceKey.Items.Add(ApplicationCommon.DetailsAEFLModeEnabled);
            ddlUserPreferenceKey.Items.Add(ApplicationCommon.DetailsButtonPanelVisible);
            ddlUserPreferenceKey.Items.Add(ApplicationCommon.DetailsPagingEnabled);
        }

        private void BindGrid(string userPreferenceKey)
        {
            var result = new ArrayList();
            if (SessionVariables.UserPreferences == null)
            {
                PreferenceUtility.RefreshUserPreferencesCache();
            }

            var lst = SessionVariables.UserPreferences.FindAll(item => item.UserPreferenceKey == userPreferenceKey);
            if (lst != null && lst.Count > 0)
            {
                for (var i = 0; i < lst.Count; i++)
                {
                    result.Add(lst[i].UserPreferenceCategory);
                }
                CheckBoxList1.DataSource = result;
                CheckBoxList1.DataBind();
            }
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            BindKeys();
            BindGrid(ddlUserPreferenceKey.SelectedValue);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            //if (CheckBoxList1.Rows.Count > 0)
            //{
            //    for (var iCount = 0; iCount < CheckBoxList1.Rows.Count; iCount++)
            //    {
            //        var data = new UserPreferenceDataModel();
            //        data.UserPreferenceId = Convert.ToInt32(CheckBoxList1.DataKeys[iCount].Value);
            //        var chkChecked = CheckBoxList1.Rows[iCount].FindControl("chkChecked");
            //        if (chkChecked != null && ((CheckBox)chkChecked).Checked)
            //        {
            //            var txtValue = CheckBoxList1.Rows[iCount].FindControl("txtValue");
            //            if (txtValue != null)
            //            {
            //                data.Value = ((TextBox)txtValue).Text.Trim();
            //                UserPreferenceDataManager.UpdateValueOnly(data, SessionVariables.RequestProfile);
            //            }
            //        }
            //    }
            //    PreferenceUtility.RefreshUserPreferencesCache();
            //    Response.Redirect("~/Default.aspx");
            //}
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

		protected void ddlUserPreferenceKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid(ddlUserPreferenceKey.SelectedValue);
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            
            //if (CheckBoxList1.Rows.Count > 0)
            //{
            //    var chkAll = CheckBoxList1.HeaderRow.FindControl("chkSelectAll");
            //    if (chkAll != null)
            //    {
            //        var isAllChecked = ((CheckBox)chkAll).Checked;
            //        for (var iCount = 0; iCount < CheckBoxList1.Rows.Count; iCount++)
            //        {
            //            var chkChecked = CheckBoxList1.Rows[iCount].FindControl("chkChecked");
            //            if (chkChecked != null)
            //            {
            //                ((CheckBox)chkChecked).Checked = isAllChecked;
            //            }
            //        }
            //    }
            //}
        }

        #endregion

    }
}