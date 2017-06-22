using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class ClassDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string ClassId = "ClassId";
			public const string FundId = "FundId";
			public const string Fund = "Fund";
		}

		public static readonly ClassDataModel Empty = new ClassDataModel();

	}
}
