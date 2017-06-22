using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.CapitalMarkets
{

	[Serializable]
	public partial class AnnotationsDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string AnnotationsId = "AnnotationsId";
		}

		public static readonly AnnotationsDataModel Empty = new AnnotationsDataModel();

	}
}
