using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.ReleaseLog
{
    [Serializable]
	public class ReleaseLogStatusDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string ReleaseLogStatusId	= "ReleaseLogStatusId";			
			public const string Application			= "Application";
		}

		public static readonly ReleaseLogStatusDataModel Empty = new ReleaseLogStatusDataModel();

		public int? ReleaseLogStatusId { get; set; }		
		public string Application { get; set; }

	}
}


