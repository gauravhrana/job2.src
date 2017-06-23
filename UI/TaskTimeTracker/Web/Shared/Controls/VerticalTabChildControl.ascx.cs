using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Shared.UI.WebFramework;

namespace Shared.UI.Web.Controls
{
    public partial class VerticalTabChildControl : BaseControl
    {

        #region properties

        public bool IsCollapsed
        {
            get
            {
                if (ViewState["IsCollapsed"] != null)
                {
                    return Convert.ToBoolean(ViewState["IsCollapsed"]);
                }
                return true;
            }
            set
            {
                ViewState["IsCollapsed"] = value;
            }
        }

        public string Title
        {
            get
            {
                return lblTitle.Text;
            }
        }

        public Control ChildGenericControl
        {
            get
            {
                if (pnlCollapsibleContent.Controls.Count > 1)
                {
                    return pnlCollapsibleContent.Controls[1];
                }
                else
                {
                    return pnlCollapsibleContent.Controls[0];
                }
            }
        }

        #endregion

        #region Methods

        public void RaiseCallbackEvent(String eventArgument)
        {
            try
            {
                //var isExists = false;
                //var x = new List<string>();
                //foreach(var xString in LastOpenVerticalTabs)
                //{
                //    if (xString != eventArgument)
                //    {
                //        x.Add(xString);
                //    }
                //    else
                //    {
                //        isExists = true;
                //    }
                //}
                //if (!isExists)
                //{
                //    x.Add(eventArgument);
                //}
                //LastOpenVerticalTabs = x;

                // Save state with PageStatePersister and place it to Page.ClientState
                //System.Reflection.MethodInfo mi = typeof(Page).GetMethod("SaveAllState", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                //mi.Invoke(this.Page, null);

                //// Get serialized viewstate from Page's ClientState
                //System.Reflection.PropertyInfo stateProp = typeof(Page).GetProperty("ClientState", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                //string state = stateProp.GetValue(this.Page, null).ToString();

                //System.Reflection.MethodInfo mi1 = typeof(ClientScriptManager).GetMethod("EnsureEventValidationFieldLoaded", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                //mi1.Invoke(this.Page.ClientScript, null);

                //System.Reflection.MethodInfo mi2 = typeof(ClientScriptManager).GetMethod("GetEventValidationFieldValue", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                //string s = (string)mi2.Invoke(this.Page.ClientScript, null);

                //is.Value = eventArgument;
            }
            catch { }
        }

        public string GetCallbackResult()
        {
            return String.Empty;
        }

        public void Setup(string id, Control control, string title, bool isCollapsed = true, string backColor = "")
        {
            hdnId.Value = id;
            lblTitle.Text = title;

            if (control != null)
            {
                pnlCollapsibleContent.Controls.Add(control);
            }
            
			if (!string.IsNullOrEmpty(backColor))
            {
                try
                {
                    pnlHeader.BackColor = Color.FromName(backColor);
                }
                catch
                {
                    pnlHeader.BackColor = Color.Gray;
                }
            }
            else
            {
                pnlHeader.BackColor = Color.Gray;
            }

            if (txtIsCollapsed.Text == "false")
            {
                cpExtender.Collapsed = false;
            }
            else
            {
                cpExtender.Collapsed = true;
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            //ClientScriptManager cm = Page.ClientScript;
            //String cbReference = cm.GetCallbackEventReference(this, "arg",
            //    "ReceiveServerData", "");
            //String callbackScript = "function CallServer(arg, context) {" +
            //    cbReference + "; }";
            //cm.RegisterClientScriptBlock(this.GetType(),
            //    "CallServer", callbackScript, true);
        }

        #endregion

    }
}