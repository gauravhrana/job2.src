using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class ManagementFirmDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string ManagementFirmId = "ManagementFirmId";
		}

		public static readonly ManagementFirmDataModel Empty = new ManagementFirmDataModel();
		[PrimaryKey, IncludeInSearch]
		public int? ManagementFirmId { get; set; }

	}
}
