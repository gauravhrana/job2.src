using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class MallDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string MallId = "MallId";
		}

		public static readonly MallDataModel Empty = new MallDataModel();

	}
}
