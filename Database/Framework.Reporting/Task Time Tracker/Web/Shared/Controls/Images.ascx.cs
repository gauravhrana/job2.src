using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Shared.UI.Web.Controls
{
    public partial class Images : UserControl
    {

        public delegate DataTable GetDataDelegate(int pkId);
        private GetDataDelegate _getData;
        private DataTable dtGlobal = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        public void Setup(GetDataDelegate _dt)
        {
            _getData = _dt;
            ImagesRepeater.DataSource = _dt;
            ImagesRepeater.DataBind();
        }

        public void Setup(DataTable _dt)
        {
            ImagesRepeater.DataSource = _dt;
            ImagesRepeater.DataBind();
        }

        public void SetSession(string sessionUpdated)
        {
            ViewState["SessionUpdated"] = sessionUpdated;
        }
    }
}