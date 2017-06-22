using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.SkillXPersonXSkillLevel
{
    public partial class InlineUpdate : PageInlineUpdate
    {
        #region Methods

        protected override DataTable GetData()
        {
            try
            {
                var superKey = ApplicationCommon.GetSuperKey();
                var setId = ApplicationCommon.GetSetId();

				var selectedrows = new DataTable();
				var skillXPersonXSkillLeveldata = new DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel();

                selectedrows = SkillXPersonXSkillLevelDataManager.GetDetails(skillXPersonXSkillLeveldata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
                {
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        skillXPersonXSkillLeveldata.SkillLevelId = entityKey;
                        var result = SkillXPersonXSkillLevelDataManager.GetDetails(skillXPersonXSkillLeveldata, SessionVariables.RequestProfile);
                        selectedrows.ImportRow(result.Rows[0]);
                    }
                }
                else
                {
                    skillXPersonXSkillLeveldata.SkillLevelId = SetId;
                    var result = SkillXPersonXSkillLevelDataManager.GetDetails(skillXPersonXSkillLeveldata, SessionVariables.RequestProfile);
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
			var data = new DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel();

			data.SkillXPersonXSkillLevelId      = int.Parse(values[DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel.DataColumns.SkillXPersonXSkillLevelId].ToString());
			data.SkillId                        = int.Parse(values[DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel.DataColumns.SkillId].ToString());
			data.PersonId                       = int.Parse(values[DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel.DataColumns.PersonId].ToString());
			data.SkillLevelId                   = int.Parse(values[DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevelId].ToString());

            SkillXPersonXSkillLevelDataManager.Update(data, SessionVariables.RequestProfile);
			InlineEditingList.Data = GetData();
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            InlineEditingListCore = InlineEditingList;
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SkillXPersonXSkillLevel;
            PrimaryEntityKey = "SkillXPersonXSkillLevel";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}