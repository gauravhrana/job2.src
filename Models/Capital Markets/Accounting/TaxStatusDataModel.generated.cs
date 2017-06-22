using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class TaxStatusDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string TaxStatusId = "TaxStatusId";
		}

		public static readonly TaxStatusDataModel Empty = new TaxStatusDataModel();

	}
}
