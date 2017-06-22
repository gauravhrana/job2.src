using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TaskTimeTracker.ApplicationDevelopment;
using Shared.UI.Web;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.ApplicationDevelopment.DeveloperRole.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int developerRoleId)
        {
            base.ShowData(developerRoleId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new DeveloperRoleDataModel();
            data.DeveloperRoleId = developerRoleId;

			var items = TaskTimeTracker.Components.Module.ApplicationDevelopment.DeveloperRoleDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

               lblApplication.Text = item.Application;

                SetData(item);

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, developerRoleId, "DeveloperRole");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblDeveloperRoleIdText, lblNameText, lblDescriptionText, 
                    lblSortOrderText});
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new DeveloperRoleDataModel();

            SetData(data);
        }

        public void SetData(DeveloperRoleDataModel item)
        {
            SystemKeyId = item.DeveloperRoleId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = Shared.UI.Web.CacheConstants.DeveloperRoleLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.DeveloperRole;

            PlaceHolderCore = dynDeveloperRoleId;
            PlaceHolderAuditHistory = dynAuditHistory;

            BorderDiv = borderdiv;

            CoreSystemKey = lblDeveloperRoleId;
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
                lblDeveloperRoleIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}