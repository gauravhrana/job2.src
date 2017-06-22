using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{

	[Serializable]
	public partial class SystemForeignRelationshipDatabaseDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string SystemForeignRelationshipDatabaseId = "SystemForeignRelationshipDatabaseId";
		}

		public static readonly SystemForeignRelationshipDatabaseDataModel Empty = new SystemForeignRelationshipDatabaseDataModel();

		public int? SystemForeignRelationshipDatabaseId { get; set; }

	}
}
