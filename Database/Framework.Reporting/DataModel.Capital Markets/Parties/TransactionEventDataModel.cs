using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
    [Table("Position")]
    public class TransactionEventDataModel : BaseModel
    {
        public class DataColumns
        {
            public const string TransactionEventId       = "TransactionEventId";
            public const string TransactionEventDate     = "TransactionEventDate";
            public const string TransactionSettleDate    = "TransactionSettleDate";
            public const string TransactionTypeCode      = "TransactionTypeCode";
            public const string CustodianCode            = "CustodianCode";
            public const string StrategyCode             = "StrategyCode";
            public const string AccountCode              = "AccountCode"; 
            public const string InvestmentCode           = "InvestmentCode";
            public const string Quantity                 = "Quantity";
            public const string Price                    = "Price";
            public const string Fees                     = "Fees";

        }

        public static readonly TransactionEventDataModel Empty = new TransactionEventDataModel();

        [PrimaryKey, IncludeInSearch]

        public int? TransactionEventId { get; set; }

        public DateTime?     TransactionEventDate    { get; set; }
        public DateTime?     TransactionSettleDate   { get; set; }
        public string        TransactionTypeCode     { get; set; }
        public string        CustodianCode           { get; set; }
        public string        StrategyCode            { get; set; }
        public string        AccountCode             { get; set; }
        public string        InvestmentCode          { get; set; }
        public int           Quantity                { get; set; }
        public int           Price                   { get; set; }
        public int           Fees                    { get; set; }

    }
}

