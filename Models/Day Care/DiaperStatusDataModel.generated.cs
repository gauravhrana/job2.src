using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	public partial class DiaperStatusDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string DiaperStatusId = "DiaperStatusId";
		}

		public static readonly DiaperStatusDataModel Empty = new DiaperStatusDataModel();

	}
}
