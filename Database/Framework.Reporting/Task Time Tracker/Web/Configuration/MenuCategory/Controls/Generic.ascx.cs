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

namespace Shared.UI.Web.Configuration.MenuCategory.Controls
{
    public partial class Generic : ControlGenericStandard
    {

        #region methods

        public override int? Save(string action)
        {
            var data = new MenuCategoryDataModel();

            data.MenuCategoryId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
				var dtMenuCategory = MenuCategoryDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtMenuCategory.Rows.Count == 0)
                {
					MenuCategoryDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				MenuCategoryDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.MenuCategoryId;
        }

        public override void SetId(int setId, bool chkMenuCategoryId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkMenuCategoryId);
            CoreSystemKey.Enabled = chkMenuCategoryId;
            //txtDescription.Enabled = !chkMenuCategoryId;
            //txtName.Enabled = !chkMenuCategoryId;
            //txtSortOrder.Enabled = !chkMenuCategoryId;
        }

        public void LoadData(int menuCategoryId, bool showId)
        {
            Clear();

            var data = new MenuCategoryDataModel();
			data.MenuCategoryId = menuCategoryId;

			var items = MenuCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.MenuCategoryId;
                oHistoryList.Setup(PrimaryEntity, menuCategoryId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new MenuCategoryDataModel();

            SetData(data);
        }

		public void SetData(MenuCategoryDataModel data)
        {
            SystemKeyId = data.MenuCategoryId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblMenuCategoryId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity					= SystemEntity.MenuCategory;
            PrimaryEntityKey				= "MenuCategory";
            FolderLocationFromRoot			= "MenuCategory";

            PlaceHolderCore					= dynMenuCategoryId;
            PlaceHolderAuditHistory			= dynAuditHistory;
            BorderDiv						= borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey					= txtMenuCategoryId;
            CoreControlName					= txtName;
            CoreControlDescription			= txtDescription;
            CoreControlSortOrder			= txtSortOrder;

            CoreUpdateInfo					= oUpdateInfo;
        }

        #endregion

    }
}