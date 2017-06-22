using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Competency;
using Framework.Components;
using Framework.Components.ApplicationUser;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.Aptitude.SkillXPersonXSkillLevel.Controls
{
    public partial class Generic : ControlGeneric
    {

        #region properties

        public int? SkillXPersonXSkillLevelId
        {
            get
            {
                if (txtSkillXPersonXSkillLevelId.Enabled)
                {
                    return DefaultDataRules.CheckAndGetEntityId(txtSkillXPersonXSkillLevelId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtSkillXPersonXSkillLevelId.Text);
                }
            }
        }

        public int? SkillLevelId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtSkillLevelId.Text.Trim());
                else
                    return int.Parse(drpSkillLevelList.SelectedItem.Value);
            }

        }

        public int? SkillId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtSkillId.Text.Trim());
                else
                    return int.Parse(drpSkillList.SelectedItem.Value);
            }

        }

        public int? PersonId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtPersonId.Text.Trim());
                else
                    return int.Parse(drpPersonList.SelectedItem.Value);
            }

        }

        protected override string ValidationConfigFile
        {
            get
            {
                return Server.MapPath("~/Aptitude/SkillXPersonXSkillLevel/Controls/Validation.xml"); //"R:\SkillXPersonXSkillLevels\Skill Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
            }
        }

        #endregion properties

        #region private methods

        public override void SetId(int setId, bool chkSkillXPersonXSkillLevelId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkSkillXPersonXSkillLevelId);
            txtSkillXPersonXSkillLevelId.Enabled = chkSkillXPersonXSkillLevelId;
            //txtSkillId.Enabled = !chkSkillXPersonXSkillLevelId;
            //txtPersonId.Enabled = !chkSkillXPersonXSkillLevelId;
            //txtSkillLevelId.Enabled = !chkSkillXPersonXSkillLevelId;

            //drpPersonList.Enabled = !chkSkillXPersonXSkillLevelId;
            //drpSkillList.Enabled = !chkSkillXPersonXSkillLevelId;
            //drpSkillLevelList.Enabled = !chkSkillXPersonXSkillLevelId;
        }

        public void LoadData(int skillXPersonXSkillLevelId, bool showId)
        {
			var data = new SkillXPersonXSkillLevelDataModel();
            data.SkillXPersonXSkillLevelId = skillXPersonXSkillLevelId;
            var oSkillXPersonXSkillLevelTable = SkillXPersonXSkillLevelDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (oSkillXPersonXSkillLevelTable.Rows.Count == 1)
            {
                var row = oSkillXPersonXSkillLevelTable.Rows[0];

                if (!showId)
                {
                    txtSkillXPersonXSkillLevelId.Text = Convert.ToString(row[SkillXPersonXSkillLevelDataModel.DataColumns.SkillXPersonXSkillLevelId]);

                    // only show Audit History in case of Update page, not for Clone.
                    oHistoryList.Setup((int)SystemEntity.SkillXPersonXSkillLevel, skillXPersonXSkillLevelId, "SkillXPersonXSkillLevel");
                    dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "SkillXPersonXSkillLevel");
                   
                }
                else
                {
                    txtSkillXPersonXSkillLevelId.Text = String.Empty;
                }
                txtSkillLevelId.Text              = Convert.ToString(row[SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevelId]);
                txtSkillId.Text                   = Convert.ToString(row[SkillXPersonXSkillLevelDataModel.DataColumns.SkillId]);
                txtPersonId.Text                  = Convert.ToString(row[SkillXPersonXSkillLevelDataModel.DataColumns.PersonId]);

                drpPersonList.SelectedValue       = Convert.ToString(row[SkillXPersonXSkillLevelDataModel.DataColumns.PersonId]);
                drpSkillList.SelectedValue        = Convert.ToString(row[SkillXPersonXSkillLevelDataModel.DataColumns.SkillId]);
                drpSkillLevelList.SelectedValue   = Convert.ToString(row[SkillXPersonXSkillLevelDataModel.DataColumns.SkillLevelId]);
            }
            else
            {
                txtSkillXPersonXSkillLevelId.Text = String.Empty;
                txtSkillId.Text                   = String.Empty;
                txtPersonId.Text                  = String.Empty;
                txtSkillLevelId.Text              = String.Empty;

            }
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

            var taskPriorityTypeData = SkillLevelDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(taskPriorityTypeData, drpSkillLevelList, StandardDataModel.StandardDataColumns.Name,
				SkillLevelDataModel.DataColumns.SkillLevelId);

            var taskData = SkillDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(taskData, drpSkillList, StandardDataModel.StandardDataColumns.Name,
                SkillDataModel.DataColumns.SkillId);

			var personData = ApplicationUserDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(personData, drpPersonList, ApplicationUserDataModel.DataColumns.FirstName, ApplicationUserDataModel.DataColumns.ApplicationUserId);

            if (isTesting)
            {
                drpSkillList.AutoPostBack = true;
                drpSkillLevelList.AutoPostBack = true;
                drpPersonList.AutoPostBack = true;
                if (drpSkillLevelList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtSkillLevelId.Text.Trim()))
                    {
                        drpSkillLevelList.SelectedValue = txtSkillLevelId.Text;
                    }
                    else
                    {
                        txtSkillLevelId.Text = drpSkillLevelList.SelectedItem.Value;
                    }
                }
                if (drpSkillList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtSkillId.Text.Trim()))
                    {
                        drpSkillList.SelectedValue = txtSkillId.Text;
                    }
                    else
                    {
                        txtSkillId.Text = drpSkillList.SelectedItem.Value;
                    }
                }
                if (drpPersonList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtPersonId.Text.Trim()))
                    {
                        drpPersonList.SelectedValue = txtPersonId.Text;
                    }
                    else
                    {
                        txtPersonId.Text = drpPersonList.SelectedItem.Value;
                    }
                }
                txtSkillLevelId.Visible = true;
                txtSkillId.Visible = true;
                txtPersonId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtSkillLevelId.Text.Trim()))
                {
                    drpSkillLevelList.SelectedValue = txtSkillLevelId.Text;
                }
                if (!string.IsNullOrEmpty(txtSkillId.Text.Trim()))
                {
                    drpSkillList.SelectedValue = txtSkillId.Text;
                }
                if (!string.IsNullOrEmpty(txtPersonId.Text.Trim()))
                {
                    drpPersonList.SelectedValue = txtPersonId.Text;
                }
            }
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                txtSkillXPersonXSkillLevelId.Visible = isTesting;
                lblSkillXPersonXSkillLevelId.Visible = isTesting;
                SetupDropdown();
            }
        }
        
        protected void drpSkillLevelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSkillLevelId.Text = drpSkillLevelList.SelectedItem.Value;
        }

        protected void drpSkillList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSkillId.Text = drpSkillList.SelectedItem.Value;
        }

        protected void drpPersonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPersonId.Text = drpPersonList.SelectedItem.Value;
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity           = SystemEntity.SkillXPersonXSkillLevel;
			PrimaryEntityKey        = "SkillXPersonXSkillLevel";
			FolderLocationFromRoot  = "SkillXPersonXSkillLevel";
			        
			PlaceHolderCore         = dynSkillXPersonXSkillLevelId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv               = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
			
		}

        #endregion

    }
}