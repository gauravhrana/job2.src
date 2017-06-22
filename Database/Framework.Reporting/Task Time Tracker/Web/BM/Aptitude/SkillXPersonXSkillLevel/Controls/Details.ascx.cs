using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Competency;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.Aptitude.SkillXPersonXSkillLevel.Controls
{
    public partial class Details : ControlDetails
    {

        #region properties


        #endregion

        #region private methods

        protected override void ShowData(int skillXPersonXSkillLevelId)
        {
            oDetailButtonPanel.SetId = SetId;
			var data = new SkillXPersonXSkillLevelDataModel();
            data.SkillXPersonXSkillLevelId = skillXPersonXSkillLevelId;

            var dt = SkillXPersonXSkillLevelDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];

				lblSkillXPersonXSkillLevelId.Text    = Convert.ToString(row[SkillXPersonXSkillLevelDataModel.DataColumns.SkillXPersonXSkillLevelId]);
				lblSkillLevel.Text                   = Convert.ToString(row[SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevel]);
				lblSkill.Text                        = Convert.ToString(row[SkillXPersonXSkillLevelDataModel.DataColumns.Skill]);
				lblPerson.Text                       = Convert.ToString(row[SkillXPersonXSkillLevelDataModel.DataColumns.Person]);

				oUpdateInfo.LoadText(dt.Rows[0]);

                oHistoryList.Setup((int)SystemEntity.SkillXPersonXSkillLevel, skillXPersonXSkillLevelId, "SkillXPersonXSkillLevel");
                dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "SkillXPersonXSkillLevel");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblSkillXPersonXSkillLevelId.Text = String.Empty;
            lblSkill.Text = String.Empty;
            lblSkillLevel.Text = String.Empty;
			lblPerson.Text = String.Empty;
        }

        

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblSkillXPersonXSkillLevelIdText.Visible = isTesting;
                lblSkillXPersonXSkillLevelId.Visible = isTesting;
            }
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			DictionaryLabel = CacheConstants.SkillXPersonXSkillLevelLabelDictionary;
			PrimaryEntity = SystemEntity.SkillXPersonXSkillLevel;

			PlaceHolderCore = dynSkillXPersonXSkillLevelId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;		

		}

        #endregion

    }
}