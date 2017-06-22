using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.ReferenceDataSample
{

	public partial class ContinentDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ContinentId = "ContinentId";
		}

		public static readonly ContinentDataModel Empty = new ContinentDataModel();

		public int? ContinentId { get; set; }

	}
}
