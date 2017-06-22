using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Configuration.MenuCategory.Controls
{
    public partial class Details : ControlDetails
    {

        #region variables

        

#endregion

        #region private methods

        protected override void ShowData(int menuCategoryId)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new MenuCategoryDataModel();
            data.MenuCategoryId = menuCategoryId;

			var items = MenuCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblMenuCategoryId.Text = item.MenuCategoryId.ToString();
                lblName.Text = item.Name.ToString();
                lblDescription.Text = item.Description.ToString();
                lblSortOrder.Text = item.SortOrder.ToString();

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, menuCategoryId, "MenuCategoryId");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblMenuCategoryId.Text = String.Empty;
            lblName.Text = String.Empty;
            lblDescription.Text = String.Empty;
            lblSortOrder.Text = String.Empty;
        }

        protected override void PopulateLabelsText()
        {
            var validColumns = new Dictionary<string, string>();
            var labelslist = new List<Label>(new Label[] { lblMenuCategoryIdText
													  , lblNameText, lblDescriptionText, lblSortOrderText});
            if (Cache[CacheConstants.MenuCategoryLabelDictionary] == null)
            {
                validColumns = UIHelper.GetLabelDictonaryObject((int)SystemEntity.MenuCategory, SessionVariables.RequestProfile.AuditId);
                Cache.Add(CacheConstants.SystemEntityTypeLabelDictionary, validColumns, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), CacheItemPriority.Default, null);

            }
            else
            {
                validColumns = (Dictionary<string, string>)Cache[CacheConstants.MenuCategoryLabelDictionary];
            }
            UIHelper.PopulateLabelsText(validColumns, (int)SystemEntity.MenuCategory, SessionVariables.RequestProfile.AuditId, labelslist);


        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblMenuCategoryIdText, lblNameText, lblDescriptionText, lblSortOrderText });
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
                lblMenuCategoryIdText.Visible = isTesting;
                lblMenuCategoryId.Visible = isTesting;
            }
            PopulateLabelsText();
        }

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.MenuCategoryLabelDictionary;
            PrimaryEntity = SystemEntity.MenuCategory;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynMenuCategoryId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

        #endregion

    }
}