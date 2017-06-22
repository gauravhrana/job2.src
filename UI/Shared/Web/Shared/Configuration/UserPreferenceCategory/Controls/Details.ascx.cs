using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Shared.WebCommon.UI.Web;



namespace Shared.UI.Web.Configuration.UserPreferenceCategory.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region variables


        #endregion

        #region private methods

        protected override void ShowData(int userpreferencecategoryId)
        {
            oDetailButtonPanel.SetId = SetId;
            var data = new UserPreferenceCategoryDataModel();
            data.UserPreferenceCategoryId = userpreferencecategoryId;

            var items = Framework.Components.UserPreference.UserPreferenceCategoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblUserPreferenceCategoryId.Text = Convert.ToString(item.UserPreferenceCategoryId);
                lblName.Text                     = Convert.ToString(item.Name);
                lblDescription.Text              = Convert.ToString(item.Description);
                lblSortOrder.Text                = Convert.ToString(item.SortOrder);

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, userpreferencecategoryId, "UserPreferenceCategory");
            }
            else
            {
                Clear();
            }
        }

		protected override void Clear()
		{
			lblUserPreferenceCategoryId.Text = String.Empty;
			lblName.Text = String.Empty;
			lblDescription.Text = String.Empty;
			lblSortOrder.Text = String.Empty;
		}

        protected override void PopulateLabelsText()
		{
			var validColumns = new Dictionary<string, string>();
			var labelslist = new List<Label>(new Label[] { lblUserPreferenceCategoryIdText, lblNameText, lblDescriptionText
													  , lblSortOrderText });
			if (Cache[CacheConstants.UserPreferenceCategoryLabelDictionary] == null)
			{
				validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.UserPreferenceCategory, Shared.WebCommon.UI.Web.SessionVariables.RequestProfile.AuditId);
				Cache.Add(CacheConstants.UserPreferenceCategoryLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

			}
			else
			{
				validColumns = (Dictionary<string, string>)Cache[CacheConstants.UserPreferenceCategoryLabelDictionary];
			}
			UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.UserPreferenceCategory, SessionVariables.RequestProfile.AuditId, labelslist);


		}

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblUserPreferenceCategoryIdText, lblNameText, lblDescriptionText, lblSortOrderText });
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
				lblUserPreferenceCategoryIdText.Visible = isTesting;
				lblUserPreferenceCategoryId.Visible = isTesting;
			}
			PopulateLabelsText();
		}

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.UserPreferenceCategoryLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserPreferenceCategory;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynUserPreferenceCategoryId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;
        }

		#endregion

	}

}