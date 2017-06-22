﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.TestCaseManagement;
using Shared.WebCommon.UI.Web;
using System.Data;
using System.Collections;

namespace ApplicationContainer.UI.Web.TCM.TestCaseOwner
{
    public partial class Settings : Framework.UI.Web.BaseClasses.PageSettings
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eSettingsList.SetUp((int)Framework.Components.DataAccess.SystemEntity.TestCaseOwner, "TestCaseOwner");
        }

        private void UpdateData(ArrayList values)
        {
            var data = new TestCaseOwnerDataModel();

            data.TestCaseOwnerId     = int.Parse(values[0].ToString());
            data.Name                = values[1].ToString();
            data.Description         = values[2].ToString();
            data.SortOrder           = int.Parse(values[3].ToString());

            TestCaseManagement.Components.DataAccess.TestCaseOwnerDataManager.Update(data, SessionVariables.RequestProfile);
            ReBindEditableGrid();
        }

        private void ReBindEditableGrid()
        {
            var data = new TestCaseOwnerDataModel();
            var dtTestCaseOwner = TestCaseManagement.Components.DataAccess.TestCaseOwnerDataManager.Search(data, SessionVariables.RequestProfile);

        }
    }
}