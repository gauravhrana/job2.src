using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.ReleaseLog.DomainModel
{
    [Serializable]
	public class ReleaseFeatureDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string ReleaseFeatureId		= "ReleaseFeatureId";
			public const string DateCreated				= "DateCreated";
			public const string DateModified			= "DateModified";			
		}

		public static readonly ReleaseFeatureDataModel Empty = new ReleaseFeatureDataModel();

		public int? ReleaseFeatureId	{ get; set; }
		public DateTime? DateCreated	{ get; set; }
		public DateTime? DateModified	{ get; set; }
		
	}

}
