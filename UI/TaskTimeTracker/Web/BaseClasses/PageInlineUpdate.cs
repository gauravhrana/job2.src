using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using Framework.Components.DataAccess;
using System.Data;
using Shared.UI.Web.Controls;
using System.Web;
using Framework.Components.UserPreference;
using System.Configuration;

namespace Framework.UI.Web.BaseClasses
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:PageInlineUpdate runat=server></{0}:PageInlineUpdate>")]
	public class PageInlineUpdate : PageCommon
	{

		#region Variables & Properties

		protected eList InlineEditingListCore { get; set; }

		public delegate void UpdateDelegate(Dictionary<string, string> values);

		protected int InlineUpdateColumnsModeId
		{
			get
			{
				if (HttpContext.Current.Session["InlineUpdateColumnsModeId"] == null)
				{
					var fcModeName = ConfigurationManager.AppSettings["InlineUpdateColumnsModeName"];
					HttpContext.Current.Session["InlineUpdateColumnsModeId"] = FieldConfigurationModeDataManager.GetFCModeIdByName(fcModeName, SessionVariables.RequestProfile);
				}
				return Convert.ToInt32(HttpContext.Current.Session["InlineUpdateColumnsModeId"]);
			}
		}

		#endregion

		#region Methods

		protected virtual DataTable GetData()
		{
			return null;
		}

		protected virtual string[] GetColumns()
		{
			return FieldConfigurationUtility.GetEntityColumns(InlineUpdateColumnsModeId.ToString(), PrimaryEntity, SessionVariables.RequestProfile);
		}

		protected virtual void Update(Dictionary<string, string> values)
		{
			InlineEditingListCore.Data = GetData();
		}

		#endregion

        #region Events

        protected override void OnPreInit(EventArgs e)
        {
            base.SetSiteMasterPagePath();

            base.OnPreInit(e);
        }

		protected override void OnInit(EventArgs e)
		{
			InlineEditingListCore.AddColumns(GetColumns());

			// done here, because its not in view state
			ViewName = "InlineUpdate";
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);


			var delupdate = new UpdateDelegate(Update);
			InlineEditingListCore.DelUpdateRef = delupdate;

			if (!IsPostBack)
			{
				InlineEditingListCore.SetUp(GetColumns(), PrimaryEntityKey, GetData());
			}
		}

		#endregion

	}
}