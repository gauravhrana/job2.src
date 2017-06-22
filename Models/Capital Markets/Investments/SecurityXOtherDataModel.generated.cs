using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class SecurityXOtherDataModel
	{

		public partial class DataColumns : BaseColumns
		{
			public const string SecurityXOtherId = "SecurityXOtherId";
			public const string SourcedFromThomsonReuters = "SourcedFromThomsonReuters";
			public const string WhenIssued = "WhenIssued";
			public const string SecurityId = "SecurityId";
			public const string Security = "Security";
		}

		public static readonly SecurityXOtherDataModel Empty = new SecurityXOtherDataModel();

	}
}
