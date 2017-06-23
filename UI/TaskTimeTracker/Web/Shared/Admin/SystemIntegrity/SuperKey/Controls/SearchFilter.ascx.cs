using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.SuperKey.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables

		public int SystemEntityTypeId
		{
			get
			{
				return Convert.ToInt32(ViewState["SystemEntityTypeId"]);
			}
			set
			{
				ViewState["SystemEntityTypeId"] = value;
			}
		}

		public SuperKeyDataModel SearchParameters
        {
            get
            {
                var data = new SuperKeyDataModel();

                data.Name = UIHelper.RefineAndGetSearchText(txtSearchConditionName.Text, SettingCategory);
				if (drpSearchConditionSystemEntityType.SelectedValue != "-1")
				{
					data.SystemEntityTypeId = Convert.ToInt32(drpSearchConditionSystemEntityType.SelectedValue);
					SystemEntityTypeId = Convert.ToInt32(drpSearchConditionSystemEntityType.SelectedValue);
					txtSearchConditionSystemEntityType.Text = drpSearchConditionSystemEntityType.Text;
				}
                return data;
            }
        }

        #endregion

        #region private methods

		private void SetDefaultValues()
		{
			txtSearchConditionName.Text = String.Empty;
			txtSearchConditionSystemEntityType.Text = String.Empty;
			drpSearchConditionSystemEntityType.SelectedIndex = 0;
		}
	
	   protected override void GetSettings()
	   {
		   var category = SettingCategory;
		   var value = String.Empty;

		   drpSearchConditionSystemEntityType.SelectedIndex = drpSearchConditionSystemEntityType.Items.IndexOf(drpSearchConditionSystemEntityType.Items.
			   FindByText(PreferenceUtility.GetUserPreferenceByKey(SuperKeyDataModel.DataColumns.SystemEntityTypeId,
				   category)));
		   txtSearchConditionSystemEntityType.Text = drpSearchConditionSystemEntityType.SelectedValue;

		   txtSearchConditionName.Text = PreferenceUtility.GetUserPreferenceByKey(StandardDataModel.StandardDataColumns.Name, category);
	   }

	   protected override void SaveSettings()
	   {
		   PreferenceUtility.UpdateUserPreference
		   (
				  SettingCategory
			  , ApplicationCommon.SearchFieldName
			  , txtSearchConditionName.Text
		   );

		   PreferenceUtility.UpdateUserPreference(SettingCategory,
		   SuperKeyDataModel.DataColumns.SystemEntityTypeId,
		   drpSearchConditionSystemEntityType.SelectedItem.Text);
	   }

	   private void SetAutoSearchOn()
	   {
		   var autoSearchOn = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.AutoSearchOn, SettingCategory);

		   if (autoSearchOn)
		   {
			   drpSearchConditionSystemEntityType.AutoPostBack = true;
		   }
		   else
		   {
			   drpSearchConditionSystemEntityType.AutoPostBack = false;
		   }
	   }

	   private void LoadReferenceData()
	   {
		   // LoadDropDown

		   var systemEntityData = Framework.Components.Core.SystemEntityTypeDataManager.GetList(SessionVariables.RequestProfile);
		   UIHelper.LoadDropDown(systemEntityData, drpSearchConditionSystemEntityType, SystemEntityTypeDataModel.DataColumns.EntityName, SystemEntityTypeDataModel.DataColumns.SystemEntityTypeId);
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

				oSearchActionBar.Setup("SuperKey");	
			}
		}

		protected void drpSearchConditionSystemEntityType_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtSearchConditionSystemEntityType.Text = drpSearchConditionSystemEntityType.SelectedValue;
		}

		#endregion

    }
}