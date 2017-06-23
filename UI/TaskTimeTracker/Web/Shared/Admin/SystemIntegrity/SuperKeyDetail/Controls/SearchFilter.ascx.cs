using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.SuperKeyDetail.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables

		public int SuperKeyId
		{
			get
			{
				return Convert.ToInt32(ViewState["SuperKeyId"]);
			}
			set
			{
				ViewState["SuperKeyId"] = value;
			}
		}
        public SuperKeyDetailDataModel SearchParameters
        {
            get
            {
                var data = new SuperKeyDetailDataModel();

				if (drpSearchConditionSuperKey.SelectedValue != "-1")
				{
					data.SuperKeyId = Convert.ToInt32(drpSearchConditionSuperKey.SelectedValue);
					SuperKeyId = Convert.ToInt32(drpSearchConditionSuperKey.SelectedValue);
					txtSearchConditionSuperKey.Text = drpSearchConditionSuperKey.Text;
				}

                return data;
            }
        }

        #endregion

        #region private methods

		

		private void SetDefaultValues()
		{
			txtSearchConditionSuperKey.Text = String.Empty;
			drpSearchConditionSuperKey.SelectedIndex = 0;
		}

		protected override void GetSettings()
		{
			var category = SettingCategory;
			var value = String.Empty;

			drpSearchConditionSuperKey.SelectedIndex = drpSearchConditionSuperKey.Items.IndexOf(drpSearchConditionSuperKey.Items.
				FindByText(PreferenceUtility.GetUserPreferenceByKey(SuperKeyDetailDataModel.DataColumns.SuperKeyId,
					category)));
			txtSearchConditionSuperKey.Text = drpSearchConditionSuperKey.SelectedValue;

		}

		protected override void SaveSettings()
		{
			PreferenceUtility.UpdateUserPreference(SettingCategory,
			SuperKeyDetailDataModel.DataColumns.SuperKeyId,
			drpSearchConditionSuperKey.SelectedItem.Text);
		}

		private void SetAutoSearchOn()
		{
			var autoSearchOn = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.AutoSearchOn, SettingCategory);

			if (autoSearchOn)
			{
				drpSearchConditionSuperKey.AutoPostBack = true;
			}
			else
			{
				drpSearchConditionSuperKey.AutoPostBack = false;
			}
		}

		private void LoadReferenceData()
		{
			// LoadDropDown

			var superKeyData = Framework.Components.Core.SuperKeyDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(superKeyData, drpSearchConditionSuperKey, StandardDataModel.StandardDataColumns.Name, SuperKeyDataModel.DataColumns.SuperKeyId);
		}

		#endregion

        #region Events

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				IsControlValid();

				SetAutoSearchOn();

				LoadReferenceData();

				GetSettings();

				RaiseSearch();

				oSearchActionBar.Setup("SuperKeyDetail");	
			}
		}

		

		protected void drpSearchConditionSuperKey_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtSearchConditionSuperKey.Text = drpSearchConditionSuperKey.SelectedValue;
		}


		

        #endregion

    }
}