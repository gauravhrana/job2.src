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

namespace ApplicationContainer.UI.Web.TCM.TestCaseOwner
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
				var testCaseOwnerdata = new TestCaseOwnerDataModel();

                selectedrows = TestCaseManagement.Components.DataAccess.TestCaseOwnerDataManager.GetDetails(testCaseOwnerdata, SessionVariables.RequestProfile).Clone();
                if (!string.IsNullOrEmpty(SuperKey))
                {
                    var systemEntityTypeId = (int)PrimaryEntity;
                    var lstEntityKeys = ApplicationCommon.GetSuperKeyDetails(systemEntityTypeId, SuperKey);

                    foreach (var entityKey in lstEntityKeys)
                    {
                        testCaseOwnerdata.TestCaseOwnerId = entityKey;
                        var result = TestCaseManagement.Components.DataAccess.TestCaseOwnerDataManager.GetDetails(testCaseOwnerdata, SessionVariables.RequestProfile);
                        selectedrows.ImportRow(result.Rows[0]);
                    }
                }
                else
                {
                    testCaseOwnerdata.TestCaseOwnerId = SetId;
                    var result = TestCaseManagement.Components.DataAccess.TestCaseOwnerDataManager.GetDetails(testCaseOwnerdata, SessionVariables.RequestProfile);
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
			var data = new TestCaseOwnerDataModel();

			PropertyMapper.CopyProperties(data, values);

            TestCaseManagement.Components.DataAccess.TestCaseOwnerDataManager.Update(data, SessionVariables.RequestProfile);
			base.Update(values);
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestCaseOwner;
            PrimaryEntityKey = "TestCaseOwner";

            InlineEditingListCore = InlineEditingList;
            BreadCrumbObject = Master.BreadCrumbObject;

            base.OnInit(e);
        }

        #endregion

    }
}