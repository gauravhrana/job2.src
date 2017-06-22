using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shared.WebCommon.UI.Web
{
	public class UIHelper
	{
		public static void PopulateLabelsText(Dictionary<string, string> columns, int systemEntityTypeId, int auditId, List<Label> labelslist)
		{
			if(columns == null)
			{
				columns = GetLabelDictonaryObject(systemEntityTypeId, auditId);
			}
		

				foreach (Label label in labelslist )
					SetLabelText(columns, label);
			return;
		}

		private static void SetLabelText(Dictionary<string, string> dictionary, Label label)
		{
			try
			{
				label.Text = dictionary[label.Text] + ":";
			}
			catch (Exception exception)
			{
				Framework.Components.DataAccess.Audit.LogError(exception.Message, exception);
			}

		}

		public static string[] GetGridViewColumns(int systemEntityTypeId,  int auditId)
		{
			var obj = new Framework.Components.UserPreference.ApplicationEntityFieldLabel.Data();
			obj.SystemEntityTypeId = systemEntityTypeId;
			var odt = Framework.Components.UserPreference.ApplicationEntityFieldLabel.GetGridViewColumns(obj, auditId);
			var validColumns = new string[odt.Rows.Count];
			for (int i = 0; i < odt.Rows.Count; i++)
			{
				validColumns[i] = odt.Rows[i][Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.Name].ToString();
			}
			return validColumns;
		}

		public static string[] GetDetailsViewColumns(int systemEntityTypeId, int auditId)
		{
			var obj = new Framework.Components.UserPreference.ApplicationEntityFieldLabel.Data();
			obj.SystemEntityTypeId = systemEntityTypeId;
			var odt = Framework.Components.UserPreference.ApplicationEntityFieldLabel.GetDetailsViewColumns(obj, auditId);
			var validColumns = new string[odt.Rows.Count];
			for (int i = 0; i < odt.Rows.Count; i++)
			{
				validColumns[i] = odt.Rows[i][Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.Name].ToString();
			}
			return validColumns;
		}

		public static Dictionary<string, string> GetLabelDictonaryObject(int systemEntityTypeId, int auditId)
		{
			var validColumns = new Dictionary<string, string>();
			var obj = new Framework.Components.UserPreference.ApplicationEntityFieldLabel.Data();
			obj.SystemEntityTypeId = systemEntityTypeId;
			var odt = Framework.Components.UserPreference.ApplicationEntityFieldLabel.GetDetailsViewColumns(obj, auditId);
			for (var i = 0; i < odt.Rows.Count; i++)
			{
				var key = odt.Rows[i][Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.Name].ToString();
				var value =
					odt.Rows[i][Framework.Components.UserPreference.ApplicationEntityFieldLabel.DataColumns.Value].ToString();
				validColumns.Add(key, value);
			}
			return validColumns;
		}

	}
}