using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Configuration
{

	[Serializable]
	public partial class FieldConfigurationModeAccessModeDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string FieldConfigurationModeAccessModeId = "FieldConfigurationModeAccessModeId";
		}

		public static readonly FieldConfigurationModeAccessModeDataModel Empty = new FieldConfigurationModeAccessModeDataModel();

		public int? FieldConfigurationModeAccessModeId { get; set; }

	}
}
