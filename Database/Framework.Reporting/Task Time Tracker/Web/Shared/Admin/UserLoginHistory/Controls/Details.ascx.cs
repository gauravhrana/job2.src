using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.UserLoginHistory.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
	{

		#region private methods

		protected override void ShowData(int userLoginHistoryId)
		{
			base.ShowData(userLoginHistoryId);			

			Clear();

			var data = new Framework.Components.LogAndTrace.UserLoginHistoryDataModel();
			data.UserLoginHistoryId = userLoginHistoryId;

			var items = Framework.Components.LogAndTrace.UserLoginHistoryDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match 
			if (items.Count == 1)
			{
				var item = items[0];

				lblUserLoginHistoryId.Text	= item.UserLoginHistoryId.ToString();
				lblUserName.Text			= item.UserName.ToString();
				lblUserId.Text				= item.UserId.ToString();
				lblServerName.Text			= item.ServerName.ToString();
				lblURL.Text					= item.URL.ToString();
				lblDateVisited.Text			= item.DateVisited.ToString();			

				oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

				oHistoryList.Setup(PrimaryEntity, userLoginHistoryId, "UserLoginHistory");
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblUserLoginHistoryIdText, lblUserIdText,lblUserNameText,lbURLText,lblDateVisitedText, lblServerNameText });
			}

			return LabelListCore;
		}

		#endregion

		#region Events

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.UserLoginHistoryLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserLoginHistory;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynUserLoginHistoryId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblUserLoginHistoryIdText.Visible = isTesting;
				lblUserLoginHistoryId.Visible = isTesting;
			}

			PopulateLabelsText();
		}

		#endregion  

		

	}
}