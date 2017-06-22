using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.CompetencyXSkill
{
	public partial class InlineUpdate : PageInlineUpdate
	{
		#region Methods

		protected override DataTable GetData()
		{
			try
			{
				SuperKey = ApplicationCommon.GetSuperKey();
				SetId = ApplicationCommon.GetSetId();

				var selectedrows = new DataTable();
                var competencyXSkilldata = new DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel();

                selectedrows = TaskTimeTracker.Components.Module.Competency.CompetencyXSkillDataManager.GetDetails(competencyXSkilldata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var systemEntityTypeId = (int)PrimaryEntity;
					var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

					foreach (var entityKey in lstEntityKeys)
					{
						competencyXSkilldata.CompetencyXSkillId = entityKey;
                        var result = TaskTimeTracker.Components.Module.Competency.CompetencyXSkillDataManager.GetDetails(competencyXSkilldata, SessionVariables.RequestProfile);
						selectedrows.ImportRow(result.Rows[0]);
					}
				}
				else 
				{
					competencyXSkilldata.CompetencyXSkillId = SetId;
                    var result = TaskTimeTracker.Components.Module.Competency.CompetencyXSkillDataManager.GetDetails(competencyXSkilldata, SessionVariables.RequestProfile);
					selectedrows.ImportRow(result.Rows[0]);

				}
				return selectedrows;
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
			return null;
		}        

		protected override void Update(Dictionary<string, string> values)
		{
			var data = new DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel();

			data.CompetencyXSkillId     = int.Parse(values[DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel.DataColumns.CompetencyXSkillId].ToString());
			data.SkillId                = int.Parse(values[DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel.DataColumns.SkillId].ToString());
			data.CompetencyId           = int.Parse(values[DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel.DataColumns.CompetencyId].ToString());

            TaskTimeTracker.Components.Module.Competency.CompetencyXSkillDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			InlineEditingListCore = InlineEditingList;
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.CompetencyXSkill;
			PrimaryEntityKey = "CompetencyXSkill";
			BreadCrumbObject = Master.BreadCrumbObject;
		}

		#endregion
	}
}