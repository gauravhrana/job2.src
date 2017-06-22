using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TestCaseManagement;
using Shared.UI.Web.Controls;
using Shared.WebCommon.UI.Web;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.TCM.TestCase
{
    public partial class InlineUpdate : PageInlineUpdate
    {      
        #region Methods

        protected override DataTable GetData()
		{
			try
			{
                SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();
				
                var selectedrows = new DataTable();
				var testCasedata = new TestCaseDataModel();

                selectedrows = TestCaseManagement.Components.DataAccess.TestCaseDataManager.GetDetails(testCasedata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
                {
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        testCasedata.TestCaseId = entityKey;
                        var result = TestCaseManagement.Components.DataAccess.TestCaseDataManager.GetDetails(testCasedata, SessionVariables.RequestProfile);
                        selectedrows.ImportRow(result.Rows[0]);
                    }
                }
                else
                {
                    testCasedata.TestCaseId = SetId;
                    var result = TestCaseManagement.Components.DataAccess.TestCaseDataManager.GetDetails(testCasedata, SessionVariables.RequestProfile);
                    selectedrows.ImportRow(result.Rows[0]);

                }
                return selectedrows;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

            return null;
        }

        protected override void Update(Dictionary<string, string> values)
        {
			var data = new TestCaseDataModel();

			// copies properties from values dictionary object to data object
			PropertyMapper.CopyProperties(data, values);

            TestCaseManagement.Components.DataAccess.TestCaseDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestCase;
            PrimaryEntityKey = "TestCase";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}