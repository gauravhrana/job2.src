using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.LogAndTrace;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace Shared.UI.Web.Admin.UserLoginStatus.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
	{
		#region private methods

		protected override void ShowData(int userLoginStatusId)
        {
			base.ShowData(userLoginStatusId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new UserLoginStatusDataModel();

			data.UserLoginStatusId = userLoginStatusId;

			var items = Framework.Components.LogAndTrace.UserLoginStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

				SetData(item);

				oHistoryList.Setup(PrimaryEntity, userLoginStatusId, "UserLoginStatus");
			}
		}				

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblUserLoginStatusIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

		protected override void Clear()
		{
			base.Clear();

			var data = new UserLoginStatusDataModel();

			SetData(data);
		}

		public void SetData(UserLoginStatusDataModel item)
		{
			SystemKeyId = item.UserLoginStatusId;

			base.SetData(item);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblUserLoginStatusIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
			}
			PopulateLabelsText();
		}

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.UserLoginStatusLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserLoginStatus;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynUserLoginStatusId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

			CoreSystemKey = lblUserLoginStatusId;
			CoreControlName = lblName;
			CoreControlDescription = lblDescription;
			CoreControlSortOrder = lblSortOrder;

			CoreUpdateInfo = oUpdateInfo;
        }

		#endregion
	}
}