using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.Audit.AuditHistory
{
    public partial class Export : Shared.UI.WebFramework.BasePage
    {

        #region variables

        string SearchCondition = String.Empty;

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // see what parameter was passed 
                SearchCondition = Request.QueryString["SearchCondition"];
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// After all the control have loaded, we can set values
        /// try to follow stratedy in general avoid doing alot of work in OnLoad
        /// OnLoad is quick operation ...
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            this.mySearchControl.ShowData(SearchCondition, true);
        }

        #endregion

    }
}