using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.ReferenceDataSample
{

	public partial class StateDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string StateId = "StateId";
		}

		public static readonly StateDataModel Empty = new StateDataModel();

		public int? StateId { get; set; }

	}
}
