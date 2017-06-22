using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Competency;
using Shared.WebCommon.UI.Web;
using System.Data;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.Aptitude.CompetencyXSkill.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {
        #region variables               

        public DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel SearchParameters
        {
            get
            {
                var data = new DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel();


                if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                    DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel.DataColumns.CompetencyId + "Visibility", SettingCategory)
                    && !CheckAndGetFieldValue(
                        DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel.DataColumns.CompetencyId).ToString().Equals("-1"))				
                {
                    data.CompetencyId = Convert.ToInt32(
                        CheckAndGetFieldValue(DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel.DataColumns.CompetencyId));
                }

                if (SearchParametersRepeater.Items.Count != 0 && PerferenceUtility.GetUserPreferenceByKeyAsBoolean(
                   DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel.DataColumns.SkillId + "Visibility", SettingCategory)
                   && !CheckAndGetFieldValue(
                       DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel.DataColumns.SkillId).ToString().Equals("-1"))				
                {
                    data.SkillId = Convert.ToInt32(CheckAndGetFieldValue(
                       DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel.DataColumns.SkillId));
                }               

                return data;
            }
        }

        #endregion

        #region methods

		public override void LoadDropDownListSources(string fieldName, DropDownList dropDownListControl)
		{
			base.LoadDropDownListSources(fieldName, dropDownListControl);

			if (fieldName.Equals("CompetencyId"))
			{
                var competencydata = CompetencyDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(competencydata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					CompetencyDataModel.DataColumns.CompetencyId);

			}

			if (fieldName.Equals("SkillId"))
			{
                var skilldata = SkillDataManager.GetList(SessionVariables.RequestProfile);
				UIHelper.LoadDropDown(skilldata, dropDownListControl,
					StandardDataModel.StandardDataColumns.Name,
					SkillDataModel.DataColumns.SkillId);

			}
		}

        #endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "CompetencyXSkill";
			FolderLocationFromRoot = "CompetencyXSkill";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.CompetencyXSkill;

			SearchActionBarCore = oSearchActionBar;
			SearchParametersRepeaterCore = SearchParametersRepeater;
		}

        #endregion
    }
}