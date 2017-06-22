using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;


namespace DataModel.TaskTimeTracker
{
	public class ClientXProjecDataModel : BaseDataModel
	{
		public class DataColumns 
		{
			public const string ClientId			= "ClientId";
			public const string ClientXProjectId	= "ClientXProjectId";			
			public const string ProjectId           = "ProjectId";

			public const string Client              = "Client";
			public const string Project             = "Project";
		}

		public int? ClientXProjectId { get; set; }
		public int? ClientId         { get; set; }
		public int? ProjectId        { get; set; }

		public string Client         { get; set; }
		public string Project        { get; set; }

		public string ToURLQuery()
		{
			return String.Empty; //"ClientId=" + ClientId
		}
	}
}


