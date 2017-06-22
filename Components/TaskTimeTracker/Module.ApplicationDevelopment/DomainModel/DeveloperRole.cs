using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.TaskTimeTracker.ApplicationDevelopment
{

    [Serializable]
	public class DeveloperRoleDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string DeveloperRoleId = "DeveloperRoleId";
            public const string Application     = "Application";
                
		}

		public static readonly DeveloperRoleDataModel Empty = new DeveloperRoleDataModel();

		public int? DeveloperRoleId { get; set; }
        public string Application { get; set; }
	}
}
