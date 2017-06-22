using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using DataModel.TaskTimeTracker.RequirementAnalysis;
using ApplicationContainer.UI.Web.CommonCode;

namespace ApplicationContainer.UI.Web.UseCasePackage.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int UseCasePackageId)
        {
            base.ShowData(UseCasePackageId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new UseCasePackageDataModel();
            data.UseCasePackageId = UseCasePackageId;

            var items = TaskTimeTracker.Components.BusinessLayer.RequirementAnalysis.UseCasePackageDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblCreatedByAuditId.Text = item.CreatedByAuditId.ToString();
                lblModifiedByAuditId.Text = item.ModifiedByAuditId.ToString();
                lblCreatedDate.Text = item.CreatedDate.Value.ToString(SessionVariables.UserDateFormat);
                lblModifiedDate.Text = item.ModifiedDate.Value.ToString(SessionVariables.UserDateFormat);

                SetData(item);

                oUpdateInfo.LoadText(item.UpdatedDate, item.UpdatedBy, item.LastAction);

                oHistoryList.Setup(PrimaryEntity, UseCasePackageId, "UseCasePackage");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblUseCasePackageIdText, lblNameText, lblDescriptionText, lblSortOrderText,
                lblCreatedByAuditIdText, lblCreatedDateText, lblModifiedDateText,  lblModifiedByAuditIdText });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new UseCasePackageDataModel();

            SetData(data);
        }

        public void SetData(UseCasePackageDataModel item)
        {
            SystemKeyId = item.UseCasePackageId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.UseCasePackageLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.UseCasePackage;

            PlaceHolderCore = dynUseCasePackageId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblUseCasePackageId;
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
                lblUseCasePackageIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}