using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.ReleaseLog.DomainModel
{
    [Serializable]
	public class ReleaseNoteLogisticsDifficultyDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string ReleaseNoteLogisticsDifficultyId = "ReleaseNoteLogisticsDifficultyId";
			public const string DateCreated						 = "DateCreated";
			public const string DateModified					 = "DateModified";			
		}

		public static readonly ReleaseNoteLogisticsDifficultyDataModel Empty = new ReleaseNoteLogisticsDifficultyDataModel();

		public int? ReleaseNoteLogisticsDifficultyId { get; set; }
		public DateTime? DateCreated                 { get; set; }
		public DateTime? DateModified				 { get; set; }
		
	}

}
