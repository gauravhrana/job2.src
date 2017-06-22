using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess;

namespace DataModel.ReferenceData
{

	public partial class StateDataModel : StandardModel
	{

		public class DataColumns : StandardColumns
		{
			public const string StateId = "StateId";

		}

		public static readonly StateDataModel Empty = new StateDataModel();

		[PrimaryKey]
		public int? StateId { get; set; }

	}
}
