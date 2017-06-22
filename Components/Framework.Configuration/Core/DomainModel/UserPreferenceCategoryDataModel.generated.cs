using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Configuration
{

	[Serializable]
	public partial class UserPreferenceCategoryDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string UserPreferenceCategoryId = "UserPreferenceCategoryId";
		}

		public static readonly UserPreferenceCategoryDataModel Empty = new UserPreferenceCategoryDataModel();

		public int? UserPreferenceCategoryId { get; set; }

	}
}
