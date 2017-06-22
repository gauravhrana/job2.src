using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;
using Framework.Components.DataAccess.DataModel;
using Newtonsoft.Json;

namespace DataModel.CapitalMarkets
{
	public partial class TxTradeAndSettleDatesDataModel : BaseAuditDataModel
	{
		[PrimaryKey, IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter))]
		public int? TxTradeAndSettleDatesId { get; set; }

		[ForeignKey("TransactionEvent"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
		public int? TransactionEventId { get; set; }

		[ForeignKeyName("TransactionEvent", "TransactionEventId", "TransactionEventId", "TransactionEventId"), OnlyProperty]
		public string TransactionEvent { get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableDateConverter)), DateRange("FromSearchTradeDate", "ToSearchTradeDate")]
		public DateTime? TradeDate { get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableDateConverter)), DateRange("FromSearchContractualDate", "ToSearchContractualDate")]
		public DateTime? ContractualDate { get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableDateConverter)), DateRange("FromSearchActualDate", "ToSearchActualDate")]
		public DateTime? ActualDate { get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableDateConverter))]
		public DateTime? SpotDate { get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableDateConverter))]
		public DateTime? SettlementDate { get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableDateConverter)), OnlyProperty]
		public DateTime? FromSearchTradeDate { get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), OnlyProperty]
		public DateTime? ToSearchTradeDate { get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableDateConverter)), OnlyProperty]
		public DateTime? FromSearchActualDate { get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), OnlyProperty]
		public DateTime? ToSearchActualDate { get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableDateConverter)), OnlyProperty]
		public DateTime? FromSearchContractualDate { get; set; }

		[Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), OnlyProperty]
		public DateTime? ToSearchContractualDate { get; set; }


	}


}
