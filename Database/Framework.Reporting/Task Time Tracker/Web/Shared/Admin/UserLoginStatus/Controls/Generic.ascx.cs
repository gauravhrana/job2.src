using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.LogAndTrace;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace Shared.UI.Web.Admin.UserLoginStatus.Controls
{
	public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
	{        

		#region private methods

		public override int? Save(string action)
		{
			var data = new UserLoginStatusDataModel();

			data.UserLoginStatusId = SystemKeyId;
			data.Name = Name;
			data.Description = Description;
			data.SortOrder = SortOrder;

			if (action == "Insert")
			{
				var dtUserLoginStatus = Framework.Components.LogAndTrace.UserLoginStatusDataManager.DoesExist(data, SessionVariables.RequestProfile);

				if (dtUserLoginStatus.Rows.Count == 0)
				{
					Framework.Components.LogAndTrace.UserLoginStatusDataManager.Create(data, SessionVariables.RequestProfile);
				}
				else
				{
					throw new Exception("Record with given ID already exists.");
				}
			}
			else
			{
				Framework.Components.LogAndTrace.UserLoginStatusDataManager.Update(data, SessionVariables.RequestProfile);
			}

			return data.UserLoginStatusId;
		}

		public override void SetId(int setId, bool chkUserLoginStatusId)
		{
			ViewState["SetId"] = setId;

			// load data
			LoadData((int)ViewState["SetId"], chkUserLoginStatusId);
			CoreSystemKey.Enabled = chkUserLoginStatusId;
			//txtDescription.Enabled = !chkUserLoginStatusId;
			//txtName.Enabled = !chkUserLoginStatusId;
			//txtSortOrder.Enabled = !chkUserLoginStatusId;
		}

		public void LoadData(int userLoginStatusId, bool showId)
        {
            // clear UI				
            Clear();

            // set up parameters
            var data = new UserLoginStatusDataModel();
            data.UserLoginStatusId = userLoginStatusId;

            // get data
			var items = Framework.Components.LogAndTrace.UserLoginStatusDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            // should only have single match -- should log exception.
            if (items.Count != 1) return;

            var item = items[0];

			SetData(item);

            if (!showId)
            {
				SystemKeyId = item.UserLoginStatusId;                
                oHistoryList.Setup(PrimaryEntity, userLoginStatusId, PrimaryEntityKey);
            }
            else
            {
				CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
			base.Clear();

            var data = new UserLoginStatusDataModel();

			SetData(data);
        }

		public void SetData(UserLoginStatusDataModel data)
		{
			SystemKeyId = data.UserLoginStatusId;

			base.SetData(data);
		}

		#endregion

		#region Events

		protected override void Page_Load(object sender, EventArgs e)
		{
			var isTesting = SessionVariables.IsTesting;
			CoreSystemKey.Visible = isTesting;
			lblUserLoginStatusId.Visible = isTesting;
		}

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntityKey = "UserLoginStatus";
			FolderLocationFromRoot = "/Shared/Admin/UserLoginStatus";
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UserLoginStatus;

            // set object variable reference            
            PlaceHolderCore = dynUserLoginStatusId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

			CoreSystemKey = txtUserLoginStatusId;
			CoreControlName = txtName;
			CoreControlDescription = txtDescription;
			CoreControlSortOrder = txtSortOrder;

			CoreUpdateInfo = oUpdateInfo;	
        }

		#endregion
	}
}