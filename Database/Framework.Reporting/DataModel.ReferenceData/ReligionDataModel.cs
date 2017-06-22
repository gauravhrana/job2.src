using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class ReligionDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string ReligionId = "ReligionId";

		}

		public static readonly ReligionDataModel Empty = new ReligionDataModel();

		[PrimaryKey]
		public int? ReligionId { get; set; }

	}
}
