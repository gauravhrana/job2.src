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

namespace Shared.UI.Web.Configuration.Menu.Controls
{
    public partial class Details : ControlDetails
    {

        #region private methods

        protected override void ShowData(int menuid)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new MenuDataModel();
            data.MenuId = menuid;
            var items = MenuDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblMenuId.Text             = item.MenuId.ToString();
                lblName.Text               = item.Name;
                lblDescription.Text        = item.Description;
                lblSortOrder.Text          = item.SortOrder.ToString();
                lblParentMenuId.Text       = item.ParentMenuId.ToString();
                lblPrimaryDeveloper.Text   = item.PrimaryDeveloper;
                lblIsVisible.Text          = item.IsVisible.ToString();
                lblIsChecked.Text          = item.IsChecked.ToString();
                lblNavigateURL.Text        = item.NavigateURL;

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, menuid, "Menu");
                
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblMenuId.Text             = String.Empty;
            lblName.Text               = String.Empty;
            lblDescription.Text        = String.Empty;
            lblSortOrder.Text          = String.Empty;
            lblParentMenuId.Text       = String.Empty;
            lblPrimaryDeveloper.Text   = String.Empty;
            lblIsVisible.Text          = String.Empty;
            lblIsChecked.Text          = String.Empty;
            lblNavigateURL.Text        = String.Empty;
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblTextMenuId, lblNameText, lblParentMeuText, lblPrimaryDeveloperText, lblDescriptionText,
                    lblSortOrderText, lblIsVisibleText, lblIsCheckedText, lblNavigateURLText });
            }

            return LabelListCore;
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblMenuId.Visible = false;
                lblTextMenuId.Visible = false;
            }

            PopulateLabelsText();
        }

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            // set basic variables
            DictionaryLabel         = CacheConstants.MenuCategoryLabelDictionary;
            PrimaryEntity           = SystemEntity.Menu;

            // set object variable reference            
            PlaceHolderCore         = dynMenuId;
            PlaceHolderAuditHistory = dynAuditHistory;
            MainTable               = tblMain1;
            BorderDiv               = borderdiv;
        }

        #endregion

    }
}