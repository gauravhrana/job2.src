using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Import
{
    [Serializable]
	public class BatchFileHistoryDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string BatchFileHistoryId		= "BatchFileHistoryId";			
			public const string UpdatedByPersonId		= "UpdatedByPersonId";
			public const string BatchFileId				= "BatchFileId";
			public const string BatchFileSetId			= "BatchFileSetId";
			public const string BatchFileStatusId		= "BatchFileStatusId";
		}

        public static readonly BatchFileHistoryDataModel Empty = new BatchFileHistoryDataModel();

		public int? BatchFileHistoryId   { get; set; }		
		public int? UpdatedByPersonId    { get; set; }
		public int? BatchFileId          { get; set; }
		public int? BatchFileSetId       { get; set; }
		public int? BatchFileStatusId    { get; set; }
	}
}