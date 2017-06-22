using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{
	public class LayerDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string LayerId = "LayerId";
		}

		public int? LayerId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"LayerId=" + LayerId
		}
	}
}
