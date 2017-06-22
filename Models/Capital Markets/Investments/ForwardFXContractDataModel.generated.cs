using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class ForwardFXContractDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string ForwardFXContractId = "ForwardFXContractId";
		}

		public static readonly ForwardFXContractDataModel Empty = new ForwardFXContractDataModel();

	}
}
