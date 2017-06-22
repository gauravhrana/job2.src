using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class MallDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string MallId = "MallId";

		}

		public static readonly MallDataModel Empty = new MallDataModel();

		[PrimaryKey]
		public int? MallId { get; set; }

	}
}
