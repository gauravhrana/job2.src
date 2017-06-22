using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper
{
	public static class SqlMapperUtil
	{
		public static DataTable ToDataTable<T>(this IList<T> list)
		{
			var props = TypeDescriptor.GetProperties(typeof(T));
			var table = new DataTable();
			for (var i = 0; i < props.Count; i++)
			{
				var prop = props[i];
				table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
			}

			var values = new object[props.Count];

			foreach (var item in list)
			{
				for (var i = 0; i < values.Length; i++)
					values[i] = props[i].GetValue(item) ?? DBNull.Value;

				table.Rows.Add(values);
			}

			return table;
		}

	}

}
