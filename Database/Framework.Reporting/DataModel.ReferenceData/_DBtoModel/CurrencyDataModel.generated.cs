using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.ReferenceDataSample
{

	public partial class CurrencyDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string CurrencyId = "CurrencyId";
		}

		public static readonly CurrencyDataModel Empty = new CurrencyDataModel();

		public int? CurrencyId { get; set; }

	}
}
