using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Components.DataAccess
{
	public class StandardListDataModel
	{
		public class DataColumns
		{
			public const string Name  = "Name";
			public const string Value = "Value";
		}

		public string		Name			{ get; set; }
		public string		Value			{ get; set; }
	}
}
