using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.ReleaseLog
{
    [Serializable]
	public class ReleaseLogDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string ReleaseLogId		= "ReleaseLogId";
			public const string ReleaseLogStatusId	= "ReleaseLogStatusId";
			public const string Application			= "Application";
			public const string ReleaseLogStatus	= "ReleaseLogStatus";			
			public const string VersionNo			= "VersionNo";
			public const string ReleaseDate			= "ReleaseDate";			
			public const string ReleaseDateMin		= "ReleaseDateMin";
			public const string ReleaseDateMax		= "ReleaseDateMax";
		}

		public static readonly ReleaseLogDataModel Empty = new ReleaseLogDataModel();

		public int? ReleaseLogId		{ get; set; }
		public int? ReleaseLogStatusId	{ get; set; }
		public string Application		{ get; set; }
		public string ReleaseLogStatus	{ get; set; }
		public string VersionNo			{ get; set; }
		public DateTime? ReleaseDate	{ get; set; }		
		public DateTime? ReleaseDateMin { get; set; }
		public DateTime? ReleaseDateMax { get; set; }

	}
}


