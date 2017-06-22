using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    [Serializable]
    public class SystemEntityXSystemEntityCategoryDataModel : BaseDataModel
    {

        public class DataColumns : BaseDataColumns
        {
            public const string SystemEntityXSystemEntityCategoryId = "SystemEntityXSystemEntityCategoryId";
            public const string SystemEntityId                      = "SystemEntityId";
            public const string SystemEntityCategoryId              = "SystemEntityCategoryId";
            public const string SystemEntity                        = "SystemEntity";
            public const string SystemEntityCategory                = "SystemEntityCategory";
        }

		public static readonly SystemEntityXSystemEntityCategoryDataModel Empty = new SystemEntityXSystemEntityCategoryDataModel();

        public int?     SystemEntityXSystemEntityCategoryId { get; set; }
        public int?     SystemEntityId                      { get; set; }
        public int?     SystemEntityCategoryId              { get; set; }
        public string   SystemEntity                        { get; set; }
        public string   SystemEntityCategory                { get; set; }

    }
}
