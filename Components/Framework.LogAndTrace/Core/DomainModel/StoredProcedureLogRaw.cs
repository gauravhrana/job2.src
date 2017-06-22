using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Framework.DataAccess;

namespace Framework.Components.LogAndTrace
{

    [Serializable]
    public class StoredProcedureLogRawDataModel : StandardDataModel
    {

        public int?		StoredProcedureLogRawId	 { get; set; }
		public int?		StoredProcedureLogId	 { get; set; }
		public string	InputParameters			 { get; set; }
		public string	InputValues				 { get; set; }

        public static readonly StoredProcedureLogRawDataModel Empty = new StoredProcedureLogRawDataModel();

        public class DataColumns : StandardDataColumns
        {
            public const string StoredProcedureLogRawId = "StoredProcedureLogRawId";
            public const string StoredProcedureLogId    = "StoredProcedureLogId";
            public const string InputParameters         = "InputParameters";
            public const string InputValues             = "InputValues";
        }

    }

}
 