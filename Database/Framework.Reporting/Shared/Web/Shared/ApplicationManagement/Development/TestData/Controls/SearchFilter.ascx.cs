using System;
using System.Web.UI.WebControls;
using DataModel.Framework.Application;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationManagement.Development.TestData.Controls
{
    public partial class SearchFilter : Framework.UI.Web.BaseClasses.ControlSearchFilter
    {

        #region variables
        public int? ApplicationId
        {
            get
            {
                int? applicationId = null;

                if (drpSearchConditionAplication.SelectedValue != "-1")
                    applicationId = Convert.ToInt32(drpSearchConditionAplication.SelectedValue);

                return applicationId;
            }
        }

        #endregion

        #region private methods
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var applicationData = Framework.Components.ApplicationUser.Application.GetList(SessionVariables.AuditId);
                UIHelper.LoadDropDown(applicationData, drpSearchConditionAplication,
					ApplicationDataModel.DataColumns.Name,
					ApplicationDataModel.DataColumns.ApplicationId);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RaiseSearch();
        }

        #endregion

    }
}