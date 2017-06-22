using Framework.Components.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.CapitalMarkets
{
    public partial class ExShortTransactionDataModel : StandardModel
    {
        [PrimaryKey, IncludeInSearch]
        public int? ExShortTransactionId { get; set; }
    }
}
