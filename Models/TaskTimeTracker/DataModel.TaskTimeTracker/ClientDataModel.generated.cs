using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker
{

	[Serializable]
	public partial class ClientDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ClientId = "ClientId";
		}

		public static readonly ClientDataModel Empty = new ClientDataModel();

		public int? ClientId { get; set; }

	}
}
