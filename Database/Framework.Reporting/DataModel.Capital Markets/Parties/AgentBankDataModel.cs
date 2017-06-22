using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class AgentBankDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string AgentBankId = "AgentBankId";
			public const string Url = "Url";
			public const string Code = "Code";
		}

		public static readonly AgentBankDataModel Empty = new AgentBankDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? AgentBankId  { get; set; }
		public string Url        { get; set; }
		public string Code       { get; set; }
	}
}
