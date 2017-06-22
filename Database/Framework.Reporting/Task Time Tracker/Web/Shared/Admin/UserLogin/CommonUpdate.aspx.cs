using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;

namespace Shared.UI.Web.Admin.UserLogin
{
	public partial class CommonUpdate : Framework.UI.Web.BaseClasses.PageCommonUpdate
	{


		protected void Page_Load(object sender, EventArgs e)
		{
			MainGridView.DataSource = SelectedData;
			MainGridView.DataBind();
		}

		override protected void btnBack_Click(object sender, EventArgs e)
		{
			Response.Redirect(Page.GetRouteUrl("UserLoginEntityRoute", new { Action = "Default", SetId = true }), false);

		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "UserLoginDefaultView";
			
		}

		override protected void btnUpdate_Click(object sender, EventArgs e)
		{
			var UpdatedData = new DataTable();
			var data = new Framework.Components.LogAndTrace.UserLoginDataModel();
			UpdatedData = Framework.Components.LogAndTrace.UserLoginDataManager.Search(data, SessionVariables.RequestProfile).Clone();
			for (var i = 0; i < SelectedData.Rows.Count; i++)
			{
				data.UserLoginId =
					Convert.ToInt32(SelectedData.Rows[i][Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.UserLoginId].ToString());
				data.UserName = !string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.UserName))
					? CheckAndGetRepeaterTextBoxValue(Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.UserName).ToString()
					: SelectedData.Rows[i][Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.UserName].ToString();

				data.UserLoginStatusId =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.UserLoginStatusId))
					?  int.Parse(CheckAndGetRepeaterTextBoxValue(Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.UserLoginStatusId).ToString())
					:  int.Parse(SelectedData.Rows[i][Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.UserLoginStatusId].ToString());

				data.RecordDate =
					!string.IsNullOrEmpty(CheckAndGetRepeaterTextBoxValue(Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.RecordDate))
					? DateTime.Parse(CheckAndGetRepeaterTextBoxValue(Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.RecordDate).ToString())
					: DateTime.Parse(SelectedData.Rows[i][Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.RecordDate].ToString());

				Framework.Components.LogAndTrace.UserLoginDataManager.Update(data, SessionVariables.RequestProfile);
				data = new Framework.Components.LogAndTrace.UserLoginDataModel();
				data.UserLoginId = Convert.ToInt32(SelectedData.Rows[i][Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.UserLoginId].ToString());
				var dt = Framework.Components.LogAndTrace.UserLoginDataManager.Search(data, SessionVariables.RequestProfile);

				if (dt.Rows.Count == 1)
				{
					UpdatedData.ImportRow(dt.Rows[0]);
				}
				

			}
			MainGridView.DataSource = UpdatedData;
			MainGridView.DataBind();
		}

		protected override void OnInit(EventArgs e)
		{

			try
			{
				SuperKey = ApplicationCommon.GetSuperKey();
				SetId = ApplicationCommon.GetSetId();
				var key = 0;
				var UserLogindata = new Framework.Components.LogAndTrace.UserLoginDataModel();
				var results = Framework.Components.LogAndTrace.UserLoginDataManager.Search(UserLogindata, SessionVariables.RequestProfile).Clone();

				if (!string.IsNullOrEmpty(SuperKey))
				{
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(SuperKey);

					// Change System Entity Type
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.UserLogin;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);
					if (dt != null && dt.Rows.Count > 0)
					{
						foreach (System.Data.DataRow dr in dt.Rows)
						{
							key = Convert.ToInt32(dr[SuperKeyDetailDataModel.DataColumns.EntityKey]);
							UserLogindata.UserLoginId = key;
							var UserLogindt = Framework.Components.LogAndTrace.UserLoginDataManager.Search(UserLogindata, SessionVariables.RequestProfile);
							if (UserLogindt.Rows.Count == 1)
							{
								results.ImportRow(UserLogindt.Rows[0]);
							}
						}
					}
				}
				else
				{
					key = SetId;
					UserLogindata.UserLoginId = key;
					var UserLogindt = Framework.Components.LogAndTrace.UserLoginDataManager.Search(UserLogindata, SessionVariables.RequestProfile);
					if (UserLogindt.Rows.Count == 1)
					{
						results.ImportRow(UserLogindt.Rows[0]);
					}
				}
				SelectedData = new DataTable();
				SelectedData = results.Copy();
				base.OnInit(e);
			}
			catch (Exception ex)
			{

				System.Diagnostics.Debug.WriteLine(ex.Message);
				//throw
			}
		}
	}
}