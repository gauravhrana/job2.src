using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.ApplicationManagement.HelpPageContext
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {

        #region Events

        private bool showMultipleUpdateView = false;

        protected override void OnInit(EventArgs e)
        {

            try
            {
				base.OnInit(e);

				SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();
				var path = ApplicationCommon.GetControlPath("HelpPageContext", ControlType.DetailsControl);
				var genericcontrolpath = ApplicationCommon.GetControlPath("HelpPageContext", ControlType.GenericControl);

               
                if (showMultipleUpdateView)
                {
                    var data = new SuperKeyDetailDataModel();
                    data.SuperKeyId = Convert.ToInt32(SuperKey);
                    // Change System Entity Type
                    data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.HelpPageContext;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);

                            var HelpPageContextupdateControl = (Controls.Generic)Page.LoadControl(genericcontrolpath);
                            HelpPageContextupdateControl.SetId(key, false);
                            plcUpdateList.Controls.Add(HelpPageContextupdateControl);
                            if (dt.Rows.Count > 1)
                            {
                                HelpPageContextupdateControl.BorderClass = ApplicationCommon.DetailsBorderClassName;
                                HelpPageContextupdateControl.Controls.Add(new LiteralControl("<br />"));
                            }
                            chkVisible.Checked = HelpPageContextupdateControl.IsHistoryVisible;
                        }
                    }
                }
                else
                {
                    var key = SetId;

                    var HelpPageContextupdateControl = (Controls.Generic)Page.LoadControl(genericcontrolpath);
                    HelpPageContextupdateControl.SetId(key, false);
                    plcUpdateList.Controls.Add(HelpPageContextupdateControl);

                    chkVisible.Checked = HelpPageContextupdateControl.IsHistoryVisible;
                }
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
                //throw
            }
        }

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "HelpPageContextDefaultView";
			
		}

        override protected void btnUpdate_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < plcUpdateList.Controls.Count; i++)
            {
                var myGenericControl = (Controls.Generic)plcUpdateList.Controls[i];
                var data = new HelpPageContextDataModel();

                data.HelpPageContextId   = myGenericControl.HelpPageContextId;
                data.Name                = myGenericControl.Name;
                data.Description         = myGenericControl.Description;
                data.SortOrder           = myGenericControl.SortOrder;

				Framework.Components.Core.HelpPageContextDataManager.Update(data, SessionVariables.RequestProfile);
             
                Response.Redirect(Page.GetRouteUrl("HelpPageContextEntityRoute", new { Action = "Default", SetId = true }), false);
            }
        }


        #endregion

    }

}