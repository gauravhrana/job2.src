using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemDevNumbers.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables

		public int PersonId
		{
			get
			{
				return Convert.ToInt32(ViewState["PersonId"]);
			}
			set
			{
				ViewState["PersonId"] = value;
			}
		}
        public SystemDevNumbersDataModel SearchParameters
        {
            get
            {
                var data = new SystemDevNumbersDataModel();

				if (drpSearchConditionPerson.SelectedValue != "-1")
				{
					data.PersonId = Convert.ToInt32(drpSearchConditionPerson.SelectedValue);
					PersonId = Convert.ToInt32(drpSearchConditionPerson.SelectedValue);
					txtSearchConditionPerson.Text = drpSearchConditionPerson.Text;
				}

                return data;
            }
        }

        #endregion

        #region private methods

		protected override void GetSettings()
		{
			var category = SettingCategory;
			var value = String.Empty;

			drpSearchConditionPerson.SelectedIndex = drpSearchConditionPerson.Items.IndexOf(drpSearchConditionPerson.Items.
				FindByText(PerferenceUtility.GetUserPreferenceByKey(SystemDevNumbersDataModel.DataColumns.PersonId,
					category)));
			txtSearchConditionPerson.Text = drpSearchConditionPerson.SelectedValue;
		}

		protected override void SaveSettings()
		{
			PerferenceUtility.UpdateUserPreference(SettingCategory,
			SystemDevNumbersDataModel.DataColumns.PersonId,
			drpSearchConditionPerson.SelectedItem.Text);
		}

		private void SetAutoSearchOn()
		{
			var autoSearchOn = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.AutoSearchOn, SettingCategory);

			if (autoSearchOn)
			{
				drpSearchConditionPerson.AutoPostBack = true;
			}
			else
			{
				drpSearchConditionPerson.AutoPostBack = false;
			}
		}

		private void LoadReferenceData()
		{
			// LoadDropDown

			var personData = Framework.Components.ApplicationUser.ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(personData, drpSearchConditionPerson, ApplicationUserDataModel.DataColumns.FullName, ApplicationUserDataModel.DataColumns.ApplicationUserId);
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

				oSearchActionBar.Setup("SystemDevNumbers");	
			}
		}

		

		protected void drpSearchConditionPerson_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtSearchConditionPerson.Text = drpSearchConditionPerson.SelectedValue;
		}

		

        #endregion

    }
}