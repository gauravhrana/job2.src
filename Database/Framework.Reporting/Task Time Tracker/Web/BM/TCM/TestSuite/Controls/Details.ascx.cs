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

namespace ApplicationContainer.UI.Web.TCM.TestSuite.Controls
{
    public partial class Details : ControlDetailsStandard
    {

        #region private methods

        protected override void ShowData(int testSuiteId)
        {
            base.ShowData(testSuiteId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new TestSuiteDataModel();
            data.TestSuiteId = testSuiteId;

            var items = TestCaseManagement.Components.DataAccess.TestSuiteDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, testSuiteId, "TestSuite");
            }
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new Label[] { lblTestSuiteIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        protected override void Clear()
        {
            base.Clear();

            var data = new TestSuiteDataModel();

            SetData(data);
        }

        public void SetData(TestSuiteDataModel item)
        {
            SystemKeyId = item.TestSuiteId;

            base.SetData(item);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel = CacheConstants.TestSuiteLabelDictionary;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestSuite;

            PlaceHolderCore = dynTestSuiteId;
            PlaceHolderAuditHistory = dynAuditHistory;
            
            BorderDiv = borderdiv;

            CoreSystemKey = lblTestSuiteId;
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
                lblTestSuiteIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }
}