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

namespace Shared.UI.Web.Configuration.FieldConfiguration.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {        

        #region private methods
		
        protected override void ShowData(int fieldConfigurationId)
        {
			base.ShowData(fieldConfigurationId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new FieldConfigurationDataModel();
			dataQuery.FieldConfigurationId = fieldConfigurationId;

            var entityList = FieldConfigurationDataManager.GetEntityDetails(dataQuery, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfo);
			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{
					lblFieldConfigurationId.Text = entityItem.FieldConfigurationId.ToString();
					lblName.Text                 = entityItem.Name;
					lblValue.Text                = entityItem.Value;
					lblSystemEntityTypeId.Text   = entityItem.SystemEntityTypeId.ToString();
					lblWidth.Text                = entityItem.Width.ToString();
					lblFormatting.Text           = entityItem.Formatting;
					lblControlType.Text          = entityItem.ControlType;
					lblHorizontalAlignment.Text  = entityItem.HorizontalAlignment;
                    lblDisplayColumn.Text        = entityItem.DisplayColumn.ToString();
					lblCellCount.Text            = entityItem.CellCount.ToString();

					//oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.FieldConfiguration, fieldConfigurationId, "FieldConfiguration");

				}
			}
        }

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] {lblFieldConfigurationIdText
													  , lblNameText, lblValueText, lblSystemEntityTypeIdText
													  , lblWidthText, lblFormattingText, lblControlTypeText 
													  , lblHorizontalAlignmentText, lblDisplayColumnText, lblCellCountText});
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
                lblFieldConfigurationIdText.Visible = isTesting;
                lblFieldConfigurationId.Visible = isTesting;
            }
            PopulateLabelsText();
        }

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.FieldConfigurationLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.FieldConfiguration;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynFieldConfigurationId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

        #endregion

    }
}