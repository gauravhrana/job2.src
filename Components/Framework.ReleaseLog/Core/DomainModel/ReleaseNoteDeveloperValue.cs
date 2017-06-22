using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.ReleaseLog.DomainModel
{
    [Serializable]
	public class ReleaseNoteDeveloperValueDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string ReleaseNoteDeveloperValueId = "ReleaseNoteDeveloperValueId";
			public const string DateCreated = "DateCreated";
			public const string DateModified = "DateModified";			
		}

		public static readonly ReleaseNoteDeveloperValueDataModel Empty = new ReleaseNoteDeveloperValueDataModel();

		public int? ReleaseNoteDeveloperValueId { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateModified { get; set; }		
	}

}
