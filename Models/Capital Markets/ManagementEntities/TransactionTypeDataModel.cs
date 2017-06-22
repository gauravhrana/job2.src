using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

    public partial class TransactionTypeDataModel : BaseModel
    {
		       
        [PrimaryKey, IncludeInSearch]
        public int? TransactionTypeId { get; set; }

        [IncludeInSearch, IncludeInUnique]
        public string Name { get; set; }
        public string Description { get; set; }
        public int? SortOrder { get; set; }

        [IncludeInSearch]
        public string Code { get; set; }

    }
}
