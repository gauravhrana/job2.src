using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.WebCommon.UI.Web;


namespace ApplicationContainer.UI.Web.ApplicationDevelopment.Module.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new ModuleDataModel();

            data.ModuleId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtModule = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtModule.Rows.Count == 0)
                {
					TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.ModuleId;
        }

        public override void SetId(int setId, bool chkModuleId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkModuleId);
            CoreSystemKey.Enabled = chkModuleId;
            //txtDescription.Enabled = !chkModuleId;
            //txtName.Enabled = !chkModuleId;
            //txtSortOrder.Enabled = !chkModuleId;
        }

        public void LoadData(int moduleId, bool showId)
        {
            Clear();

            var data = new ModuleDataModel();
            data.ModuleId = moduleId;

			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.ModuleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.ModuleId;
                oHistoryList.Setup(PrimaryEntity, moduleId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new ModuleDataModel();

            SetData(data);
        }

        public void SetData(ModuleDataModel data)
        {
            SystemKeyId = data.ModuleId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblModuleId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Module;
            PrimaryEntityKey = "Module";
            FolderLocationFromRoot = "Module";

            PlaceHolderCore = dynModuleId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtModuleId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}