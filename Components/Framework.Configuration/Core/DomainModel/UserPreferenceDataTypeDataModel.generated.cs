using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Configuration
{

	[Serializable]
	public partial class UserPreferenceDataTypeDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string UserPreferenceDataTypeId = "UserPreferenceDataTypeId";
		}

		public static readonly UserPreferenceDataTypeDataModel Empty = new UserPreferenceDataTypeDataModel();

		public int? UserPreferenceDataTypeId { get; set; }

	}
}
