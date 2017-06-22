using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TestCaseManagement;
using Shared.WebCommon.UI.Web;
using ApplicationContainer.UI.Web.CommonCode;
using TestCaseManagement.Components.DataAccess;

namespace ApplicationContainer.UI.Web.TCM.TestSuiteXTestCaseArchive.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region variables


        #endregion

        #region private methods

        

        protected override void ShowData(int testSuiteXTestCaseArchiveid)
        {
            //oDetailButtonPanel.SetId = SetId;
            var data = new TestSuiteXTestCaseArchiveDataModel();
            data.TestSuiteXTestCaseArchiveId = testSuiteXTestCaseArchiveid;
			var dt = TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseArchiveDataManager.GetDetails(data, SessionVariables.RequestProfile);

            if (dt.Rows.Count == 1)
            {
                var row = dt.Rows[0];

                lblTestSuiteXTestCaseArchiveId.Text = Convert.ToString(row[TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteXTestCaseArchiveId]);
                lblRecordDate.Text = Convert.ToString(row[TestSuiteXTestCaseArchiveDataModel.DataColumns.RecordDate]);
                lblTestSuite.Text = Convert.ToString(row[TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuite]);
                lblTestCase.Text = Convert.ToString(row[TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCase]);
                lblTestCaseStatus.Text = Convert.ToString(row[TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCaseStatus]);
                lblTestCasePriority.Text = Convert.ToString(row[TestSuiteXTestCaseArchiveDataModel.DataColumns.TestCasePriority]);
                lblTestSuiteXTestCaseId.Text = Convert.ToString(row[TestSuiteXTestCaseArchiveDataModel.DataColumns.TestSuiteXTestCaseId]);             
                
                lblKnowledgeDate.Text = Convert.ToString(row[TestSuiteXTestCaseArchiveDataModel.DataColumns.KnowledgeDate]);                
                

                oUpdateInfo.LoadText(dt.Rows[0]);

                oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.TestSuiteXTestCaseArchive, testSuiteXTestCaseArchiveid, "TestSuiteXTestCaseArchive");
                dynAuditHistory.Visible = PerferenceUtility.GetUserPreferenceByKeyAsBoolean(ApplicationCommon.HistoryGridVisibilityKey, "TestSuiteXTestCaseArchive");
            }
            else
            {
                Clear();
            }
        }

        protected override void Clear()
        {
            lblTestSuiteXTestCaseArchiveId.Text = String.Empty;
            lblRecordDate.Text = String.Empty;
            lblTestSuite.Text = String.Empty;
            lblTestCase.Text = String.Empty;
            lblTestCaseStatus.Text = String.Empty;
            lblTestCasePriority.Text = String.Empty;
            lblTestSuiteXTestCaseId.Text = String.Empty;         
           
            lblKnowledgeDate.Text = String.Empty;
            
        }

        protected override void PopulateLabelsText()
        {
            var validColumns = new Dictionary<string, string>();
            var labelslist = new List<Label>(new Label[] { lblTestSuiteXTestCaseArchiveIdText,  lblRecordDateText,
                                                        lblTestSuiteText, lblTestCaseText, lblTestCaseStatusText, lblTestCasePriorityText, lblTestSuiteXTestCaseIdText,													    
                                                        lblKnowledgeDateText
            });
            if (Cache[CacheConstants.TestSuiteXTestCaseArchiveLabelDictionary] == null)
            {
                validColumns = UIHelper.GetLabelDictonaryObject((int)Framework.Components.DataAccess.SystemEntity.TestSuiteXTestCaseArchive, SessionVariables.RequestProfile.AuditId);
                Cache.Add(CacheConstants.TestSuiteXTestCaseArchiveLabelDictionary, validColumns, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 60), System.Web.Caching.CacheItemPriority.Default, null);

            }
            else
            {
                validColumns = (Dictionary<string, string>)Cache[CacheConstants.TestSuiteXTestCaseArchiveLabelDictionary];
            }
            UIHelper.PopulateLabelsText(validColumns, (int)Framework.Components.DataAccess.SystemEntity.TestSuiteXTestCaseArchive, SessionVariables.RequestProfile.AuditId, labelslist);

        }


        #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblTestSuiteXTestCaseArchiveIdText.Visible = isTesting;
                lblTestSuiteXTestCaseArchiveId.Visible = isTesting;
            }
            PopulateLabelsText();
        }

        

        #endregion

    }
}