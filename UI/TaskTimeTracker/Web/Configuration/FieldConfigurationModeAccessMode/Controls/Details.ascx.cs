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

namespace Shared.UI.Web.Configuration.FieldConfigurationModeAccessMode.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {
     
        #region private methods

        protected override void ShowData(int FieldConfigurationModeAccessModeId)
        {
			base.ShowData(FieldConfigurationModeAccessModeId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data                                = new FieldConfigurationModeAccessModeDataModel();

			data.FieldConfigurationModeAccessModeId = FieldConfigurationModeAccessModeId;

			var items                               = FieldConfigurationModeAccessModeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				SetData(item);

				oHistoryList.Setup(PrimaryEntity, FieldConfigurationModeAccessModeId, "FieldConfigurationModeAccessMode");
			}  
                
        }

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblFieldConfigurationModeAccessModeIdText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new FieldConfigurationModeAccessModeDataModel();

			SetData(data);
		}

		public void SetData(FieldConfigurationModeAccessModeDataModel item)
		{
			SystemKeyId = item.FieldConfigurationModeAccessModeId;

			base.SetData(item);
		}

        #endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

            // set basic variables
            DictionaryLabel = CacheConstants.FieldConfigurationModeAccessModeLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfigurationModeAccessMode;



            // set object variable reference            
            PlaceHolderCore = dynFieldConfigurationModeAccessModeId;
            PlaceHolderAuditHistory = dynAuditHistory;

            BorderDiv = borderdiv;

            CoreSystemKey = lblFieldConfigurationModeAccessModeId;
            CoreControlName = lblName;
            CoreControlDescription = lblDescription;
            CoreControlSortOrder = lblSortOrder;


			CoreUpdateInfo          = oUpdateInfo;
		}

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblFieldConfigurationModeAccessModeIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
            }
            PopulateLabelsText();
        }

        

        #endregion
    }
}