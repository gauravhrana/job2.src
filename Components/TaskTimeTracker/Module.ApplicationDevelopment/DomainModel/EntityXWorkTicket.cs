using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
	public class EntityXWorkTicketDataModel : BaseDataModel
	{
		public class DataColumns : BaseDataColumns
		{
			public const string EntityXWorkTicketId = "EntityXWorkTicketId";
			public const string Entity = "Entity";
			public const string EntityId = "EntityId";
			public const string WorkTicket = "WorkTicket";
			public const string WorkTicketId = "WorkTicketId";
			public const string AcknowledgedBy = "AcknowledgedById";
		    public const string KnowledgeDate =	"KnowledgeDate";
			public const string Memo = "Memo";
			


		}

		public static readonly EntityXWorkTicketDataModel Empty = new EntityXWorkTicketDataModel();

		public int? EntityXWorkTicketId { get; set; }
		public int? EntityId { get; set; }
		public int? WorkTicketId { get; set; }


		public string Entity { get; set; }

		public string WorkTicket { get; set; }

		public string AcknowledgedBy { get; set; }

		public string Memo { get; set; }

		public DateTime? KnowledgeDate { get; set; }
	}
}
