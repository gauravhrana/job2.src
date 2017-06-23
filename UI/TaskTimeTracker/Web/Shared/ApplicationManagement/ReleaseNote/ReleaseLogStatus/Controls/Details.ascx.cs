using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.ReleaseLog;
using Shared.WebCommon.UI.Web;
using System.Data;

namespace Shared.UI.Web.ApplicationManagement.ReleaseLogStatus.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
	{

		#region private methods

		protected override void ShowData(int releaseLogStatusId)
		{
			base.ShowData(releaseLogStatusId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new ReleaseLogStatusDataModel();
			data.ReleaseLogStatusId = releaseLogStatusId;

			var items = Framework.Components.ReleaseLog.ReleaseLogStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);		
			

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];
				
				lblApplicationId.Text		= item.Application.ToString();

				SetData(item);

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, releaseLogStatusId, "ReleaseLogStatus");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblReleaseLogStatusIdText,lblApplicationIdText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		public void SetData(ReleaseLogStatusDataModel data)
		{
			SystemKeyId = data.ReleaseLogStatusId;

			base.SetData(data);
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new ReleaseLogStatusDataModel();

			SetData(data);
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.ReleaseLogStatusLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ReleaseLogStatus;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynReleaseLogStatusId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;

			CoreSystemKey			= lblReleaseLogStatusId;
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
				lblReleaseLogStatusIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;				
			}

			PopulateLabelsText();
		}

		#endregion		
	}
}