using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.SystemIntegrity.StoredProcedureLogRaw
{
	public partial class Details : Framework.UI.Web.BaseClasses.PageDetails
	{
		
        #region variables

		private bool showDeleteView = false;
		private bool showDetailsView = false;
		private int _setId = -1;

		private int SetId
		{
			get
			{

				return _setId;
			}
			set { _setId = value; }
		}

		#endregion

		#region Events	

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			
			SettingCategory = "StoredProcedureLogRawDefaultView";			
		}

		protected override void OnInit(EventArgs e)
		{
			try
			{
                var deleteIds = ApplicationCommon.GetDeleteIdList();
                var detailIds = ApplicationCommon.GetDetailIdList();
                var setId = ApplicationCommon.GetSetId();

				var indexes = "";
				var path = "~/SystemIntegrity/StoredProcedureLogRaw/Controls/Details.ascx";
				var genericcontrolpath = "~/SystemIntegrity/StoredProcedureLogRaw/Controls/Generic.ascx";

                if (string.IsNullOrEmpty(deleteIds))
				{
                    indexes = deleteIds;
					btnBack.Visible = true;

					showDeleteView = true;
				}
                else if (string.IsNullOrEmpty(detailIds))
				{
                    indexes = detailIds;
					btnBack.Visible = true;

					showDetailsView = true;
				}
				
				if (showDeleteView || showDetailsView)
				{
					var deleteindexlist = indexes.Split(',');

					foreach (var s in deleteindexlist)
					{
						var key = int.Parse(s);

						var Detaildetailscontrol = (Controls.Details)Page.LoadControl(path);
						Detaildetailscontrol.SetId = key;
						//Detaildetailscontrol.Border = "1px";
						plcDetailsList.Controls.Add(Detaildetailscontrol);
						plcDetailsList.Controls.Add(new LiteralControl("<br />"));
						chkVisible.Checked = Detaildetailscontrol.IsHistoryVisible;
					}
				}
				else
				{
                    var key = setId != 0 ? setId : 0; 

					var Detaildetailscontrol = (Controls.Details)Page.LoadControl(path);
					Detaildetailscontrol.SetId = key;
					plcDetailsList.Controls.Add(Detaildetailscontrol);
					chkVisible.Checked = Detaildetailscontrol.IsHistoryVisible;
				}


				base.OnInit(e);
			}
			catch (Exception ex)
			{

				System.Diagnostics.Debug.WriteLine(ex.Message);
				//throw
			}
		}
		protected void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
                var deleteIds = ApplicationCommon.GetDeleteIdList();

                var deleteindexlist = deleteIds.Split(',');

				foreach (string index in deleteindexlist)
				{
					var data = new Framework.Components.LogAndTrace.StoredProcedureLogRawDataModel();
					data.StoredProcedureLogRawId = int.Parse(index);

					Framework.Components.LogAndTrace.StoredProcedureLogRawDataManager.Delete(data, SessionVariables.RequestProfile);
				}
				Response.Redirect("Default.aspx?Deleted=" + true, false);
				//ShowData();
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
		}
		override protected void btnBack_Click(object sender, EventArgs e)
		{
			Response.Redirect("Default.aspx");
		}

		#endregion

	}
}