using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{

	[Serializable]
	public partial class SubscriberApplicationRoleDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string SubscriberApplicationRoleId = "SubscriberApplicationRoleId";
		}

		public static readonly SubscriberApplicationRoleDataModel Empty = new SubscriberApplicationRoleDataModel();

		public int? SubscriberApplicationRoleId { get; set; }

	}
}
