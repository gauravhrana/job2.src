using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Configuration
{

	[Serializable]
	public partial class ApplicationModeDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ApplicationModeId = "ApplicationModeId";
		}

		public static readonly ApplicationModeDataModel Empty = new ApplicationModeDataModel();

		public int? ApplicationModeId { get; set; }

	}
}
