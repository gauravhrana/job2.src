using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components.Core;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.Menu.Controls
{
    public partial class MenuDisplayName : BaseControl
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

        private int? MenuId
        {
            get
            {
                if (ViewState["MenuId"] != null)
                {
                    return Convert.ToInt32(ViewState["MenuId"]);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                ViewState["MenuId"] = value;
            }
        }

        #endregion

        #region private methods

        public void Setup(int menuId)
        {
            MenuId = menuId;
            bindGrid();
        }

        private void bindGrid()
        {
            var data = new MenuDisplayNameDataModel();
            data.MenuId = MenuId;
			var dt = MenuDisplayNameDataManager.Search(data, SessionVariables.RequestProfile);
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


                    drpLanguage.SelectedValue = Convert.ToString(row[MenuDisplayNameDataModel.DataColumns.LanguageId]);
                }

                var chkIsDefault = (CheckBox)e.Row.FindControl("chkIsDefault");
                if (chkIsDefault != null)
                {
                    if (Convert.ToInt32(row[MenuDisplayNameDataModel.DataColumns.IsDefault]) > 0)
                    {
                        chkIsDefault.Checked = true;
                    }
                }
            }
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            var data = new MenuDisplayNameDataModel();
            data.MenuId = MenuId;
            data.LanguageId = Convert.ToInt32(drpLanguage.SelectedValue);
            data.Value = txtValue.Text.Trim();
            data.IsDefault = chkIsDefault.Checked ? 1 : 0;
			MenuDisplayNameDataManager.Create(data, SessionVariables.RequestProfile);
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
                            var data = new MenuDisplayNameDataModel();
                            data.MenuDisplayNameId = Convert.ToInt32(dgvDisplayNames.DataKeys[dr.DataItemIndex].Value);
							MenuDisplayNameDataManager.Delete(data, SessionVariables.RequestProfile);
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
                    var data = new MenuDisplayNameDataModel();
                    data.MenuDisplayNameId = Convert.ToInt32(dgvDisplayNames.DataKeys[dr.DataItemIndex].Value);

                    var chkIsDefault1 = (CheckBox)dr.FindControl("chkIsDefault");
                    var drpLanguage1 = (DropDownList)dr.FindControl("drpLanguage");
                    var txtValue1 = (TextBox)dr.FindControl("txtValue");

                    data.MenuId = MenuId;
                    data.LanguageId = Convert.ToInt32(drpLanguage1.SelectedValue);
                    data.Value = txtValue1.Text.Trim();
                    data.IsDefault = chkIsDefault1.Checked ? 1 : 0;

                    MenuDisplayNameDataManager.Update(data, SessionVariables.RequestProfile);
                }
            }
            bindGrid();
        }

        #endregion

    }
}