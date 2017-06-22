﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class TraderDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
            public const string TraderId = "TraderId";

            public const string FundId = "FundId";
            public const string Fund = "Fund";
		}

		public static readonly TraderDataModel Empty = new TraderDataModel();

		[PrimaryKey, IncludeInSearch]
        public int? TraderId { get; set; }

        [ForeignKey("Fund"), IncludeInSearch, Newtonsoft.Json.JsonConverter(typeof(NullableIntConverter)), IncludeInUnique]
        public int? FundId { get; set; }

        [ForeignKeyName("Fund", "FundId", "FundId", "Name"), OnlyProperty]
        public string Fund { get; set; }

	}
}
