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

namespace ApplicationContainer.UI.Web.TCM.TestSuiteXTestCase.Controls
{
    public partial class Details : Framework.UI.Web.BaseClasses.ControlDetails
    {

        #region private methods

		protected override void ShowData(int testSuiteXTestCaseId)
		{
			base.ShowData(testSuiteXTestCaseId);

			oDetailButtonPanel.SetId = SetId;

			Clear();

			var dataQuery = new TestSuiteXTestCaseDataModel();
			dataQuery.TestSuiteXTestCaseId = testSuiteXTestCaseId;

			var entityList = TestCaseManagement.Components.DataAccess.TestSuiteXTestCaseDataManager.GetEntityList(dataQuery, SessionVariables.RequestProfile);

			if (entityList.Count == 1)
			{
				foreach (var entityItem in entityList)
				{
					lblTestSuiteXTestCaseId.Text = entityItem.TestSuiteXTestCaseId.ToString();
					lblTestCaseId.Text = entityItem.TestCaseId.ToString();
					lblTestSuiteId.Text = entityItem.TestSuiteId.ToString();
					lblTestCaseStatusId.Text = entityItem.TestCaseStatusId.ToString();
					lblTestCasePriorityId.Text = entityItem.TestCasePriorityId.ToString();

					oUpdateInfo.LoadText(entityItem.UpdatedDate, entityItem.UpdatedBy, entityItem.LastAction);

					oHistoryList.Setup((int)Framework.Components.DataAccess.SystemEntity.TestSuiteXTestCase, testSuiteXTestCaseId, "TestSuiteXTestCase");
					
				}
			}
		}

		protected override List<Label> GetLabels()
		{
			if (LabelListCore == null)
			{
				LabelListCore = new List<Label>(new Label[] { lblTestSuiteXTestCaseId, lblTestSuiteText, lblTestCaseText, lblTestCaseStatusText,lblTestCasePriorityText });
			}

			return LabelListCore;
		}		

       #endregion

        #region Events

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var isTesting = SessionVariables.IsTesting;
                lblTestSuiteXTestCaseIdText.Visible = isTesting;
                lblTestSuiteXTestCaseId.Visible = isTesting;
            }
			PopulateLabelsText();
        }

		protected override void OnInit(EventArgs e)
		{
			// set basic variables
			DictionaryLabel = CacheConstants.TestSuiteXTestCaseLabelDictionary;
			PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestSuiteXTestCase;

			base.OnInit(e);

			// set object variable reference            
			PlaceHolderCore = dynTestSuiteXTestCaseId;
			PlaceHolderAuditHistory = dynAuditHistory;
			
			BorderDiv = borderdiv;
		}

        #endregion

    }
}