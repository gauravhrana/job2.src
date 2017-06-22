﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class AgentBankDataModel : StandardModel
	{
		[PrimaryKey, IncludeInSearch]
		public int? AgentBankId  { get; set; }
		public string Url        { get; set; }
		public string Code       { get; set; }
	}
}
