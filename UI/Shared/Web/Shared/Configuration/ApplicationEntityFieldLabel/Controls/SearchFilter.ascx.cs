using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.ApplicationEntityFieldLabel.Controls
{
	public partial class SearchFilter : Shared.UI.WebFramework.BaseControl
	{
		public event System.EventHandler OnSearch;

        public String SearchControlName
        {
            get
            {
                return Convert.ToString(ViewState["SearchControlName"]);
            }
            set
            {
                ViewState["SearchControlName"] = value;
            }
        }

		public Framework.Components.UserPreference.ApplicationEntityFieldLabel.Data SearchParameters
		{
			get
			{
				var data = new Framework.Components.UserPreference.ApplicationEntityFieldLabel.Data();

				data.Name = txtSearchConditionName.Text.Trim();
				if (drpSearchConditionSystemEntityType.SelectedValue != "-1")
					data.SystemEntityTypeId = Convert.ToInt32(drpSearchConditionSystemEntityType.SelectedValue);
				return data;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                if (!string.IsNullOrEmpty(SearchControlName))
                {
                    ApplicationCommon.CheckUserPreferenceCategoryExists(SearchControlName, "Search Control Name");
                }
                else
                {
                    throw new Exception("Search control is not named");
                }
                var autoSearchOn = ApplicationCommon.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.AutoSearchOn, SearchControlName);

                if (autoSearchOn)
                {
                    drpSearchConditionSystemEntityType.AutoPostBack = true;
                }
                else
                {
                    drpSearchConditionSystemEntityType.AutoPostBack = false;
                }

                var systemEntityTypeData = Framework.Components.Core.SystemEntityType.GetList(SessionVariables.AuditId);
                LoadDropDown(systemEntityTypeData, drpSearchConditionSystemEntityType, Framework.Components.Core.SystemEntityType.DataColumns.EntityName, Framework.Components.Core.SystemEntityType.DataColumns.SystemEntityTypeId);
			}
			
		}
		protected void drpSearchConditionSystemEntityType_SelectedIndexChanged(object sender, EventArgs e)
		{
			RaiseSearch();
		}
		private void LoadDropDown(System.Data.DataTable dt, DropDownList drpSource, string textField, string valueField)
		{
			drpSource.DataSource = dt;
			drpSource.DataTextField = textField;
			drpSource.DataValueField = valueField;

			drpSource.DataBind();
			drpSource.SelectedIndex = -1;
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			RaiseSearch();
		}

		private void RaiseSearch()
		{
			if (OnSearch != null)
			{
				OnSearch(this, EventArgs.Empty);
			}
		}

	}
}