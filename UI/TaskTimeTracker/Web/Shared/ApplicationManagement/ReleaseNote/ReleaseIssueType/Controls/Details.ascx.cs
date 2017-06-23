using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace Shared.UI.Web.ApplicationManagement.ReleaseIssueType.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {        

        #region private methods        

        protected override void ShowData(int releaseIssueTypeId)
        {
			base.ShowData(releaseIssueTypeId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ReleaseIssueTypeDataModel();
			data.ReleaseIssueTypeId = releaseIssueTypeId;

			var items = Framework.Components.ReleaseLog.ReleaseIssueTypeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblApplicationId.Text = item.Application.ToString();

				SetData(item);

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);
				           
				oHistoryList.Setup(PrimaryEntity, releaseIssueTypeId, "ReleaseIssueType");
			}  
            
        }

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblReleaseIssueTypeIdText,lblApplicationIdText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		public void SetData(ReleaseIssueTypeDataModel data)
		{
			SystemKeyId = data.ReleaseIssueTypeId;

			base.SetData(data);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ReleaseIssueTypeDataModel();

			SetData(data);
		}
        
        #endregion

        #region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ReleaseIssueTypeLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleaseIssueType;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynReleaseIssueTypeId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

			CoreSystemKey			= lblReleaseIssueTypeId;
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
                lblReleaseIssueTypeIdText.Visible = isTesting;
                lblReleaseIssueTypeId.Visible = isTesting;
            }
            PopulateLabelsText();
        }

        

        #endregion
    }
}