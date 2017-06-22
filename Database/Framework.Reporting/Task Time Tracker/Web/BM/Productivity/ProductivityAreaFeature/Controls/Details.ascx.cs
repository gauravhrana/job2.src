using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TaskTimeTracker.Components.BusinessLayer;


namespace ApplicationContainer.UI.Web.Productivity.ProductivityAreaFeature.Controls
{
    public partial class Details : ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int productivityAreaFeatureFeatureId)
        {
            base.ShowData(productivityAreaFeatureFeatureId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

			var data = new ProductivityAreaFeatureDataModel();
            data.ProductivityAreaFeatureId = productivityAreaFeatureFeatureId;

            var items = ProductivityAreaFeatureDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, productivityAreaFeatureFeatureId, "ProductivityAreaFeature");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblProductivityAreaFeatureIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

			var data = new ProductivityAreaFeatureDataModel();

            SetData(data);
        }

		public void SetData(ProductivityAreaFeatureDataModel item)
        {
            SystemKeyId = item.ProductivityAreaFeatureId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.ProductivityAreaFeatureLabelDictionary;
            PrimaryEntity = SystemEntity.ProductivityAreaFeature;

            PlaceHolderCore = dynProductivityAreaFeatureId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblProductivityAreaFeatureId;
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
                lblProductivityAreaFeatureIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}