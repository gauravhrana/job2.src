using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Web.UI.HtmlControls;

namespace Shared.UI.Web.Controls
{
    public partial class SlideAndPushMenu : System.Web.UI.UserControl
    {

        #region Methods

        private void bindApplicationMode()
        {
            var dt = Framework.Components.UserPreference.ApplicationMode.GetList(SessionVariables.AuditId);
            UIHelper.LoadDropDown(dt, drpApplicationMode, Framework.Components.UserPreference.DomainModel.ApplicationMode.DataColumns.Name,
                Framework.Components.UserPreference.DomainModel.ApplicationMode.DataColumns.Name);
        }

        private void GenerateAccordinHTML()
        {
            var xUl = new HtmlGenericControl("ul");

            List<string> lstMenus = new List<string>();
            lstMenus.Add("DashBoard");
            lstMenus.Add("Tasks");
            lstMenus.Add("Calendar");
            lstMenus.Add("Heart");

            foreach (var strLiHeader in lstMenus)
            {
                var xLi = new HtmlGenericControl("li");

                var xH3 = new HtmlGenericControl("h3");
                var subSpan = new HtmlGenericControl("span");
                subSpan.Attributes.Add("class", "icon-" + strLiHeader.ToLower());
                xH3.Controls.Add(subSpan);

                var subSpan2 = new HtmlGenericControl("span");
                subSpan2.InnerText = strLiHeader;
                xH3.Controls.Add(subSpan2);

                xLi.Controls.Add(xH3);

                var subUl = new HtmlGenericControl("ul");

                var subLi1 = new HtmlGenericControl("li");
                var subAnchor1 = new HtmlGenericControl("a");
                subAnchor1.Attributes.Add("href", "#");
                subAnchor1.InnerText = "Anchor 1";

                subLi1.Controls.Add(subAnchor1);
                subUl.Controls.Add(subLi1);

                var subLi2 = new HtmlGenericControl("li");
                var subAnchor2 = new HtmlGenericControl("a");
                subAnchor2.Attributes.Add("href", "#");
                subAnchor2.InnerText = "Anchor 2";

                subLi2.Controls.Add(subAnchor2);
                subUl.Controls.Add(subLi2);

                var subLi3 = new HtmlGenericControl("li");
                var subAnchor3 = new HtmlGenericControl("a");
                subAnchor3.Attributes.Add("href", "#");
                subAnchor3.InnerText = "Anchor 3";

                subLi3.Controls.Add(subAnchor3);
                subUl.Controls.Add(subLi3);

                xLi.Controls.Add(subUl);
                xUl.Controls.Add(xLi);
            }


            serverAccordin.Controls.Add(xUl);




        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateAccordinHTML();
                bindApplicationMode();
                drpApplicationMode.SelectedValue = Convert.ToString(SessionVariables.UserApplicationMode);
            }
        }

        protected void drpApplicationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessionVariables.UserApplicationMode = drpApplicationMode.SelectedValue;

            var appMode = drpApplicationMode.SelectedValue;
            if (appMode != "Live")
            {
                SessionVariables.IsTesting = true;
            }
            else
            {
                SessionVariables.IsTesting = false;
            }
            PerferenceUtility.UpdateUserPreference("General", ApplicationCommon.UserApplicationMode, drpApplicationMode.SelectedValue);
        }

        #endregion

    }
}