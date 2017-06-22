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

namespace Shared.UI.Web.Configuration.FieldConfigurationMode.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {
        #region private methods

        protected override void ShowData(int fieldConfigurationModeId)
        {
			base.ShowData(fieldConfigurationModeId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new FieldConfigurationModeDataModel();

			data.FieldConfigurationModeId = fieldConfigurationModeId;

			var items = FieldConfigurationModeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				SetData(item);

				oHistoryList.Setup(PrimaryEntity, fieldConfigurationModeId, "FieldConfigurationMode");
			}  
                
        }

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblFieldConfigurationModeIdText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new FieldConfigurationModeDataModel();

			SetData(data);
		}

		public void SetData(FieldConfigurationModeDataModel item)
		{
			SystemKeyId = item.FieldConfigurationModeId;

			base.SetData(item);
		}

        #endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			// set basic variables
			DictionaryLabel = CacheConstants.FieldConfigurationModeLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfigurationMode;

			

			// set object variable reference            
			PlaceHolderCore = dynFieldConfigurationModeId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

			CoreSystemKey = lblFieldConfigurationModeId;
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
                lblFieldConfigurationModeIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
            }
            PopulateLabelsText();
        }

        

        #endregion
    }
}