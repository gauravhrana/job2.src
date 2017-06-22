using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

    public partial class AllocationGroupDataModel : StandardModel
    {

        public class DataColumns : StandardColumns
        {
            public const string AllocationGroupId = "AllocationGroupId";
        }

        public static readonly AllocationGroupDataModel Empty = new AllocationGroupDataModel();
        [PrimaryKey, IncludeInSearch]
        public int? AllocationGroupId { get; set; }

    }
}
