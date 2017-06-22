using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace Shared.UI.Web.Configuration.FieldConfigurationModeCategory.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {
        #region private methods        

        protected override void ShowData(int fieldConfigurationModeCategoryId)
        {
			base.ShowData(fieldConfigurationModeCategoryId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new FieldConfigurationModeCategoryDataModel();
			data.FieldConfigurationModeCategoryId = fieldConfigurationModeCategoryId;

			var items = FieldConfigurationModeCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				SetData(item);

				oHistoryList.Setup(PrimaryEntity, fieldConfigurationModeCategoryId, "FieldConfigurationModeCategory");
			}            
        }

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblFieldConfigurationModeCategoryId, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new FieldConfigurationModeCategoryDataModel();

			SetData(data);
		}

		public void SetData(FieldConfigurationModeCategoryDataModel item)
		{
			SystemKeyId = item.FieldConfigurationModeCategoryId;

			base.SetData(item);
		}

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblFieldConfigurationModeCategoryIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
            }
            PopulateLabelsText();
        }

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.FieldConfigurationModeCategoryLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeCategory;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynFieldConfigurationModeCategoryId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

			CoreSystemKey = lblFieldConfigurationModeCategoryId;
			CoreControlName = lblName;
			CoreControlDescription = lblDescription;
			CoreControlSortOrder = lblSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

        #endregion
    }
}