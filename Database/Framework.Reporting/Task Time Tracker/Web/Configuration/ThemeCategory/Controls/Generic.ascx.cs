using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.ThemeCategory.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new ThemeCategoryDataModel();

            data.ThemeCategoryId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;
            //data.IsAllTab = IsAllTab;

            if (action == "Insert")
            {
				var dtTheme = Framework.Components.Core.ThemeCategoryDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtTheme.Rows.Count == 0)
                {
					Framework.Components.Core.ThemeCategoryDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				Framework.Components.Core.ThemeCategoryDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.ThemeCategoryId;
        }

        public override void SetId(int setId, bool chkThemeCategoryId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkThemeCategoryId);
            CoreSystemKey.Enabled = chkThemeCategoryId;
            //txtDescription.Enabled = !chkThemeCategoryId;
            //txtName.Enabled = !chkThemeCategoryId;
            //txtSortOrder.Enabled = !chkThemeCategoryId;
            //txtIsAllTab.Enabled = !chkThemeCategoryId;
        }

        public void LoadData(int themeCategoryId, bool showId)
        {
            Clear();

            var data = new ThemeCategoryDataModel();
			data.ThemeCategoryId = themeCategoryId;

			var items = Framework.Components.Core.ThemeCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.ThemeCategoryId;
               // oHistoryList.Setup(PrimaryEntity, ThemeCategoryId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new ThemeCategoryDataModel();

            SetData(data);
        }

		public void SetData(ThemeCategoryDataModel data)
        {
            SystemKeyId = data.ThemeCategoryId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblThemeCategoryId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ThemeCategory;
            PrimaryEntityKey = "ThemeCategory";
            FolderLocationFromRoot = "Theme";

            PlaceHolderCore = dynThemeCategoryId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtThemeCategoryId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}