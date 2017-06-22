using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;


namespace ApplicationContainer.UI.Web.Aptitude.CompetencyXSkill.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {
        #region private methods

        protected override void ShowData(int competencyXSkillId)
        {
			base.ShowData(competencyXSkillId);

            oDetailButtonPanel.SetId = SetId;

			Clear();

            var data = new DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel();
            data.CompetencyXSkillId = competencyXSkillId;

            var entityList = TaskTimeTracker.Components.Module.Competency.CompetencyXSkillDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{

					lblCompetencyXSkillId.Text = entityItem.CompetencyXSkillId.ToString();
					lblCompetency.Text = entityItem.Competency;
					lblSkill.Text = entityItem.Skill;

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.CompetencyXSkill, competencyXSkillId, "CompetencyXSkill");
					dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "CompetencyXSkill");
				}
			}
        }

        protected override void Clear()
        {
            lblCompetencyXSkillId.Text = String.Empty;
			lblSkill.Text = String.Empty;
            lblCompetency.Text = String.Empty;
        }

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblCompetencyXSkillIdText, lblCompetencyText, lblSkillText});
			}

			return LabelListCore;
		}

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblCompetencyXSkillIdText.Visible = isTesting;
                lblCompetencyXSkillId.Visible = isTesting;
            }

			PopulateLabelsText();
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DictionaryLabel = CacheConstants.CompetencyXSkillLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.CompetencyXSkill;

			PlaceHolderCore = dynCompetencyXSkillId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

		}

        #endregion

    }
}