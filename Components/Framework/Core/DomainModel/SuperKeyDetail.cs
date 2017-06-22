using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    [Serializable]
    public class SuperKeyDetailDataModel : BaseDataModel
    {

        public class DataColumns : BaseDataColumns
        {
            public const string SuperKeyDetailId   = "SuperKeyDetailId";
            public const string SuperKeyId         = "SuperKeyId";
            public const string EntityKey          = "EntityKey";
            public const string SuperKey           = "SuperKey";
            public const string SystemEntityTypeId = "SystemEntityTypeId"; 
        }

		public static readonly SuperKeyDetailDataModel Empty = new SuperKeyDetailDataModel();

        public int? SuperKeyDetailId	 { get; set; }
		public int? SuperKeyId			 { get; set; }
		public int? EntityKey			 { get; set; }
		public int? SuperKey			 { get; set; }
		public int? SystemEntityTypeId	 { get; set; }

    }
}
