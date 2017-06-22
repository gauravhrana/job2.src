using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference.DomainModel
{
	public class ApplicationMode : StandardModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string ApplicationModeId = "ApplicationModeId";
		}

		public int? ApplicationModeId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty;
		}
	}
}
