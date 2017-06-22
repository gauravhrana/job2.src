using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference.DomainModel
{
	public class FieldConfigurationMode : StandardModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string FieldConfigurationModeId = "FieldConfigurationModeId";
		}

		public int? FieldConfigurationModeId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty;
		}
	}
}
