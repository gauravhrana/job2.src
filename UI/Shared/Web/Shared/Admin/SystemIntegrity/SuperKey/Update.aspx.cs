using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.SuperKey
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {

        #region Events

        int setId = -1;
        private bool showMultipleUpdateView = false;

        protected override void OnInit(EventArgs e)
        {
            try
            {
				base.OnInit(e);

				var path = "~/SystemIntegrity/SuperKey/Controls/Details.ascx";
                var genericcontrolpath = "~/SystemIntegrity/SuperKey/Controls/Generic.ascx";

                SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();

                
                if (showMultipleUpdateView)
                {
                    var data = new SuperKeyDetailDataModel();
                    data.SuperKeyId = Convert.ToInt32(SuperKey);
                    // Change System Entity Type
                    data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.SuperKey;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);

                            var ApplicationupdateControl = (Controls.Generic)Page.LoadControl(genericcontrolpath);
                            ApplicationupdateControl.SetId(key, false);
                            plcUpdateList.Controls.Add(ApplicationupdateControl);
                            if (dt.Rows.Count > 1)
                            {
                                ApplicationupdateControl.BorderClass = ApplicationCommon.DetailsBorderClassName;
                                ApplicationupdateControl.Controls.Add(new LiteralControl("<br />"));
                            }
                            chkVisible.Checked = ApplicationupdateControl.IsHistoryVisible;
                        }
                    }
                }
                else if (SetId != 0)
                {
                    var ApplicationupdateControl = (Controls.Generic)Page.LoadControl(genericcontrolpath);
                    ApplicationupdateControl.SetId(SetId, false);
                    plcUpdateList.Controls.Add(ApplicationupdateControl);
                    chkVisible.Checked = ApplicationupdateControl.IsHistoryVisible;
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

			
			SettingCategory = "SuperKeyDefaultView";
			
		}

        override protected void btnUpdate_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < plcUpdateList.Controls.Count; i++)
            {
                var myGenericControl = (Controls.Generic)plcUpdateList.Controls[i];
                var data = new SuperKeyDataModel();

                data.SuperKeyId         = myGenericControl.SuperKeyId;
                data.Name               = myGenericControl.Name;
                data.Description        = myGenericControl.Description;
                data.SortOrder          = myGenericControl.SortOrder;
                data.SystemEntityTypeId = myGenericControl.SystemEntityTypeId;
                data.ExpirationDate     = myGenericControl.ExpirationDate;

                Framework.Components.Core.SuperKeyDataManager.Update(data, SessionVariables.RequestProfile);               
            }

            // if everything is done and good THEN move from thsi page.
            Response.Redirect(Page.GetRouteUrl("SuperKeyEntityRoute", new { Action = "Default", SetId = true }), false);
        }


        
        

        #endregion

    }
}