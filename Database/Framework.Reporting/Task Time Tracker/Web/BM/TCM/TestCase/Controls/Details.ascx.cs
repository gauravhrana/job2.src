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

namespace ApplicationContainer.UI.Web.TCM.TestCase.Controls
{
    public partial class Details : ControlDetailsStandard
    {
        #region private methods

        protected override void ShowData(int testCaseId)
        {
            base.ShowData(testCaseId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new TestCaseDataModel();
            data.TestCaseId = testCaseId;

            var items = TestCaseDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblTestCaseId.Text = item.TestCaseId.ToString();
                lblApplication.Text = item.ApplicationId.ToString();

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, testCaseId, "TestCase");
            }
        }

        public void SetData(TestCaseDataModel data)
        {
            SystemKeyId = data.TestCaseId;


            base.SetData(data);
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new[] { lblTestCaseIdText, lblApplication, lblNameText, lblDescriptionText, lblSortOrderText });
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

            DictionaryLabel = CacheConstants.TestCaseLabelDictionary;
            PrimaryEntity = SystemEntity.TestCase;

            PlaceHolderCore = dynTestCaseId;
            PlaceHolderAuditHistory = dynAuditHistory;

            BorderDiv = borderdiv;

            CoreSystemKey = lblTestCaseId;
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
                lblTestCaseIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }

}