using Framework.Components.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.CapitalMarkets.Parties
{
    public partial class ExSellTransactionDataModel : BaseModel
    {
		[PrimaryKey, IncludeInSearch]
        public int? ExSellTransactionId { get; set; }
    }
}
