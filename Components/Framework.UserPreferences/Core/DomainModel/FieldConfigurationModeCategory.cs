using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Components.DataAccess.DomainModel;

namespace Framework.Components.UserPreference.DomainModel
{
	public class FieldConfigurationModeCategory : StandardModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string FieldConfigurationModeCategoryId = "FieldConfigurationModeCategoryId";
		}

		public int? FieldConfigurationModeCategoryId { get; set; }

		public string ToURLQuery()
		{
			return String.Empty;
		}

	}
}
