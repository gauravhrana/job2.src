using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace Shared.UI.Web.Configuration.Application.Controls
{
	public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int applicationId)
        {
            base.ShowData(applicationId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

			var data = new ApplicationDataModel();
            data.ApplicationId = applicationId;

			var items = Framework.Components.ApplicationUser.ApplicationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

				SetData(item);

				lblCode.Text = item.Code;

                oHistoryList.Setup(PrimaryEntity, applicationId, "Application");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblApplicationIdText, lblNameText, lblCodeText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

		protected override void Clear()
		{
			base.Clear();

			var data = new ApplicationDataModel();

			SetData(data);
		}

		public void SetData(ApplicationDataModel item)
		{
			SystemKeyId = item.ApplicationId;

			base.SetData(item);
		}
        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
			base.OnInit(e);

			DictionaryLabel = CacheConstants.ApplicationLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.Application;

            

            PlaceHolderCore = dynApplicationId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

			CoreSystemKey = lblApplicationId;
			CoreControlName = lblName;
			CoreControlDescription = lblDescription;
			CoreControlSortOrder = lblSortOrder;

			CoreUpdateInfo = oUpdateInfo;
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblApplicationIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }

}