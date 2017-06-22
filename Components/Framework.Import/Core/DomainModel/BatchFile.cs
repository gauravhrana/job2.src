using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Import
{
    [Serializable]
	public class BatchFileDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string Folder             = "Folder";
			public const string BatchFile          = "BatchFile";			
			public const string CreatedByPersonId  = "CreatedByPersonId";
			public const string Errors             = "Errors";
			public const string FileTypeId         = "FileTypeId";			
			public const string UpdatedByPersonId  = "UpdatedByPersonId";
			public const string BatchFileId        = "BatchFileId";
			public const string BatchFileSetId     = "BatchFileSetId";
			public const string BatchFileStatusId  = "BatchFileStatusId";
			public const string SystemEntityTypeId = "SystemEntityTypeId";
		}

        public static readonly BatchFileDataModel Empty = new BatchFileDataModel();

		public int? UpdatedByPersonId        { get; set; }
		public int? BatchFileId              { get; set; }
		public int? BatchFileSetId           { get; set; }
		public int? BatchFileStatusId        { get; set; }
		public int? FileTypeId               { get; set; }
		public int? SystemEntityTypeId       { get; set; }				
		public int? CreatedByPersonId        { get; set; }
		public string Folder                 { get; set; }
		public string BatchFile              { get; set; }
		public string Errors                 { get; set; }
	}
}