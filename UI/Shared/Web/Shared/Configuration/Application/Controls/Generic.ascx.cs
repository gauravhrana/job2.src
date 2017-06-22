using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using TaskTimeTracker.Components.BusinessLayer;

namespace Shared.UI.Web.Configuration.Application.Controls
{
    public partial class Generic : Framework.UI.Web.BaseClasses.ControlGenericStandard
	{
		#region properties

		public string Code
		{
			get
			{
				return txtCode.Text;
			}
			set
			{
				txtCode.Text = (value == null) ? String.Empty : value.ToString();
			}
		}

		#endregion

		#region methods

		public override int? Save(string action)
        {
            var data = new ApplicationDataModel();

            data.ApplicationId	= SystemKeyId;
            data.Name			= Name;
            data.Description	= Description;
            data.SortOrder		= SortOrder;
			data.Code			= Code;

            if (action == "Insert")
            {
				var dtApplication = Framework.Components.ApplicationUser.ApplicationDataManager.DoesExist(data, SessionVariables.RequestProfile);

                if (dtApplication.Rows.Count == 0)
                {
					Framework.Components.ApplicationUser.ApplicationDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
				Framework.Components.ApplicationUser.ApplicationDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.ApplicationId;
        }

        public override void SetId(int setId, bool chkApplicationId)
        {
            ViewState["SetId"] = setId;

            // load data
            LoadData((int)ViewState["SetId"], chkApplicationId);
            CoreSystemKey.Enabled = chkApplicationId;
            //txtDescription.Enabled = !chkApplicationId;
            //txtName.Enabled = !chkApplicationId;
            //txtSortOrder.Enabled = !chkApplicationId;
        }

        public void LoadData(int applicationId, bool showId)
        {
            Clear();

            var data = new ApplicationDataModel();
            data.ApplicationId = applicationId;
			data.Code = Code;

            var items = Framework.Components.ApplicationUser.ApplicationDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];
         

            SetData(item);

			Code = item.Code;

            if (!showId)
            {
                SystemKeyId = item.ApplicationId;
                oHistoryList.Setup(PrimaryEntity, applicationId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new ApplicationDataModel();
			Code = data.Code;

            SetData(data);
        }

        public void SetData(ApplicationDataModel data)
        {
            SystemKeyId = data.ApplicationId;			

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblApplicationId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity            = Framework.Components.DataAccess.SystemEntity.Application;
            PrimaryEntityKey         = "Application";
            FolderLocationFromRoot   = "Application";

            PlaceHolderCore          = dynApplicationId;
            PlaceHolderAuditHistory  = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtApplicationId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}