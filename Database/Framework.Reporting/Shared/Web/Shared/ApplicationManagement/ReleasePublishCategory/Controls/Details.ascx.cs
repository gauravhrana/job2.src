using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.UI.Web.CommonCode;

namespace Shared.UI.Web.ApplicationManagement.ReleasePublishCategory.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {
        #region private methods		      

        protected override void ShowData(int releasePublishCategoryId)
        {
			base.ShowData(releasePublishCategoryId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ReleasePublishCategoryDataModel();
			data.ReleasePublishCategoryId = releasePublishCategoryId;

			var items = Framework.Components.ReleaseLog.ReleasePublishCategoryDataManager.GetEntityDetails(data, AuditId);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				SetData(item);

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, releasePublishCategoryId, "ReleasePublishCategory");
			}  
        }

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblReleasePublishCategoryId, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ReleasePublishCategoryDataModel();

			SetData(data);

		}

		public void SetData(ReleasePublishCategoryDataModel data)
		{
			SystemKeyId = data.ReleasePublishCategoryId;

			base.SetData(data);
		}

        #endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ReleasePublishCategoryLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleasePublishCategory;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynReleasePublishCategoryId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

			CoreSystemKey			= lblReleasePublishCategoryId;
			CoreControlName			= lblName;
			CoreControlDescription	= lblDescription;
			CoreControlSortOrder	= lblSortOrder;

			CoreUpdateInfo = oUpdateInfo;
		}

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblReleasePublishCategoryIdText.Visible = isTesting;
                lblReleasePublishCategoryId.Visible = isTesting;
            }
            PopulateLabelsText();
        }

        

        #endregion
    }
}