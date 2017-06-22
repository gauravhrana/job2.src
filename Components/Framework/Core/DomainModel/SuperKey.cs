using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    [Serializable]
    public class SuperKeyDataModel : StandardDataModel
    {
        public class DataColumns : StandardDataColumns
        {
            public const string SuperKeyId			= "SuperKeyId";
            public const string SystemEntityTypeId	= "SystemEntityTypeId";
			public const string SystemEntityType	= "SystemEntityType";
            public const string ExpirationDate		= "ExpirationDate";
        }

		public static readonly SuperKeyDataModel Empty = new SuperKeyDataModel();

        public int? SuperKeyId { get; set; }
        public int? SystemEntityTypeId { get; set; }
		public string SystemEntityType { get; set; }
        public DateTime? ExpirationDate { get; set; }

    }
}
