using System;
using DataModel.TaskTimeTracker;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace ApplicationContainer.UI.Web.PMO.Client.Controls
{
	public partial class Generic : ControlGenericStandard
    {
        
        #region methods

        public override int? Save(string action)
        {
            var data = new ClientDataModel();

            data.ClientId		= SystemKeyId;
            data.Name			= Name;
            data.Description	= Description;
            data.SortOrder		= SortOrder;

            if (action == "Insert")
            {
                var dtClient = ClientDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtClient.Rows.Count == 0)
                {
                    ClientDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                ClientDataManager.Update(data, SessionVariables.RequestProfile);
            }

			// not correct ... when doing insert, we didn't get/change the value of ClientID ?
			return data.ClientId;
        }

        public override void SetId(int setId, bool chkClientId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkClientId);
			CoreSystemKey.Enabled = chkClientId;
        }

        public void LoadData(int clientId, bool showId)
        {
			// clear UI
			Clear();

			// set up parameters
			var data = new ClientDataModel();
            data.ClientId = clientId;

			// get data
			var items = ClientDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			// should only have single match -- should log exception.
	        if (items.Count != 1) return;

	        var item = items[0];

			SetData(item);

	        if (!showId)
	        {
		        SystemKeyId = item.ClientId;

		        // only show Audit History in case of Update page, not for Clone.
		        oHistoryList.Setup(PrimaryEntity, clientId, PrimaryEntityKey);
	        }
	        else
	        {
				CoreSystemKey.Text = String.Empty;
	        }
        }

		protected override void Clear()
		{
			base.Clear();
			var data = new ClientDataModel();
			SetData(data);
		}

		public void SetData(ClientDataModel data)
		{
			SystemKeyId		= data.ClientId;
			base.SetData(data);
		}

		#endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
            lblClientId.Visible = isTesting;
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntity			= SystemEntity.Client;
			PrimaryEntityKey		= "Client";
			FolderLocationFromRoot  = "Client";

			// set object variable reference
			PlaceHolderCore			= dynClientId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv				= borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey			= txtClientId;
			CoreControlName			= txtName;
			CoreControlDescription	= txtDescription;
			CoreControlSortOrder	= txtSortOrder;

			CoreUpdateInfo			= oUpdateInfo;
		}

        #endregion

    }
}