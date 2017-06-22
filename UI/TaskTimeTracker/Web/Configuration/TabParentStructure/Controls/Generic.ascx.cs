using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using System.Data;
using Framework.Components.Core;

namespace Shared.UI.Web.Configuration.TabParentStructure.Controls
{
    public partial class Generic : ControlGenericStandard
    {
        public decimal? IsAllTab
        {
            get
            {
                return int.Parse(txtIsAllTab.Text);
            }
            set
            {
                txtIsAllTab.Text = (value == null) ? String.Empty : value.ToString();
            }
        }
        #region methods

        public override int? Save(string action)
        {
            var data = new TabParentStructureDataModel();

            data.TabParentStructureId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;
            data.IsAllTab = IsAllTab;
           
            if (action == "Insert")
            {
                if(!TabParentStructureDataManager.DoesExist(data, SessionVariables.RequestProfile))
                {
                    TabParentStructureDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                TabParentStructureDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.TabParentStructureId;
        }

        public override void SetId(int setId, bool chkTabParentStructureId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkTabParentStructureId);
            CoreSystemKey.Enabled = chkTabParentStructureId;
            //txtDescription.Enabled = !chkTabParentStructureId;
            //txtName.Enabled = !chkTabParentStructureId;
            //txtSortOrder.Enabled = !chkTabParentStructureId;
            //txtIsAllTab.Enabled = !chkIsAllTabId;
        }

        public void LoadData(int TabParentStructureId, bool showId)
        {
            Clear();

            var data = new TabParentStructureDataModel();
            data.TabParentStructureId = TabParentStructureId;

            var items = TabParentStructureDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];
            txtIsAllTab.Text = item.IsAllTab.ToString();
            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.TabParentStructureId;
                oHistoryList.Setup(PrimaryEntity, TabParentStructureId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TabParentStructureDataModel();

            SetData(data);
        }

        public void SetData(TabParentStructureDataModel data)
        {
            SystemKeyId = data.TabParentStructureId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblTabParentStructureId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.TabParentStructure;
            PrimaryEntityKey = "TabParentStructure";
            FolderLocationFromRoot = "/RequirementAnalysis";

            PlaceHolderCore = dynTabParentStructureId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtTabParentStructureId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}