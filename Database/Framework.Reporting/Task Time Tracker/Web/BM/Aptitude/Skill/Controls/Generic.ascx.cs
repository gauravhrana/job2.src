using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.Competency;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.Aptitude.Skill.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new SkillDataModel();

            data.SkillId        = SystemKeyId;
            data.Name           = Name;
            data.Description    = Description;
            data.SortOrder      = SortOrder;

            if (action == "Insert")
            {
                var dtSkill = SkillDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtSkill.Rows.Count == 0)
                {
                    SkillDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                SkillDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.SkillId;
        }

        public override void SetId(int setId, bool chkSkillId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkSkillId);
            CoreSystemKey.Enabled = chkSkillId;
            //txtDescription.Enabled = !chkSkillId;
            //txtName.Enabled = !chkSkillId;
            //txtSortOrder.Enabled = !chkSkillId;
        }

        public void LoadData(int skillId, bool showId)
        {
            Clear();

            var data = new SkillDataModel();
			data.SkillId = skillId;

            var items = SkillDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.SkillId;
                oHistoryList.Setup(PrimaryEntity, skillId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

			var data = new SkillDataModel();

            SetData(data);
        }

        public void SetData(SkillDataModel data)
        {
            SystemKeyId = data.SkillId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblSkillId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity           = Framework.Components.DataAccess.SystemEntity.Skill;
            PrimaryEntityKey        = "Skill";
            FolderLocationFromRoot  = "Skill";

            PlaceHolderCore = dynSkillId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey           = txtSkillId;
            CoreControlName         = txtName;
            CoreControlDescription  = txtDescription;
            CoreControlSortOrder    = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}