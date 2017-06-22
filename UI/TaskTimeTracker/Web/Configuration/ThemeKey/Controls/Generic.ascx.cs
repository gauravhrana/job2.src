using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.ThemeKey.Controls
{
    public partial class Generic : ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new ThemeKeyDataModel();

            data.ThemeKeyId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;
            //data.IsAllTab = IsAllTab;

            if (action == "Insert")
            {
				if(!ThemeKeyDataManager.DoesExist(data, SessionVariables.RequestProfile))
                {
					ThemeKeyDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				ThemeKeyDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.ThemeKeyId;
        }

        public override void SetId(int setId, bool chkThemeKeyId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkThemeKeyId);
            CoreSystemKey.Enabled = chkThemeKeyId;
            //txtDescription.Enabled = !chkThemeKeyId;
            //txtName.Enabled = !chkThemeKeyId;
            //txtSortOrder.Enabled = !chkThemeKeyId;
            //txtIsAllTab.Enabled = !chkThemeKeyId;
        }

        public void LoadData(int themeKeyId, bool showId)
        {
            Clear();

            var data = new ThemeKeyDataModel();
			data.ThemeKeyId = themeKeyId;

			var items = ThemeKeyDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.ThemeKeyId;
                // oHistoryList.Setup(PrimaryEntity, ThemeKeyId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new ThemeKeyDataModel();

            SetData(data);
        }

        public void SetData(ThemeKeyDataModel data)
        {
            SystemKeyId = data.ThemeKeyId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblThemeKeyId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.ThemeKey;
            PrimaryEntityKey = "Theme";
            FolderLocationFromRoot = "Theme";

            PlaceHolderCore = dynThemeKeyId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtThemeKeyId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}