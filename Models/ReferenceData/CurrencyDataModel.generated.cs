using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class CurrencyDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string CurrencyId = "CurrencyId";
		}

		public static readonly CurrencyDataModel Empty = new CurrencyDataModel();

	}
}
