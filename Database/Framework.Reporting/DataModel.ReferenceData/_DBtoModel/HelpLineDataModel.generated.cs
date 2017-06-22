using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.ReferenceDataSample
{

	public partial class HelpLineDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string HelpLineId = "HelpLineId";
		}

		public static readonly HelpLineDataModel Empty = new HelpLineDataModel();

		public int? HelpLineId { get; set; }

	}
}
