using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.Configuration.ApplicationEntityParentalHierarchy.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new ApplicationEntityParentalHierarchyDataModel();

            data.ApplicationEntityParentalHierarchyId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
				var dtApplicationEntityParentalHierarchy = Framework.Components.Core.ApplicationEntityParentalHierarchyDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtApplicationEntityParentalHierarchy.Rows.Count == 0)
                {
					Framework.Components.Core.ApplicationEntityParentalHierarchyDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				Framework.Components.Core.ApplicationEntityParentalHierarchyDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.ApplicationEntityParentalHierarchyId;
        }

        public override void SetId(int setId, bool chkApplicationEntityParentalHierarchyId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkApplicationEntityParentalHierarchyId);
            CoreSystemKey.Enabled = chkApplicationEntityParentalHierarchyId;
            //txtDescription.Enabled = !chkApplicationEntityParentalHierarchyId;
            //txtName.Enabled = !chkApplicationEntityParentalHierarchyId;
            //txtSortOrder.Enabled = !chkApplicationEntityParentalHierarchyId;
        }

        public void LoadData(int applicationEntityParentalHierarchyId, bool showId)
        {
            Clear();

            var data = new ApplicationEntityParentalHierarchyDataModel();
            data.ApplicationEntityParentalHierarchyId = SystemKeyId;

			var items = Framework.Components.Core.ApplicationEntityParentalHierarchyDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.ApplicationEntityParentalHierarchyId;
                oHistoryList.Setup(PrimaryEntity, applicationEntityParentalHierarchyId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new ApplicationEntityParentalHierarchyDataModel();

            SetData(data);
        }

		public void SetData(ApplicationEntityParentalHierarchyDataModel data)
        {
            SystemKeyId = data.ApplicationEntityParentalHierarchyId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblApplicationEntityParentalHierarchyId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationEntityParentalHierarchy;
            PrimaryEntityKey = "ApplicationEntityParentalHierarchy";
            FolderLocationFromRoot = "ApplicationEntityParentalHierarchy";

            PlaceHolderCore = dynApplicationEntityParentalHierarchyId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtApplicationEntityParentalHierarchyId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}