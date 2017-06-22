using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Configuration.UserPreferenceDataType.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region variables

		#endregion

		#region private methods

		protected override void ShowData(int userPreferenceDataTypeId)
		{
			oDetailButtonPanel.SetId = SetId;
			var data = new DataModel.Framework.Configuration.UserPreferenceDataTypeDataModel();
			data.UserPreferenceDataTypeId = userPreferenceDataTypeId;

			var items = Framework.Components.UserPreference.UserPreferenceDataTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

				lblUserPreferenceDataTypeId.Text	 = Convert.ToString(item.UserPreferenceDataTypeId);
				lblName.Text						 = Convert.ToString(item.Name);
				lblDescription.Text					 = Convert.ToString(item.Description);
				lblSortOrder.Text					 = Convert.ToString(item.SortOrder);

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, userPreferenceDataTypeId, "UserPreferenceDataType");
			}
			else
			{
				Clear();
			}
		}

		protected override void Clear()
		{
			lblUserPreferenceDataTypeId.Text = String.Empty;
			lblName.Text = String.Empty;
			lblDescription.Text = String.Empty;
			lblSortOrder.Text = String.Empty;
		}

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblUserPreferenceDataTypeIdText, lblNameText, lblDescriptionText
													  , lblSortOrderText});
			
			if (Cache[CacheConstants.UserPreferenceDataTypeLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.UserPreferenceDataType, Shared.WebCommon.UI.Web.SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.UserPreferenceDataTypeLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.UserPreferenceDataTypeLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.UserPreferenceDataType, SessionVariables.RequestProfile.AuditId, labelslist);


		}

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblUserPreferenceDataTypeIdText, lblNameText, lblDescriptionText, lblSortOrderText });
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
				lblUserPreferenceDataTypeIdText.Visible = isTesting;
				lblUserPreferenceDataTypeId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.UserPreferenceDataTypeLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserPreferenceDataType;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynUserPreferenceDataTypeId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

		#endregion

	}

}