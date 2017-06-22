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

namespace Shared.UI.Web.Configuration.ThemeKey.Controls
{
    public partial class Details : ControlDetails
    {

        #region private methods

        protected override void ShowData(int ThemeKeyId)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new ThemeKeyDataModel();
            data.ThemeKeyId = ThemeKeyId;

			var items = ThemeKeyDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblThemeKeyId.Text = item.ThemeKeyId.ToString();
                lblName.Text = item.Name;
                lblDescription.Text = item.Description;
                lblSortOrder.Text = item.SortOrder.ToString();
                //lblIsAllTab.Text = item.IsAllTab.ToString();

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, ThemeKeyId, "ThemeKey");
            }
            else
            {
                Clear();
            }
        }

        protected override void PopulateLabelsText()
        {
            var validColumns = new Dictionary<string, string>();
            var labelslist = new List<Label>(new Label[] { lblThemeKeyIdText
													  , lblNameText, lblDescriptionText, lblSortOrderText});
            if (Cache[CacheConstants.ThemeKeyLabelDictionary] == null)
            {
                validColumns = UIHelper.GetLabelDictonaryObject((int)SystemEntity.ThemeKey, SessionVariables.RequestProfile.AuditId);
                Cache.Add(CacheConstants.ThemeKeyLabelDictionary, validColumns, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), CacheItemPriority.Default, null);

            }
            else
            {
                validColumns = (Dictionary<string, string>)Cache[CacheConstants.ThemeKeyLabelDictionary];
            }
            UIHelper.PopulateLabelsText(validColumns, (int)SystemEntity.ThemeKey, SessionVariables.RequestProfile.AuditId, labelslist);


        }

        protected override void Clear()
        {
            lblThemeKeyId.Text = String.Empty;
            lblName.Text = String.Empty;
            lblDescription.Text = String.Empty;
            lblSortOrder.Text = String.Empty;
            //lblIsAllTab.Text = String.Empty;
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblThemeKeyIdText, lblNameText, lblDescriptionText, lblSortOrderText });
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
                lblThemeKeyIdText.Visible = isTesting;
                lblThemeKeyId.Visible = isTesting;
            }
            PopulateLabelsText();
        }

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.ThemeKeyLabelDictionary;
            PrimaryEntity = SystemEntity.ThemeKey;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynThemeKeyId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

        #endregion

    }
}