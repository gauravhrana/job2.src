using System;
using System.Collections.Generic;

namespace Generators.SQL
{
	public class Helper
	{
		public static string GetName(Type pType)
		{
			var name = pType.Name;

			name = name.Replace("DataModel", string.Empty);

			return name;
		}

		public static string GetColumnList(Type pType, string options = "", string layout = "Horizontal")
		{
			var list = new List<string>();

			var prefix = string.Empty;

			switch (options)
			{
				case "Parameters":
					prefix = "@";
					break;
			}

			foreach (var property in pType.GetProperties())
			{
				list.Add(prefix + property.Name);
			}

			return string.Join(", ", list);

		}
	}
}
