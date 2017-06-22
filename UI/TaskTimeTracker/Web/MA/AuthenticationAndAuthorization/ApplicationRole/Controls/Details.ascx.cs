using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using DataModel.Framework.AuthenticationAndAuthorization;
using Shared.WebCommon.UI.Web;
using Framework.Components.ApplicationUser;

namespace Shared.UI.Web.AuthenticationAndAuthorization.ApplicationRole.Controls
{
    public partial class Details : ControlDetailsStandard
    {
        #region private methods

        protected override void ShowData(int applicationRoleId)
        {
            base.ShowData(applicationRoleId);            

            Clear();

            var data = new ApplicationRoleDataModel();
            data.ApplicationRoleId = applicationRoleId;
           
            var items = ApplicationRoleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblApplicationRoleId.Text = item.ApplicationRoleId.ToString();
                lblApplication.Text = item.Application.ToString();
               
                SetData(item);

                oHistoryList.Setup(PrimaryEntity, applicationRoleId, "ApplicationRole");
            }
        }

        public void SetData(ApplicationRoleDataModel data)
        {
            SystemKeyId = data.ApplicationRoleId;


            base.SetData(data);
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new[] { lblApplicationRoleIdText, lblApplication, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        #endregion

        #region Events

        protected override void Clear()
        {
            base.Clear();

            var data = new ApplicationRoleDataModel();

            SetData(data);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.ApplicationRoleLabelDictionary;
            PrimaryEntity = SystemEntity.ApplicationRole;

            PlaceHolderCore = dynApplicationRoleId;
            PlaceHolderAuditHistory = dynAuditHistory;

            BorderDiv = borderdiv;

            CoreSystemKey = lblApplicationRoleId;
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
                lblApplicationRoleIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }

}