using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Competency;
using Framework.Components.ApplicationUser;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.Module.Competency;


namespace ApplicationContainer.UI.Web.Aptitude.SkillXPersonXSkillLevel.Controls
{
    public partial class SearchFilter : ControlSearchFilter
    {

        #region variables        

        public string Name
        {
            get
            {
                if (ViewState["Name"] != null)
                {
                    return ViewState["Name"].ToString();
                }
                return String.Empty;
            }
            set
            {
                ViewState["Name"] = value;
            }
        }

		public SkillXPersonXSkillLevelDataModel SearchParameters
        {
            get
            {
                var data = new SkillXPersonXSkillLevelDataModel();

                if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
					SkillXPersonXSkillLevelDataModel.DataColumns.Skill + "Visibility", SettingCategory)
                    && !CheckAndGetFieldValue(
						SkillXPersonXSkillLevelDataModel.DataColumns.Skill).ToString().Equals("-1"))				
                {
                    data.SkillId = Convert.ToInt32(CheckAndGetFieldValue(
                       SkillXPersonXSkillLevelDataModel.DataColumns.Skill));
                }
                if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                   SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevel + "Visibility", SettingCategory)
                   && !CheckAndGetFieldValue(
					   SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevel).ToString().Equals("-1"))				
                {
                    data.SkillLevelId = Convert.ToInt32(CheckAndGetFieldValue(
                       SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevel));
                }

                if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                   SkillXPersonXSkillLevelDataModel.DataColumns.Person + "Visibility", SettingCategory)
                   && !CheckAndGetFieldValue(
					   SkillXPersonXSkillLevelDataModel.DataColumns.Person).ToString().Equals("-1"))				
                {
                    data.PersonId = Convert.ToInt32(CheckAndGetFieldValue(
                       SkillXPersonXSkillLevelDataModel.DataColumns.Person));
                }

                return data;
            }
        }
        #endregion

        #region methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("Skill"))
			{
                var skilldata = SkillDataManager.GetList(SessionVariables.RequestProfile)      ;
                                UIHelper.LoadDropDown(skilldata, dropDownListControl,
                                    StandardDataModel.StandardDataColumns.Name,
									SkillDataModel.DataColumns.SkillId);
			}
			if (fieldName.Equals("SkillLevel"))
			{
                var skillLeveldata = SkillLevelDataManager.GetList(SessionVariables.RequestProfile);
                                UIHelper.LoadDropDown(skillLeveldata, dropDownListControl,
									StandardDataModel.StandardDataColumns.Name,
									SkillLevelDataModel.DataColumns.SkillLevelId);
			}

			if (fieldName.Equals("Person"))
			{
				var applicationUserdata = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(applicationUserdata, dropDownListControl,
					ApplicationUserDataModel.DataColumns.FirstName,
					ApplicationUserDataModel.DataColumns.ApplicationUserId);
			}
		}        
         
        #endregion
       
		#region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = SystemEntity.SkillXPersonXSkillLevel;
			PrimaryEntityKey = "SkillXPersonXSkillLevel";
			FolderLocationFromRoot = "SkillXPersonXSkillLevel";

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

		#endregion
    }
}