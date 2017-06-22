using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

    public partial class TransactionEventSellShortDataModel : BaseModel
    {
        [PrimaryKey, IncludeInSearch]
        public int? TransactionEventSellShortId { get; set; }

        public DateTime TransactionEventDate { get; set; }
        public DateTime TransactionSettleDate { get; set; }

        [ForeignKey("TransactionType"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? TransactionTypeId { get; set; }

        [ForeignKeyName("TransactionType", "TransactionTypeId", "TransactionTypeId", "Name"), OnlyProperty]
        public string TransactionType { get; set; }

        [ForeignKey("Custodian"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? CustodianId { get; set; }

        [ForeignKeyName("Custodian", "CustodianId", "CustodianId", "Name"), OnlyProperty]
        public string Custodian { get; set; }

        [ForeignKey("Strategy"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? StrategyId { get; set; }

        [ForeignKeyName("Strategy", "StrategyId", "StrategyId", "Name"), OnlyProperty]
        public string Strategy { get; set; }

        [ForeignKey("AccountSpecificType"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? AccountSpecificTypeId { get; set; }

        [ForeignKeyName("AccountSpecificType", "AccountSpecificTypeId", "AccountSpecificTypeId", "Name"), OnlyProperty]
        public string AccountSpecificType { get; set; }

        [ForeignKey("InvestmentType"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? InvestmentTypeId { get; set; }

        [ForeignKeyName("InvestmentType", "InvestmentTypeId", "InvestmentTypeId", "Name"), OnlyProperty]
        public string InvestmentType { get; set; }

        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Fees { get; set; }

    }
}

