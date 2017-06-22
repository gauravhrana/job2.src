using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Data;
using DataModel.Framework.ReleaseLog;

namespace Shared.UI.Web.ApplicationManagement.ReleaseLog.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {
		#region private methods

		protected override void ShowData(int releaseLogId)
		{
			base.ShowData(releaseLogId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ReleaseLogDataModel();
			data.ReleaseLogId = releaseLogId;

			var items = Framework.Components.ReleaseLog.ReleaseLogDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblReleaseLogId.Text		= item.ReleaseLogId.ToString();
				lblApplicationId.Text		= item.Application.ToString();
				lblReleaseDate.Text			= item.ReleaseDate.Value.ToString(SessionVariables.UserDateFormat);
				lblReleaseLogStatus.Text	= item.ReleaseLogStatus.ToString();
				lblVersionNo.Text			= item.VersionNo.ToString();
				lblName.Text				= item.Name;
				lblDescription.Text			= item.Description;
				lblSortOrder.Text			= item.SortOrder.ToString();
				
				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, releaseLogId, "ReleaseLog");
			}

		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{				
				LabelListCore = new List<Label>()
				{ 
					  lblReleaseLogIdText
					, lblApplicationIdText
					, lblVersionNoText
					, lblReleaseLogStatusText
					, lblReleaseDateText
					, lblNameText
					, lblDescriptionText
					, lblSortOrderText 
				};
			}

			return LabelListCore;
		}

		public void LoadTabData(int releaseLogId)
		{
			ShowData(releaseLogId);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ReleaseLogLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleaseLog;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynReleaseLogId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblReleaseLogIdText.Visible = isTesting;
				lblReleaseLogId.Visible = isTesting;
			}
			PopulateLabelsText();
		}



		#endregion     
        

    }
}