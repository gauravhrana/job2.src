using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class AccidentPlaceDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string AccidentPlaceId = "AccidentPlaceId";
		}

		public static readonly AccidentPlaceDataModel Empty = new AccidentPlaceDataModel();

	}
}
