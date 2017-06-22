using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.HelpPage
{
    public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
    {

        #region Events

        protected override void OnInit(EventArgs e)
        {
            try
            {
				base.OnInit(e);

                SuperKey = ApplicationCommon.GetSuperKey();
                SetId = ApplicationCommon.GetSetId();
				var detailsControlPath = ApplicationCommon.GetControlPath("HelpPage", ControlType.DetailsControl);

                if (!string.IsNullOrEmpty(SuperKey))
                {
                    btnClone.Visible = false;

                    var data = new SuperKeyDetailDataModel();
                    data.SuperKeyId = Convert.ToInt32(SuperKey);

                    // Change System Entity Type
                    data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.HelpPage;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (System.Data.DataRow dr in dt.Rows)
                        {
                            var key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);

                            var detailsControl = (Controls.Details)Page.LoadControl(detailsControlPath);
                            detailsControl.SetId = key;
                            detailsControl.BorderClass = ApplicationCommon.DetailsBorderClassName;
                            plcDetailsList.Controls.Add(detailsControl);
                            plcDetailsList.Controls.Add(new LiteralControl("<br />"));

                            chkVisible.Checked = detailsControl.IsHistoryVisible;
                        }
                    }
                }
                else if (SetId != 0)
                {
                    var key = SetId;
                    var detailsControl = (Controls.Details)Page.LoadControl(detailsControlPath);
                    detailsControl.SetId = key;
                    plcDetailsList.Controls.Add(detailsControl);
                    chkVisible.Checked = detailsControl.IsHistoryVisible;
                }
              
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			
			SettingCategory = "HelpPageDefaultView";
			
		}
        
        #endregion

    }
}