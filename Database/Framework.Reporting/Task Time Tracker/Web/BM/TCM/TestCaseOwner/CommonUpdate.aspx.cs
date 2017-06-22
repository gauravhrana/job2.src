﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using DataModel.TestCaseManagement;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.TCM.TestCaseOwner
{
    public partial class CommonUpdate : PageCommonUpdate
    {
        #region Methods

        protected override DataTable UpdateData()
        {
            var UpdatedData = new DataTable();

            var data = new TestCaseOwnerDataModel();
            UpdatedData = TestCaseManagement.Components.DataAccess.TestCaseOwnerDataManager.Search(data, SessionVariables.RequestProfile).Clone();
            for (var i = 0; i < SelectedData.Rows.Count; i++)
            {
				data.TestCaseOwnerId =
					Convert.ToInt32(SelectedData.Rows[i][TestCaseOwnerDataModel.DataColumns.TestCaseOwnerId].ToString());
				data.Name = SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Name].ToString();
				data.Description =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description))
					? CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.Description)
					: SelectedData.Rows[i][StandardDataModel.StandardDataColumns.Description].ToString();

				data.SortOrder =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder))
					? int.Parse(CheckAndGetRepeaterTextBoxValue(StandardDataModel.StandardDataColumns.SortOrder).ToString())
					: int.Parse(SelectedData.Rows[i][StandardDataModel.StandardDataColumns.SortOrder].ToString());

                TestCaseManagement.Components.DataAccess.TestCaseOwnerDataManager.Update(data, SessionVariables.RequestProfile);
				data = new TestCaseOwnerDataModel();
				data.TestCaseOwnerId = Convert.ToInt32(SelectedData.Rows[i][TestCaseOwnerDataModel.DataColumns.TestCaseOwnerId].ToString());
                var dt = TestCaseManagement.Components.DataAccess.TestCaseOwnerDataManager.Search(data, SessionVariables.RequestProfile);

                if (dt.Rows.Count == 1)
                {
                    UpdatedData.ImportRow(dt.Rows[0]);
                }
            }
            return UpdatedData;
        }

        protected override DataTable GetEntityData(int? entityKey)
        {
            var testCaseOwnerdata = new TestCaseOwnerDataModel();
            testCaseOwnerdata.TestCaseOwnerId = entityKey;
            var results = TestCaseManagement.Components.DataAccess.TestCaseOwnerDataManager.Search(testCaseOwnerdata, SessionVariables.RequestProfile);
            return results;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {

            base.OnInit(e);

            DynamicUpdatePanelCore = DynamicUpdatePanel;
            PrimaryEntity = Framework.Components.DataAccess.SystemEntity.TestCaseOwner;
            PrimaryEntityKey = "TestCaseOwner";
            BreadCrumbObject = Master.BreadCrumbObject;
        }

        #endregion

    }
}