using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.UI.WebFramework;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Controls
{
    public partial class DetailButtonPanelControl : BaseControl
    {

        #region Variables

		string entityName = String.Empty;

        private int _setId;

        public int SetId
        {
            set
            {
                _setId = value;
                SetButtonVisibility();
            }
            get
            {
                return _setId;
            }
        }
				

        #endregion

        #region private methods

        private void SetButtonVisibility()
        {
           // if (!string.IsNullOrEmpty(Request.QueryString["setid"]))
            //{
				if (Request.Path.ToLower().Contains("delete"))
				{
					ButtonDelete.Visible = false;
					ButtonDetails.Visible = false;
				}
				else if (Request.Path.ToLower().Contains("details"))
					ButtonDetails.Visible = false;
			//}
			//else if (!string.IsNullOrEmpty(Request.QueryString["deleteids"]))
			//{
			//    ButtonDelete.Visible = false;
			//    ButtonDetails.Visible = true;
			//}
        }

        #endregion

        #region Events
				
        protected void Page_Load(object sender, EventArgs e)
        {
			 entityName = Page.RouteData.Values["EntityName"].ToString();			
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
						
			Response.Redirect(Page.GetRouteUrl(entityName + "EntityRoute", new { Action = "Delete", SetId = SetId }), false);			
        }

        protected void ButtonDetails_Click(object sender, EventArgs e)
        {
			Response.Redirect(Page.GetRouteUrl(entityName + "EntityRoute", new { Action = "Details", SetId = SetId }), false);			
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {					
			Response.Redirect(Page.GetRouteUrl(entityName + "EntityRoute", new { Action = "Update", SetId = SetId }), false);			
        }

        #endregion

    }

}