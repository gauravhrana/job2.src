using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.UserPreference;
using Shared.WebCommon.UI.Web;


namespace Shared.UI.Web.Configuration.UserPreference.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region variables


        #endregion

        #region private methods

       protected override void ShowData(int userPreferenceId)
		{
			oDetailButtonPanel.SetId = SetId;
			var data = new UserPreferenceDataModel();
			data.UserPreferenceId = userPreferenceId;

			var items = UserPreferenceDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
            {
                var item =items[0];

                lblUserPreferenceId.Text        = item.UserPreferenceId.ToString();
                lblApplicationUserId.Text       = item.ApplicationUserId.ToString();
                lblUserPreferenceCategory.Text  = item.UserPreferenceCategoryId.ToString();                
                lblDataTypeId.Text              = item.DataTypeId.ToString();
				lblUserPreferenceKeyId.Text     = item.UserPreferenceKeyId.ToString();
				lblValue.Text					= item.Value.ToString();

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, userPreferenceId, "UserPreference");
			}
			else
			{
				Clear();
			}
		}

	   protected override void Clear()
	   {
		   lblUserPreferenceId.Text = String.Empty;
		   lblApplicationUserId.Text = String.Empty;
		   lblUserPreferenceCategory.Text = String.Empty;
		   lblDataTypeId.Text = String.Empty;
		   lblUserPreferenceKeyId.Text = String.Empty;
		   lblValue.Text = String.Empty;
	   }

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblUserPreferenceIdText, lblApplicationUserIdText, lblUserPreferenceCategoryText
													  , lblDataTypeIdText,lblUserPreferenceKeyIdText, lblValueText});
			if (Cache[CacheConstants.UserPreferenceLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.UserPreference, Shared.WebCommon.UI.Web.SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.UserPreferenceLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.UserPreferenceLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.UserPreference, SessionVariables.RequestProfile.AuditId, labelslist);

		}

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblUserPreferenceIdText, lblUserPreferenceCategoryText, lblUserPreferenceKeyIdText, 
                    lblValueText, lblDataTypeIdText, lblApplicationUserIdText });
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
				lblUserPreferenceIdText.Visible = isTesting;
				lblUserPreferenceId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.UserPreferenceLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserPreference;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynUserPreferenceId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

		#endregion

	}

}