using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class TuitionDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string TuitionId = "TuitionId";
		}

		public static readonly TuitionDataModel Empty = new TuitionDataModel();

	}
}
