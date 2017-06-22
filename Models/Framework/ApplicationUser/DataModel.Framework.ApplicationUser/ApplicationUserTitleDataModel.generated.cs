using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.AuthenticationAndAuthorization
{

	[Serializable]
	public partial class ApplicationUserTitleDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string ApplicationUserTitleId = "ApplicationUserTitleId";
		}

		public static readonly ApplicationUserTitleDataModel Empty = new ApplicationUserTitleDataModel();

		public int? ApplicationUserTitleId { get; set; }

	}
}
