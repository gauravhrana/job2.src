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

namespace ApplicationContainer.UI.Web.Aptitude.Competency.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new CompetencyDataModel();

            data.CompetencyId    = SystemKeyId;
            data.Name            = Name;
            data.Description     = Description;
            data.SortOrder       = SortOrder;

            if (action == "Insert")
            {
                var dtCompetency = CompetencyDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtCompetency.Rows.Count == 0)
                {
                    CompetencyDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                CompetencyDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.CompetencyId;
        }

        public override void SetId(int setId, bool chkCompetencyId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkCompetencyId);
            CoreSystemKey.Enabled = chkCompetencyId;
            //txtDescription.Enabled = !chkCompetencyId;
            //txtName.Enabled = !chkCompetencyId;
            //txtSortOrder.Enabled = !chkCompetencyId;
        }

        public void LoadData(int competencyId, bool showId)
        {
            Clear();

            var data = new CompetencyDataModel();
			data.CompetencyId = competencyId;

            var items = CompetencyDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.CompetencyId;
                oHistoryList.Setup(PrimaryEntity, competencyId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new CompetencyDataModel();

            SetData(data);
        }

        public void SetData(CompetencyDataModel data)
        {
            SystemKeyId = data.CompetencyId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblCompetencyId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity           = Framework.Components.DataAccess.SystemEntity.Competency;
            PrimaryEntityKey        = "Competency";
            FolderLocationFromRoot  = "Competency";

            PlaceHolderCore         = dynCompetencyId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv               = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey           = txtCompetencyId;
            CoreControlName         = txtName;
            CoreControlDescription  = txtDescription;
            CoreControlSortOrder    = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}