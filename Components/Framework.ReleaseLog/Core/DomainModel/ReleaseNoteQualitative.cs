using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.ReleaseLog.DomainModel
{

    [Serializable]
    public class ReleaseNoteQualitativeDataModel : StandardDataModel
    {
        
        public class DataColumns : StandardDataColumns
        {
            public const string ReleaseNoteQualitativeId = "ReleaseNoteQualitativeId";
            public const string DateCreated              = "DateCreated";
            public const string DateModified             = "DateModified";            
        }

		public static readonly ReleaseNoteQualitativeDataModel Empty = new ReleaseNoteQualitativeDataModel();

        public int? ReleaseNoteQualitativeId    { get; set; }
        public DateTime? DateCreated            { get; set; }
        public DateTime? DateModified           { get; set; }        
    }

}
