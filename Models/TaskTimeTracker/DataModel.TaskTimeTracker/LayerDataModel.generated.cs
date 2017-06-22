using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{

	[Serializable]
	public partial class LayerDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string LayerId = "LayerId";
		}

		public static readonly LayerDataModel Empty = new LayerDataModel();

		public int? LayerId { get; set; }

	}
}
