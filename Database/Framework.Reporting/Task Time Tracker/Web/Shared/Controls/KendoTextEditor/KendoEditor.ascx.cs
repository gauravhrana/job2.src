using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shared.UI.Web.Controls.KendoTextEditor
{
    public partial class KendoEditor : UserControl
    {
       
        public string Text
        {
			get
			{
				return Server.HtmlDecode(hdnDescription.Value);
		}
            set {
				hdnDescription.Value = value; 
			}
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}