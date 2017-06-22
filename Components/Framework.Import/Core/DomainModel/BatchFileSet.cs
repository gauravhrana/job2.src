using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace DataModel.Framework.Import
{
    [Serializable]
	public class BatchFileSetDataModel : StandardDataModel
	{
		public class DataColumns : StandardDataColumns
		{
			public const string BatchFileSetId  	= "BatchFileSetId";			
			public const string CreatedByPersonId   = "CreatedByPersonId";
		}

        public static readonly BatchFileSetDataModel Empty = new BatchFileSetDataModel();

		public int? BatchFileSetId      { get; set; }		
		public int? CreatedByPersonId   { get; set; }
		
	}
}
