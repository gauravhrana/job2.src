using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.Components.LogAndTrace;
using Dapper;

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
			var UpdatedData = new List<UserLoginDataModel>();
			var data = new Framework.Components.LogAndTrace.UserLoginDataModel();

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
				var dt = Framework.Components.LogAndTrace.UserLoginDataManager.GetEntityDetails(data, SessionVariables.RequestProfile);

				if (dt.Count == 1)
				{
					UpdatedData.Add(dt[0]);
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
                var results = new List<UserLoginDataModel>();

				if (!string.IsNullOrEmpty(SuperKey))
				{
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(SuperKey);

					// Change System Entity Type
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.UserLogin;
					var listSuperKeyDetails = Framework.Components.Core.SuperKeyDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

                    if (listSuperKeyDetails != null && listSuperKeyDetails.Count > 0)
                    {
                        foreach (var itemSuperKeyDetail in listSuperKeyDetails)
                        {
                            key = itemSuperKeyDetail.EntityKey.Value;
							UserLogindata.UserLoginId = key;
							var UserLogindt = Framework.Components.LogAndTrace.UserLoginDataManager.GetEntityDetails(UserLogindata, SessionVariables.RequestProfile);
							if (UserLogindt.Count == 1)
							{
								results.Add(UserLogindt[0]);
							}
						}
					}
				}
				else
				{
					key = SetId;
					UserLogindata.UserLoginId = key;
					var UserLogindt = Framework.Components.LogAndTrace.UserLoginDataManager.GetEntityDetails(UserLogindata, SessionVariables.RequestProfile);
					if (UserLogindt.Count == 1)
					{
						results.Add(UserLogindt[0]);
					}
				}
				SelectedData = results.ToDataTable();
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