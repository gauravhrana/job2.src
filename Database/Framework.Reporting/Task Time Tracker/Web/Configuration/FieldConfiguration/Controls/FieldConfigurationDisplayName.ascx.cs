using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.Configuration.FieldConfiguration.Controls
{

    public partial class FieldConfigurationDisplayName : Shared.UI.WebFramework.BaseControl
    {

        #region private variables

        private DataTable LanguageSource
        {
            get
            {
                if (ViewState["LanguageSource"] != null)
                {
                    return (DataTable)ViewState["LanguageSource"];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["LanguageSource"] = value;
            }
        }

        private int? FieldConfigurationId
        {
            get
            {
                if (ViewState["FieldConfigurationId"] != null)
                {
                    return Convert.ToInt32(ViewState["FieldConfigurationId"]);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["FieldConfigurationId"] = value;
            }
        }

        #endregion

        #region private methods

        public void Setup(int fieldConfigurationId)
        {
            this.FieldConfigurationId = fieldConfigurationId;
            bindGrid();
        }

        private void bindGrid()
        {
            var data = new FieldConfigurationDisplayNameDataModel();
            data.FieldConfigurationId = FieldConfigurationId;
			var dt = FieldConfigurationDisplayNameDataManager.Search(data, SessionVariables.RequestProfile, SessionVariables.ApplicationMode);
            dgvDisplayNames.DataSource = dt;
            dgvDisplayNames.DataBind();
        }

        private DataTable GetLanguageSource()
        {
			var dt = Framework.Components.Core.LanguageDataManager.GetList(SessionVariables.RequestProfile);
            return dt;
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                drpLanguage.DataSource = LanguageSource;
                drpLanguage.DataTextField = StandardDataModel.StandardDataColumns.Name;
                drpLanguage.DataValueField = LanguageDataModel.DataColumns.LanguageId;
                drpLanguage.DataBind();
            }
        }

        protected void dgvDisplayNames_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var row = (DataRowView)e.Row.DataItem;
                if (LanguageSource == null)
                {
                    LanguageSource = GetLanguageSource();
                }
                var drpLanguage = (DropDownList)e.Row.FindControl("drpLanguage");
                if (drpLanguage != null)
                {
                    drpLanguage.DataSource = LanguageSource;
                    drpLanguage.DataTextField = StandardDataModel.StandardDataColumns.Name;
                    drpLanguage.DataValueField = LanguageDataModel.DataColumns.LanguageId;
                    drpLanguage.DataBind();


                    drpLanguage.SelectedValue = Convert.ToString(row[FieldConfigurationDisplayNameDataModel.DataColumns.LanguageId]);
                }

                var chkIsDefault = (CheckBox)e.Row.FindControl("chkIsDefault");
                if (chkIsDefault != null)
                {
                    if (Convert.ToInt32(row[FieldConfigurationDisplayNameDataModel.DataColumns.IsDefault]) > 0)
                    {
                        chkIsDefault.Checked = true;
                    }
                }
            }
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            var data = new FieldConfigurationDisplayNameDataModel();
            data.FieldConfigurationId = FieldConfigurationId;
            data.LanguageId = Convert.ToInt32(drpLanguage.SelectedValue);
            data.Value = txtValue.Text.Trim();
            data.IsDefault = chkIsDefault.Checked ? 1 : 0;
            FieldConfigurationDisplayNameDataManager.Create(data, SessionVariables.RequestProfile);
            bindGrid();
            formContainer.Visible = false;
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            formContainer.Visible = false;
        }

        protected void lnkAddDisplayName_Click(object sender, EventArgs e)
        {
            formContainer.Visible = true;
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            if (dgvDisplayNames.Rows.Count > 0)
            {
                foreach (GridViewRow dr in dgvDisplayNames.Rows)
                {
                    var chkSelected = (CheckBox)dr.FindControl("chkSelected");
                    if (chkSelected != null)
                    {
                        if (chkSelected.Checked)
                        {
                            var data = new FieldConfigurationDisplayNameDataModel();
                            data.FieldConfigurationDisplayNameId = Convert.ToInt32(dgvDisplayNames.DataKeys[dr.DataItemIndex].Value);
                            FieldConfigurationDisplayNameDataManager.Delete(data, SessionVariables.RequestProfile);
                        }
                    }
                }
            }
            bindGrid();
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            if (dgvDisplayNames.Rows.Count > 0)
            {
                foreach (GridViewRow dr in dgvDisplayNames.Rows)
                {
                    var data = new FieldConfigurationDisplayNameDataModel();
                    data.FieldConfigurationDisplayNameId = Convert.ToInt32(dgvDisplayNames.DataKeys[dr.DataItemIndex].Value);

                    var chkIsDefault1 = (CheckBox)dr.FindControl("chkIsDefault");
                    var drpLanguage1 = (DropDownList)dr.FindControl("drpLanguage");
                    var txtValue1 = (TextBox)dr.FindControl("txtValue");

                    data.FieldConfigurationId = FieldConfigurationId;
                    data.LanguageId = Convert.ToInt32(drpLanguage1.SelectedValue);
                    data.Value = txtValue1.Text.Trim();
                    data.IsDefault = chkIsDefault1.Checked ? 1 : 0;

                    FieldConfigurationDisplayNameDataManager.Update(data, SessionVariables.RequestProfile);
                }
            }
            bindGrid();
        }

        #endregion

    }

}