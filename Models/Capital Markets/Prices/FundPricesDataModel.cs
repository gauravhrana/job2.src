﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class FundPricesDataModel : StandardModel
	{
        [PrimaryKey, IncludeInSearch]
		public int? FundPricesId { get; set; }

	}
}