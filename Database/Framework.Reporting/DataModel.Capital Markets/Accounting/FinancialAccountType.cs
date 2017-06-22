using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

    public partial class FinancialAccountTypeDataModel : StandardModel
    {

        public class DataColumns : StandardColumns
        {
            public const string FinancialAccountTypeId = "FinancialAccountTypeId"; 
        }

        public static readonly FinancialAccountTypeDataModel Empty = new FinancialAccountTypeDataModel();
        [PrimaryKey, IncludeInSearch]
        public int? FinancialAccountTypeId { get; set; }

    }
}
