using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{
	public partial class SecurityDataModel : StandardModel
	{

		[PrimaryKey, IncludeInSearch]
		public int? SecurityId { get; set; }

	}
}
