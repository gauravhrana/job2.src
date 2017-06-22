using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataModel.TaskTimeTracker
{
    [Serializable]
    [Table("ClientXProject")]
	public class ClientXProjectDataModel : BaseDataModel
	{
        public class DataColumns
        {
            public const string ClientId = "ClientId";
            public const string ClientXProjectId = "ClientXProjectId";
            public const string ProjectId = "ProjectId";

            public const string Client = "Client";
            public const string Project = "Project";
        }

        public static readonly ClientXProjectDataModel Empty = new ClientXProjectDataModel();

        [Key]
		public int? ClientXProjectId { get; set; }
		public int? ClientId         { get; set; }
		public int? ProjectId        { get; set; }

		public string Client         { get; set; }
		public string Project        { get; set; }

	}
}


