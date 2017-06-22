using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.Controls
{
    public partial class ControlVisibilityManager : UserControl
    {

        #region Variables

        private ManageControlVisibilityDelegate _manageControlVisibility;
        string ParentSettingCategory;

        #endregion

        #region Methods

        public delegate void ManageControlVisibilityDelegate(string controlTitle);

        private MenuItem AddSettingsMenu()
        {
            var menuItem = new MenuItem();
            menuItem.Value = "View Settings";
            menuItem.NavigateUrl = "~/ViewSettings/" + ParentSettingCategory;
            return menuItem;
        }

        public void ClearChildMenuItems()
        {
            managerMenu.Items[0].ChildItems.Clear();
            managerMenu.Items[0].ChildItems.Add(AddSettingsMenu());
        }

        public void AddChildControl(string detailControlTitle, bool detailControlVisible)
        {
            if (!detailControlVisible)
            {
                var menuItem = new MenuItem(detailControlTitle);
                //menuItem.NavigateUrl = "";
                managerMenu.Items[0].ChildItems.Add(menuItem);
            }
        }

        public void Setup(ManageControlVisibilityDelegate manageControlVisibility, string parentSettingCategory)
        {
            _manageControlVisibility = manageControlVisibility;
            ParentSettingCategory = parentSettingCategory;
        }

        #endregion

        #region Events

        protected void managerMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (_manageControlVisibility != null)
            {
                _manageControlVisibility(e.Item.Text);
                managerMenu.Items[0].ChildItems.Remove(e.Item);
            }
        }

        #endregion

    }
}