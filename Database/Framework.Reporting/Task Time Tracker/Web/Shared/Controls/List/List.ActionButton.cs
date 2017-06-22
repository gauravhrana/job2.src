using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shared.WebCommon.UI.Web;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.UserPreference;
using System.Collections.Specialized;
using System.Collections;

namespace Shared.UI.Web.Controls
{
	public partial class ListControl
	{
		private enum ActionCommand
		{
			Delete = 0,
			Details = 1,
			Update = 2,
			TestData = 3,
			RealData = 4,
			CommonUpdate = 5,
			InlineUpdate = 6,
			Renumber = 7
		} 

		protected void ButtonDelete_Click(object sender, EventArgs e)
		{
			try
			{
				var sc = new StringCollection();
				sc = GetSelectedRecordIDs();
				RetrieveRecords(sc, ActionCommand.Delete);
			}
			catch (Exception ex)
			{
				var msg = "Deletion Error:";
				msg += ex.Message;
				throw new Exception(msg);
			}
		}

        protected void ButtonInsert_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Page.GetRouteUrl(CoreTableName + "EntityRoute", new { Action = "Insert" }), false);			
            }
            catch (Exception ex)
            {
                throw;
            }
        }

		protected void ButtonDetails_Click(object sender, EventArgs e)
		{
			try
			{
				var sc = new StringCollection();
				sc = GetSelectedRecordIDs();
				RetrieveRecords(sc, ActionCommand.Details);

			}
			catch (Exception ex)
			{
				var msg = "Details Error:";
				msg += ex.Message;
				throw new Exception(msg);
			}
		}

		protected void ButtonUpdate_Click(object sender, EventArgs e)
		{
			try
			{
				var sc = new StringCollection();
                var page = ButtonUpdate.CommandArgument;
				sc = GetSelectedRecordIDs();
				RetrieveRecords(sc, ActionCommand.Update);
			}
			catch (Exception ex)
			{
				var msg = "Updation Error:";
				msg += ex.Message;
				throw new Exception(msg);
			}
		}

		protected void ButtonTestData_Click(object sender, EventArgs e)
		{
			try
			{
				var sc = new StringCollection();
				sc = GetSelectedRecordIDs();
				RetrieveRecords(sc, ActionCommand.TestData);
			}
			catch (Exception ex)
			{
				var msg = "Updation Error:";
				msg += ex.Message;
				throw new Exception(msg);
			}
		}

		protected void ButtonRealData_Click(object sender, EventArgs e)
		{
			try
			{
				var sc = new StringCollection();
				sc = GetSelectedRecordIDs();
				RetrieveRecords(sc, ActionCommand.RealData);
			}
			catch (Exception ex)
			{
				var msg = "Updation Error:";
				msg += ex.Message;
				throw new Exception(msg);
			}
		}

		protected void ButtonCommonUpdate_Click(object sender, EventArgs e)
		{
			try
			{
				var sc = new StringCollection();
				sc = GetSelectedRecordIDs();
				RetrieveRecords(sc, ActionCommand.CommonUpdate);
			}
			catch (Exception ex)
			{
				var msg = "Updation Error:";
				msg += ex.Message;
				throw new Exception(msg);
			}
		}

		protected void ButtonInlineUpdate_Click(object sender, EventArgs e)
		{
			try
			{
				var sc = new StringCollection();
				sc = GetSelectedRecordIDs();
				RetrieveRecords(sc, ActionCommand.InlineUpdate);
			}
			catch (Exception ex)
			{
				var msg = "Updation Error:";
				msg += ex.Message;
				throw new Exception(msg);
			}
		}

		protected void ButtonRenumber_Click(object sender, EventArgs e)
		{
			pnlRenumber.Visible = true;
			txtSeed.Focus();
			SortGridView(String.Empty, SortDirection.Ascending.ToString());
		}

		protected void btnRenumber_Click(object sender, EventArgs e)
		{
			try
			{
				var sc = new StringCollection();
				sc = GetSelectedRecordIDs();
				RetrieveRecords(sc, ActionCommand.Renumber);
			}
			catch (Exception ex)
			{
				var msg = "Updation Error:";
				msg += ex.Message;
				throw new Exception(msg);
			}
		}

		//private void RenumberData(StringCollection sc, ActionCommand cmd)
		//{
		//	var _tableFolder = Convert.ToString(ViewState["TableFolder"]);
		//	var _tableName = Convert.ToString(ViewState["TableName"]);
		//}
	}
}