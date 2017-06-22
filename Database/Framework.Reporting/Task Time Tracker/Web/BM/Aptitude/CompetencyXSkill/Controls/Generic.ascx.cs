using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.DataAccess;
using DataModel.TaskTimeTracker.Competency;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using System.Data;
using TaskTimeTracker.Components.BusinessLayer;
using System.Globalization;
using TaskTimeTracker.Components.Module.Competency;

namespace ApplicationContainer.UI.Web.Aptitude.CompetencyXSkill.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGeneric
    {
        #region properties

        public int? CompetencyXSkillId
        {
            get
            {
                if (txtCompetencyXSkillId.Enabled)
                {
                    return Framework.Components.DefaultDataRules.CheckAndGetEntityId(txtCompetencyXSkillId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtCompetencyXSkillId.Text);
                }
            }
			set
			{
				txtCompetencyXSkillId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }

        public int? CompetencyId
        {
            get
            {
                var isTesting = SessionVariables.IsTesting;
                if (isTesting)
                    return int.Parse(txtCompetencyId.Text.Trim());
                else
                    return int.Parse(drpCompetencyList.SelectedItem.Value);
            }
			set
			{
				txtCompetencyId.Text = (value == null) ? String.Empty : value.ToString();
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
			set
			{
				txtSkillId.Text = (value == null) ? String.Empty : value.ToString();
			}
        }        

        #endregion properties

        #region private methods

		public override int? Save(string action)
		{
            var data = new DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel();

			data.CompetencyXSkillId = CompetencyXSkillId;
			data.CompetencyId = CompetencyId;
			data.SkillId = SkillId;			

			if (action == "Insert")
			{
                var dtCompetencyXSkill = TaskTimeTracker.Components.Module.Competency.CompetencyXSkillDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtCompetencyXSkill.Rows.Count == 0)
				{
                    TaskTimeTracker.Components.Module.Competency.CompetencyXSkillDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                TaskTimeTracker.Components.Module.Competency.CompetencyXSkillDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return data.CompetencyXSkillId;
		}

        public override void SetId(int setId, bool chkCompetencyXSkillId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkCompetencyXSkillId);
            txtCompetencyXSkillId.Enabled = chkCompetencyXSkillId;
            //txtSkillId.Enabled = !chkCompetencyXSkillId;
            //txtPersonId.Enabled = !chkCompetencyXSkillId;
            //txtCompetencyId.Enabled = !chkCompetencyXSkillId;

            //drpPersonList.Enabled = !chkCompetencyXSkillId;
            //drpSkillList.Enabled = !chkCompetencyXSkillId;
            //drpCompetencyList.Enabled = !chkCompetencyXSkillId;
        }

        public void LoadData(int competencyXSkillId, bool showId)
        {
            var data = new DataModel.CompetencyTimeTracker.Skill.CompetencyXSkillDataModel();
            data.CompetencyXSkillId = competencyXSkillId;

            var oCompetencyXSkillTable = TaskTimeTracker.Components.Module.Competency.CompetencyXSkillDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (oCompetencyXSkillTable.Count != 1) return;

			var item = oCompetencyXSkillTable[0];			

	        if (!showId)
	        {
					CompetencyXSkillId = item.CompetencyXSkillId;					
					CompetencyId=item.CompetencyId;
					SkillId = item.SkillId;
                    dynAuditHistory.Visible = true;

                    // only show Audit History in case of Update page, not for Clone.
                    oHistoryList.Setup(PrimaryEntity, competencyXSkillId, PrimaryEntityKey);
					dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "ProjectTimeLine");
                }

                else
                {
                    txtCompetencyXSkillId.Text  = String.Empty;
                }
                
                //drpSkillList.SelectedValue      = Convert.ToString(row[TaskTimeTracker.Components.Module.Competency.CompetencyXSkillDataManager.DataColumns.SkillId]);
                //drpCompetencyList.SelectedValue = Convert.ToString(row[TaskTimeTracker.Components.Module.Competency.CompetencyXSkillDataManager.DataColumns.CompetencyId]);

				//oUpdateInfo.LoadText(oCompetencyXSkillTable.Rows[0]);
			
        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

            var taskPriorityTypeData = CompetencyDataManager.GetList(SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(taskPriorityTypeData, drpCompetencyList, StandardDataModel.StandardDataColumns.Name, CompetencyDataModel.DataColumns.CompetencyId);

            var taskData = SkillDataManager.GetList(SessionVariables.RequestProfile);
            UIHelper.LoadDropDown(taskData, drpSkillList, StandardDataModel.StandardDataColumns.Name, SkillDataModel.DataColumns.SkillId);

            if (isTesting)
            {
                drpSkillList.AutoPostBack = true;
                drpCompetencyList.AutoPostBack = true;
                if (drpCompetencyList.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtCompetencyId.Text.Trim()))
                    {
                        drpCompetencyList.SelectedValue = txtCompetencyId.Text;
                    }
                    else
                    {
                        txtCompetencyId.Text = drpCompetencyList.SelectedItem.Value;
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
                txtCompetencyId.Visible = true;
                txtSkillId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtCompetencyId.Text.Trim()))
                {
                    drpCompetencyList.SelectedValue = txtCompetencyId.Text;
                }
                if (!string.IsNullOrEmpty(txtSkillId.Text.Trim()))
                {
                    drpSkillList.SelectedValue = txtSkillId.Text;
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
                txtCompetencyXSkillId.Visible = isTesting;
                lblCompetencyXSkillId.Visible = isTesting;
                SetupDropdown();
            }
        }        

        protected void drpCompetencyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCompetencyId.Text = drpCompetencyList.SelectedItem.Value;
        }

        protected void drpSkillList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSkillId.Text = drpSkillList.SelectedItem.Value;
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.CompetencyXSkill;
			PrimaryEntityKey = "CompetencyXSkill";
			FolderLocationFromRoot = "CompetencyXSkill";

			// set object variable reference            
			PlaceHolderCore = dynCompetencyXSkillId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
			
		}


        #endregion

    }
}