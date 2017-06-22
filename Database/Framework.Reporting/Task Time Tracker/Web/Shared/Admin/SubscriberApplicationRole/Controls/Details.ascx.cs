using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace Shared.UI.Web.Admin.SubscriberApplicationRole.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
	{
		#region private methods

		protected override void ShowData(int SubscriberApplicationRoleId)
        {
			base.ShowData(SubscriberApplicationRoleId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var data = new SubscriberApplicationRoleDataModel();

			data.SubscriberApplicationRoleId = SubscriberApplicationRoleId;

			var items = Framework.Components.Core.SubscriberApplicationRoleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
			{
				var item = items[0];

				SetData(item);

                oHistoryList.Setup(PrimaryEntity, SubscriberApplicationRoleId, "SubscriberApplicationRole");
			}
			else
			{
				Clear();
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblSubscriberApplicationRoleIdText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();

			var data = new SubscriberApplicationRoleDataModel();

			SetData(data);
		}

		public void SetData(SubscriberApplicationRoleDataModel item)
		{
			SystemKeyId = item.SubscriberApplicationRoleId;

			base.SetData(item);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblSubscriberApplicationRoleIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
			}
			PopulateLabelsText();
		}

        protected override void OnInit(EventArgs e)
        {
            // set basic variables
            DictionaryLabel = CacheConstants.SubscriberApplicationRoleLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.SubscriberApplicationRole;

            base.OnInit(e);

            // set object variable reference            
            PlaceHolderCore = dynSubscriberApplicationRoleId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

			CoreSystemKey = lblSubscriberApplicationRoleId;
			CoreControlName = lblName;
			CoreControlDescription = lblDescription;
			CoreControlSortOrder = lblSortOrder;

			CoreUpdateInfo = oUpdateInfo;
        }


		#endregion

	}
}