//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ComponentModel;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using Shared.WebCommon.UI.Web;
//using Framework.Components.DataAccess;

//namespace Framework.UI.Web.BaseClasses
//{
//    [DefaultProperty("Text")]
//    [ToolboxData("<{0}:WebCustomControl1 runat=server></{0}:WebCustomControl1>")]

//    public class PageGeneric : PageCommon
//    {
//        #region Variables

//        protected ControlGeneric PrimaryGenericControl { get; set; }

//        #endregion

//        #region Methods

//        protected virtual void InsertData()
//        { }

//        #endregion

//        #region Events

//        protected void btnInsert_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                PrimaryGenericControl.Save("Insert");
//                Response.Redirect(Page.GetRouteUrl(PrimaryEntityKey + "EntityRoute", new { Action = "Default", SetId = true }), false);
//            }
//            catch (Exception ex)
//            {
//                Response.Write(ex.Message);
//            }
//        }
		
//        protected virtual void btnClone_Click(object sender, EventArgs e)
//        {
//            Response.Redirect(Page.GetRouteUrl(PrimaryEntityKey + "EntityRoute", new { Action = "Clone", SetId = SetId }), false);
//        }

//        #endregion
//    }
//}