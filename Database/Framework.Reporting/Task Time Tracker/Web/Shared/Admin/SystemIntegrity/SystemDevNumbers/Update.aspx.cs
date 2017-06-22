using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemDevNumbers
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {

        #region Events

        int setId = -1;
        private bool showMultipleUpdateView = false;

        

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "SystemDevNumbersDefaultView";
			
		}

        protected override void OnInit(EventArgs e)
        {

            try
            {
				base.OnInit(e);

				var path = "~/Shared/Admin/SystemIntegrity/SystemDevNumbers/Controls/Details.ascx";
                var genericcontrolpath = "~/Shared/Admin/SystemIntegrity/SystemDevNumbers/Controls/Generic.ascx";

                SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();

                if (!string.IsNullOrEmpty(SuperKey))
                {
                    btnCancel.Visible = true;
                    btnUpdate.Visible = true;
                    showMultipleUpdateView = true;
                }
                else if (Request.QueryString["SetId"] != null)
                {
                    setId = int.Parse(Request.QueryString["SetId"]);
                    btnUpdate.Visible = true;
                    btnCancel.Visible = true;
                }
                if (showMultipleUpdateView)
                {
                    var data = new SuperKeyDetailDataModel();
                    data.SuperKeyId = Convert.ToInt32(SuperKey);
                    // Change System Entity Type
                    data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.SystemDevNumbers;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);

                            var SystemDevNumbersupdateControl = (Controls.Generic)Page.LoadControl(genericcontrolpath);
                            SystemDevNumbersupdateControl.SetId(key, false);
                            plcUpdateList.Controls.Add(SystemDevNumbersupdateControl);
                            if (dt.Rows.Count > 1)
                            {
                                SystemDevNumbersupdateControl.BorderClass = ApplicationCommon.DetailsBorderClassName;
                                SystemDevNumbersupdateControl.Controls.Add(new LiteralControl("<br />"));
                            }
                            chkVisible.Checked = SystemDevNumbersupdateControl.IsHistoryVisible;
                        }
                    }
                }
                else if (SetId != 0)
                {
                    var SystemDevNumbersupdateControl = (Controls.Generic)Page.LoadControl(genericcontrolpath);
                    SystemDevNumbersupdateControl.SetId(SetId, false);
                    plcUpdateList.Controls.Add(SystemDevNumbersupdateControl);
                    chkVisible.Checked = SystemDevNumbersupdateControl.IsHistoryVisible;
                }

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
                //throw
            }
        }       

        #endregion

    }
}