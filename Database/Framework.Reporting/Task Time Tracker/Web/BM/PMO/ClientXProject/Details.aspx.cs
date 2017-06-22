using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Framework.Components.Core;
using Framework.Components.DataAccess;
using Framework.UI.Web.BaseClasses;
using Shared.WebCommon.UI.Web;

namespace ApplicationContainer.UI.Web.ClientXProject
{
	public partial class Details : PageDetails
	{

		#region Events

		protected override void OnInit(EventArgs e)
		{
			try
			{
				base.OnInit(e);

				var detailsControlPath = ApplicationCommon.GetControlPath("ClientXProject", ControlType.DetailsControl);
				SuperKey = ApplicationCommon.GetSuperKey();
				SetId = ApplicationCommon.GetSetId();

				if (!string.IsNullOrEmpty(SuperKey))
				{
					btnClone.Visible = false;

					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(SuperKey);

					// Change System Entity Type
					data.SystemEntityTypeId = (int)SystemEntity.ClientXProject;
					var dt = SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);
					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (DataRow dr in dt.Rows)
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
				else
				{
					var detailsControl = (Controls.Details)Page.LoadControl(detailsControlPath);
					detailsControl.SetId = SetId;
					plcDetailsList.Controls.Add(detailsControl);
					chkVisible.Checked = detailsControl.IsHistoryVisible;
				}

			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}

		

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "ClientXProjectDefaultView";
			
		}
		

		

		#endregion

	}
}