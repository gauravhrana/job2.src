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
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region private methods
        
        protected override void ShowData(int ThemeCategoryId)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new ThemeCategoryDataModel();
            data.ThemeCategoryId = ThemeCategoryId;

			var items = Framework.Components.Core.ThemeCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblThemeCategoryId.Text = item.ThemeCategoryId.ToString();
                lblName.Text = item.Name;
                lblDescription.Text = item.Description;
                lblSortOrder.Text = item.SortOrder.ToString();
                //lblIsAllTab.Text = item.IsAllTab.ToString();

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, ThemeCategoryId, "ThemeCategory");
            }
            else
            {
                Clear();
            }
        }

        protected override void PopulateLabelsText()
        {
            var validColumns = new Dictionary<string, string>();
            var labelslist = new List<Label>(new Label[] { lblThemeCategoryIdText
													  , lblNameText, lblDescriptionText, lblSortOrderText});
            if (Cache[CacheConstants.ThemeCategoryLabelDictionary] == null)
            {
                validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.ThemeCategory, SessionVariables.RequestProfile.AuditId);
                Cache.Add(CacheConstants.ThemeCategoryLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

            }
            else
            {
                validColumns = (Dictionary<string, string>)Cache[CacheConstants.ThemeCategoryLabelDictionary];
            }
            UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.ThemeCategory, SessionVariables.RequestProfile.AuditId, labelslist);


        }

        protected override void Clear()
        {
            lblThemeCategoryId.Text = String.Empty;
            lblName.Text = String.Empty;
            lblDescription.Text = String.Empty;
            lblSortOrder.Text = String.Empty;
            //lblIsAllTab.Text = String.Empty;
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblThemeCategoryIdText, lblNameText, lblDescriptionText, lblSortOrderText });
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
                lblThemeCategoryIdText.Visible = isTesting;
                lblThemeCategoryId.Visible = isTesting;
            }
            PopulateLabelsText();
        }

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.ThemeCategoryLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ThemeCategory;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynThemeCategoryId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

        #endregion

    }
}