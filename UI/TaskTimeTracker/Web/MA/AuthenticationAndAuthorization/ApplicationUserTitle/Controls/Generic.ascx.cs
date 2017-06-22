using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;
using Shared.UI.WebFramework;
using Framework.Components.ApplicationUser;
using System.Data;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationUserTitle.Controls
{
    public partial class Generic : ControlGenericStandard
    {
        #region methods

        public override int? Save(string action)
        {
            var data = new ApplicationUserTitleDataModel();

            data.ApplicationUserTitleId = SystemKeyId;
            data.Name = Name;
            data.Description = Description;
            data.SortOrder = SortOrder;

            if (action == "Insert")
            {
                if(!ApplicationUserTitleDataManager.DoesExist(data, SessionVariables.RequestProfile))
                {
                    ApplicationUserTitleDataManager.Create(data, SessionVariables.RequestProfile);
                }
                else
                {
                    throw new Exception("Record with given ID already exists.");
                }
            }
            else
            {
                ApplicationUserTitleDataManager.Update(data, SessionVariables.RequestProfile);
            }

            return data.ApplicationUserTitleId;
        }

        public override void SetId(int setId, bool chkApplicationUserTitleId)
        {
            ViewState["SetId"] = setId;
          
            LoadData((int)ViewState["SetId"], chkApplicationUserTitleId);

            CoreSystemKey.Enabled = chkApplicationUserTitleId;
        }

        public void LoadData(int ApplicationUserTitleId, bool showId)
        {            			
            Clear();

            var data = new ApplicationUserTitleDataModel();
            data.ApplicationUserTitleId = ApplicationUserTitleId;

            var items = ApplicationUserTitleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count != 1) return;

            var item = items[0];

            SetData(item);

            if (!showId)
            {
                SystemKeyId = item.ApplicationUserTitleId;
                oHistoryList.Setup(PrimaryEntity, ApplicationUserTitleId, PrimaryEntityKey);
            }
            else
            {
                CoreSystemKey.Text = String.Empty;
            }
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new ApplicationUserTitleDataModel();

            SetData(data);
        }

        public void SetData(ApplicationUserTitleDataModel data)
        {
            SystemKeyId = data.ApplicationUserTitleId;

            base.SetData(data);
        }

        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            var isTesting = SessionVariables.IsTesting;
            CoreSystemKey.Visible = isTesting;
            lblApplicationUserTitleId.Visible = isTesting;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PrimaryEntity = SystemEntity.ApplicationUserTitle;
            PrimaryEntityKey = "ApplicationUserTitle";
            FolderLocationFromRoot = "ApplicationUserTitle";

                     
            PlaceHolderCore = dynApplicationUserTitleId;
            PlaceHolderAuditHistory = dynAuditHistory;
            BorderDiv = borderdiv;

            PlaceHolderAuditHistory.Visible = PreferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, PrimaryEntityKey);

            CoreSystemKey = txtApplicationUserTitleId;
            CoreControlName = txtName;
            CoreControlDescription = txtDescription;
            CoreControlSortOrder = txtSortOrder;

            CoreUpdateInfo = oUpdateInfo;
        }

        #endregion

    }
}