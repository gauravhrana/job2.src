using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class ManagerDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string ManagerId = "ManagerId";
		}

		public static readonly ManagerDataModel Empty = new ManagerDataModel();

	}
}
