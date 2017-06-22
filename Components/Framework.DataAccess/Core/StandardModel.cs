using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Components.DataAccess
{
    [Serializable]
	public class StandardModel : BaseModel
	{
        public class StandardColumns : BaseColumns
		{
			public const string Name			= "Name";
			public const string Description  = "Description";
			public const string SortOrder		= "SortOrder";
		}


        [IncludeInSearch, IncludeInUnique]
		public string Name					{ get; set; }
		public string Description			{ get; set; }
		public int? SortOrder				{ get; set; }	
	}
}
