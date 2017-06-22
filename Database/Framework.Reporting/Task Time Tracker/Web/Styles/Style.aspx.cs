using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.Styles
{
	public partial class Style : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		public string GetSize()
		{
			return "12.6em;";
		}

        public string GetColor()
        {
            if (SessionVariables.IsTesting)
                return "Blue";
            else
                return "White";
        }

        public string GetWidth()
        {
            if (SessionVariables.IsTesting)
                return "1px";
            else
                return "0px";
        }

        public string GetStyle()
        {
            if (SessionVariables.IsTesting)
                return "Groove";
            else
                return "none";
        }
	}
}