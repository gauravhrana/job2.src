using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class ForwardCashDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string ForwardCashId = "ForwardCashId";
		}

		public static readonly ForwardCashDataModel Empty = new ForwardCashDataModel();

	}
}
