using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.ReleaseLog.DomainModel
{
    [Serializable]
	public class ReleaseNoteBusinessDifficultyDataModel : StandardDataModel
    {        
        public class DataColumns : StandardDataColumns
        {
			public const string ReleaseNoteBusinessDifficultyId = "ReleaseNoteBusinessDifficultyId";
            public const string DateCreated						= "DateCreated";
            public const string DateModified					= "DateModified";           
        }

		public static readonly ReleaseNoteBusinessDifficultyDataModel Empty = new ReleaseNoteBusinessDifficultyDataModel();

		public int? ReleaseNoteBusinessDifficultyId		{ get; set; }
        public DateTime? DateCreated					{ get; set; }
        public DateTime? DateModified					{ get; set; }
      
    }

}
