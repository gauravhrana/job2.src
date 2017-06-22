using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Framework.Components;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Configuration.Menu.Controls
{
    public partial class Generic : ControlGeneric
    {

        #region properties

        public int? MenuId
        {
            get
            {
                if (txtMenuId.Enabled)
                {
                    return DefaultDataRules.CheckAndGetEntityId(txtMenuId.Text, SessionVariables.IsTesting);
                }
                else
                {
                    return int.Parse(txtMenuId.Text);
                }
            }
            set
            {
                txtMenuId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public string Name
        {
            get
            {
                return txtName.Text;
            }
            set
            {
                txtName.Text = value ?? String.Empty;
            }
        }

        public string PrimaryDeveloper
        {
            get
            {
                return string.IsNullOrEmpty(txtPrimaryDeveloper.Text.Trim()) == true ? " " : txtPrimaryDeveloper.Text.Trim();            
            }
            set
            {
                txtPrimaryDeveloper.Text = value ?? String.Empty;
            }
        }

        public string DisplayName
        {
            get
            {
                return txtDisplayName.Text;
            }
            set
            {
                txtDisplayName.Text = value ?? String.Empty;
            }
        }

        public string Description
        {
            get
            {
                return DefaultDataRules.CheckAndGetDescription(txtName.Text, txtDescription.Value);
            }
            set
            {
                txtDescription.Value = value ?? String.Empty;
            }
        }

        public int? SortOrder
        {
            get
            {
                return DefaultDataRules.CheckAndGetSortOrder(txtSortOrder.Text);
            }
            set
            {
                txtSortOrder.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public int? ParentMenuId
        {
            get
            {
                if (drpParentMenu.SelectedValue != "-1")
                {
					var isTesting = SessionVariables.IsTesting;
					if (isTesting)
                        return int.Parse(txtParentMenuId.Text.Trim());
					else
						return int.Parse(drpParentMenu.SelectedItem.Value);
                }
                else
                    return null;
            }
            set
            {
                txtParentMenuId.Text = (value == null) ? String.Empty : value.ToString();
            }
        }

        public int? IsVisible
        {
            get
            {
                return chkIsVisible.Checked == true ? 1 : 0;
            }
        }

        public int? IsChecked
        {
            get
            {
                return chkIsChecked.Checked == true ? 1 : 0;
            }
        }

        public string NavigateURL
        {
            get
            {
                return txtNavigateURL.Text;
            }
            set
            {
                txtNavigateURL.Text = value ?? String.Empty;
            }
        }

        protected override string ValidationConfigFile
        {
            get
            {
                return Server.MapPath("~/Shared/Configuration/Menu/Controls/Validation.xml"); //"R:\Layers\Task Time Tracker\Dev\UI\Web\ValidationDataFile\dvc.xml";
            }
        }

        #endregion properties

        #region methods

        public override int? Save(string action)
        {
            var data = new MenuDataModel();

            data.Name             = Name;
            data.Description      = Description;
            data.SortOrder        = SortOrder;
            data.MenuDisplayName  = DisplayName;
            data.NavigateURL      = NavigateURL;
            data.IsChecked        = IsChecked;
            data.IsVisible        = IsVisible;
            data.ParentMenuId     = ParentMenuId;
            data.PrimaryDeveloper = PrimaryDeveloper;

            if (action == "Insert")
            {
                var dtMenu = MenuDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtMenu.Rows.Count == 0)
                {
                    data.MenuId = MenuDataManager.Create(data, SessionVariables.RequestProfile);

                    var dataDisplayName        = new MenuDisplayNameDataModel();
                    dataDisplayName.MenuId     = data.MenuId;
                    dataDisplayName.Value      = DisplayName;
                    dataDisplayName.LanguageId = ApplicationCommon.LanguageId;
                    dataDisplayName.IsDefault  = 1;

					MenuDisplayNameDataManager.Create(dataDisplayName, SessionVariables.RequestProfile);
					
					
                }
					
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                data.MenuId = MenuId;
                MenuDataManager.Update(data, SessionVariables.RequestProfile);
            }

            // not correct ... when doing insert, we didn't get/change the value of ClientID ?
            return data.MenuId;
        }

        public override void SetId(int setId, bool chkMenuId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkMenuId);
            txtMenuId.Enabled = chkMenuId;
            txtDisplayName.Enabled = chkMenuId;
        }

        public void LoadData(int menuId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new MenuDataModel();
            data.MenuId = menuId;

            // get data
            var items = MenuDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

            txtDescription.Value        = item.Description;
            txtName.Text                = item.Name;
            txtSortOrder.Text           = item.SortOrder.ToString();
            txtDisplayName.Text         = item.MenuDisplayName;
            txtNavigateURL.Text         = item.NavigateURL;
            txtParentMenuId.Text        = item.ParentMenuId.ToString();
            txtPrimaryDeveloper.Text    = item.PrimaryDeveloper;
			txtSortOrder.Text			= item.SortOrder.ToString();

            if (item.ParentMenuId != null)
            {
				var parentMenu = new MenuDataModel();
				parentMenu.ParentMenuId= item.ParentMenuId;
				parentMenu.Name = item.Name;
				parentMenu.PrimaryDeveloper = item.PrimaryDeveloper;
				var parentMenudata = MenuDataManager.GetDetails(parentMenu, SessionVariables.RequestProfile);
				drpParentMenu.SelectedItem.Value = parentMenudata.ToString();
            }

            if (!showId)
            {
                txtMenuId.Text = item.MenuId.ToString();
                //PlaceHolderAuditHistory.Visible = true;
                // only show Audit History in case of Update page, not for Clone.
                oHistoryList.Setup(PrimaryEntity, menuId, PrimaryEntityKey);
            }
            else
            {
                txtMenuId.Text = String.Empty;
            }

            oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

        }

        protected override void Clear()
        {
            base.Clear();

            var data = new MenuDataModel();

            MenuId           = data.MenuId;
            Name             = data.Name;
            Description      = data.Description;
            SortOrder        = data.SortOrder;
            DisplayName      = data.MenuDisplayName;
            NavigateURL      = data.NavigateURL;
            ParentMenuId     = data.ParentMenuId;
            PrimaryDeveloper = data.PrimaryDeveloper;

        }

        private void SetupDropdown()
        {
            var isTesting = SessionVariables.IsTesting;

			drpParentMenu.ClearSelection();

            var parentMenudata = MenuDataManager.GetList(null, SessionVariables.RequestProfile);
			UIHelper.LoadDropDown(parentMenudata, drpParentMenu, MenuDataModel.DataColumns.MenuDisplayName,
                MenuDataModel.DataColumns.MenuId);

            if (isTesting)
            {
                drpParentMenu.AutoPostBack = true;
                if (drpParentMenu.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtParentMenuId.Text.Trim()))
                    {
                        drpParentMenu.SelectedValue = txtParentMenuId.Text;
                    }
                    else
                    {
                        txtParentMenuId.Text = drpParentMenu.SelectedItem.Value;
                    }
                }
                txtParentMenuId.Visible = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtParentMenuId.Text.Trim()))
                {
                    drpParentMenu.SelectedValue = txtParentMenuId.Text;
                }
            }
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            txtMenuId.Visible = isTesting;
            lblMenuId.Visible = isTesting;
            if (!IsPostBack)
            {
                SetupDropdown();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey                = "Menu";
            FolderLocationFromRoot          = "/Shared/Configuration";
            PrimaryEntity                   = SystemEntity.Menu;

            // set object variable reference            
            PlaceHolderCore                 = dynMenuId;
            PlaceHolderAuditHistory         = dynAuditHistory;
            BorderDiv                       = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);
        }

        protected void drpParentMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtParentMenuId.Text = drpParentMenu.SelectedItem.Value;
        }

        #endregion

    }
}