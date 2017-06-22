using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.AuthenticationAndAuthorization;
using DataModel.TestCaseManagement;
using Shared.WebCommon.UI.Web;
using DataModel.Framework.DataAccess;
using Framework.UI.Web.BaseClasses;

namespace ApplicationContainer.UI.Web.TCM.TestCase.Controls
{
    public partial class SearchFilter : ControlSearchFilterEntity
    {

        public TestCaseDataModel SearchParameters
        {
            get
            {
                var data = new TestCaseDataModel();

                SearchFilterControl.SetSearchParameters(data);

                CommonSearchParameters();

                return data;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            BaseSearchFilterControl                      = SearchFilterControl;
            SearchFilterControl.LoadComboBoxSourceMethod = LoadDropDownListSources;
        }


    }
}