using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class SleepDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string SleepId = "SleepId";
		}

		public static readonly SleepDataModel Empty = new SleepDataModel();

	}
}