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

namespace ApplicationContainer.UI.Web.Aptitude.SkillLevel.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new SkillLevelDataModel();

            data.SkillLevelId    = SystemKeyId;
            data.Name            = Name;
            data.Description     = Description;
            data.SortOrder       = SortOrder;

            if (action == "Insert")
            {
                var dtSkillLevel = SkillLevelDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtSkillLevel.Rows.Count == 0)
                {
                    SkillLevelDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                SkillLevelDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.SkillLevelId;
        }

        public override void SetId(int setId, bool chkSkillLevelId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkSkillLevelId);
            CoreSystemKey.Enabled = chkSkillLevelId;
            //txtDescription.Enabled = !chkSkillLevelId;
            //txtName.Enabled = !chkSkillLevelId;
            //txtSortOrder.Enabled = !chkSkillLevelId;
        }

        public void LoadData(int skillLevelId, bool showId)
        {
            Clear();

            var data = new SkillLevelDataModel();
			data.SkillLevelId = skillLevelId;

            var items = SkillLevelDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.SkillLevelId;
                oHistoryList.Setup(PrimaryEntity, skillLevelId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new SkillLevelDataModel();

            SetData(data);
        }

		public void SetData(SkillLevelDataModel data)
        {
            SystemKeyId = data.SkillLevelId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblSkillLevelId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity            = Framework.Components.DataAccess.SystemEntity.SkillLevel;
            PrimaryEntityKey         = "SkillLevel";
            FolderLocationFromRoot   = "SkillLevel";

            PlaceHolderCore          = dynSkillLevelId;
            PlaceHolderAuditHistory  = dynAuditHistory;
            BorderDiv                = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey            = txtSkillLevelId;
            CoreControlName          = txtName;
            CoreControlDescription   = txtDescription;
            CoreControlSortOrder     = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}