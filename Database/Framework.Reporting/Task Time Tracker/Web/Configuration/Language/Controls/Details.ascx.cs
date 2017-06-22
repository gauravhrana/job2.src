using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.Framework.DataAccess;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Framework.Components.Core;

namespace Shared.UI.Web.Configuration.Language.Controls
{
    public partial class Details : ControlDetailsStandard
    {
        #region private methods

        protected override void ShowData(int LanguageId)
        {
            base.ShowData(LanguageId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new LanguageDataModel();
            data.LanguageId = LanguageId;

            var items = LanguageDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, LanguageId, "Language");
            }
        }

        public void SetData(LanguageDataModel data)
        {
            SystemKeyId = data.LanguageId;

            base.SetData(data);
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new[] { lblLanguageIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        #endregion

        #region Events

        protected override void Clear()
        {
            base.Clear();

            var data = new LanguageDataModel();

            SetData(data);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.LanguageLabelDictionary;
            PrimaryEntity = SystemEntity.Language;

            PlaceHolderCore = dynLanguageId;
            PlaceHolderAuditHistory = dynAuditHistory;

            BorderDiv = borderdiv;

            CoreSystemKey = lblLanguageId;
            CoreControlName = lblName;
            CoreControlDescription = lblDescription;
            CoreControlSortOrder = lblSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblLanguageIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }

}