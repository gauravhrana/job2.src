using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Components.DataAccess
{
	public class TestDataModel : BaseModel
	{
		public class TestColumns : BaseColumns
		{
			public const string Code = "Code";
			public const string URL = "URL";
		
		}

		public string		Code	{ get; set; }
		public string		URL		{ get; set; }
	
	}
}
