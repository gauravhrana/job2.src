using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.ReferenceDataSample
{

	public partial class MonumentDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string MonumentId = "MonumentId";
		}

		public static readonly MonumentDataModel Empty = new MonumentDataModel();

		public int? MonumentId { get; set; }

	}
}
