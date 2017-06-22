using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.Aptitude.SkillXPersonXSkillLevel
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();
            var data = new DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel();
            UpdatedData = SkillXPersonXSkillLevelDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
                data.SkillXPersonXSkillLevelId =
                    Convert.ToInt32(SelectedData.Rows[i][DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel.DataColumns.SkillXPersonXSkillLevelId].ToString());

                data.SkillId =
                    !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel.DataColumns.SkillId))
                    ? int.Parse(CheckAndGetRepeaterTextBoxValue(DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel.DataColumns.SkillId).ToString())
                    : int.Parse(SelectedData.Rows[i][DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel.DataColumns.SkillId].ToString());

                data.SkillLevelId =
                     !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevelId))
                     ? int.Parse(CheckAndGetRepeaterTextBoxValue(DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevelId).ToString())
                     : int.Parse(SelectedData.Rows[i][DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevelId].ToString());

                data.PersonId =
                     !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel.DataColumns.PersonId))
                     ? int.Parse(CheckAndGetRepeaterTextBoxValue(DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel.DataColumns.PersonId).ToString())
                     : int.Parse(SelectedData.Rows[i][DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel.DataColumns.PersonId].ToString());

                SkillXPersonXSkillLevelDataManager.Update(data, SessionVariables.RequestProfile);
                data = new DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel();
                data.SkillXPersonXSkillLevelId = Convert.ToInt32(SelectedData.Rows[i][DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel.DataColumns.SkillXPersonXSkillLevelId].ToString());
                var dt = SkillXPersonXSkillLevelDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }

            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var skillXPersonXSkillLeveldata = new DataModel.TaskTimeTracker.Competency.SkillXPersonXSkillLevelDataModel();
            skillXPersonXSkillLeveldata.SkillXPersonXSkillLevelId = entityKey;
            var results = SkillXPersonXSkillLevelDataManager.Search(skillXPersonXSkillLeveldata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SkillXPersonXSkillLevel;
            PrimaryEntityKey = "SkillXPersonXSkillLevel";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}