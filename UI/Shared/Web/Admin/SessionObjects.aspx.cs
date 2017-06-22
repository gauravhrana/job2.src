﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data;

namespace Shared.UI.Web.Admin
{
    public partial class SessionObjects : System.Web.UI.Page
    {

        #region Methods

        private void LoadSessionObjects()
        {
            if (Session.Count > 0)
            {               
                foreach (var key in Session.Keys)
                {
                    var linkButton = new LinkButton();
                    linkButton.Text = Convert.ToString(key);
                    linkButton.Click += new EventHandler(linkButton_Click);

                    var innerDiv = new HtmlGenericControl("div");
                    innerDiv.Attributes.CssStyle.Add("height", "40px");
                    innerDiv.Attributes.CssStyle.Add("width", "40px");
                    innerDiv.Controls.Add(linkButton);
                    divObjects.Controls.Add(innerDiv);
                }
                LoadSessionObjectValue(Session.Keys[0]);
            }
        }

        private void LoadSessionObjectValue(string sessionKey)
        {
            divValue.InnerHtml = string.Empty;
            var sessionValues = Session[sessionKey]; 
            var objType = sessionValues.GetType().Name;
            if (sessionValues is int || sessionValues is string || sessionValues is bool)
            {                
                divValue.InnerText = Convert.ToString(sessionValues);
            }
            else if (sessionValues is DataTable || sessionValues is List<ApplicationUserRole> || sessionValues is List<UPreference>)
            {
                var gv = new GridView();
                gv.AutoGenerateColumns = true;
                gv.AutoGenerateDeleteButton = false;
                gv.AutoGenerateEditButton = false;
                gv.AutoGenerateSelectButton = false;
                gv.Style.Add("table-layout", "fixed");
                gv.Width = Unit.Parse("900px");
                gv.DataSource = sessionValues;
                gv.DataBind();
                divValue.Controls.Add(gv);
            }
            else if (sessionValues is DataSet)
            {
                var ds = (DataSet)sessionValues;
                foreach (DataTable dt in ds.Tables)
                {
                    var gv = new GridView();
                    gv.AutoGenerateColumns = true;
                    gv.AutoGenerateDeleteButton = false;
                    gv.AutoGenerateEditButton = false;
                    gv.AutoGenerateSelectButton = false;
                    gv.Style.Add("table-layout", "fixed");
                    gv.Width = Unit.Parse("900px");
                    gv.DataSource = dt;
                    gv.DataBind();
                    divValue.Controls.Add(gv);
                }
            }
            lblObjectType.Text = objType;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            LoadSessionObjects();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        void linkButton_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var sessionKey = btn.Text;
            LoadSessionObjectValue(sessionKey);
        }

        #endregion

    }
}