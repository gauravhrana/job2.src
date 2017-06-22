using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.Aptitude.CompetencyXSkill
{
	public partial class CommonUpdate : PageCommonUpdate
	{
		#region Methods

		protected override DataTable UpdateData()
		{
			var UpdatedData = new DataTable();
            var data = new DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel();
            UpdatedData = TaskTimeTracker.Components.Module.Competency.CompetencyXSkillDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.CompetencyXSkillId =
                    Convert.ToInt32(SelectedData.Rows[i][DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel.DataColumns.CompetencyXSkillId].ToString());

				data.SkillId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel.DataColumns.SkillId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel.DataColumns.SkillId).ToString())
					: int.Parse(SelectedData.Rows[i][DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel.DataColumns.SkillId].ToString());

				data.CompetencyId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel.DataColumns.CompetencyId))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel.DataColumns.CompetencyId).ToString())
					: int.Parse(SelectedData.Rows[i][DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel.DataColumns.CompetencyId].ToString());

                TaskTimeTracker.Components.Module.Competency.CompetencyXSkillDataManager.Update(data, SessionVariables.RequestProfile);
				data = new DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel();
				data.CompetencyXSkillId = Convert.ToInt32(SelectedData.Rows[i][DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel.DataColumns.CompetencyXSkillId].ToString());
                var dt = TaskTimeTracker.Components.Module.Competency.CompetencyXSkillDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}
				
			}
			return UpdatedData;
		}		

		protected override DataTable GetEntityData(int? entityKey)
		{
			var competencyXSkilldata = new DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel();
			competencyXSkilldata.CompetencyXSkillId = entityKey;
            var results = TaskTimeTracker.Components.Module.Competency.CompetencyXSkillDataManager.Search(competencyXSkilldata, SessionVariables.RequestProfile);
			return results;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{

			base.OnInit(e);

			DynamicUpdatePanelCore = DynamicUpdatePanel;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.CompetencyXSkill;
			PrimaryEntityKey = "CompetencyXSkill";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
		
	}
}