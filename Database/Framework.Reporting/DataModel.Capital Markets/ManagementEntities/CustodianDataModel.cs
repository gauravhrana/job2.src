﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class CustodianDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string CustodianId = "CustodianId";
			public const string Url = "Url";
			public const string Code = "Code";
			
		}

		public static readonly CustodianDataModel Empty = new CustodianDataModel();
		[PrimaryKey, IncludeInSearch]
		public int?     CustodianId     { get; set; }
		public string   Url             { get; set; }
		public string   Code            { get; set; }

		
	}
}
