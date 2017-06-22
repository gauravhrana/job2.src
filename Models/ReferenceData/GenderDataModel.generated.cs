using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	[Serializable]
	public partial class GenderDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string GenderId = "GenderId";
		}

		public static readonly GenderDataModel Empty = new GenderDataModel();

	}
}
