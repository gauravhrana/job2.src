using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class CurrencyDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string CurrencyId = "CurrencyId";

		}

		public static readonly CurrencyDataModel Empty = new CurrencyDataModel();

		[PrimaryKey]
		public int? CurrencyId { get; set; }

	}
}
