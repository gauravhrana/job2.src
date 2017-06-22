using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Shared.UI.Web.Controls
{
	public class ListHelperSort
	{
		public static DataTable SortDataTable(DataTable dt, string sort, string sortdirection)
		{
			var newDt = dt.Clone();
			var rowCount = dt.Rows.Count;

			var sortstring = sort + " " + sortdirection;
			var foundRows = dt.Select(null, sortstring);

			// Sort with Column name 
			for (var i = 0; i < rowCount; i++)
			{
				var arr = new object[dt.Columns.Count];

				for (var j = 0; j < dt.Columns.Count; j++)
				{
					arr[j] = foundRows[i][j];
				}

				var dataRow = newDt.NewRow();

				dataRow.ItemArray = arr;
				newDt.Rows.Add(dataRow);
			}

			//clear the incoming dt 
			dt.Rows.Clear();

			for (var i = 0; i < newDt.Rows.Count; i++)
			{
				var arr = new object[dt.Columns.Count];

				for (var j = 0; j < dt.Columns.Count; j++)
				{
					arr[j] = newDt.Rows[i][j];
				}

				var dataRow = dt.NewRow();
				dataRow.ItemArray = arr;
				dt.Rows.Add(dataRow);
			}


			return dt;
		}
	}
}