﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
	public class WorkTicketDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string EntityId = "EntityId";
			public const string WorkTicket = "WorkTicket";
			public const string WorkTicketId = "WorkTicketId";
			public const string Application = "Application";
			public const string ApplicationId = "ApplicationId";
			

		
		}
		public static readonly WorkTicketDataModel Empty = new WorkTicketDataModel();

		public int? EntityId { get; set; }
		public int? WorkTicket { get; set; }
		public int? WorkTicketId { get; set; }
		public string ApplicationId { get; set; }
		public string Application { get; set; }
		
	}
}
