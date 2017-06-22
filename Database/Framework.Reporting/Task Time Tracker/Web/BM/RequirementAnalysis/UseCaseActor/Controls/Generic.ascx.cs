using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis;
using DataModel.TaskTimeTracker.RequirementAnalysis;

namespace ApplicationContainer.UI.Web.UseCaseActor.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new UseCaseActorDataModel();

            data.UseCaseActorId  = SystemKeyId;
            data.Name            = Name;
            data.Description     = Description;
            data.SortOrder       = SortOrder;

            if (action == "Insert")
            {
                var dtUseCaseActor = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtUseCaseActor.Rows.Count == 0)
                {
                    TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {               
                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.UseCaseActorId;
        }

        public override void SetId(int setId, bool chkUseCaseActorId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkUseCaseActorId);
            CoreSystemKey.Enabled = chkUseCaseActorId;
            //txtDescription.Enabled = !chkUseCaseActorId;
            //txtName.Enabled = !chkUseCaseActorId;
            //txtSortOrder.Enabled = !chkUseCaseActorId;
        }

        public void LoadData(int UseCaseActorId, bool showId)
        {
            Clear();

            var data = new UseCaseActorDataModel();
            data.UseCaseActorId = UseCaseActorId;

            var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCaseActorDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.UseCaseActorId;
                oHistoryList.Setup(PrimaryEntity, UseCaseActorId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new UseCaseActorDataModel();

            SetData(data);
        }

        public void SetData(UseCaseActorDataModel data)
        {
            SystemKeyId = data.UseCaseActorId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblUseCaseActorId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity                = Framework.Components.DataAccess.SystemEntity.UseCaseActor;
            PrimaryEntityKey             = "UseCaseActor";
            FolderLocationFromRoot       = "RequirementAnalysis/";

            PlaceHolderCore              = dynUseCaseActorId;
            PlaceHolderAuditHistory      = dynAuditHistory;
            BorderDiv                    = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey            = txtUseCaseActorId;
            CoreControlName          = txtName;
            CoreControlDescription   = txtDescription;
            CoreControlSortOrder     = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}