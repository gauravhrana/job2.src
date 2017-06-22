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
	public partial class TxOtherDataModel : BaseModel
	{
		[PrimaryKey, IncludeInSearch]
		public int? TxOtherId { get; set; }

		[ForeignKey("TransactionEvent"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
		public int? TransactionEventId { get; set; }

		[ForeignKeyName("TransactionEvent", "TransactionEventId", "TransactionEventId", "TransactionEventId"), OnlyProperty]
		public string TransactionEvent { get; set; }

		public int? FundStructureId { get; set; }
		public int? CashSourceId { get; set; }
		public int? StrategyId { get; set; }
		public int? GenericLegId { get; set; }
		public int? DistributionParentId { get; set; }
		public int? SettlementTypeId { get; set; }
	}
}

