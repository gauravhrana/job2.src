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

namespace ApplicationContainer.UI.Web.UseCasePackage.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new UseCasePackageDataModel();

            data.UseCasePackageId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtUseCasePackage = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtUseCasePackage.Rows.Count == 0)
                {
                    TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.UseCasePackageId;
        }

        public override void SetId(int setId, bool chkUseCasePackageId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkUseCasePackageId);
            CoreSystemKey.Enabled = chkUseCasePackageId;
            //txtDescription.Enabled = !chkUseCasePackageId;
            //txtName.Enabled = !chkUseCasePackageId;
            //txtSortOrder.Enabled = !chkUseCasePackageId;
        }

        public void LoadData(int UseCasePackageId, bool showId)
        {
            Clear();

            var data = new UseCasePackageDataModel();
            data.UseCasePackageId = UseCasePackageId;

            var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];
         
            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.UseCasePackageId;
                oHistoryList.Setup(PrimaryEntity, UseCasePackageId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new UseCasePackageDataModel();

            SetData(data);
        }

        public void SetData(UseCasePackageDataModel data)
        {
            SystemKeyId = data.UseCasePackageId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblUseCasePackageId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity            = Framework.Components.DataAccess.SystemEntity.UseCasePackage;
            PrimaryEntityKey         = "UseCasePackage";
            FolderLocationFromRoot   = "RequirementAnalysis/";

            PlaceHolderCore          = dynUseCasePackageId;
            PlaceHolderAuditHistory  = dynAuditHistory;
            BorderDiv                = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey            = txtUseCasePackageId;
            CoreControlName          = txtName;
            CoreControlDescription   = txtDescription;
            CoreControlSortOrder     = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}