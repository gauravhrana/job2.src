using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class ContinentDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string ContinentId = "ContinentId";

		}

		public static readonly ContinentDataModel Empty = new ContinentDataModel();

		[PrimaryKey]
		public int? ContinentId { get; set; }

	}
}
