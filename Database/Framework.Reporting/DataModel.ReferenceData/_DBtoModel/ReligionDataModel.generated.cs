using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.ReferenceDataSample
{

	public partial class ReligionDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ReligionId = "ReligionId";
		}

		public static readonly ReligionDataModel Empty = new ReligionDataModel();

		public int? ReligionId { get; set; }

	}
}
