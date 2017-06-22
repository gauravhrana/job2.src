using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.DataAccess;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.QuickPaginationRun
{
    public partial class Update : Framework.UI.Web.BaseClasses.PageUpdate
    {

        #region variables

        private bool showMultipleUpdateView = false;

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            try
            {
				base.OnInit(e);

				PrimaryEntity = SystemEntity.QuickPaginationRun;


				GenericControlPath = ApplicationCommon.GetControlPath("QuickPaginationRun", ControlType.GenericControl);
				PrimaryPlaceHolder = plcUpdateList;
				PrimaryEntityKey = "Client";
				BreadCrumbObject = Master.BreadCrumbObject;

				BtnUpdate = btnUpdate;
				BtnClone = btnClone;
				BtnCancel = btnCancel;

				//SuperKey = ApplicationCommon.GetSuperKey();
				//SetId = ApplicationCommon.GetSetId();
				//var path = "~/Shared/Admin/SystemIntegrity/QuickPaginationRun/Controls/Details.ascx";
				//var genericcontrolpath = "~/Shared/Admin/SystemIntegrity/QuickPaginationRun/Controls/Generic.ascx";
                
				//if (showMultipleUpdateView)
				//{
				//	var data = new SuperKeyDetailDataModel();
				//	data.SuperKeyId = Convert.ToInt32(SuperKey);
				//	// Change System Entity Type
				//	data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.QuickPaginationRun;
				//	var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

				//	if (dt != null && dt.Rows.Count > 0)
				//	{
				//		foreach (System.Data.DataRow dr in dt.Rows)
				//		{
				//			var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);

				//			var ApplicationupdateControl = (Controls.Generic)Page.LoadControl(genericcontrolpath);
				//			ApplicationupdateControl.SetId(key, false);
				//			plcUpdateList.Controls.Add(ApplicationupdateControl);
				//			if (dt.Rows.Count > 1)
				//			{
				//				ApplicationupdateControl.BorderClass = ApplicationCommon.DetailsBorderClassName;
				//				ApplicationupdateControl.Controls.Add(new LiteralControl("<br />"));
				//			}
				//			chkVisible.Checked = ApplicationupdateControl.IsHistoryVisible;
				//		}
				//	}
				//}
				//else
				//{
				//	var key = SetId;

				//	var ApplicationupdateControl = (Controls.Generic)Page.LoadControl(genericcontrolpath);
				//	ApplicationupdateControl.SetId(key, false);
				//	plcUpdateList.Controls.Add(ApplicationupdateControl);
				//	chkVisible.Checked = ApplicationupdateControl.IsHistoryVisible;
				//}

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
                //throw
            }
        }

		//protected override void OnPreRender(EventArgs e)
		//{
		//	base.OnPreRender(e);
			
		//	SettingCategory = "QuickPaginationRunDefaultView";
			
		//}    

        #endregion

    }
}