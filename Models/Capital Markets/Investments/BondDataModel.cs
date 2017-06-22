using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;


namespace DataModel.CapitalMarkets
{

	public partial class BondDataModel : StandardModel
	{

		[PrimaryKey,IncludeInSearch]
		public int? BondId { get; set; }

	}
}
