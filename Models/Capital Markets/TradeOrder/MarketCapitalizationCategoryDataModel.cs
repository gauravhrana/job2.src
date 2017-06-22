using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
    public partial class MarketCapitalizationCategoryDataModel : StandardModel
    {
 
        [PrimaryKey, IncludeInSearch]
        public int? MarketCapitalizationCategoryId { get; set; }

    }
}
