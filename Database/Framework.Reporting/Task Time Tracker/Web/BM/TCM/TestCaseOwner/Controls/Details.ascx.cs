using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TestCaseManagement;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using Framework.UI.Web.BaseClasses;
using TestCaseManagement.Components.DataAccess;
using Framework.Components.DataAccess;

namespace ApplicationContainer.UI.Web.TCM.TestCaseOwner.Controls
{
    public partial class Details : ControlDetailsStandard
    {
        #region private methods

        protected override void ShowData(int testCaseOwnerId)
        {
            base.ShowData(testCaseOwnerId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new TestCaseOwnerDataModel();
            data.TestCaseOwnerId = testCaseOwnerId;

            var items = TestCaseOwnerDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblTestCaseOwnerId.Text = item.TestCaseOwnerId.ToString();
               
                SetData(item);

                oHistoryList.Setup(PrimaryEntity, testCaseOwnerId, "TestCaseOwner");
            }
        }

        public void SetData(TestCaseOwnerDataModel data)
        {
            SystemKeyId = data.TestCaseOwnerId;


            base.SetData(data);
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new[] { lblTestCaseOwnerIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        #endregion

        #region Events

        protected override void Clear()
        {
            base.Clear();

            var data = new TestCaseDataModel();

            SetData(data);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.TestCaseOwnerLabelDictionary;
            PrimaryEntity = SystemEntity.TestCaseOwner;

            PlaceHolderCore = dynTestCaseOwnerId;
            PlaceHolderAuditHistory = dynAuditHistory;

            BorderDiv = borderdiv;

            CoreSystemKey = lblTestCaseOwnerId;
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
                lblTestCaseOwnerIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }

}