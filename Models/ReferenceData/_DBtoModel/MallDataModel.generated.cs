using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.ReferenceDataSample
{

	public partial class MallDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string MallId = "MallId";
		}

		public static readonly MallDataModel Empty = new MallDataModel();

		public int? MallId { get; set; }

	}
}
