using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.WebFramework;
using System.Data;
using System.Text;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Prototype.TestPages
{
	public partial class JquerySorting : Framework.UI.Web.BaseClasses.PageBasePage
    {

        #region Private Methods

        private void GenerateHTML()
        {

            //var strHTML = new StringBuilder();

            //var data = new DataModel.TaskTimeTracker.ClientDataModel();

            //var dt = TaskTimeTracker.Components.BusinessLayer.ClientDataManager.Search(data, AuditId, SessionVariables.ApplicationMode);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        //strHTML.AppendLine("<li class="ui-state-default"><span class="ui-icon ui-icon-arrowthick-2-n-s"></span>Item 1</li>

            //        var innerLine = string.Empty;

            //        innerLine = "<div>";
            //        innerLine += "<span>::</span> " + dr["Name"].ToString() + "";
            //        innerLine += "<div class=\"itemId\"> " + dr["ClientId"].ToString() + "</div>";

            //        innerLine += "<div>Description: " + dr["Description"].ToString() + "</div>";
            //        innerLine += "<div>SortOrder: " + dr["SortOrder"].ToString() + "</div>";

            //        innerLine += "</div>";

            //        strHTML.AppendLine(innerLine);
            //    }
            //}

            //divRecordsContainer2.InnerHtml = strHTML.ToString();
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateHTML();
            }
        }

        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

    }
}