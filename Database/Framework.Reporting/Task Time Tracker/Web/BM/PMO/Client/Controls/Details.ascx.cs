using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.PMO.Client.Controls
{
    public partial class Details : ControlDetailsStandard
    {

        protected override void ShowData(int clientId) 
        {
			base.ShowData(clientId);

            oDetailButtonPanel.SetId = SetId;

			Clear();

            var data = new ClientDataModel();
            data.ClientId = clientId;

			var items = ClientDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count == 1)
            {
				var item = items[0];
                SetData(item);
	            oHistoryList.Setup(PrimaryEntity, clientId, "Client");
            }
        }

	    public void SetData(ClientDataModel data)
	    {
			SystemKeyId = data.ClientId;
			base.SetData(data);
	    }

	    protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new[] { lblClientIdText, lblNameText, lblDescriptionText, lblSortOrderText });
			}

			return LabelListCore;
		}

		protected override void Clear()
		{
			base.Clear();
			var data = new ClientDataModel();
			SetData(data);
		}

		protected override void OnInit(EventArgs e)
        {
			base.OnInit(e);

			DictionaryLabel = CacheConstants.ClientLabelDictionary;
			PrimaryEntity = SystemEntity.Client;

            PlaceHolderCore			= dynClientId;
            PlaceHolderAuditHistory = dynAuditHistory;

            BorderDiv				= borderdiv;

			CoreSystemKey			= lblClientId;
			CoreControlName			= lblName;
			CoreControlDescription	= lblDescription;
			CoreControlSortOrder	= lblSortOrder;

			CoreUpdateInfo			= oUpdateInfo;
		}

		protected override void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var isTesting = SessionVariables.IsTesting;
				lblClientIdText.Visible = isTesting;
				CoreSystemKey.Visible = isTesting;
			}

			PopulateLabelsText();
		}

	}

}