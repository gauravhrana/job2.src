using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class TxSettlementInfoDataModel : BaseModel
	{
		[PrimaryKey, IncludeInSearch]
		public int? TxSettlementInfoId { get; set; }

		[ForeignKey("TransactionEvent"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
		public int? TransactionEventId { get; set; }

		[ForeignKeyName("TransactionEvent", "TransactionEventId", "TransactionEventId", "TransactionEventId"), OnlyProperty]
		public string TransactionEvent { get; set; }


        public int? SettlementCurrencyId { get; set; }
		public int? SellCurrencyId { get; set; }

		public string TradeDateFXRate { get; set; }
		public string NetSettlementAmount { get; set; }
		public string NetSettlementCashAmount { get; set; }
		public string AccruedInterest { get; set; }
	}
}

