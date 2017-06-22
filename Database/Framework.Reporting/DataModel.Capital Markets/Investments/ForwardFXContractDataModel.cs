using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	public partial class ForwardFXContractDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string ForwardFXContractId = "ForwardFXContractId";
		}

		public static readonly ForwardFXContractDataModel Empty = new ForwardFXContractDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? ForwardFXContractId { get; set; }

	}
}
