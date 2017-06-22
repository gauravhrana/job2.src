using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Configuration
{

	[Serializable]
	public partial class FieldConfigurationModeCategoryDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string FieldConfigurationModeCategoryId = "FieldConfigurationModeCategoryId";
		}

		public static readonly FieldConfigurationModeCategoryDataModel Empty = new FieldConfigurationModeCategoryDataModel();

		public int? FieldConfigurationModeCategoryId { get; set; }

	}
}
