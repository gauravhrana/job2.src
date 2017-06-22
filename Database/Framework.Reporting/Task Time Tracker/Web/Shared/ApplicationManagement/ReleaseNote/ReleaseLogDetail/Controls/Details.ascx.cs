using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.ApplicationManagement.ReleaseLogDetail.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

		#region private methods

		protected override void ShowData(int releaseLogDetailId)
		{
			base.ShowData(releaseLogDetailId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ReleaseLogDetailDataModel();
			data.ReleaseLogDetailId = releaseLogDetailId;

			var items = Framework.Components.ReleaseLog.ReleaseLogDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblReleaseLogDetailId.Text		= item.ReleaseLogDetailId.ToString();
				lblReleaseLogId.Text			= item.ReleaseLog;
				lblApplicationId.Text			= item.Application;
				lblItemNo.Text					= item.ItemNo.ToString();
				lblDescription.Text				= item.Description;
				lblRequestedBy.Text				= item.RequestedBy.ToString();
				lblPrimaryDeveloper.Text		= item.PrimaryDeveloper;
				//lblRequestedDate.Text			= item.RequestedDate.Value.ToString(SessionVariables.UserDateFormat);
				lblSortOrder.Text				= item.SortOrder.ToString();
				lblReleaseIssueType.Text		= item.ReleaseIssueType;
				lblReleasePublishCategory.Text	= item.ReleasePublishCategory;
				lblFeature.Text					= item.Feature;
				lblModule.Text					= item.Module;
				lblReleaseFeature.Text          = item.ReleaseFeature;
				lblJIRA.Text					= item.JIRA;
				lblPrimaryEntity.Text			= item.PrimaryEntity;
				lblSystemEntityType.Text        = item.SystemEntityType;
				lblTimeSpent.Text				= item.TimeSpent.ToString();

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, releaseLogDetailId, "ReleaseLogDetail");
			}

		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblReleaseLogDetailIdText, lblReleaseLogIdText, lblApplicationIdText, lblTimeSpentText, lblModuleText, lblReleaseFeatureText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel         = CacheConstants.ReleaseLogDetailLabelDictionary;
			PrimaryEntity           = Framework.Components.DataAccess.SystemEntity.ReleaseLogDetail;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore         = dynReleaseLogDetailId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv               = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting                     = SessionVariables.IsTesting;
				lblReleaseLogDetailIdText.Visible = isTesting;
				lblReleaseLogDetailId.Visible     = isTesting;
			}
			PopulateLabelsText();
		}
		#endregion      
       
    }
}