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
	public partial class InlineUpdate : Shared.UI.WebFramework.BasePage
	{
		public delegate void UpdateDelegate(Dictionary<string, string> values);
		private DataTable GetData()
		{
			try
			{
				SuperKey = ApplicationCommon.GetSuperKey();
				SetId = ApplicationCommon.GetSetId();
				var selectedrows = new DataTable();
				var UserLogindata = new Framework.Components.LogAndTrace.UserLoginDataModel();

				selectedrows = Framework.Components.LogAndTrace.UserLoginDataManager.GetDetails(UserLogindata, SessionVariables.RequestProfile).Clone();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(SuperKey);
					// Change System Entity Type
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.UserLogin;
					var dt = Framework.Components.Core.SuperKeyDetailDataManager.Search(data, SessionVariables.RequestProfile);

					if (dt != null && dt.Rows.Count > 0)
					{
						var keys = new int[dt.Rows.Count];
						for (var i = 0; i < dt.Rows.Count; i++)
						{

							keys[i] = Convert.ToInt32(dt.Rows[i][SuperKeyDetailDataModel.DataColumns.EntityKey]);
							UserLogindata.UserLoginId = keys[i];
							var result = Framework.Components.LogAndTrace.UserLoginDataManager.GetDetails(UserLogindata, SessionVariables.RequestProfile);
							selectedrows.ImportRow(result.Rows[0]);


						}
					}
				}
				else if (SetId != 0)
				{
					var key = SetId;
					UserLogindata.UserLoginId = key;
					var result = Framework.Components.LogAndTrace.UserLoginDataManager.GetDetails(UserLogindata, SessionVariables.RequestProfile);
					selectedrows.ImportRow(result.Rows[0]);

				}
				return selectedrows;
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