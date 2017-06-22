using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    public class SystemEntityTypeDataModel : BaseDataModel
    {

        public class DataColumns : BaseDataColumns
        {
            public const string SystemEntityTypeId	= "SystemEntityTypeId";
            public const string EntityName			= "EntityName";
            public const string EntityDescription	= "EntityDescription";
            public const string NextValue			= "NextValue";
            public const string IncreaseBy			= "IncreaseBy";			
			public const string PrimaryDatabase		= "PrimaryDatabase";
        }

		public static readonly SystemEntityTypeDataModel Empty = new SystemEntityTypeDataModel();

        public int?     SystemEntityTypeId  { get; set; }
        public string   EntityName          { get; set; }
		public string   PrimaryDatabase		{ get; set; }
        public string   EntityDescription   { get; set; }		
        public int?     NextValue           { get; set; }
        public int?     IncreaseBy          { get; set; }

    }
}
