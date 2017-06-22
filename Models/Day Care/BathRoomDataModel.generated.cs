using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{

	[Serializable]
	public partial class BathRoomDataModel
	{

		public partial class DataColumns : StandardColumns
		{
			public const string BathRoomId = "BathRoomId";
		}

		public static readonly BathRoomDataModel Empty = new BathRoomDataModel();

	}
}
