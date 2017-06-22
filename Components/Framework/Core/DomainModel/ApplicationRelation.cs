using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{
    [Serializable]
	public class ApplicationRelationDataModel : BaseDataModel
	{
		public class DataColumns : BaseDataColumns
		{
			public const string ApplicationRelationId       = "ApplicationRelationId";
			public const string PublisherApplicationId      = "PublisherApplicationId";
			public const string SubscriberApplicationId     = "SubscriberApplicationId";
			public const string SystemEntityTypeId          = "SystemEntityTypeId";
			public const string SubscriberApplicationRoleId = "SubscriberApplicationRoleId";

			public const string PublisherApplication        = "PublisherApplication";
			public const string SubscriberApplication       = "SubscriberApplication";
			public const string SystemEntityType            = "SystemEntityType";
			public const string SubscriberApplicationRole   = "SubscriberApplicationRole";
		}

		public static readonly ApplicationRelationDataModel Empty = new ApplicationRelationDataModel();

		public int? ApplicationRelationId		{ get; set; }
		public int? PublisherApplicationId		{ get; set; }
		public int? SubscriberApplicationId		{ get; set; }
		public int? SystemEntityTypeId			{ get; set; }
		public int? SubscriberApplicationRoleId { get; set; }

		public string PublisherApplication		{ get; set; }
		public string SubscriberApplication		{ get; set; }
		public string SystemEntityType			{ get; set; }
		public string SubscriberApplicationRole { get; set; }

	}

}
