using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
    public partial class FinancialAccountClassDataModel : StandardModel
    {

        public class DataColumns : StandardColumns 
        {
            public const string FinancialAccountClassId = "FinancialAccountClassId";
        }

        public static readonly FinancialAccountClassDataModel Empty = new FinancialAccountClassDataModel();
        [PrimaryKey]
        public int? FinancialAccountClassId { get; set; }

    }
}
