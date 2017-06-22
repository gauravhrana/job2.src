﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class BrokerDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string BrokerId = "BrokerId";
			public const string Url = "Url";
			public const string Code = "Code";
		}		

		public static readonly BrokerDataModel Empty = new BrokerDataModel();

		[PrimaryKey, IncludeInSearch]
		public int? BrokerId { get; set; }
		public string Url { get; set; }
		public string Code { get; set; }
		
	

	}
}