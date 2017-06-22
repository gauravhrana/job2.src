using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using TaskTimeTracker.Components.BusinessLayer;
using DataModel.TaskTimeTracker;

namespace ApplicationContainer.UI.Web.Productivity.ProductivityAreaFeature.Controls
{
    public partial class Generic : ControlGenericStandard
    {
        #region methods

        public override int? Save(string action)
        {
            var data = new ProductivityAreaFeatureDataModel();

            data.ProductivityAreaFeatureId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                var dtProductivityAreaFeature = ProductivityAreaFeatureDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtProductivityAreaFeature.Rows.Count == 0)
                {
                    ProductivityAreaFeatureDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                ProductivityAreaFeatureDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.ProductivityAreaFeatureId;
        }

        public override void SetId(int setId, bool chkProductivityAreaFeatureId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkProductivityAreaFeatureId);
            CoreSystemKey.Enabled = chkProductivityAreaFeatureId;
            //txtDescription.Enabled = !chkProductivityAreaFeatureId;
            //txtName.Enabled = !chkProductivityAreaFeatureId;
            //txtSortOrder.Enabled = !chkProductivityAreaFeatureId;
        }

        public void LoadData(int ProductivityAreaFeatureId, bool showId)
        {
            Clear();

            var data = new ProductivityAreaFeatureDataModel();
            data.ProductivityAreaFeatureId = ProductivityAreaFeatureId;

            var items = ProductivityAreaFeatureDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.ProductivityAreaFeatureId;
                oHistoryList.Setup(PrimaryEntity, ProductivityAreaFeatureId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new ProductivityAreaFeatureDataModel();

            SetData(data);
        }

        public void SetData(ProductivityAreaFeatureDataModel data)
        {
            SystemKeyId = data.ProductivityAreaFeatureId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblProductivityAreaFeatureId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.ProductivityAreaFeature;
            PrimaryEntityKey = "ProductivityAreaFeature";
            FolderLocationFromRoot = "/Productivity";

            PlaceHolderCore = dynProductivityAreaFeatureId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtProductivityAreaFeatureId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}