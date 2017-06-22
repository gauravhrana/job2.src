using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.DayCare
{
	public partial class BathRoomDataModel : StandardModel
	{
		public class DataColumns : StandardColumns
		{
			public const string BathRoomId = "BathRoomId";

		}

		public static readonly BathRoomDataModel Empty = new BathRoomDataModel();

		[PrimaryKey]
		public int? BathRoomId { get; set; }
	}
}
