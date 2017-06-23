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

namespace Shared.UI.Web.Admin.ConnectionString
{
	public partial class InlineUpdate : Shared.UI.WebFramework.BasePage
	{
		public delegate void UpdateDelegate(Dictionary<string, string> values);

		#region Methods

		private DataTable GetData()
		{
			try
			{
				SuperKey = ApplicationCommon.GetSuperKey();
				SetId = ApplicationCommon.GetSetId();
				var selectedrows = new List<ConnectionStringDataModel>();
				var ConnectionStringdata = new ConnectionStringDataModel();
				if (!string.IsNullOrEmpty(SuperKey))
				{
					var data = new SuperKeyDetailDataModel();
					data.SuperKeyId = Convert.ToInt32(SuperKey);
					// Change System Entity Type
					data.SystemEntityTypeId = (int)Framework.Components.DataAccess.SystemEntity.ConnectionString;
					var listSuperKeyDetails = SuperKeyDetailDataManager.GetEntityDetails(data, SessionVariables.RequestProfile, ApplicationCommon.ReturnAuditInfoFalse);

                    if (listSuperKeyDetails != null && listSuperKeyDetails.Count > 0)
                    {
                        var keys = new int[listSuperKeyDetails.Count];
                        for (var i = 0; i < listSuperKeyDetails.Count; i++)
                        {

                            keys[i] = listSuperKeyDetails[i].EntityKey.Value;
							ConnectionStringdata.ConnectionStringId = keys[i];
							var result = Framework.Components.Core.ConnectionStringDataManager.GetDetails(ConnectionStringdata, SessionVariables.RequestProfile);
                            selectedrows.Add(result);


						}
					}
				}
				else 
				{
					var key = SetId;
					ConnectionStringdata.ConnectionStringId = key;
					var result = Framework.Components.Core.ConnectionStringDataManager.GetDetails(ConnectionStringdata, SessionVariables.RequestProfile);
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

			return FieldConfigurationUtility.GetEntityColumns(Framework.Components.DataAccess.SystemEntity.ConnectionString, "DBColumns", SessionVariables.RequestProfile);
		}

		private void Update(Dictionary<string, string> values)
		{
			var data = new ConnectionStringDataModel();

            // copies properties from values dictionary object to data object
            PropertyMapper.CopyProperties(data, values);

			Framework.Components.Core.ConnectionStringDataManager.Update(data, SessionVariables.RequestProfile);
			InlineEditingList.Data  = GetData();
		}
		
		#endregion

		#region Events

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
				InlineEditingList.SetUp(GetColumns(), "ConnectionString", GetData());
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			
			SettingCategory = "ConnectionStringDefaultView";

		}

		#endregion

	}
}