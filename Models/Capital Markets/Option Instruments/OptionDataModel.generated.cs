using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class OptionDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string OptionId = "OptionId";
		}

		public static readonly OptionDataModel Empty = new OptionDataModel();

	}
}
