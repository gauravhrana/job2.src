using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using TaskTimeTracker.Components.BusinessLayer;
using DataModel.TaskTimeTracker;

namespace ApplicationContainer.UI.Web.Productivity.ProductivityArea.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {
        #region methods

        public override int? Save(string action)
        {
            var data = new ProductivityAreaDataModel();

            data.ProductivityAreaId  = SystemKeyId;
            data.Name                = Name;
            data.Description         = Description;
            data.SortOrder           = SortOrder;

            if (action == "Insert")
            {
                var dtProductivityArea = ProductivityAreaDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtProductivityArea.Rows.Count == 0)
                {
                    ProductivityAreaDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                ProductivityAreaDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.ProductivityAreaId;
        }

        public override void SetId(int setId, bool chkProductivityAreaId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkProductivityAreaId);
            CoreSystemKey.Enabled = chkProductivityAreaId;
            //txtDescription.Enabled = !chkProductivityAreaId;
            //txtName.Enabled = !chkProductivityAreaId;
            //txtSortOrder.Enabled = !chkProductivityAreaId;
        }

        public void LoadData(int ProductivityAreaId, bool showId)
        {
            Clear();

            var data = new ProductivityAreaDataModel();
            data.ProductivityAreaId = ProductivityAreaId;

            var items = ProductivityAreaDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.ProductivityAreaId;
                oHistoryList.Setup(PrimaryEntity, ProductivityAreaId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new ProductivityAreaDataModel();

            SetData(data);
        }

        public void SetData(ProductivityAreaDataModel data)
        {
            SystemKeyId = data.ProductivityAreaId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblProductivityAreaId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ProductivityArea;
            PrimaryEntityKey = "ProductivityArea";
            FolderLocationFromRoot = "/Productivity";

            PlaceHolderCore = dynProductivityAreaId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtProductivityAreaId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}