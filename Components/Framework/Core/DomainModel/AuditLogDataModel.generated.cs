using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Core
{

	[Serializable]
	public partial class AuditLogDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string AuditLogId = "AuditLogId";
		}

		public static readonly AuditLogDataModel Empty = new AuditLogDataModel();

		public int? AuditLogId { get; set; }

	}
}
