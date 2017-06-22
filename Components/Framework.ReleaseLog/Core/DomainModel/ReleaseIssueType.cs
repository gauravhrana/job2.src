using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.ReleaseLog
{
    [Serializable]
	public class ReleaseIssueTypeDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string ReleaseIssueTypeId	= "ReleaseIssueTypeId";			
			public const string Application			= "Application";
		}

		public static readonly ReleaseIssueTypeDataModel Empty = new ReleaseIssueTypeDataModel();
		
		public int? ReleaseIssueTypeId { get; set; }		
		public string Application { get; set; }

	}
}


