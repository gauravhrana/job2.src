using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;



namespace Shared.UI.Web.Configuration.UserPreferenceKey.Controls
{
    public partial class Details : ControlDetails
    {

        #region Variables


        #endregion

		#region private methods

        protected override void ShowData(int userPreferenceKeyId)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new UserPreferenceKeyDataModel();
            data.UserPreferenceKeyId = userPreferenceKeyId;

			var items = UserPreferenceKeyDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);
            
            if (items.Count == 1)
            {
                var item = items[0];

                lblUserPreferenceKeyId.Text		= Convert.ToString(item.UserPreferenceKeyId);
                lblDataTypeId.Text				= Convert.ToString(item.DataTypeId);
                lblName.Text				    = Convert.ToString(item.Name);
				lblValue.Text				    = Convert.ToString(item.Value);
				lblDescription.Text				= Convert.ToString(item.Description);
                lblSortOrder.Text				= Convert.ToString(item.SortOrder);

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, userPreferenceKeyId, "UserPreferenceKey");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblUserPreferenceKeyId.Text = String.Empty;            
            lblDataTypeId.Text	= String.Empty;
            lblName.Text	= String.Empty;
			lblValue.Text = String.Empty;
            lblDescription.Text	= String.Empty;
            lblSortOrder.Text = String.Empty;
        }

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblUserPreferenceKeyIdText, lblNameText, lblDescriptionText
													  , lblSortOrderText});
			if (Cache[CacheConstants.UserPreferenceKeyLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)SystemEntity.UserPreferenceKey, Shared.WebCommon.UI.Web.SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.UserPreferenceKeyLabelDictionary, validColumns, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.UserPreferenceKeyLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)SystemEntity.UserPreferenceKey, SessionVariables.RequestProfile.AuditId, labelslist);

		}

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblUserPreferenceKeyIdText, lblNameText, lblDescriptionText, lblSortOrderText });
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
				lblUserPreferenceKeyIdText.Visible = isTesting;
				lblUserPreferenceKeyId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.UserPreferenceKeyLabelDictionary;
            PrimaryEntity = SystemEntity.UserPreferenceKey;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynUserPreferenceKeyId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

		#endregion

	}

}