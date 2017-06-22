using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Audit
{

	[Serializable]
	public partial class AuditActionDataModel : StandardDataModel
	{

		public class DataColumns : StandardDataColumns
		{
			public const string AuditActionId = "AuditActionId";
		}

		public static readonly AuditActionDataModel Empty = new AuditActionDataModel();

		public int? AuditActionId { get; set; }

	}
}
