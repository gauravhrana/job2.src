using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Configuration;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace Shared.UI.Web.Configuration.ApplicationMode.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
    {       

        #region private methods

		public override int? Save(string action)
		{
			var data = new ApplicationModeDataModel();

			data.ApplicationModeId = SystemKeyId;
			data.Name = Name;
			data.Description = Description;
			data.SortOrder = SortOrder;

			if (action == "Insert")
			{
                var dtApplicationMode = Framework.Components.UserPreference.ApplicationModeDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtApplicationMode.Rows.Count == 0)
				{
                    Framework.Components.UserPreference.ApplicationModeDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
                Framework.Components.UserPreference.ApplicationModeDataManager.Update(data, SessionVariables.RequestProfile);
			}

			// not correct ... when doing insert, we didn't get/change the value of ClientID ?
			return data.ApplicationModeId;
		}

        public override void SetId(int setId, bool chkApplicationModeId)
        {
            ViewState["SetId"] = setId;

            //load data
            LoadData((int)ViewState["SetId"], chkApplicationModeId);
			CoreSystemKey.Enabled = chkApplicationModeId;
            //txtDescription.Enabled = !chkApplicationModeId;
            //txtName.Enabled = chkApplicationModeId;
            //txtSortOrder.Enabled = !chkApplicationModeId;
        }

        public void LoadData(int applicationModeId, bool showId)
        {
			Clear();

			var data = new ApplicationModeDataModel();
			data.ApplicationModeId = applicationModeId;

            var items = Framework.Components.UserPreference.ApplicationModeDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

			if (items.Count != 1) return;

			var item = items[0];

			SetData(item);

			if (!showId)
			{
				SystemKeyId = item.ApplicationModeId;
				oHistoryList.Setup(PrimaryEntity, applicationModeId, PrimaryEntityKey);
			}
			else
			{
				CoreSystemKey.Text = String.Empty;
			}
        }

		protected override void Clear()
		{
			base.Clear();

			var data = new ApplicationModeDataModel();

			SetData(data);
		}

		public void SetData(ApplicationModeDataModel data)
		{
			SystemKeyId = data.ApplicationModeId;

			base.SetData(data);
		}

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
            lblApplicationModeId.Visible = isTesting;
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			PrimaryEntityKey = "ApplicationMode";
			FolderLocationFromRoot = "ApplicationMode";
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.ApplicationMode;
         
			PlaceHolderCore = dynApplicationModeId;
			PlaceHolderAuditHistory = dynAuditHistory;
			BorderDiv = borderdiv;

			PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtApplicationModeId;
			CoreControlName = txtName;
			CoreControlDescription = txtDescription;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;	
		}        

        #endregion
    }
}