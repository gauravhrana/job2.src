﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class ExchangeDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string ExchangeId = "ExchangeId";
			public const string Url = "Url";
			public const string Code = "Code";
		}

		public static readonly ExchangeDataModel Empty = new ExchangeDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? ExchangeId  { get; set; }
		public string Url       { get; set; }
		public string Code      { get; set; }
	}
}