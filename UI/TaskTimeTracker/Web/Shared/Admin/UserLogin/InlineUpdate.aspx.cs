using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModel.Framework.Core;
using Shared.WebCommon.UI.Web;
using Framework.Components.Core;
using Dapper;
using Framework.Components.LogAndTrace;

namespace Shared.UI.Web.Admin.UserLogin
{
	public partial class InlineUpdate : Shared.UI.WebFramework.BasePage
	{
		public delegate void UpdateDelegate(Dictionary<string, string> values);
		private DataTable GetData()
		{
			try
			{
				SuperKey = ApplicationCommon.GetSuperKey();
				SetId = ApplicationCommon.GetSetId();
				var selectedrows = new List<UserLoginDataModel>();
				var UserLogindata = new UserLoginDataModel();

				if (!string.IsNullOrEmpty(SuperKey))
				{
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(SuperKey);
					// Change System Entity Type
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.UserLogin;
					var listSuperKeyDetails = SuperKeyDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

                    if (listSuperKeyDetails != null && listSuperKeyDetails.Count > 0)
                    {
                        var keys = new int[listSuperKeyDetails.Count];
                        for (var i = 0; i < listSuperKeyDetails.Count; i++)
                        {

                            keys[i] = listSuperKeyDetails[i].EntityKey.Value;
							UserLogindata.UserLoginId = keys[i];
							var result = Framework.Components.LogAndTrace.UserLoginDataManager.GetDetails(UserLogindata, SessionVariables.RequestProfile);
                            selectedrows.Add(result);


						}
					}
				}
				else if (SetId != 0)
				{
					var key = SetId;
					UserLogindata.UserLoginId = key;
					var result = Framework.Components.LogAndTrace.UserLoginDataManager.GetDetails(UserLogindata, SessionVariables.RequestProfile);
                    selectedrows.Add(result);

				}
				return selectedrows.ToDataTable();
			}
			catch (Exception ex)
			{
				Response.Write(ex.Message);
			}
			return null;
		}

		private string[] GetColumns()
		{

			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.UserLogin, "DBColumns", SessionVariables.RequestProfile);
		}

		protected override void OnInit(EventArgs e)
		{
			InlineEditingList.AddColumns(GetColumns());

		}


		protected void Page_Load(object sender, EventArgs e)
		{
			UpdateDelegate delupdate = new UpdateDelegate(Update);
			this.InlineEditingList.DelUpdateRef = delupdate;
			if (!IsPostBack)
			{
				InlineEditingList.SetUp(GetColumns(), "UserLogin", GetData());
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "UserLoginDefaultView";
			
		}

		private void Update(Dictionary<string, string> values)
		{
			var data = new Framework.Components.LogAndTrace.UserLoginDataModel();
			data.UserLoginId = int.Parse(values[Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.UserLoginId].ToString());
			data.UserName = values[Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.UserName].ToString();
			data.UserLoginStatusId =  int.Parse(values[Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.UserLoginStatusId].ToString());
			data.RecordDate = DateTime.Parse(values[Framework.Components.LogAndTrace.UserLoginDataModel.DataColumns.RecordDate].ToString());
			Framework.Components.LogAndTrace.UserLoginDataManager.Update(data, SessionVariables.RequestProfile);
			InlineEditingList.Data = GetData();
		}
	}
}