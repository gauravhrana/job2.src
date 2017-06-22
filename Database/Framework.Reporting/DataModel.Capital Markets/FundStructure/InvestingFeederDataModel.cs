using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

    public partial class InvestingFeederDataModel : StandardModel
    {

        public class DataColumns : StandardColumns
        {
            public const string InvestingFeederId = "InvestingFeederId";
        }

        public static readonly InvestingFeederDataModel Empty = new InvestingFeederDataModel();
        [PrimaryKey, IncludeInSearch]
        public int? InvestingFeederId { get; set; }

    }
}
