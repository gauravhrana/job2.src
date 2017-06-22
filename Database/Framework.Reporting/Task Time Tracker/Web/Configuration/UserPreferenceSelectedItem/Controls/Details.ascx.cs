using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Configuration.UserPreferenceSelectedItem.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region variables


        #endregion

        #region private methods

       protected override void ShowData(int userPreferenceSelectedItemId)
		{
			oDetailButtonPanel.SetId = SetId;
			var data = new Framework.Components.UserPreference.UserPreferenceSelectedItemDataModel();
            data.UserPreferenceSelectedItemId = userPreferenceSelectedItemId;

			var items = Framework.Components.UserPreference.UserPreferenceSelectedItemDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
            {
                var item = items[0];

                lblUserPreferenceSelectedItemId.Text        = item.UserPreferenceSelectedItemId.ToString();
                lblApplicationUserId.Text                   = item.ApplicationUserId.ToString();
                lblParentKey.Text                           = item.ParentKey.ToString();
				lblUserPreferenceKeyId.Text                 = item.UserPreferenceKeyId.ToString();
				lblValue.Text					            = item.Value.ToString();
                lblSortOrder.Text                           = item.SortOrder.ToString();

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, userPreferenceSelectedItemId, "UserPreferenceSelectedItem");
			}
			else
			{
				Clear();
			}
		}

	   protected override void Clear()
	   {
		   lblUserPreferenceSelectedItemId.Text = String.Empty;
		   lblApplicationUserId.Text            = String.Empty;
		   lblParentKey.Text                    = String.Empty;
		   lblUserPreferenceKeyId.Text          = String.Empty;
		   lblValue.Text                        = String.Empty;
           lblSortOrder.Text                    = String.Empty;
	   }

		protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblUserPreferenceSelectedItemIdText, lblApplicationUserIdText, 
                    lblParentKeyText,lblUserPreferenceKeyIdText, lblValueText, lblSortOrderText});
			if (Cache[CacheConstants.UserPreferenceLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.UserPreferenceSelectedItem, Shared.WebCommon.UI.Web.SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.UserPreferenceLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);
			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.UserPreferenceLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.UserPreferenceSelectedItem, Shared.WebCommon.UI.Web.SessionVariables.RequestProfile.AuditId, labelslist);

		}

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblUserPreferenceKeyIdText, lblUserPreferenceSelectedItemIdText, lblApplicationUserIdText,
                    lblSortOrderText, lblParentKeyText, lblValueText});
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
				lblUserPreferenceSelectedItemIdText.Visible = isTesting;
				lblUserPreferenceSelectedItemId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.UserPreferenceSelectedItemLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserPreferenceSelectedItem;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynUserPreferenceSelectedItemId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

		#endregion

	}

}