using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.ReleaseLog
{
    [Serializable]
	public class PublishXDevelopmenDataModel : BaseDataModel
	{
		public class DataColumns
		{
			public const string PublishXDevelopmentId	= "PublishXDevelopmentId";
			public const string PublishId				= "PublishId";
			public const string DevelopmentId			= "DevelopmentId";			
		}

		public static readonly PublishXDevelopmenDataModel Empty = new PublishXDevelopmenDataModel();

		public int? PublishXDevelopmentId { get; set; }
		public int? PublishId { get; set; }
		public int? DevelopmentId { get; set; }
		
	}
}
