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

namespace ApplicationContainer.UI.Web.TCM.TestCasePriority.Controls
{
    public partial class Details : ControlDetailsStandard
    {
        #region private methods

        protected override void ShowData(int testCasePriorityId)
        {
            base.ShowData(testCasePriorityId);

            oDetailButtonPanel.SetId = SetId;

            Clear();

            var data = new TestCasePriorityDataModel();
            data.TestCasePriorityId = testCasePriorityId;

            var items = TestCasePriorityDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

            if (items.Count == 1)
            {
                var item = items[0];

                lblTestCasePriorityId.Text = item.TestCasePriorityId.ToString();

                SetData(item);

                oHistoryList.Setup(PrimaryEntity, testCasePriorityId, "TestCasePriority");
            }
        }

        public void SetData(TestCasePriorityDataModel data)
        {
            SystemKeyId = data.TestCasePriorityId;


            base.SetData(data);
        }

        protected override List<Label> GetLabels()
        {
            if (LabelListCore == null)
            {
                LabelListCore = new List<Label>(new[] { lblTestCasePriorityIdText, lblNameText, lblDescriptionText, lblSortOrderText });
            }

            return LabelListCore;
        }

        #endregion

        #region Events

        protected override void Clear()
        {
            base.Clear();

            var data = new TestCasePriorityDataModel();

            SetData(data);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            DictionaryLabel      = CacheConstants.TestCasePriorityLabelDictionary;
            PrimaryEntity        = SystemEntity.TestCasePriority;

            PlaceHolderCore         = dynTestCasePriorityId;
            PlaceHolderAuditHistory = dynAuditHistory;

            BorderDiv = borderdiv;

            CoreSystemKey           = lblTestCasePriorityId;
            CoreControlName         = lblName;
            CoreControlDescription  = lblDescription;
            CoreControlSortOrder    = lblSortOrder;

            CoreUpdateInfo           = oUpdateInfo;
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblTestCasePriorityIdText.Visible = isTesting;
                CoreSystemKey.Visible = isTesting;
            }

            PopulateLabelsText();
        }

        #endregion

    }

}