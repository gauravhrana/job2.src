using Framework.Components.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.CapitalMarkets.Parties
{
    public partial class ExBuyTransactionDataModel : BaseModel
    {
        [PrimaryKey, IncludeInSearch]
        public int? ExBuyTransactionId { get; set; }
    }
}
