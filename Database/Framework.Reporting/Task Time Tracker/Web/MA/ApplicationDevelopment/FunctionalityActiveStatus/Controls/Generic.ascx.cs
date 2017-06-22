using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.FunctionalityActiveStatus.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new FunctionalityActiveStatusDataModel();

            data.FunctionalityActiveStatusId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
				var dtFunctionalityActiveStatus = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityActiveStatusDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtFunctionalityActiveStatus.Rows.Count == 0)
                {
					TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityActiveStatusDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityActiveStatusDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.FunctionalityActiveStatusId;
        }

        public override void SetId(int setId, bool chkFunctionalityActiveStatusId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkFunctionalityActiveStatusId);
            CoreSystemKey.Enabled = chkFunctionalityActiveStatusId;
            //txtDescription.Enabled = !chkFunctionalityActiveStatusId;
            //txtName.Enabled = !chkFunctionalityActiveStatusId;
            //txtSortOrder.Enabled = !chkFunctionalityActiveStatusId;
        }

        public void LoadData(int functionalityActiveStatusId, bool showId)
        {
            Clear();

            var data = new FunctionalityActiveStatusDataModel();
            data.FunctionalityActiveStatusId = functionalityActiveStatusId;

			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.FunctionalityActiveStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.FunctionalityActiveStatusId;
                oHistoryList.Setup(PrimaryEntity, functionalityActiveStatusId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new FunctionalityActiveStatusDataModel();

            SetData(data);
        }

        public void SetData(FunctionalityActiveStatusDataModel data)
        {
            SystemKeyId = data.FunctionalityActiveStatusId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblFunctionalityActiveStatusId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FunctionalityActiveStatus;
            PrimaryEntityKey = "FunctionalityActiveStatus";
            FolderLocationFromRoot = "FunctionalityActiveStatus";

            PlaceHolderCore = dynFunctionalityActiveStatusId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtFunctionalityActiveStatusId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}